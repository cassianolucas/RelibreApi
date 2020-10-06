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
using static RelibreApi.Utils.Constants;

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
        private readonly IAuthor _authorMananger;
        private readonly ICategory _categoryMananger;

        public LibraryController(
            [FromServices] IUnitOfWork uow,
            [FromServices] IMapper mapper,
            [FromServices] ILibraryBook libraryBookMananger,
            [FromServices] IBook bookMananger,
            [FromServices] IContact contactMananger,
            [FromServices] ILibrary libraryMananger,
            [FromServices] IType typeMananger,
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromServices] IUser userMananger,
            [FromServices] IAuthor authorMananger,
            [FromServices] ICategory categoryMananger
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
            _authorMananger = authorMananger;
            _categoryMananger = categoryMananger;
        }

        [HttpPost, Route(""), Authorize]
        public async Task<IActionResult> CreateAsync(
            [FromBody] LibraryBookViewModel libraryBook
            )
        {
            try
            {
                // parametro necessário não foi carregado
                if (string.IsNullOrEmpty(libraryBook.Book.CodeIntegration))
                    return NoContent();

                var libraryBookMap = _mapper.Map<LibraryBook>(libraryBook);

                var bookDb = await _bookMananger
                    .GetByCodeIntegration(libraryBookMap.Book.CodeIntegration);

                // realizar validações nos campos do livro quando não existir
                if (bookDb != null) libraryBookMap.Book = bookDb;

                // quando livro não existir criar 
                if (bookDb == null)
                {
                    // verificar se existe autores
                    var authors = new List<Author>();
                    foreach (var authorMap in libraryBookMap.Book.AuthorBooks)
                    {
                        var authorDb = await _authorMananger
                            .GetByName(authorMap.Author.Name);

                        if (authorDb != null)
                        {
                            libraryBookMap.Book.AuthorBooks.Add(new AuthorBook
                            {
                                Author = authorDb,
                                Book = libraryBookMap.Book
                            });
                        }
                    }

                    // verificar se existe categorias
                    foreach (var categoryMap in libraryBookMap.Book.CategoryBooks)
                    {
                        var categoryDb = await _categoryMananger
                            .GetByName(categoryMap.Category.Name);

                        if (categoryDb != null)
                        {
                            libraryBookMap.Book.CategoryBooks.Add(new CategoryBook
                            {
                                Category = categoryDb,
                                Book = libraryBookMap.Book
                            });
                        }
                    }

                    bookDb.CreatedAt = Util.CurrentDateTime();

                    await _bookMananger.CreateAsync(libraryBookMap.Book);
                }

                // capturar do cadastro
                var login = Util.GetClaim(_httpContext,
                    Constants.UserClaimIdentifier);

                var userDb = await _userMananger.GetByLogin(login);

                // capturar email de usuario logado se vier nulo
                if (libraryBook.Contact == null ||
                    string.IsNullOrEmpty(libraryBook.Contact.Email))
                {
                    libraryBookMap.Contact = new Contact();
                    libraryBookMap.Contact.Phone = userDb.Person.Phones
                        .SingleOrDefault(x => x.Master == true).Number;
                    libraryBookMap.Contact.Email = userDb.Login;
                    libraryBookMap.Contact.Active = true;
                    libraryBookMap.Contact.CreatedAt = Util.CurrentDateTime();
                    libraryBookMap.Contact.UpdatedAt = Util.CurrentDateTime();
                }
                else
                {
                    var contactDb = await _contactMananger
                        .GetByEmail(libraryBookMap.Contact.Email);

                    libraryBookMap.Contact = contactDb;
                }

                var libraryDb = await _libraryMananger
                    .GetLibraryByPerson(userDb.IdPerson);

                if (libraryDb != null) libraryBookMap.Library = libraryDb;

                // capturar tipos 
                var typesDb = new List<Models.Type>();
                libraryBookMap.LibraryBookTypes = new List<LibraryBookType>();

                foreach (var type in libraryBook.Types)
                {
                    var typeDb = await _typeMananger
                        .GetByDescriptionAsync(type.Description);

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

                // não localizou
                if (libraryDb == null) return NoContent();

                libraryDb.UpdatedAt = Util.CurrentDateTime();

                _libraryBookMananger.Update(libraryDb);

                return Ok("");
            }
            catch (Exception ex)
            {
                return BadRequest(Util.ReturnException(ex));
            }
        }

        [HttpDelete, Route(""), Authorize]
        public async Task<IActionResult> RemoveAsync(
            [FromQuery(Name = "id")] int id
            )
        {
            try
            {
                var libraryDb = await _libraryBookMananger.GetByIdAsync(id);

                // não localizou
                if (libraryDb == null) return NoContent();

                _libraryBookMananger.RemoveAsync(id);

                return Ok("");
            }
            catch (Exception ex)
            {
                return BadRequest(Util.ReturnException(ex));
            }
        }

        [HttpGet, Route(""), Authorize]
        public async Task<IActionResult> GetAsync(
            [FromQuery(Name = "id_library")] int idLibrary,
            [FromQuery(Name = "type")] string type,
            [FromQuery(Name = "title")] string title,
            [FromQuery(Name = "offset")] int offset,
            [FromQuery(Name = "limit")] int limit
            )
        {
            try
            {
                // offset é a partir de qual registro você quer
                // limit é o valor máximo de registros a serem retornados

                // retorna livros da biblioteca do usuario
                if (idLibrary > 0)
                {
                    var libraryBookDb =  await GetByIdLibrary(idLibrary, offset, limit);

                    var librariesBooksMap = _mapper
                        .Map<ICollection<LibraryBookViewModel>>(libraryBookDb);

                    return Ok(librariesBooksMap);
                }

                // capturar usuario que realizou a requisição
                var login = Util.GetClaim(_httpContext, Constants.UserClaimIdentifier);
                var user = await _userMananger.GetByLoginOrDocumentNoTracking(login, "");

                var address = user != null ?
                        user.Person.Addresses.SingleOrDefault(x => x.Master == true) : null;

                if (!string.IsNullOrEmpty(type))
                {

                    // retorna todos os livros de todas as bibliotecas que 
                    // forem diferente do usuario da requisição de acordo com tipo 
                    var libraryBookDb = await GetByType(type, user.Person.Library.Id, offset, limit);

                    var librariesBooksMap = _mapper
                        .Map<ICollection<LibraryBookViewModel>>(libraryBookDb)
                        .Select(x => new
                        {
                            Distance = string.Format("{0:0}",
                                Util.Distance(Convert.ToDouble(address.Latitude),
                                Convert.ToDouble(address.Longitude),
                                Convert.ToDouble(x.Addresses.SingleOrDefault(x => x.Master == true).Latitude),
                                Convert.ToDouble(x.Addresses.SingleOrDefault(x => x.Master == true).Longitude))),
                            x.Book,
                            x.Contact,
                            x.id,
                            x.IdLibrary,
                            x.Images,
                            x.Reating,
                            x.Types
                        })
                        .OrderBy(x => x.Distance);

                    // TRAZER LIVROS RELACIONADOS POR CATEGORIA

                    return Ok(librariesBooksMap);
                }

                // retorna todos os livros de todas as bibliotecas de acordo com título
                var libraryBooks = await GetByBookTitle(title, user.Person.Library.Id, offset, limit);

                // verificar, não está mapeando os autores, categorias, e tipos
                var libraryBooksMaps = _mapper
                    .Map<ICollection<LibraryBookViewModel>>(libraryBooks)
                    .Select(x => new
                    {
                        Distance = string.Format("{0:0}",
                                Util.Distance(Convert.ToDouble(address.Latitude),
                                Convert.ToDouble(address.Longitude),
                                Convert.ToDouble(x.Addresses.SingleOrDefault(x => x.Master == true).Latitude),
                                Convert.ToDouble(x.Addresses.SingleOrDefault(x => x.Master == true).Longitude))),
                        x.Book,
                        x.Contact,
                        x.id,
                        x.IdLibrary,
                        x.Images,
                        x.Reating,
                        x.Types
                    })
                    .OrderBy(x => x.Distance);

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

        private Task<List<LibraryBook>> GetByBookTitle(string title, long idLibraryRequest, int offset, int limit)
        {
            return _libraryBookMananger
                .GetByBookTitle(Util.RemoveSpecialCharacter(title), idLibraryRequest, offset, limit);
        }
        private async Task<List<LibraryBook>> GetByType(string type, long idLibraryRequest, int offset, int limit)
        {
            // TROCAR
            // EMPRESTAR
            // DOAR
            // VENDER                        
            var typeDb = await _typeMananger.GetByDescriptionAsync(type);

            if (type == null) throw new ArgumentNullException();
            
            idLibraryRequest = (typeDb.Description.ToLower().Equals("interesse"))? -1: idLibraryRequest;
            
            return await _libraryBookMananger.GetByTypeNoTracking(typeDb, idLibraryRequest, offset, limit);
        }

    }
}