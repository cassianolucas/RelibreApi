using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RelibreApi.Models;
using RelibreApi.Services;
using RelibreApi.Utils;
using RelibreApi.ViewModel;

namespace RelibreApi.Controllers
{
    [Route("api/v1/[controller]"), ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;    
        private readonly HttpContext _httpContext;
        private readonly INotification _notificationMananger;
        private readonly INotificationPerson _notificationPersonMananger;
        private readonly IUser _userMananger;

        public NotificationController(
            [FromServices] IUnitOfWork uow,
            [FromServices] IMapper mapper,
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromServices] INotification notificationMananger,
            [FromServices] IUser userMananger,
            [FromServices] INotificationPerson notificationPersonMananger
            )
        {
            _uow = uow;
            _mapper = mapper;
            _httpContext = httpContextAccessor.HttpContext;
            _notificationMananger = notificationMananger;
            _userMananger = userMananger;
            _notificationPersonMananger = notificationPersonMananger;
        }
        
        [HttpPut, Route(""), Authorize]
        public async Task<IActionResult> UpdateAsync(
            [FromForm(Name = "id")] long idNotification
            )
        {
            try
            {
                if (idNotification <= 0) 
                    return BadRequest(new ResponseErrorViewModel
                    {
                        Status = Constants.Error,
                        Errors = new List<object>
                        {
                            new { Message = Constants.InvalidNotification }
                        }
                    });
                
                var notificationPersonDb = await _notificationPersonMananger
                    .GetByIdAsync(idNotification);

                if (notificationPersonDb == null) return NoContent();

                notificationPersonDb.Active = false;

                _notificationPersonMananger.Update(notificationPersonDb);

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
                return BadRequest(new
                {
                    Status = Constants.Error,
                    Errors = new List<object>
                    {
                        Util.ReturnException(ex)
                    }
                });
            }            
        }        
        [HttpGet, Route(""), Authorize]
        public async Task<IActionResult> GetAsync(
            [FromQuery(Name = "limit")] int limit, 
            [FromQuery(Name = "offset")] int offset
        )
        {
            try
            {
                var login = Util.GetClaim(_httpContext,
                    Constants.UserClaimIdentifier);

                var userDb = await _userMananger.GetByLogin(login);

                if (userDb == null) 
                    return BadRequest(new ResponseErrorViewModel
                    {                        
                        Status = Constants.Error,
                        Errors = new List<object>
                        {
                            new { Message = Constants.UserNotFound }
                        }
                    });

                // capturar usuario da requisição para listar apenas as notificações dele
                var notificationsDb = await GetByPerson(userDb.Person.Id, offset, limit);

                var notificationsMap = _mapper
                    .Map<ICollection<NotificationPersonViewModel>>(notificationsDb);
                
                return Ok(new ResponseViewModel
                {
                    Result = notificationsMap,
                    Status = Constants.Sucess
                });
            }
            catch (Exception ex)
            {                
                // gerar log
                return BadRequest(new
                {
                    Status = Constants.Error,
                    Errors = new List<object>
                    {
                        Util.ReturnException(ex)
                    }
                });
            }            
        }

        private Task<List<NotificationPerson>> GetByPerson(long idPerson, int offset, int limit)
        {
            return _notificationMananger.GetByPersonAsyncNoTracking(idPerson, offset, limit);
        }
    }
}