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

    [Route("api/v1/[controller]"), ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUser _userMananger;
        private readonly IProfile _profileMananger;
        private readonly ILibrary _libraryMananger;

        public AccountController(
            [FromServices] IUnitOfWork uow,
            [FromServices] IConfiguration configuration,
            [FromServices] IMapper mapper,
            [FromServices] IUser userMananger,
            [FromServices] IProfile profileMananger,
            [FromServices] ILibrary libraryMananger
            )
        {
            _uow = uow;
            _configuration = configuration;
            _mapper = mapper;
            _userMananger = userMananger;
            _profileMananger = profileMananger;
            _libraryMananger = libraryMananger;
        }

        [HttpPost, Route("Register"), AllowAnonymous]
        public async Task<IActionResult> RegisterAsync(
            [FromBody] UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userMap = _mapper.Map<User>(user);

                    var userDb = await _userMananger.GetByLoginOrDocument(
                            userMap.Login, userMap.Person.Document);

                    // usuario já existe
                    if (userDb != null) throw new ArgumentException();

                    // atribui perfil padrão de acordo com usuario PF ou PJ
                    var profileDb = await _profileMananger.GetByIdAsync(
                            user.Document.Replace(".", "").Replace("-", "").Length > 11 ? 2 : 1);

                    userMap.Profile = profileDb;
                    userMap.Password = Util.Encrypt(userMap.Password);
                    userMap.Person.Active = true;
                    userMap.Person.PersonType = user.Document.Replace(".", "").Length > 11 ? "PJ" : "PF";
                    userMap.Person.CreatedAt = Util.CurrentDateTime();
                    userMap.Person.UpdatedAt = userMap.Person.CreatedAt;

                    await _userMananger.CreateAsync(userMap);

                    // create library
                    var lib = new Library
                    {                        
                        Person = userMap.Person,                        
                        Active = true,
                        CreatedAt = Util.CurrentDateTime(),
                        UpdatedAt = Util.CurrentDateTime()
                    };

                    await _libraryMananger.CreateAsync(lib);        

                    user = _mapper.Map<UserViewModel>(userMap);

                    // não retornar a senha
                    user.Password = null;

                    _uow.Commit();

                    return Created(new Uri(Url.ActionLink("Register", "Account")), user);
                }
                catch (ArgumentException)
                {
                    return Conflict("");
                }
                catch (Exception)
                {
                    // gerar log
                    return BadRequest("");
                }
            }

            return NoContent();
        }

        [HttpPost, Route("Login"), AllowAnonymous]
        public async Task<IActionResult> LoginAsync(
            [FromBody] UserViewModel user)
        {
            var userMap = await _userMananger.LoginAsync(
                user.Login, Util.Encrypt(user.Password.Trim()));

            if (userMap != null)
            {
                var access_token = Util.CreateToken(_configuration, userMap);

                return Ok(new
                {
                    userMap.Login,
                    access_token
                });
            }

            return NoContent();
        }

        [HttpPut, Route(""), Authorize]
        public async Task<IActionResult> UpdateAsync(
            [FromBody] UserViewModel user
            )
        {
            try
            {
                var userMap = _mapper.Map<User>(user);

                var userDb = await _userMananger.GetByLogin(userMap.Login);

                // usuario não existe
                if (userDb == null) throw new ArgumentNullException();

                userDb.Person.Name = userMap.Person.Name;
                userDb.Person.LastName = userMap.Person.LastName;
                userDb.Person.Addresses = userMap.Person.Addresses;
                userDb.Person.Phones = userMap.Person.Phones;
                userDb.Person.UpdatedAt = Util.CurrentDateTime();

                userDb = await _userMananger.UpdateAsync(userDb);

                user = _mapper.Map<UserViewModel>(userDb);

                return Ok(User);
            }
            catch (Exception)
            {
                // gerar log
                return BadRequest("");
            }
        }
    }
}