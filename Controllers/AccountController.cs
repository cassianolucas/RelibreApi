using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RelibreApi.Models;
using RelibreApi.Services;
using RelibreApi.Utils;
using RelibreApi.ViewModel;
using static RelibreApi.Utils.Constants;

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
        private readonly IEmailVerification _emailVerificationService;

        public AccountController(
            [FromServices] IUnitOfWork uow,
            [FromServices] IConfiguration configuration,
            [FromServices] IMapper mapper,
            [FromServices] IUser userMananger,
            [FromServices] IProfile profileMananger,
            [FromServices] ILibrary libraryMananger,
            [FromServices] IEmailVerification emailVerificationService
            )
        {
            _uow = uow;
            _configuration = configuration;
            _mapper = mapper;
            _userMananger = userMananger;
            _profileMananger = profileMananger;
            _libraryMananger = libraryMananger;
            _emailVerificationService = emailVerificationService;
        }

        [HttpPost, Route("Register"), AllowAnonymous]
        public async Task<IActionResult> RegisterAsync(
            [FromBody] UserRegisterViewModel user)
        {
            try
            {
                var userMap = _mapper.Map<User>(user);

                var userDb = await _userMananger.GetByLogin(userMap.Login);

                // usuario já existe
                if (userDb != null) throw new ArgumentException();

                // captura perfil de usuario padrão
                var profileDb = await _profileMananger.GetByIdAsync(2);

                var newPhone = userMap.Person.Phones.FirstOrDefault(x => x.Number.Equals(user.Phone));
                newPhone.Active = true;
                newPhone.Master = true;
                newPhone.CreatedAt = Util.CurrentDateTime();
                newPhone.UpdatedAt = newPhone.CreatedAt;

                userMap.LoginVerified = false;
                userMap.Profile = profileDb;
                userMap.Password = Util.Encrypt(userMap.Password);
                userMap.Person.Active = true;
                userMap.Person.PersonType = "PF";
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

                user = _mapper.Map<UserRegisterViewModel>(userMap);

                var emailVerification = new EmailVerification
                {
                    Login = userMap.Login,
                    CreatedAt = Util.CurrentDateTime(),
                    CodeVerification = Util.GenerateGuid()
                };

                await _emailVerificationService.CreateAsync(emailVerification);

                Util.SendEmailAsync(_configuration, user.Login,
                    "Confirmação de conta",
                    $"Account/EmailVerification?verification_code={emailVerification.CodeVerification}");

                _uow.Commit();

                return Created(new Uri(Url.ActionLink("Register", "Account")), null);
            }
            catch (ArgumentException)
            {
                return Conflict("");
            }
            catch (Exception ex)
            {
                // gerar log
                return BadRequest(Util.ReturnException(ex));
            }
        }

        [HttpPost, Route("Login"), AllowAnonymous]
        public async Task<IActionResult> LoginAsync(
            [FromBody] UserRegisterViewModel user)
        {
            var userMap = await _userMananger.GetByLogin(user.Login);

            // não localizou usuario
            if (userMap == null) return NoContent();

            // senha não confere com cadastro
            if (!userMap.Password.Equals(Util.Encrypt(user.Password))) return NoContent();

            // usaurio não foi verificado
            if (!userMap.IsVerified()) return Forbid();

            var access_token = Util.CreateToken(_configuration, userMap);

            return Ok(new
            {
                userMap.Login,
                access_token
            });
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

                // não validou usuario
                if (!userDb.IsVerified()) return Forbid();

                userDb.Person.Name = userMap.Person.Name;
                userDb.Person.LastName = userMap.Person.LastName;
                userDb.Person.UpdatedAt = Util.CurrentDateTime();

                if (userMap.Person.Phones != null &&
                    userMap.Person.Phones.Count > 0)
                {
                    userDb.Person.Phones = userMap.Person.Phones;
                }

                var addressDb = userDb.Person.Addresses.SingleOrDefault();

                addressDb = (addressDb == null ? new Address() : addressDb);

                // quando não existir endereço, adicionar data de criação
                addressDb.CreatedAt = (addressDb.CreatedAt.Year.Equals(1) ?
                    Util.CurrentDateTime() : addressDb.CreatedAt);

                if (userMap.Person.Addresses != null &&
                    userMap.Person.Addresses.Count > 0)
                {
                    // atualizar endereço
                    var addressMap = userMap.Person.Addresses.SingleOrDefault();
                    addressDb.Active = addressMap.Active;

                    addressDb.Latitude = addressMap.Latitude;
                    addressDb.Longitude = addressMap.Longitude;
                    addressDb.Master = true;
                    addressDb.NickName = "Principal";
                    addressDb.UpdatedAt = Util.CurrentDateTime();

                    // realizar chamada para api para buscar endereço
                    var endPoint = _configuration.GetValue<string>(
                            Constants.GeolocationApi);

                    if (!string.IsNullOrEmpty(addressDb.Latitude) &&
                    !string.IsNullOrEmpty(addressDb.Longitude))
                    {
                        var reader = Util.HttpRequest(
                            string.Format(endPoint, addressDb.Latitude,
                            addressDb.Longitude), Requests.Get);

                        object objResponse = reader.ReadToEnd();

                        using (JsonDocument doc = JsonDocument.Parse(objResponse.ToString()))
                        {
                            JsonElement root = doc.RootElement;
                            var results = root.GetProperty("results");
                            var formatted = results[0].GetProperty("formatted");
                            addressDb.FullAddress = formatted.ToString();

                            userDb.Person.Addresses.Add(addressDb);
                        };



                        // var request = WebRequest.CreateHttp(
                        //     string.Format(endPoint, addressDb.Latitude, 
                        //         addressDb.Longitude));

                        // request.Method = "GET";
                        // request.UserAgent = "RequisicaoWebDemo";

                        // using (var response = request.GetResponse())
                        // {
                        //     var streamData = response.GetResponseStream();
                        //     StreamReader reader = new StreamReader(streamData);

                        //     object objResponse = reader.ReadToEnd();

                        //     using (JsonDocument doc = JsonDocument.Parse(objResponse.ToString()))
                        //     {
                        //         JsonElement root = doc.RootElement;
                        //         var results = root.GetProperty("results");
                        //         var formatted = results[0].GetProperty("formatted");
                        //         addressDb.FullAddress = formatted.ToString();
                        //     };

                        //     userDb.Person.Addresses.Add(addressDb);

                        //     streamData.Close();
                        //     response.Close();
                        // }
                    }
                }

                _userMananger.Update(userDb);

                user = _mapper.Map<UserViewModel>(userDb);

                _uow.Commit();

                return Ok(user);
            }
            catch (Exception ex)
            {
                // gerar log
                return BadRequest(Util.ReturnException(ex));
            }
        }

        [HttpPost, Route("EmailVerification"), AllowAnonymous]
        public async Task<IActionResult> EmailConfirmation(
            [FromQuery(Name = "verification_code")] string verificationCode
        )
        {
            try
            {
                // realizar busca de email de acordo com código e aplicar update em email verificado
                var emailVerificationDb =
                    await _emailVerificationService.GetByCodeAsync(verificationCode);

                // não encontrou o código informado para realizar validação
                if (emailVerificationDb == null) return NoContent();

                var userDb = await _userMananger.GetByLogin(emailVerificationDb.Login);

                // não encontrou usuario cadastrado no banco
                if (userDb == null) return NoContent();

                if (userDb.IsVerified()) return Forbid();

                userDb.LoginVerified = true;

                // atualiza usuario passando login verificado como true
                _userMananger.Update(userDb);

                _uow.Commit();

                return Ok("");
            }
            catch (Exception ex)
            {
                return BadRequest(Util.ReturnException(ex));
            }
        }

        [HttpPost, Route("ForgotPassword"), AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(
            [FromQuery(Name = "login")] string login
        )
        {
            try
            {
                // não informou login
                if (string.IsNullOrEmpty(login)) return NoContent();

                var userDb = await _userMananger.GetByLogin(login);

                // não existe usuario
                if (userDb == null) return NoContent();

                // gerar email onde link deve redirecionar para nova senha
                var emailVerification = new EmailVerification
                {
                    Login = userDb.Login,
                    CreatedAt = Util.CurrentDateTime(),
                    CodeVerification = Util.GenerateGuid()
                };

                await _emailVerificationService.CreateAsync(emailVerification);

                // verificar para redirecionar para url do front 
                Util.SendEmailAsync(_configuration, userDb.Login,
                    "Redefinição de senha",
                    $"Account/ForgotPassword?verification_code={emailVerification.CodeVerification}");

                return Ok("");
            }
            catch (Exception ex)
            {
                // gerar log
                return BadRequest(Util.ReturnException(ex));
            }
        }

        [HttpPost, Route("ForgotPassword"), AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(
            [FromQuery(Name = "verification_code")] string verificationCode,
            [FromForm(Name = "new_password")] string newPassword
        )
        {
            try
            {
                // senha sem valor
                if (string.IsNullOrEmpty(newPassword)) return NoContent();

                // nenhum código informado
                if (string.IsNullOrEmpty(verificationCode)) return Forbid();

                var emailVerificationDb =
                    await _emailVerificationService.GetByCodeAsync(verificationCode);

                // não encontrou o código informado para realizar validação
                if (emailVerificationDb == null) return NoContent();

                // buscar usuario por login                
                var userDb = await _userMananger.GetByLogin(emailVerificationDb.Login);

                if (userDb == null) return NoContent();

                // criptografar nova senha
                userDb.Password = Util.Encrypt(newPassword.Trim());

                _userMananger.Update(userDb);

                _uow.Commit();

                return Ok("");
            }
            catch (Exception ex)
            {
                // gerar log
                return BadRequest(Util.ReturnException(ex));
            }
        }
    }
}