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

        [HttpPost, Route(""), Authorize]
        public async Task<IActionResult> CreateAsync(
            [FromBody] LibraryBookViewModel libraryBook
            )
        {
            try
            {
                // parametro necessário não foi carregado
                if (string.IsNullOrEmpty(libraryBook.Book.CodeIntegration))
                    return NotFound(new ResponseErrorViewModel
                    {
                        Status = Constants.Error,
                        Errors = new List<object>
                        {
                            new { Message = Constants.LibraryNotFound }
                        }
                    });

                var libraryBookMap = _mapper.Map<LibraryBook>(libraryBook);

                var bookDb = await _bookMananger
                    .GetByCodeIntegration(libraryBookMap.Book.CodeIntegration);

                // verificar se já existe livro ativo na biblioteca



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

                        if (authorDb == null)
                        {
                            authorDb = new Author
                            {
                                Name = authorMap.Author.Name,
                                Active = true,
                                CreatedAt = Util.CurrentDateTime(),
                                UpdatedAt = Util.CurrentDateTime()
                            };
                        }

                        if (bookDb.AuthorBooks == null)
                            bookDb.AuthorBooks = new List<AuthorBook>();

                        bookDb.AuthorBooks.Add(
                            new AuthorBook
                            {
                                Author = authorDb,
                                Book = bookDb
                            }
                        );
                    }

                    // verificar se existe categorias
                    foreach (var categoryMap in libraryBookMap.Book.CategoryBooks)
                    {
                        var categoryDb = await _categoryMananger
                            .GetByName(categoryMap.Category.Name);

                        if (categoryDb == null)
                        {
                            categoryDb = new Category
                            {
                                Name = categoryMap.Category.Name,
                                CreatedAt = Util.CurrentDateTime()
                            };
                        }

                        if (bookDb.CategoryBooks == null)
                            bookDb.CategoryBooks = new List<CategoryBook>();

                        bookDb.CategoryBooks.Add(
                            new CategoryBook
                            {
                                Category = categoryDb,
                                Book = bookDb
                            }
                        );
                    }
                    
                    bookDb.AverageRating = libraryBookMap.Book.AverageRating;
                    bookDb.CodeIntegration = libraryBookMap.Book.CodeIntegration;
                    bookDb.Isbn13 = libraryBookMap.Book.Isbn13;
                    bookDb.Title = libraryBookMap.Book.Title;
                    bookDb.MaturityRating = libraryBookMap.Book.MaturityRating;
                    bookDb.CreatedAt = Util.CurrentDateTime();
                }

                // capturar do cadastro
                var login = Util.GetClaim(_httpContext,
                    Constants.UserClaimIdentifier);

                var userDb = await _userMananger.GetByLogin(login);

                if (userDb.Person.Addresses == null ||
                    userDb.Person.Addresses.Count <= 0)
                    return NotFound(new ResponseErrorViewModel
                    {
                        Status = Constants.Error,
                        Errors = new List<object>
                        {
                            new { Message = Constants.UserAddressNotFound }
                        }
                    });

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

                    if (typeDb != null)
                    {
                        libraryBookMap.LibraryBookTypes.Add(new LibraryBookType
                        {
                            LibraryBook = libraryBookMap,
                            Type = typeDb
                        });
                    }
                }

                // não localizou os tipos
                if (libraryBookMap.LibraryBookTypes.Count <= 0)
                    return BadRequest(new ResponseErrorViewModel
                    {
                        Errors = new List<object>
                        {
                            new { Message = Constants.InvalidType }
                        },
                        Result = Constants.Error
                    });
                libraryBookMap.Description = libraryBook.Book.Description;
                libraryBookMap.Book = bookDb;
                libraryBookMap.Active = true;
                libraryBookMap.Price = userDb.Person.PersonType
                    .Equals("PF") ? 0 : libraryBookMap.Price;
                libraryBookMap.CreatedAt = Util.CurrentDateTime();
                libraryBookMap.UpdatedAt = libraryBookMap.CreatedAt;

                await _libraryBookMananger.CreateAsync(libraryBookMap);

                _uow.Commit();

                var libraryBookCreated = _mapper.Map<LibraryBookViewModel>(libraryBookMap);

                return Created(
                    new Uri(Url.ActionLink("Create", "Book")),
                        new ResponseViewModel
                        {
                            Result = libraryBookCreated,
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

        [HttpPut, Route(""), Authorize]
        public async Task<IActionResult> UpdateAsync(
            [FromBody] LibraryBookViewModel library
            )
        {
            try
            {
                var libraryDb = await _libraryBookMananger
                    .GetByIdAsync(library.id);

                // não localizou
                if (libraryDb == null)
                    return BadRequest(new ResponseErrorViewModel
                    {
                        Status = Constants.Error,
                        Errors = new List<object>
                        {
                            new { Message = Constants.LibraryNotFound }
                        }
                    });

                var libraryBookMap = _mapper.Map<LibraryBook>(library);
                
                // captura objetos que estão no banco
                var typesDb = libraryDb.LibraryBookTypes.ToArray();

                if (libraryBookMap.LibraryBookTypes.Count > 0)
                {                    
                    // percorrer tipos dos livros cadastrados
                    foreach (var type in typesDb)
                    {   
                        // verificar se existe no objeto enviado
                        if (!libraryBookMap.LibraryBookTypes
                            .Any(x => x.Type.Description.Equals(type.Type.Description)))
                        {
                            libraryDb.LibraryBookTypes.Remove(type);
                        }
                    }
                }

                libraryDb.UpdatedAt = Util.CurrentDateTime();
                libraryDb.Description = libraryBookMap.Description;
                _libraryBookMananger.Update(libraryDb);

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

        [HttpDelete, Route(""), Authorize]
        public async Task<IActionResult> RemoveAsync(
            [FromQuery(Name = "id")] int id
            )
        {
            try
            {
                var libraryDb = await _libraryBookMananger
                    .GetByIdAsync(id);

                // não localizou
                if (libraryDb == null) return NotFound(
                new ResponseErrorViewModel
                {
                    Errors = new List<object>
                    {
                        new { Message = Constants.BooksNotFound }
                    },
                    Result = Constants.Error
                });

                _libraryBookMananger.RemoveAsync(libraryDb);

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
            [FromQuery(Name = "title")] string title,
            [FromQuery(Name = "offset")] int offset,
            [FromQuery(Name = "limit")] int limit,
            [FromQuery(Name = "latitude")] double latitude,
            [FromQuery(Name = "longitude")] double longitude,
            [FromQuery(Name = "id_library")] int idLibrary,
            [FromQuery(Name = "id_book")] long idBook
            )
        {
            try
            {
                // offset é a partir de qual registro você quer
                // limit é o valor máximo de registros a serem retornados
                var books = new List<LibraryBook>();

                var booksMap = new List<LibraryBookViewModel>();

                // buscar apenas livro de acordo com id
                if (idBook > 0)
                {
                    var bookDb = await
                        GetByBook(idBook);

                    return Ok(new ResponseViewModel
                    {
                        Result = bookDb,
                        Status = Constants.Sucess
                    });
                }

                // buscar por biblioteca
                if (idLibrary > 0)
                    booksMap = await
                        GetByIdLibrary(idLibrary, title, offset, limit);

                // captura login de usuario logado
                var login = Util
                    .GetClaim(_httpContext,
                        Constants.UserClaimIdentifier);

                // busca dados do usuario logado
                var user = await _userMananger
                    .GetByLogin(login);

                // buscar livros do usuario 
                if (string.IsNullOrEmpty(type) && idLibrary == 0 &&
                    longitude == 0 && longitude == 0)
                    booksMap = await
                        GetMyBooks(user.Person.Id, title, offset, limit);

                // buscar livros de acordo com tipo
                if (!string.IsNullOrEmpty(type))
                    booksMap = await
                        GetByType(type, user.Person.Library.Id, title, offset, limit);

                if (!string.IsNullOrEmpty(type) && (type.ToLower().Contains("trocar") ||
                    type.ToLower().Contains("emprestar") ||
                        type.ToLower().Contains("doar")))
                {
                    booksMap.AddRange(await
                        GetSuggestion(booksMap, type, user.Person.Library.Id));
                }

                if (booksMap == null || booksMap.Count <= 0)
                    throw new ArgumentNullException(Constants.BooksNotFound);

                // acerta calculo para retornar 
                var response = ResponseLibraryBook(booksMap, latitude, longitude);

                // quando for dos tipos informados a baixo, alterar response
                if (!string.IsNullOrEmpty(type) && (type.ToLower().Equals("trocar") ||
                    type.ToLower().Equals("emprestar") ||
                        type.ToLower().Equals("doar")))
                {
                    try
                    {
                        var responseCombination = (IEnumerable<LibraryBookViewModel>)
                            await GetByType("combinacao", user.Person.Library.Id, "", 9999, 0);

                        List<LibraryBookViewModel> booksResponse = new List<LibraryBookViewModel>();

                        // percorrer resultado para remover os livros que já existem nas combinações
                        if (responseCombination != null &&
                            responseCombination.Count() > 0)
                        {
                            foreach (var result in response)
                            {
                                if (!responseCombination
                                    .Any(x => x.Book.CodeIntegration
                                        .Equals(result.Book.CodeIntegration)))
                                    booksResponse.Add(result);
                            }
                        }

                        return Ok(new ResponseViewModel
                        {
                            Result = new ResponseTypesViewModel
                            {
                                Books = booksResponse,
                                Matches = responseCombination
                            },
                            Status = Constants.Sucess
                        });
                    }
                    catch (ArgumentNullException)
                    {
                        return Ok(new ResponseViewModel
                        {
                            Result = new ResponseTypesViewModel
                            {
                                Books = response,
                                Matches = new List<LibraryBookViewModel>()
                            },
                            Status = Constants.Sucess
                        });
                    }
                }

                return Ok(new ResponseViewModel
                {
                    Result = response,
                    Status = Constants.Sucess
                });
            }
            catch (ArgumentNullException ex)
            {
                // gerar log
                return NotFound(new
                {
                    Status = Constants.Error,
                    Errors = new List<object>
                    {
                        new { Message = string.IsNullOrEmpty(ex.ParamName)?
                            Constants.BooksNotFound:
                            ex.ParamName }
                    }
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

        [HttpGet, Route("Public"), AllowAnonymous]
        public async Task<IActionResult> GetPublicAsync(
            [FromQuery(Name = "title")] string title,
            [FromQuery(Name = "offset")] int offset,
            [FromQuery(Name = "limit")] int limit,
            [FromQuery(Name = "latitude")] double latitude,
            [FromQuery(Name = "longitude")] double longitude,
            [FromQuery(Name = "type")] string type
        )
        {
            try
            {
                // busca livros de acordo com tipo e titulo
                var booksMap =
                    await GetByTitle(type, title, offset, limit);

                // retorna resultados
                var response = ResponseLibraryBook(booksMap, latitude, longitude);

                return Ok(new ResponseViewModel
                {
                    Result = response,
                    Status = Constants.Sucess
                });
            }
            catch (ArgumentNullException ex)
            {
                // gerar log
                return NotFound(new
                {
                    Status = Constants.Error,
                    Errors = new List<object>
                    {
                        new { Message = string.IsNullOrEmpty(ex.ParamName)?
                            Constants.BooksNotFound:
                            ex.ParamName }
                    }
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

        private async Task<List<LibraryBookViewModel>> GetByIdLibrary(long idLibrary, string title, int offset, int limit)
        {
            var booksDb = await _libraryBookMananger
                .GetByIdLibrary(idLibrary, title, offset, limit);

            if (booksDb == null || booksDb.Count <= 0)
                throw new ArgumentNullException(Constants.BooksNotFound);

            return _mapper
                .Map<List<LibraryBookViewModel>>(booksDb);
        }
        private async Task<List<LibraryBookViewModel>> GetMyBooks(long idPerson, string title, int offset, int limit)
        {
            // busca biblioteca do usuario
            var libraryDb = await _libraryMananger
                .GetLibraryByPerson(idPerson);

            return await GetByIdLibrary(libraryDb.Id, title, offset, limit);
        }
        private async Task<List<LibraryBookViewModel>> GetByType(string type, long idLibraryRequest, string title, int offset, int limit)
        {
            // TROCAR
            // EMPRESTAR
            // DOAR
            // VENDA            
            // COMBINACAO 
            if (type.ToLower().Equals("combinacao"))
                return await GetByCombination(idLibraryRequest);

            if (!type.ToLower().Equals("interesse"))
                return await GetByTypeOnAllLibrary(type, idLibraryRequest, title, offset, limit);

            var typeDb = await _typeMananger.GetByDescriptionAsync(type);

            if (!type.ToLower().Equals("all") && typeDb == null)
                throw new ArgumentNullException(Constants.BooksNotFound);

            var booksDb = await _libraryBookMananger
                .GetByTypeNoTracking(typeDb, idLibraryRequest, title, offset, limit);

            return _mapper
                .Map<List<LibraryBookViewModel>>(booksDb);
        }
        private async Task<List<LibraryBookViewModel>> GetByCombination(long idLibraryRequest)
        {
            // trazer todos os livros de todos os tipos para realizar combinação
            var typeDb = await _typeMananger
                .GetByDescriptionAsync("interesse");

            var allBooks =
                await _libraryBookMananger.GetAllAndTypeDiferentNoTracking(idLibraryRequest, typeDb);

            if (allBooks == null || allBooks.Count <= 0)
                throw new ArgumentNullException(Constants.BooksNotFound);

            var booksInteresse = await
                GetByType("interesse", idLibraryRequest, "", 0, 0);

            if (booksInteresse == null || booksInteresse.Count <= 0)
                throw new ArgumentNullException(Constants.BooksNotFound);

            var booksCombination = new List<LibraryBook>();

            // percorre lista de interesse e verifica se existe na listagem de todos
            foreach (var libraryBookInteresse in booksInteresse)
            {
                // ocorre erro pois localiza mais de um livro                
                var combinations = allBooks.Where(x => x.Book.Title
                    .Trim().ToLower().Equals(
                            libraryBookInteresse.Book.Title
                                .Trim().ToLower())).ToList();

                if (combinations != null && combinations.Count > 0)                
                    booksCombination.AddRange(combinations);
            }

            if (booksCombination.Count <= 0)
                throw new ArgumentNullException(Constants.BooksNotFound);

            // gerar notificações quando houver combinações
            return _mapper
                .Map<List<LibraryBookViewModel>>(booksCombination);
        }
        private async Task<List<LibraryBookViewModel>> GetByTitle(string type, string title, int offset, int limit)
        {
            if (string.IsNullOrEmpty(type))
                throw new ArgumentNullException(Constants.BooksNotFound);

            var typeDb = await _typeMananger
                .GetByDescriptionAsync(type);

            if (typeDb == null)
                throw new ArgumentNullException(Constants.NotFound);


            var booksDb = await _libraryBookMananger
                .GetByTitleAndTypeNoTracking(title, typeDb, offset, limit);

            return _mapper
                .Map<List<LibraryBookViewModel>>(booksDb);
        }
        private async Task<List<LibraryBookViewModel>> GetByTypeOnAllLibrary(string type, long idLibraryRequest, string title, int offset, int limit)
        {
            var typeDb = await _typeMananger.GetByDescriptionAsync(type);

            if (typeDb == null)
                throw new ArgumentNullException(Constants.BooksNotFound);

            var booksDb = await _libraryBookMananger
                .GetByTypeOnAllLibraryNoTracking(typeDb, idLibraryRequest, title, offset, limit);

            return _mapper
                .Map<List<LibraryBookViewModel>>(booksDb);
        }
        private async Task<List<LibraryBookViewModel>> GetSuggestion(List<LibraryBookViewModel> books, string type, long idLibraryRequest)
        {
            // retorna livroz relacionados de acordo com as categorias e o tipo
            var categories = books
                .Select(x => x.Book)
                    .Select(x => x.Categories
                        .Select(x => x.Name)
                            .SingleOrDefault())
                                .Distinct()
                                    .ToList();

            var codesIntegration = books
                .Select(x => x.Book.CodeIntegration)
                    .ToList()
                        .ToArray();

            var associateds = new List<LibraryBook>();
            var associatedsMap = new List<LibraryBookViewModel>();

            var typeDb = await _typeMananger.GetByDescriptionAsync(type);

            // quando não informar o tipo, retornar vazio
            if (typeDb == null)
                return new List<LibraryBookViewModel>();

            foreach (var category in categories)
            {
                if (category != null)
                {
                    var assosiated = await
                        GetByAssociated(category, typeDb, idLibraryRequest);

                    if (assosiated.Any(x => !codesIntegration.Contains(x.Book.CodeIntegration)))
                        associateds.AddRange(assosiated);
                }
            }

            if (associateds.Count > 0)
            {
                associatedsMap = _mapper
                    .Map<List<LibraryBookViewModel>>(associateds);
            }

            return associatedsMap;
        }
        private Task<List<LibraryBook>> GetByAssociated(string category, Models.Type type, long idLibraryRequest)
        {
            return _libraryBookMananger
                .GetByAssociatedNoTracking(category, type, idLibraryRequest);
        }
        private async Task<LibraryBookViewModel> GetByBook(long IdBook)
        {
            // busca apenas livro de acordo com id
            var bookDb = await _libraryBookMananger
                .GetByIdAsyncNoTracking(IdBook);

            if (bookDb == null)
                throw new ArgumentNullException(Constants.BooksNotFound);

            return _mapper
                .Map<LibraryBookViewModel>(bookDb);
        }
        private IEnumerable<LibraryBookViewModel> ResponseLibraryBook(
            ICollection<LibraryBookViewModel> notCalculate,
            double latitude, double longitude)
        {
            return notCalculate.Select(x =>
            new LibraryBookViewModel
            {
                id = x.id,
                Distance = Util.Distance(
                    Double.Parse(x.Addresses.SingleOrDefault(x => x.Master == true).Latitude),
                    Double.Parse(x.Addresses.SingleOrDefault(x => x.Master == true).Longitude),
                    latitude, longitude),
                Book = x.Book,
                Contact = x.Contact,
                Images = x.Images,
                Types = x.Types,
                Name = x.Name
            })
            .OrderBy(x => x.Distance);
        }
    }
}