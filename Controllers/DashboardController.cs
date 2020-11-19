using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RelibreApi.Services;
using RelibreApi.Utils;
using RelibreApi.ViewModel;

namespace RelibreApi.Controllers
{
    [Route("api/v1/[controller]"), ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly HttpContext _httpContext;
        private readonly IContact _contactMananger;
        public DashboardController(
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromServices] IContact contactMananger
        )
        {
            _httpContext = httpContextAccessor.HttpContext;
            _contactMananger = contactMananger;
        }

        [HttpGet, Route(""), Authorize]
        public IActionResult GetQuantityContacts()
        {
            try
            {
                var login = Util.GetClaim(_httpContext,
                    Constants.UserClaimIdentifier);

                DateTime data;
                DateTime.TryParse("01/01/1900", out data);

                var totalDb = _contactMananger
                    .GetQuantityConcactReceivedNoTracking(login, data);
                
                var currentDb = _contactMananger
                    .GetQuantityConcactReceivedNoTracking(login, 
                        DateTime.Parse(Util.CurrentDateTime().ToString("dd/MM/yyyy")));

                return Ok(new ResponseViewModel
                    {
                        Result = new {
                            Total = totalDb,
                            Current = currentDb
                        },
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