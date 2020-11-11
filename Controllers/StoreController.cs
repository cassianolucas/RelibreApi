using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RelibreApi.Services;
using RelibreApi.Utils;
using RelibreApi.ViewModel;

namespace RelibreApi.Controllers
{
    [Route("api/v1/[controller]"), ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IUser _userMananger;
        private readonly HttpContext _httpContext;
        public StoreController(
            [FromServices] IUser userMananger,
            [FromServices] IHttpContextAccessor httpContextAccessor
        )
        {
            _userMananger = userMananger;
            _httpContext = httpContextAccessor.HttpContext;
        }

        [HttpGet, Route("Bussiness"), Authorize]
        public async Task<IActionResult> GetBussiness()
        {
            try
            {  
                var login = Util.GetClaim(_httpContext,
                    Constants.UserClaimIdentifier);

                var userDb = await _userMananger
                    .GetByLogin(login);

                var a = await _userMananger.GetAllBusiness(userDb.Person.Id);

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