using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ContactController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly HttpContext _httpContext;
        private readonly ILibraryBook _libraryBookMananger;
        private readonly IContact _contactMananger;
        private readonly INotification _notificationMananger;
        private readonly INotificationPerson _notificationPersonMananger;
        private readonly IUser _userMananger;
        public ContactController(
            [FromServices] IUnitOfWork uow,
            [FromServices] IMapper mapper,
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromServices] ILibraryBook libraryBookMananger,
            [FromServices] IContact contactMananger,
            [FromServices] INotification notificationMananger,
            [FromServices] INotificationPerson notificationPersonMananger,
            [FromServices] IUser userMananger
        )
        {
            _uow = uow;
            _mapper = mapper;
            _httpContext = httpContextAccessor.HttpContext;
            _libraryBookMananger = libraryBookMananger;
            _contactMananger = contactMananger;
            _notificationMananger = notificationMananger;
            _notificationPersonMananger = notificationPersonMananger;
            _userMananger = userMananger;
        }

        [HttpPost, Route(""), Authorize]
        public async Task<IActionResult> CreateAsync(
            [FromBody] CreateContactViewModel createContactViewModel
        )
        {
            try
            {
                if (createContactViewModel.IdLibraryBook <= 0)
                    return BadRequest(new ResponseErrorViewModel
                    {
                        Status = Constants.Error,
                        Errors = new List<object>
                        {
                            new { Message = Constants.LibraryNotFound }
                        }
                    });

                // buscar do usuario autenticado
                var login = Util.GetClaim(_httpContext,
                    Constants.UserClaimIdentifier);

                var userDbRequest = await _userMananger
                        .GetByLogin(login);

                if (userDbRequest == null)
                    return BadRequest(new ResponseErrorViewModel
                    {
                        Status = Constants.Error,
                        Errors = new List<object>
                        {
                            new { Message = Constants.UserNotFound }
                        }
                    });

                // verificar se existe contado de usuario logado
                var contactDbRequest = await _contactMananger
                    .GetByEmail(userDbRequest.Login);

                // criar contato caso não exista
                if (contactDbRequest == null)
                {
                    contactDbRequest = new Contact
                    {
                        Email = userDbRequest.Login,
                        Name = userDbRequest.Person.Name,
                        Phone = userDbRequest.Person.Phones
                            .SingleOrDefault(x => x.Master == true).Number,
                        Active = true,
                        CreatedAt = Util.CurrentDateTime()
                    };
                }
                else
                {
                    // verifica se já solicitou contato para o mesmo livro
                    var contactDb = await _contactMananger
                        .GetByIdLiraryAndContactRequest(
                            createContactViewModel.IdLibraryBook, 
                                contactDbRequest.Id);

                    if (contactDb != null)
                    return Conflict(new ResponseErrorViewModel
                    {
                        Errors = new List<object>
                        {
                            new { Message = Constants.ContactConflict }
                        },
                        Status = Constants.Error
                    });
                }

                contactDbRequest.UpdatedAt = Util.CurrentDateTime();

                if (contactDbRequest.Id > 0)
                {
                    _contactMananger.Update(contactDbRequest);
                }
                else
                {
                    await _contactMananger.CreateAsync(contactDbRequest);
                }

                // buscar livro da biblioteca 
                var libraryBook = await _libraryBookMananger
                    .GetByIdAsync(createContactViewModel.IdLibraryBook);

                if (libraryBook == null)
                    return BadRequest(new ResponseErrorViewModel
                    {
                        Status = Constants.Error,
                        Errors = new List<object>
                        {
                            new { Message = Constants.BooksNotFound }
                        }
                    });

                // buscar o login do usuario dono da biblioteca para criar cadastro
                var userOwner = await _userMananger
                    .GetByIdAsyncNoTracking(libraryBook.Library.Person.Id);

                // verifica se o livro requisitado não é do mesmo usuario
                if (userOwner.Login.Equals(userDbRequest.Login))
                    return BadRequest(new ResponseErrorViewModel
                    {
                        Status = Constants.Error,
                        Errors = new List<object>
                        {
                            new { Message = Constants.BookInvalid }
                        }
                    });

                if (userOwner == null)
                    return BadRequest(new ResponseErrorViewModel
                    {
                        Status = Constants.Error,
                        Errors = new List<object>
                        {
                            new { Message = Constants.UserNotFound }
                        }
                    });

                var contactDbOwner = await _contactMananger
                    .GetByEmail(userOwner.Login);

                if (contactDbOwner == null)
                {
                    contactDbOwner = new Contact
                    {
                        Name = userOwner.Person.Name,
                        Email = userOwner.Login,
                        Phone = userOwner.Person.Phones
                            .SingleOrDefault(x => x.Master == true).Number,
                        ContactBooksOwner = new List<ContactBook>(),
                        Active = true,
                        CreatedAt = Util.CurrentDateTime()
                    };
                }

                contactDbOwner.UpdatedAt = Util.CurrentDateTime();

                var contactBook = new ContactBook
                {
                    ContactOwner = contactDbOwner,
                    ContactRequest = contactDbRequest,
                    LibraryBook = libraryBook,
                    Approved = libraryBook.LibraryBookTypes.Any(x => x.Type.Id == 5),
                    Denied = false,
                    CreatedAt = Util.CurrentDateTime()
                };

                contactDbOwner.ContactBooksOwner.Add(contactBook);

                if (contactDbOwner.Id > 0)
                {
                    _contactMananger.Update(contactDbOwner);
                }
                else
                {
                    await _contactMananger.CreateAsync(contactDbOwner);
                }

                // gerar notificação
                var notification = new Notification
                {
                    Name = "Você tem uma solicitação de contado!",
                    Description = "Entre em seu perfil e verifique os contatos!",
                    CreatedAt = Util.CurrentDateTime()
                };

                await _notificationMananger.CreateAsync(notification);

                var personNotification = new NotificationPerson
                {
                    Notification = notification,
                    Person = libraryBook.Library.Person,
                    Active = true,
                    CreatedAt = Util.CurrentDateTime()
                };

                await _notificationPersonMananger
                    .CreateAsync(personNotification);

                _uow.Commit();

                return Created(
                    new Uri(Url.ActionLink("Create", "Contact")), new ResponseViewModel
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

        [HttpPost, Route("Approve"), Authorize]
        public async Task<IActionResult> ApproveAsync(
            [FromBody] ContactBookViewModel contactBookViewModel
        )
        {
            try
            {
                var login = Util.GetClaim(_httpContext,
                    Constants.UserClaimIdentifier);

                var contactDb = await _contactMananger.GetByEmail(login);

                if (contactDb == null)
                    return BadRequest(new ResponseErrorViewModel
                    {
                        Status = Constants.Error,
                        Errors = new List<object>
                        {
                            new { Message = "Contato não localizado!" }
                        }
                    });

                // criar nova query no banco para retornar aprovado e reprovado                

                // buscar contato de acordo com livro e contato
                var contactBook = await _contactMananger
                    .GetByOwner(contactBookViewModel.LibraryBook.Id,
                        contactDb.Id, contactBookViewModel.IdContact);

                if (contactBook == null)
                    return BadRequest(new ResponseErrorViewModel
                    {
                        Result = Constants.Error,
                        Errors = new List<object>
                        {
                            new { Message = "Contato não localizado!" }
                        }
                    });

                contactBook.Approved = contactBookViewModel.Approved;
                contactBook.Denied = contactBookViewModel.Denied;

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

        [HttpPost, Route("Public"), AllowAnonymous]
        public async Task<IActionResult> CreatePublicAsync(
            [FromBody] CreateContactPublicViewModel contact
        )
        {
            try
            {
                var errors = new List<object>();

                var libraryBookDb = new LibraryBook();

                // validar os dados
                if (string.IsNullOrEmpty(contact.Email))
                    errors.Add(new { Message = Constants.UserLoginInvalid });

                if (string.IsNullOrEmpty(contact.Name))
                    errors.Add(new { Message = Constants.UserNameInvalid });

                if (string.IsNullOrEmpty(contact.Phone))
                    errors.Add(new { Message = Constants.UserPhoneInvalid });

                if (contact.IdBook <= 0)
                {
                    errors.Add(new { Message = "Livro inválido!" });
                }
                else
                {
                    // buscar biblioteca com livro
                    libraryBookDb = await _libraryBookMananger
                        .GetByIdAsync(contact.IdBook);

                    if (libraryBookDb == null)
                        errors.Add(new { Message = Constants.BooksNotFound });
                }

                if (errors.Count > 0)
                    return NotFound(new ResponseErrorViewModel
                    {
                        Result = Constants.Error,
                        Errors = errors
                    });

                var contactDbRequest = await _contactMananger
                    .GetByEmail(contact.Email);

                // criar contato caso não exista
                if (contactDbRequest == null)
                {
                    contactDbRequest = new Contact
                    {
                        Email = contact.Email,
                        Name = contact.Name,
                        Phone = contact.Phone
                            .Replace("(", "")
                            .Replace(")", "")
                            .Replace("-", "")
                            .Replace(" ", "")
                            .Trim(),
                        Active = true,
                        CreatedAt = Util.CurrentDateTime()
                    };
                }
                else
                {
                    // atualiza ultima alteração
                    contactDbRequest.UpdatedAt = Util.CurrentDateTime();
                }

                var userDb = await _userMananger
                    .GetByIdAsync(libraryBookDb.Library.Person.Id);

                var contactDbOwner = await _contactMananger
                    .GetByEmail(userDb.Login);

                if (contactDbOwner == null)
                {
                    contactDbOwner = new Contact
                    {
                        Email = userDb.Login,
                        Name = userDb.Person.Name,
                        Phone = userDb.Person.Phones
                            .Single(x => x.Master == true).Number,
                        ContactBooksOwner = new List<ContactBook>(),
                        Active = true,
                        CreatedAt = Util.CurrentDateTime()
                    };
                }
                else
                {
                    // atualiza ultima alteração
                    contactDbOwner.UpdatedAt = Util.CurrentDateTime();
                }

                // cria novo registro de contato
                var contactBook = new ContactBook
                {
                    ContactOwner = contactDbOwner,
                    ContactRequest = contactDbRequest,
                    LibraryBook = libraryBookDb,
                    Approved = libraryBookDb.LibraryBookTypes.Any(x => x.Type.Id == 5),
                    Denied = false
                };

                contactDbOwner
                    .ContactBooksOwner.Add(contactBook);

                if (contactDbOwner.Id > 0)
                {
                    _contactMananger.Update(contactDbOwner);
                }
                else
                {
                    await _contactMananger
                        .CreateAsync(contactDbOwner);
                }

                // gerar notificação
                var notification = new Notification
                {
                    Name = "Você tem uma solicitação de contado!",
                    Description = "Entre em seu perfil e verifique os contatos!",
                    CreatedAt = Util.CurrentDateTime()
                };

                await _notificationMananger.CreateAsync(notification);

                var personNotification = new NotificationPerson
                {
                    Notification = notification,
                    Person = libraryBookDb.Library.Person,
                    Active = true,
                    CreatedAt = Util.CurrentDateTime()
                };

                await _notificationPersonMananger
                    .CreateAsync(personNotification);

                _uow.Commit();

                return Created(
                    new Uri(Url.ActionLink("CreatePublic", "Contact")),
                    new ResponseViewModel
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
            [FromQuery(Name = "type")] string type,
            [FromQuery(Name = "approved")] bool approved,
            [FromQuery(Name = "denied")] bool denied,
            [FromQuery(Name = "offset")] int offset,
            [FromQuery(Name = "limit")] int limit
        )
        {
            try
            {
                if (string.IsNullOrEmpty(type))
                    return BadRequest(new ResponseErrorViewModel
                    {
                        Status = Constants.Error,
                        Errors = new List<object>
                        {
                            new { Message = Constants.InvalidParameter }
                        }
                    });

                var login = Util.GetClaim(_httpContext,
                    Constants.UserClaimIdentifier);

                var contactsDb = new List<ContactBook>();

                if (type.ToLower().Equals("send"))
                {
                    if (string.IsNullOrEmpty(login))
                        return BadRequest(new ResponseErrorViewModel
                        {
                            Status = Constants.Error,
                            Errors = new List<object>
                            {
                                new { Message = Constants.UserNotFound }
                            }
                        });

                    contactsDb = await _contactMananger
                        .GetByRequestNoTracking(login, approved, denied, limit, offset);
                }

                if (type.ToLower().Equals("received"))
                {
                    contactsDb = await _contactMananger
                        .GetByOwnerNoTracking(login, approved, denied, limit, offset);
                }

                var userDb = await _userMananger.GetByLogin(login);

                var addressDb = userDb.Person.Addresses
                    .SingleOrDefault(x => x.Master == true);

                // trazer todos os dados quando approved for = true                
                if (approved)
                {
                    var contacsMap = _mapper
                        .Map<List<ContactBookApprovedViewModel>>(contactsDb)
                        .Select(x => new
                        {
                            Denied = x.Denied,
                            Distance = Util.Distance(addressDb,
                                _userMananger.GetByLogin(x.Email)
                                    .Result.Person.Addresses.SingleOrDefault(x => x.Master == true)),
                            Email = x.Email,
                            full_name = x.FullName,
                            id_contact = x.IdContact,
                            Phone = x.Phone,
                            Rating = _userMananger.GetRatingByLogin(x.Email),
                            created_at = x.CreatedAt,
                            x.LibraryBook.Book,
                            id_book = x.LibraryBook.Id,
                            Price = x.LibraryBook.Price
                        });

                    return Ok(new ResponseViewModel
                    {
                        Result = contacsMap
                            .OrderBy(x => x.Distance),
                        Status = Constants.Sucess
                    });
                }
                else
                {
                    var contacsMap = _mapper
                        .Map<List<ContactBookViewModel>>(contactsDb)
                        .Select(x => new
                        {
                            Denied = x.Denied,
                            Distance = Util.Distance(addressDb,
                                _userMananger.GetByLogin(x.Email)
                                    .Result.Person.Addresses.SingleOrDefault(x => x.Master == true)),
                            full_name = x.FullName,
                            id_contact = x.IdContact,
                            Rating = _userMananger.GetRatingByLogin(x.Email),
                            created_at = x.CreatedAt,
                            x.LibraryBook.Book,
                            id_book = x.LibraryBook.Id,
                            Price = x.LibraryBook.Price
                        });

                    return Ok(new ResponseViewModel
                    {
                        Result = contacsMap
                            .OrderBy(x => x.Distance),
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
    }
}