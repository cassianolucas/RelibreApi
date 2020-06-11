using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RelibreApi.Models;
using RelibreApi.Services;
using RelibreApi.Utils;
using RelibreApi.ViewModel;

namespace RelibreApi.Controllers
{

    [Route("api/v1/[controller]/"), ApiController]
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

        [HttpPost, Route("Register"), AllowAnonymous]
        public async Task<IActionResult> RegisterAsync(
            [FromBody] UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userMap = _mapper.Map<UserViewModel, User>(user);

                    userMap = await _userMananger.CreateAsync(userMap);

                    // verificar para mapear novamente usuario do banco para retornar

                    return Created(new Uri(Url.ActionContext.ToString()), userMap);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }

            return NoContent();
        }

        [HttpPost, Route("Login"), AllowAnonymous]
        public async Task<IActionResult> LoginAsync(
            [FromBody] UserViewModel user)
        {
            var userMap = await _userMananger.LoginAsync(user.Login, user.Password);

            if (userMap != null)
            {
                userMap = _mapper.Map<UserViewModel, User>(user);

                var access_token = Util.CreateToken(_configuration, userMap);

                return Ok(new
                {
                    userMap.Login,
                    access_token
                });
            }

            return NoContent();
        }
    }
}