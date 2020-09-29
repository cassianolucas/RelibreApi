using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
    public class LibraryController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILibraryBook _libraryBookMananger;
        private readonly IBook _bookMananger;
        private readonly IContact _contactMananger;
        private readonly ILibrary _libraryMananger;
        private readonly IType _typeMananger;
        private readonly HttpContext _httpContext;
        private readonly IUser _userMananger;

        public LibraryController(
            [FromServices] IUnitOfWork uow,
            [FromServices] IMapper mapper,
            [FromServices] ILibraryBook libraryBookMananger,
            [FromServices] IBook bookMananger,
            [FromServices] IContact contactMananger,
            [FromServices] ILibrary libraryMananger,
            [FromServices] IType typeMananger,
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromServices] IUser userMananger
            )
        {
            _uow = uow;
            _mapper = mapper;
            _libraryBookMananger = libraryBookMananger;
            _bookMananger = bookMananger;
            _contactMananger = contactMananger;
            _libraryMananger = libraryMananger;
            _typeMananger = typeMananger;
            _httpContext = httpContextAccessor.HttpContext;
            _userMananger = userMananger;
        }

        [HttpPost, Route(""), Authorize]
        public async Task<IActionResult> CreateAsync(
            [FromBody] LibraryBookViewModel libraryBook
            )
        {
            try
            {
                var libraryBookMap = _mapper.Map<LibraryBook>(libraryBook);

                var bookDb = await _bookMananger.GetByCodeIntegration(libraryBookMap.Book.CodeIntegration);

                // realizar validações nos campos do livro quando não existir
                if (bookDb != null) libraryBookMap.Book = bookDb;

                // quando livro não existir criar 
                if (bookDb == null)
                {
                    // verificar se existe autores
                    // verificar se existe categorias
                    await _bookMananger.CreateAsync(libraryBookMap.Book);
                }
                 
                // capturar email de usuario logado se vier nulo
                if (libraryBook.Contact == null || string.IsNullOrEmpty(libraryBook.Contact.Email))
                {
                    // capturar do cadastro
                    var login = Util.Claim(_httpContext, "email_login");

                    var userDb = await _userMananger.GetByLogin(login);

                    libraryBookMap.Contact = new Contact();
                    libraryBookMap.Contact.Phone = userDb.Person.Phones.SingleOrDefault(x => x.Master == true).Number;
                    libraryBookMap.Contact.Email = userDb.Login;
                    libraryBookMap.Contact.Active = true;
                    libraryBookMap.Contact.CreatedAt = Util.CurrentDateTime();
                    libraryBookMap.Contact.UpdatedAt = Util.CurrentDateTime();                
                }
                else
                {
                    var contactDb = await _contactMananger.GetByEmail(libraryBookMap.Contact.Email);

                    libraryBookMap.Contact = contactDb;
                }
                    
                var libraryDb = await _libraryMananger.GetByIdAsync(libraryBookMap.IdLibrary);

                if (libraryDb != null) libraryBookMap.Library = libraryDb;

                // capturar tipos 
                var typesDb = new List<Models.Type>();
                libraryBookMap.LibraryBookTypes = new List<LibraryBookType>();

                foreach (var type in libraryBook.Types)
                {
                    var typeDb = await _typeMananger.GetByDescriptionAsync(type.Description);

                    libraryBookMap.LibraryBookTypes.Add(new LibraryBookType
                    {
                        LibraryBook = libraryBookMap,
                        Type = typeDb
                    });
                }

                libraryBookMap.Active = true;
                libraryBookMap.CreatedAt = Util.CurrentDateTime();
                libraryBookMap.UpdatedAt = libraryBookMap.CreatedAt;

                await _libraryBookMananger.CreateAsync(libraryBookMap);

                _uow.Commit();

                var libraryBookCreated = _mapper.Map<LibraryBookViewModel>(libraryBookMap);

                return Created(new Uri(Url.ActionLink("Create", "Library")), libraryBookCreated);
            }
            catch (Exception ex)
            {
                return BadRequest(Util.ReturnException(ex));
            }
        }

        [HttpPut, Route(""), Authorize]
        public async Task<IActionResult> UpdateAsync(
            [FromBody] LibraryBookViewModel library
            )
        {
            try
            {
                var libraryDb = await _libraryBookMananger.GetByIdAsync(library.id);

                _libraryBookMananger.Update(null);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(Util.ReturnException(ex));
            }
        }

        [HttpDelete, Route(""), Authorize]
        public IActionResult RemoveAsync(
            [FromQuery(Name = "id")] int id
            )
        {
            try
            {
                _libraryBookMananger.RemoveAsync(0);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(Util.ReturnException(ex));
            }
        }

        [HttpGet, Route(""), Authorize]
        public async Task<IActionResult> GetAsync(
            [FromQuery(Name = "id_library")] int idLibrary,
            [FromQuery(Name = "title")] string title,
            [FromQuery(Name = "offset")] int offset,
            [FromQuery(Name = "limit")] int limit
            )
        {
            try
            {
                // offset é a partir de qual registro você quer
                // limit é o valor máximo de registros a serem retornados
                if (idLibrary > 0) return Ok(await GetByIdLibrary(idLibrary, offset, limit));

                var libraryBooks = await GetByBookTitle(title, offset, limit);

                // verificar, não está mapeando os autores, categorias, e tipos
                var libraryBooksMaps = _mapper.Map<ICollection<LibraryBookViewModel>>(libraryBooks);

                return Ok(libraryBooksMaps);
            }
            catch (Exception ex)
            {
                return BadRequest(Util.ReturnException(ex));
            }
        }

        private Task<List<LibraryBook>> GetByIdLibrary(long idLibrary, int offset, int limit)
        {
            return _libraryBookMananger.GetByIdLibrary(idLibrary, offset, limit);
        }

        private Task<List<LibraryBook>> GetByBookTitle(string title, int offset, int limit)
        {
            return _libraryBookMananger.GetByBookTitle(Util.RemoveSpecialCharacter(title), offset, limit);
        }

    }
}