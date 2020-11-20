using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly HttpContext _httpContext;
        private readonly IUser _userMananger;
        private readonly IProfile _profileMananger;
        private readonly ILibrary _libraryMananger;
        private readonly IEmailVerification _emailVerificationService;
        private readonly IContact _contactMananger;
        public AccountController(
            [FromServices] IUnitOfWork uow,
            [FromServices] IMapper mapper,
            [FromServices] IConfiguration configuration,
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromServices] IUser userMananger,
            [FromServices] IProfile profileMananger,
            [FromServices] ILibrary libraryMananger,
            [FromServices] IEmailVerification emailVerificationService,
            [FromServices] IContact contactMananger
            )
        {
            _uow = uow;
            _configuration = configuration;
            _mapper = mapper;
            _httpContext = httpContextAccessor.HttpContext;
            _userMananger = userMananger;
            _profileMananger = profileMananger;
            _libraryMananger = libraryMananger;
            _emailVerificationService = emailVerificationService;
            _contactMananger = contactMananger;
        }

        [HttpPost, Route("Register"), AllowAnonymous]
        public async Task<IActionResult> RegisterAsync(
            [FromBody] UserRegisterViewModel user)
        {
            try
            {
                var userMap = _mapper.Map<User>(user);

                var userDb = await _userMananger
                    .GetByLogin(userMap.Login);

                // usuario já existe
                if (userDb != null) return Conflict(
                    new ResponseErrorViewModel
                    {
                        Status = Constants.Error,
                        Errors = new List<object>
                        {
                            new { Message = Constants.UserFound }
                        }
                    });

                // captura perfil de usuario padrão
                var profileDb = await _profileMananger
                    .GetByIdAsync(2);

                var newPhone = userMap.Person.Phones
                    .FirstOrDefault(x => x.Number.Equals(user.Phone));

                newPhone.Active = true;
                newPhone.Master = true;
                newPhone.CreatedAt = Util.CurrentDateTime();
                newPhone.UpdatedAt = newPhone.CreatedAt;

                if (userMap.Person.Addresses != null &&
                    userMap.Person.Addresses.Count > 0)
                {
                    var address = userMap.Person.Addresses
                        .SingleOrDefault();

                    if (address != null)
                    {
                        if (!string.IsNullOrEmpty(address.Latitude) &&
                                    !string.IsNullOrEmpty(address.Longitude))
                        {
                            address.Latitude = address.Latitude;
                            address.Longitude = address.Longitude;

                            var addressResponse = await Util
                            .GetAddressByLatitudeLogintude(_configuration,
                                address.Latitude, address.Longitude);

                            address.FullAddress = addressResponse.FullAddress;

                            address.UpdatedAt = Util.CurrentDateTime();
                        };
                    }
                }

                userMap.LoginVerified = false;
                userMap.Profile = profileDb;
                userMap.Password = Util.Encrypt(userMap.Password);
                userMap.Person.Active = true;
                userMap.Person.PersonType = "PF";
                userMap.Person.BirthDate = user.BirthDate;
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

                Util.SendEmailAsync(_configuration, emailVerification.CodeVerification,
                    user.Login, user.Name, HtmlEmailType.NewAccount,  
                        HtmlEmailPersonType.IndividualPerson);

                _uow.Commit();

                return Created(new Uri(
                        Url.ActionLink("Register", "Account")),
                new ResponseViewModel
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

        [HttpPost, Route("Register/Business"), AllowAnonymous]
        public async Task<IActionResult> RegisterBusinessAsync(
            [FromBody] UserBusinessViewModel user
        )
        {
            try
            {
                var userMap = _mapper.Map<User>(user);

                // verificar se campos estão preenchidos
                if (string.IsNullOrEmpty(userMap.Person.Document))
                    return BadRequest(new ResponseErrorViewModel
                    {
                        Result = Constants.Error,
                        Errors = new List<object>
                        {
                            new { Message = Constants.UserDocumentInvalid }
                        }
                    });

                if (string.IsNullOrEmpty(userMap.Login))
                    return BadRequest(new ResponseErrorViewModel
                    {
                        Result = Constants.Error,
                        Errors = new List<object>
                        {
                            new { Message = Constants.UserLoginInvalid }
                        }
                    });

                var document = userMap.Person.Document
                    .Replace(".", "")
                    .Replace("/", "")
                    .Replace("-", "")
                    .Trim();

                if (!Util.IsValidCpf(document))
                    return BadRequest(new ResponseErrorViewModel
                    {
                        Result = Constants.Error,
                        Errors = new List<object>
                        {
                            new { Message = Constants.UserCnpjInvalid }
                        }
                    });

                var userDb = await _userMananger
                    .GetByLoginOrDocumentNoTracking(userMap.Login,
                        document);

                // usuario já existe
                if (userDb != null) return Conflict(
                    new ResponseErrorViewModel
                    {
                        Status = Constants.Error,
                        Errors = new List<object> { new { Message = Constants.UserFound } }
                    });

                // captura perfil de usuario padrão
                var profileDb = await _profileMananger
                    .GetByIdAsync(1);

                var newPhone = userMap.Person.Phones
                    .FirstOrDefault(x => x.Number.Equals(user.Phone));

                newPhone.Number = newPhone.Number
                    .Replace("(", "")
                    .Replace(")", "")
                    .Replace("-", "")
                    .Replace(" ", "");                    
                newPhone.Active = true;
                newPhone.Master = true;
                newPhone.CreatedAt = Util.CurrentDateTime();
                newPhone.UpdatedAt = newPhone.CreatedAt;

                // remover caracteres especiais do cep 
                if (userMap.Person.Addresses != null &&
                    userMap.Person.Addresses.Count > 0)
                {
                    var address = userMap.Person.Addresses.SingleOrDefault();
                    address.FullAddress = string.Concat(address.Street, ", ",
                        address.Neighborhood, ", ", address.City, " - ",
                        address.State, ", ", address.ZipCode);
                    address.ZipCode = address.ZipCode
                        .Trim().Replace("-", "");
                    address.Master = true;
                    address.Active = true;
                    address.NickName = "Principal";
                    address.CreatedAt = Util.CurrentDateTime();
                    address.UpdatedAt = address.CreatedAt;
                }

                userMap.Person.Document = document;
                userMap.LoginVerified = false;
                userMap.Profile = profileDb;
                userMap.Password = Util.Encrypt(userMap.Password);
                userMap.Person.Active = true;
                userMap.Person.PersonType = "PJ";
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

                user = _mapper.Map<UserBusinessViewModel>(userMap);

                var emailVerification = new EmailVerification
                {
                    Login = userMap.Login,
                    CreatedAt = Util.CurrentDateTime(),
                    CodeVerification = Util.GenerateGuid()
                };

                await _emailVerificationService.CreateAsync(emailVerification);

                Util.SendEmailAsync(_configuration, emailVerification.CodeVerification,
                    user.Login, user.Name, HtmlEmailType.NewAccount, 
                        HtmlEmailPersonType.LegalPerson);

                _uow.Commit();

                return Created(new Uri(Url
                    .ActionLink("RegisterBusiness", "Account")),
                    new ResponseViewModel
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

        [HttpPost, Route("Deactivate/Business"), Authorize]
        public async Task<IActionResult> DeactivateAccount()
        {
            try
            {
                var login = Util.GetClaim(_httpContext,
                    Constants.UserClaimIdentifier);

                var userDb = await _userMananger
                    .GetByLogin(login);

                userDb.Person.Active = false;
                userDb.Person.UpdatedAt = Util.CurrentDateTime();

                _userMananger.Update(userDb);

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

        [HttpPost, Route("Login"), AllowAnonymous]
        public async Task<IActionResult> LoginAsync(
            [FromBody] UserRegisterViewModel user)
        {
            var userMap = await _userMananger
                .GetByLogin(user.Login);

            // não localizou usuario
            if (userMap == null) return NotFound(
                new ResponseErrorViewModel
                {
                    Status = Constants.Error,
                    Errors = new List<object>
                    {
                        new { Message = Constants.UserNotFound }
                    }
                }
            );

            // senha não confere com cadastro
            if (!userMap.Password.Equals(Util.Encrypt(user.Password)))
                return BadRequest(
                new ResponseErrorViewModel
                {
                    Status = Constants.Error,
                    Errors = new List<object>
                    {
                        new { Message = Constants.UserInvalidOrPassword }
                    }
                }
            );

            // usuario não foi verificado
            if (!userMap.IsVerified()) return BadRequest(
                new ResponseErrorViewModel
                {
                    Status = Constants.Error,
                    Errors = new List<object>
                    {
                        new { Message = Constants.UserNotValidate }
                    }
                }
            );

            // tentar logar com usuario PJ na platatorma PF
            if (!((userMap.Person.PersonType.Equals("PF") &&
                user.Platform.ToLower().Equals("personal")) ||
                (userMap.Person.PersonType.Equals("PJ") &&
                user.Platform.ToLower().Equals("business"))))
            {
                return BadRequest(
                new ResponseErrorViewModel
                {
                    Status = Constants.Error,
                    Errors = new List<object>
                    {
                        new { Message = Constants.UserInvalidOrPassword }
                    }
                });
            }

            var accessToken = Util.CreateToken(_configuration, userMap);

            var address = userMap.Person.Addresses
                .SingleOrDefault(x => x.Master == true);

            var phones = userMap.Person.Phones
                .SingleOrDefault(x => x.Master == true);

            if (userMap.Person.PersonType.Equals("PJ"))
            {
                var userBusinessMap = _mapper
                    .Map<UserBusinessViewModel>(userMap);

                return Ok(new ResponseViewModel
                {
                    Result = new
                    {
                        Name = userBusinessMap.Name,
                        Legal_Name = userBusinessMap.LastName,
                        Login = userBusinessMap.Login,
                        Document = userBusinessMap.Document,
                        Web_site = userBusinessMap.WebSite,
                        Url_Image = userBusinessMap.UrlImage,
                        Description = userBusinessMap.Description,
                        Addresses = userBusinessMap.Addresses,
                        Phone = phones != null ? phones.Number : null,
                        Access_Token = accessToken,
                        Latitude = address != null ? address.Latitude : null,
                        Longitude = address != null ? address.Longitude : null,
                        valid_plan = userBusinessMap.ValidPlan
                    },
                    Status = Constants.Sucess
                });
            }
            else
            {
                return Ok(new ResponseViewModel
                {
                    Result = new
                    {
                        Login = userMap.Login,
                        Name = userMap.Person.Name,
                        Document = userMap.Person.Document,
                        BirthDate = userMap.Person.BirthDate,
                        Address = address != null ? address.FullAddress : null,
                        Phone = phones != null ? phones.Number : null,
                        Access_Token = accessToken,
                        Latitude = address != null ? address.Latitude : null,
                        Longitude = address != null ? address.Longitude : null
                    },
                    Status = Constants.Sucess
                });
            }
        }

        [HttpPut, Route("Business"), Authorize]
        public async Task<IActionResult> UpdateAsync(
            [FromBody] UserBusinessViewModel user)
        {
            try
            {
                var login = Util.GetClaim(_httpContext,
                    Constants.UserClaimIdentifier);

                var userMap = _mapper.Map<User>(user);

                var userDb = await _userMananger
                    .GetByLogin(login);

                // usuario não existe
                if (userDb == null)
                    return NotFound(new ResponseErrorViewModel
                    {
                        Status = Constants.Error,
                        Errors = new List<object>
                        {
                            new { Message = Constants.UserNotFound }
                        }
                    });

                // usaurio não foi verificado
                if (!userDb.IsVerified()) return BadRequest(
                    new ResponseErrorViewModel
                    {
                        Status = Constants.Error,
                        Errors = new List<object>
                        {
                        new { Message = Constants.UserNotValidate }
                        }
                    }
                );

                if (!string.IsNullOrEmpty(userMap.Person.Name))
                {
                    userDb.Person.Name = userMap.Person.Name;
                }

                if (!string.IsNullOrEmpty(userMap.Person.LastName))
                {
                    userDb.Person.LastName = userMap.Person.LastName;
                }
                userDb.Person.Description = userMap.Person.Description;
                userDb.Person.UrlImage = userMap.Person.UrlImage;
                userDb.Person.WebSite = userMap.Person.WebSite;
                userDb.Person.UpdatedAt = Util.CurrentDateTime();

                // phones
                if (userMap.Person.Phones != null &&
                    userMap.Person.Phones.Count > 0)
                {
                    foreach (var phone in userMap.Person.Phones)
                    {
                        var numberFormated = phone.Number
                            .Replace("+", "")
                            .Replace("(", "")
                            .Replace(")", "")
                            .Replace("-", "")
                            .Replace(" ", "")
                            .Trim();

                        if (!string.IsNullOrEmpty(numberFormated))
                        {
                            var phoneDb = (userDb.Person.Phones != null &&
                                userDb.Person.Phones.Count > 0) ? userDb.Person.Phones
                                    .SingleOrDefault(x => x.Master == true) : null;

                            if (phoneDb == null)
                            {
                                userDb.Person.Phones.Add(new Phone
                                {
                                    Number = numberFormated,
                                    Master = true,
                                    Person = userDb.Person,
                                    IdPerson = userDb.Person.Id,
                                    Active = true,
                                    CreatedAt = Util.CurrentDateTime(),
                                    UpdatedAt = Util.CurrentDateTime()
                                });
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(numberFormated))
                                {
                                    phoneDb.Number = numberFormated;
                                }
                                phoneDb.Active = true;
                                phoneDb.UpdatedAt = Util.CurrentDateTime();
                            }
                        }
                    }
                }

                // addresses
                if (userMap.Person.Addresses != null &&
                    userMap.Person.Addresses.Count > 0)
                {
                    foreach (var address in userMap.Person.Addresses)
                    {
                        var addressDb = userDb.Person.Addresses
                            .SingleOrDefault();

                        if (addressDb == null)
                        {
                            var addressResponse = await Util
                                .GetAddressByLatitudeLogintude(_configuration,
                                    address.Latitude, address.Longitude);

                            userDb.Person.Addresses.Add(new Address
                            {
                                Longitude = address.Longitude,
                                Latitude = address.Latitude,
                                FullAddress = addressResponse.FullAddress,
                                City = addressResponse.City,
                                Street = address.Street,
                                State = address.State,
                                Complement = address.Complement,
                                Neighborhood = address.Neighborhood,
                                Number = address.Number,
                                ZipCode = address.ZipCode
                                    .Trim()
                                        .Replace("-", "")
                                            .Replace(" ", ""),
                                Active = true,
                                IdPerson = userDb.Person.Id,
                                Person = userDb.Person,
                                Master = true,
                                NickName = "Principal",
                                CreatedAt = Util.CurrentDateTime(),
                                UpdatedAt = Util.CurrentDateTime(),
                            });
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(address.Latitude) &&
                                !string.IsNullOrEmpty(address.Longitude))
                            {
                                var addressResponse = await Util
                                .GetAddressByLatitudeLogintude(_configuration,
                                    address.Latitude, address.Longitude);

                                addressDb.Latitude = address.Latitude;
                                addressDb.Longitude = address.Longitude;
                                addressDb.City = addressResponse.City;
                                addressDb.Street = address.Street;
                                addressDb.State = address.State;
                                addressDb.Complement = address.Complement;
                                addressDb.Neighborhood = address.Neighborhood;
                                addressDb.Number = address.Number;
                                addressDb.ZipCode = address.ZipCode.Trim()
                                    .Replace("-", "")
                                        .Replace(" ", "");
                                addressDb.FullAddress = addressResponse.FullAddress;
                                addressDb.Active = true;
                                addressDb.UpdatedAt = Util.CurrentDateTime();
                            }

                            addressDb.UpdatedAt = Util.CurrentDateTime();
                        }
                    }
                }

                _userMananger.Update(userDb);

                _uow.Commit();

                userDb = await _userMananger
                    .GetByIdAsync(userDb.IdPerson);

                user = _mapper.Map<UserBusinessViewModel>(userDb);

                return Ok(new ResponseViewModel
                {
                    Result = user,
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

        [HttpPut, Route(""), Authorize]
        public async Task<IActionResult> UpdateAsync(
            [FromBody] UserViewModel user
            )
        {
            try
            {
                var login = Util.GetClaim(_httpContext,
                    Constants.UserClaimIdentifier);

                var userMap = _mapper.Map<User>(user);

                var userDb = await _userMananger.GetByLogin(login);

                // usuario não existe
                if (userDb == null)
                    return NotFound(new ResponseErrorViewModel
                    {
                        Status = Constants.Error,
                        Errors = new List<object>
                        {
                            new { Message = Constants.UserNotFound }
                        }
                    });

                // usaurio não foi verificado
                if (!userDb.IsVerified()) return BadRequest(
                    new ResponseErrorViewModel
                    {
                        Status = Constants.Error,
                        Errors = new List<object>
                        {
                        new { Message = Constants.UserNotValidate }
                        }
                    }
                );

                if (!string.IsNullOrEmpty(userMap.Person.Name))
                {
                    userDb.Person.Name = userMap.Person.Name;
                }

                if (!string.IsNullOrEmpty(userMap.Person.LastName))
                {
                    userDb.Person.LastName = userMap.Person.LastName;
                }

                userDb.Person.UpdatedAt = Util.CurrentDateTime();
                userDb.Person.BirthDate = userMap.Person.BirthDate;

                // phones
                if (userMap.Person.Phones != null &&
                    userMap.Person.Phones.Count > 0)
                {
                    foreach (var phone in userMap.Person.Phones)
                    {
                        if (!string.IsNullOrEmpty(phone.Number))
                        {
                            var numberFormated = phone.Number
                                .Replace("+", "")
                                .Replace("(", "")
                                .Replace(")", "")
                                .Replace("-", "");

                            var phoneDb = (userDb.Person.Phones != null &&
                                userDb.Person.Phones.Count > 0) ? userDb.Person.Phones
                                    .SingleOrDefault(x => x.Master == true) : null;

                            if (phoneDb == null)
                            {
                                userDb.Person.Phones.Add(new Phone
                                {
                                    Number = numberFormated,
                                    Master = false,
                                    Person = userDb.Person,
                                    IdPerson = userDb.Person.Id,
                                    Active = true,
                                    CreatedAt = Util.CurrentDateTime(),
                                    UpdatedAt = Util.CurrentDateTime()
                                });
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(numberFormated))
                                {
                                    phoneDb.Number = numberFormated;
                                }
                                phoneDb.Active = phone.Active;
                                phoneDb.UpdatedAt = Util.CurrentDateTime();
                            }
                        }
                    }
                }

                // addresses
                if (userMap.Person.Addresses != null &&
                    userMap.Person.Addresses.Count > 0)
                {
                    foreach (var address in userMap.Person.Addresses)
                    {
                        var addressDb = userDb.Person.Addresses
                            .SingleOrDefault(x => x.Master == true);

                        if (addressDb == null)
                        {
                            var addressResponse = await Util
                                .GetAddressByLatitudeLogintude(_configuration,
                                    address.Latitude, address.Longitude);

                            userDb.Person.Addresses.Add(new Address
                            {
                                Longitude = address.Longitude,
                                Latitude = address.Latitude,
                                FullAddress = addressResponse.FullAddress,
                                City = addressResponse.City,
                                Neighborhood = " ",
                                State = addressResponse.State,
                                Street = addressResponse.Road,
                                Active = true,
                                CreatedAt = Util.CurrentDateTime(),
                                UpdatedAt = Util.CurrentDateTime(),
                                IdPerson = userDb.Person.Id,
                                Person = userDb.Person,
                                Master = true,
                                NickName = "Principal"
                            });
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(address.Latitude) &&
                                !string.IsNullOrEmpty(address.Longitude))
                            {
                                addressDb.Latitude = address.Latitude;
                                addressDb.Longitude = address.Longitude;

                                var addressResponse = await Util
                                .GetAddressByLatitudeLogintude(_configuration,
                                    address.Latitude, address.Longitude);

                                addressDb.FullAddress = addressResponse.FullAddress;
                                addressDb.Longitude = address.Longitude;
                                addressDb.Latitude = address.Latitude;
                                addressDb.FullAddress = addressResponse.FullAddress;
                                addressDb.City = addressResponse.City;
                                addressDb.Neighborhood = " ";
                                addressDb.State = addressResponse.State;
                                addressDb.Street = addressResponse.Road;
                            }

                            addressDb.UpdatedAt = Util.CurrentDateTime();
                        }
                    }
                }

                _userMananger.Update(userDb);

                _uow.Commit();

                userDb = await _userMananger
                    .GetByIdAsync(userDb.IdPerson);

                user = _mapper.Map<UserViewModel>(userDb);

                return Ok(new ResponseViewModel
                {
                    Result = user,
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

        [HttpGet, Route(""), Authorize]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var login = Util.GetClaim(_httpContext,
                    Constants.UserClaimIdentifier);

                var user = await _userMananger.GetByLogin(login);

                // verificar se é pj ou pf
                if (user.Person.PersonType.Equals("PJ"))
                {
                    var userMap = _mapper
                        .Map<UserBusinessViewModel>(user);
                    
                    userMap.Password = null;

                    return Ok(new ResponseViewModel
                    {
                        Result = userMap,
                        Status = Constants.Sucess
                    });
                }
                else
                {
                    var userMap = _mapper.Map<UserViewModel>(user);

                    return Ok(new ResponseViewModel
                    {
                        Result = userMap,
                        Status = Constants.Sucess
                    });
                }
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

        [HttpGet, Route("Business"), Authorize(Policy = "PJ")]
        public async Task<IActionResult> GetBusinessAsync()
        {
            try
            {
                var login = Util.GetClaim(_httpContext,
                    Constants.UserClaimIdentifier);

                var user = await _userMananger.GetByLogin(login);

                var userMap = _mapper.Map<UserBusinessViewModel>(user);

                userMap.Password = null;

                return Ok(new ResponseViewModel
                {
                    Result = userMap,
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

        [HttpGet, Route("EmailVerification"), AllowAnonymous]
        public async Task<IActionResult> EmailConfirmation(
            [FromQuery(Name = "verification_code")] string verificationCode
        )
        {
            try
            {
                // realizar busca de email de acordo com código e aplicar update em email verificado
                var emailVerificationDb =
                    await _emailVerificationService
                        .GetByCodeAsync(verificationCode);

                // redirecionar para endereço de erro
                var endPointError = _configuration.GetValue<string>(
                            Constants.RedirectError);

                // não encontrou o código informado para realizar validação
                if (emailVerificationDb == null) return Redirect(endPointError);

                var userDb = await _userMananger
                    .GetByLogin(emailVerificationDb.Login);

                // não encontrou usuario cadastrado no banco
                if (userDb == null) return Redirect(endPointError);

                // usuario não foi verificado
                if (userDb.IsVerified()) return Redirect(endPointError);

                userDb.LoginVerified = true;

                // atualiza usuario passando login verificado como true
                _userMananger.Update(userDb);

                _uow.Commit();

                // redirecionar para login
                var endPoint = _configuration.GetValue<string>(
                        (userDb.Person.PersonType.Equals("PJ") ?
                            Constants.RedirectLoginBusiness :
                            Constants.RedirectLogin));

                return Redirect(endPoint);
            }
            catch (Exception)
            {
                // redirecionar para endereço de erro
                var endPointError = _configuration.GetValue<string>(
                            Constants.RedirectError);

                return Redirect(endPointError);
            }
        }

        /// <summary>
        /// Método realiza alteração da senha de acordo com código gerado
        /// </summary>
        /// <param name="login"></param>
        /// <param name="verificationCode"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>

        [HttpPost, Route("ForgotPassword"), AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(
            [FromQuery(Name = "login")] string login,
            [FromQuery(Name = "verification_code")] string verificationCode,
            [FromForm(Name = "new_password")] string newPassword
        )
        {
            try
            {
                if (!string.IsNullOrEmpty(login))
                    return await ForgotPassword(login);

                // senha sem valor
                if (string.IsNullOrEmpty(newPassword))
                    return BadRequest(new ResponseErrorViewModel
                    {
                        Status = Constants.Error,
                        Errors = new List<object>
                        {
                            new { Message = Constants.UserPasswordInvalid }
                        }
                    });

                // nenhum código informado
                if (string.IsNullOrEmpty(verificationCode))
                    return BadRequest(new ResponseErrorViewModel
                    {
                        Status = Constants.Error,
                        Errors = new List<object>
                        {
                            new { Message = Constants.CodeInvalid }
                        }
                    });

                var emailVerificationDb =
                    await _emailVerificationService
                        .GetByCodeAsync(verificationCode);

                // não encontrou o código informado para realizar validação
                if (emailVerificationDb == null)
                    return BadRequest(new ResponseErrorViewModel
                    {
                        Status = Constants.Error,
                        Errors = new List<object>
                        {
                            new { Message = Constants.CodeInvalid }
                        }
                    });

                // buscar usuario por login                
                var userDb = await _userMananger
                    .GetByLogin(emailVerificationDb.Login);

                if (userDb == null)
                    return BadRequest(new ResponseErrorViewModel
                    {
                        Status = Constants.Error,
                        Errors = new List<object>
                        {
                            new { Message = Constants.UserNotFound }
                        }
                    });

                // criptografar nova senha
                userDb.Password = Util.Encrypt(newPassword.Trim());

                _userMananger.Update(userDb);

                _uow.Commit();

                // redirecionar para login
                var endPoint = _configuration.GetValue<string>(
                            Constants.RedirectLogin);

                return Redirect(endPoint);
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

        [HttpPost, Route("Rate"), Authorize]
        public async Task<IActionResult> Rating(
            [FromBody] RateViewModel rate
        )
        {
            try
            {
                if (string.IsNullOrEmpty(rate.Email))
                    return NotFound(new ResponseErrorViewModel
                    {
                        Status = Constants.Error,
                        Errors = new List<object>
                        {
                            new { Message = Constants.UserNotFound }
                        }
                    });

                if (rate.Note < 0)
                    return BadRequest(new ResponseErrorViewModel
                    {
                        Status = Constants.Error,
                        Errors = new List<object>
                        {
                            new { Message = Constants.InvalidParameter }
                        }
                    });

                var userRate = await _userMananger
                    .GetByLogin(rate.Email);

                userRate.TotalCount += 1;
                userRate.TotalCount += rate.Note;

                _userMananger.Update(userRate);

                var contactDb = await _contactMananger
                    .GetByEmail(rate.Email);

                var contactBook = await _contactMananger
                    .GetByOwner(rate.IdBook, contactDb.Id, rate.IdContact);

                contactBook.Available = true;

                _contactMananger.UpdateContactBook(contactBook);

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

        private async Task<IActionResult> ForgotPassword(string login)
        {
            try
            {
                // não informou login
                if (string.IsNullOrEmpty(login))
                    return BadRequest(new ResponseErrorViewModel
                    {
                        Status = Constants.Error,
                        Errors = new List<object>
                        {
                            new { Message = Constants.InvalidParameter }
                        }
                    });

                var userDb = await _userMananger
                    .GetByLogin(login);

                // não existe usuario
                if (userDb == null)
                    return BadRequest(new ResponseErrorViewModel
                    {
                        Status = Constants.Error,
                        Errors = new List<object>
                        {
                            new { Message = Constants.UserNotFound }
                        }
                    });

                // gerar email onde link deve redirecionar para nova senha
                var emailVerification = new EmailVerification
                {
                    Login = userDb.Login,
                    CreatedAt = Util.CurrentDateTime(),
                    CodeVerification = Util.GenerateGuid()
                };

                await _emailVerificationService
                    .CreateAsync(emailVerification);

                var redirect = _configuration
                    .GetSection(Constants.RedirectChangePassword).Value;

                // verificar para redirecionar para url do front 
                Util.SendEmailAsync(_configuration, emailVerification.CodeVerification,
                    userDb.Login, userDb.Person.Name,
                        HtmlEmailType.NewAccount, 
                            userDb.Person.PersonType.Equals("PJ")? 
                                HtmlEmailPersonType.LegalPerson : 
                                    HtmlEmailPersonType.IndividualPerson);

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