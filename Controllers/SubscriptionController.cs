using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RelibreApi.Models;
using RelibreApi.Services;
using RelibreApi.Utils;
using RelibreApi.ViewModel;

namespace RelibreApi.Controllers
{
    [Route("api/v1/[controller]"), ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly HttpContext _httpContext;
        private readonly IUser _userMananger;
        private readonly ISubscription _subscriptionMananger;
        public SubscriptionController(
            [FromServices] IUnitOfWork uow,
            [FromServices] IMapper mapper,
            [FromServices] IConfiguration configuration,
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromServices] IUser userMananger,
            [FromServices] ISubscription subscriptionMananger
        )
        {
            _uow = uow;
            _configuration = configuration;
            _mapper = mapper;
            _httpContext = httpContextAccessor.HttpContext;
            _userMananger = userMananger;
            _subscriptionMananger = subscriptionMananger;
        }

        [HttpPost, Route(""), Authorize(Policy = "PJ")]
        public async Task<IActionResult> Subscribe(
            [FromQuery] int idPlan
        )
        {
            try
            {
                // captura login de usuario logado
                var login = Util
                    .GetClaim(_httpContext,
                        Constants.UserClaimIdentifier);

                // busca dados do usuario logado
                var user = await _userMananger
                    .GetByLogin(login);

                // busca planos do usuario
                var plan = await _subscriptionMananger
                    .GetByPersonAsyncNoTacking(user.Person.Id);

                // vefificar se usuario já tem plano
                if (plan != null)
                    return BadRequest(new ResponseErrorViewModel
                    {
                        Status = Constants.Error,
                        Errors = new List<object>
                        {
                            new { Message = Constants.SubscriptionInvalid }
                        }
                    });

                // buscar plano de acordo com id
                var subscription = await _subscriptionMananger
                    .GetSubscriptionById(idPlan);

                if (subscription == null)
                    return NotFound(
                        new ResponseErrorViewModel
                        {
                            Status = Constants.Error,
                            Errors = new List<object>
                            {
                                new { Message = Constants.InvalidPlan }
                            }
                        });

                var personSubscription = new PersonSubscription
                {
                    Person = user.Person,
                    IdPerson = user.Person.Id,
                    Subscription = subscription,
                    IdSubscription = subscription.Id,
                    CreatedAt = Util.CurrentDateTime(),
                    Validate = false                    
                };

                await _subscriptionMananger
                    .CreateAsync(personSubscription);
                
                _uow.Commit();

                return Ok(new ResponseViewModel
                {
                    Result = null,
                    Status = Constants.Sucess
                });
            }
            catch (Exception ex)
            {
                // gerar log
                return BadRequest(new ResponseErrorViewModel
                {
                    Status = Constants.Error,
                    Errors = new List<object> { Util.ReturnException(ex) }
                });
            }
        }


    }
}