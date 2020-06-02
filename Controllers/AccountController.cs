using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RelibreApi.Services;
using RelibreApi.ViewModel;

namespace RelibreApi.Controllers
{

    [Route("api/v1/[controller]/"), AllowAnonymous, ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUser _userMananger;

        public AccountController(
            [FromServices] IUnitOfWork uow,
            [FromServices] IConfiguration configuration,
            [FromServices] IMapper mapper,
            [FromServices] IUser userMananger)
        {
            _uow = uow;
            _configuration = configuration;
            _mapper = mapper;
            _userMananger = userMananger;
        }

        [HttpPost, Route("Register")]
        public async Task<IActionResult> RegisterAsync(
            [FromBody] UserViewModel user)
        {
            // apenas para remover aviso
            var a = await HttpContext.Request.ReadFormAsync();

            return Created(new Uri(Url.ActionContext.ToString()), user);
        }

        [HttpPost, Route("Login")]
        public async Task<IActionResult> LoginAsync(
            [FromBody] UserViewModel user)
        {
            // apenas para remover aviso
            var a = await HttpContext.Request.ReadFormAsync();

            return Ok(a);
        }
    }
}