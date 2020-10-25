using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RelibreApi.Models;
using RelibreApi.Services;
using RelibreApi.Utils;
using RelibreApi.ViewModel;

namespace RelibreApi.Controllers
{
    [Route("api/v1/[controller]"), ApiController]
    public class BookController : ControllerBase
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

        public BookController(
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

        [HttpPost, Route("Image"), Authorize]
        public async Task<IActionResult> UploadImage(
            [FromForm(Name = "file")] IFormFile file,
            [FromServices] IConfiguration configuration
        )
        {
            try
            {
                if (file == null || file.Length <= 0)
                    return BadRequest(
                        new
                        {
                            Status = Constants.Error,
                            Errors = new List<object>
                            {
                                new
                                {
                                    Message = "Nenhuma imagem localizada!"
                                }
                            }
                        }
                    );

                var identifier = Util.GenerateGuid();

                await Util.UploadImage(configuration, file, identifier);

                var endPoint = string.Concat(configuration
                    .GetSection(Constants.EndpointImage).Value,
                        identifier, ".png");

                return Ok(new ResponseViewModel
                {
                    Result = new
                    {
                        image = endPoint
                    },
                    Status = Constants.Sucess
                });
            }
            catch (Exception ex)
            {
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

        [HttpPost, Route(""), Authorize]
        public async Task<IActionResult> CreateAsync(
            [FromBody] LibraryBookViewModel libraryBook
            )
        {
            try
            {
                // parametro necessário não foi carregado
                if (string.IsNullOrEmpty(libraryBook.Book.CodeIntegration))
                    return NotFound(new
                    {
                        Status = Constants.Error,
                        Errors = new List<object>
                        {
                            new 
                            {
                                Message = "Biblioteca não localizada!"
                            }
                        }
                    });

                var libraryBookMap = _mapper.Map<LibraryBook>(libraryBook);

                // verificar se já existe livro ativo na biblioteca

                var bookDb = await _bookMananger
                    .GetByCodeIntegration(libraryBookMap.Book.CodeIntegration);

                // realizar validações nos campos do livro quando não existir
                if (bookDb != null) libraryBookMap.Book = bookDb;

                // quando livro não existir criar 
                if (bookDb == null)
                {
                    bookDb = new Book();

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
                libraryBookMap.Price = userDb.Person.PersonType.Equals("PF") ? 0 : libraryBookMap.Price;
                libraryBookMap.CreatedAt = Util.CurrentDateTime();
                libraryBookMap.UpdatedAt = libraryBookMap.CreatedAt;

                await _libraryBookMananger.CreateAsync(libraryBookMap);

                _uow.Commit();

                var libraryBookCreated = _mapper.Map<LibraryBookViewModel>(libraryBookMap);

                return Created(new Uri(Url.ActionLink("Create", "Library")), 
                new ResponseViewModel
                {
                    Result = libraryBookCreated,
                    Status = Constants.Sucess
                });
            }
            catch (Exception ex)
            {
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
            [FromQuery(Name = "type")] string type,
            [FromQuery(Name = "title")] string title,
            [FromQuery(Name = "offset")] int offset,
            [FromQuery(Name = "limit")] int limit,
            [FromQuery(Name = "latitude")] int latitude,
            [FromQuery(Name = "longitude")] int longitude
            )
        {
            try
            {
                // offset é a partir de qual registro você quer
                // limit é o valor máximo de registros a serem retornados
                // capturar usuario que realizou a requisição
                var login = Util
                    .GetClaim(_httpContext,
                        Constants.UserClaimIdentifier);

                var user = await _userMananger
                    .GetByLoginOrDocumentNoTracking(login, "");

                // retorna livros da biblioteca do usuario
                if (string.IsNullOrEmpty(type) &&
                    string.IsNullOrEmpty(title) && user != null)
                {
                    var library = await _libraryMananger
                        .GetLibraryByPerson(user.Person.Id);

                    var libraryBookDb = await
                        GetByIdLibrary(library.Id, offset, limit);

                    var librariesBooksMap = _mapper
                        .Map<ICollection<LibraryBookViewModel>>(libraryBookDb);

                    return Ok(librariesBooksMap);
                }

                if (!string.IsNullOrEmpty(type) && user != null)
                {

                    // retorna todos os livros de todas as bibliotecas que 
                    // forem diferente do usuario da requisição de acordo com tipo 
                    var libraryBookDb = await
                        GetByType(type, user.Person.Library.Id, offset, limit);

                    if (libraryBookDb.Count <= 0)
                        return NoContent();

                    // TRAZER LIVROS RELACIONADOS POR CATEGORIA
                    var categories = libraryBookDb
                        .Select(x => x.Book.CategoryBooks
                            .Select(x => x.Category.Name)
                                .Distinct().ToArray()).Single();

                    // var associateds = new List<LibraryBook>();

                    // foreach (var category in categories)
                    // {
                    //     var assosiated = await GetByAssociated(category);

                    //     associateds.AddRange(assosiated);
                    // }

                    // // adiciona livros associados
                    // libraryBookDb.AddRange(associateds);

                    if (libraryBookDb.Count <= 0)
                        return NoContent();

                    var librariesBooksMap = _mapper
                        .Map<ICollection<LibraryBookViewModel>>(libraryBookDb)
                        .Select(x => new
                        {
                            Distance = string.Format("{0:0}",
                                Util.Distance(latitude, longitude,
                                Convert.ToDouble(x.Addresses.SingleOrDefault(x => x.Master == true).Latitude),
                                Convert.ToDouble(x.Addresses.SingleOrDefault(x => x.Master == true).Longitude))).Substring(0, 4),
                            x.Book,
                            x.Contact,
                            x.id,
                            x.Images,
                            x.Types
                        })
                        .OrderBy(x => x.Distance);

                    return Ok(librariesBooksMap);
                }

                // retorna todos os livros de todas as bibliotecas de acordo com título
                var libraryBooks = await
                    GetByBookTitle(title, user.Person.Library.Id, offset, limit);

                // verificar, não está mapeando os autores, categorias, e tipos
                var libraryBooksMaps = _mapper
                    .Map<ICollection<LibraryBookViewModel>>(libraryBooks)
                    .Select(x => new
                    {
                        Distance = string.Format("{0:0}",
                                Util.Distance(latitude, longitude,
                                Convert.ToDouble(x.Addresses.SingleOrDefault(x => x.Master == true).Latitude),
                                Convert.ToDouble(x.Addresses.SingleOrDefault(x => x.Master == true).Longitude))).Substring(0, 4),
                        x.Book,
                        x.Contact,
                        x.id,
                        x.Images,
                        x.Types
                    })
                    .OrderBy(x => x.Distance);

                return Ok(libraryBooksMaps);
            }
            catch (Exception ex)
            {
                // gerar log
                return BadRequest(Util.ReturnException(ex));
            }
        }

        private async Task<List<LibraryBook>> Combination(long idLibraryRequest, int offset, int limit)
        {
            // trazer todos os livros de todos os tipos para realizar validação
            var libraryBooks = await
                GetByType("all", idLibraryRequest, offset, limit);

            if (libraryBooks.Count <= 0)
                throw new ArgumentNullException();

            var libraryBooksInteresses = await
                GetByType("interesse", idLibraryRequest, offset, limit);

            if (libraryBooksInteresses.Count <= 0)
                throw new ArgumentNullException();

            var libraryBooksCombination = new List<LibraryBook>();

            foreach (var libraryBookInteresse in libraryBooksInteresses)
            {
                var libraryBook =
                    libraryBooks.Where(x =>
                        x.Book.CodeIntegration
                            .Equals(libraryBookInteresse.Book.CodeIntegration) &&
                            x.LibraryBookTypes.Any(
                                    x => x.LibraryBook.Book.Id == libraryBookInteresse.Book.Id));

                if (libraryBook != null)
                {
                    libraryBooksCombination.Add(libraryBookInteresse);
                }
            }

            if (libraryBooksCombination.Count <= 0)
                throw new ArgumentNullException();

            // gerar notificações quando houver combinações
            return libraryBooksCombination;
        }
        private Task<List<LibraryBook>> GetByIdLibrary(long idLibrary, int offset, int limit)
        {
            return _libraryBookMananger.GetByIdLibrary(idLibrary, offset, limit);
        }
        private Task<List<LibraryBook>> GetByBookTitle(string title, long idLibraryRequest, int offset, int limit)
        {
            return _libraryBookMananger
                .GetByBookTitle(
                    Util.RemoveSpecialCharacter(title), idLibraryRequest, offset, limit);
        }
        private async Task<List<LibraryBook>> GetByType(string type, long idLibraryRequest, int offset, int limit)
        {
            // TROCAR
            // EMPRESTAR
            // DOAR
            // VENDER
            // EMPRESAS
            // COMBINACOES
            if (type.ToLower().Equals("business"))
                return await _libraryBookMananger.GetByBusiness(offset, limit);

            if (type.ToLower().Equals("combination"))
                return await Combination(idLibraryRequest, offset, limit);

            var typeDb = new Models.Type();

            if (!type.ToLower().Equals("all"))
            {
                typeDb = await _typeMananger.GetByDescriptionAsync(type);

                if (type == null) throw new ArgumentNullException();

                idLibraryRequest = (typeDb
                    .Description.ToLower()
                        .Equals("interesse")) ? -1 : idLibraryRequest;
            }

            return await _libraryBookMananger
                .GetByTypeNoTracking(typeDb, idLibraryRequest, offset, limit);
        }
        private Task<List<LibraryBook>> GetByAssociated(string category)
        {
            return _libraryBookMananger
                .GetByAssociated(category);
        }

    }
}