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

                    bookDb.Description = libraryBookMap.Book.Description;
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
                var libraryDb = await _libraryBookMananger.GetByIdAsync(library.id);

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

                libraryDb.UpdatedAt = Util.CurrentDateTime();

                _libraryBookMananger.Update(libraryDb);

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
                var libraryDb = await _libraryBookMananger.GetByIdAsync(id);

                // não localizou
                if (libraryDb == null) return NotFound(new ResponseErrorViewModel
                {
                    Errors = new List<object>
                    {
                        new { Message = Constants.BookNotFound }
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
            [FromQuery(Name = "id_library")] int idLibrary
            )
        {
            try
            {
                // offset é a partir de qual registro você quer
                // limit é o valor máximo de registros a serem retornados

                // buscar por id de biblioteca
                if (idLibrary > 0)
                {
                    var books = await _libraryBookMananger
                        .GetByIdLibrary(idLibrary, title, offset, limit);

                    if (books.Count <= 0)
                        return BadRequest(new ResponseErrorViewModel
                        {
                            Result = Constants.Error,
                            Errors = new List<object>
                            {
                                new { Message = Constants.BooksNotFound }
                            }
                        });

                    var booksMap = _mapper
                        .Map<ICollection<LibraryBookViewModel>>(books);

                    booksMap.Select(x => new
                    {
                        Distance = Util.Distance(
                            Double.Parse(x.Addresses.SingleOrDefault(x => x.Master == true).Latitude),
                            Double.Parse(x.Addresses.SingleOrDefault(x => x.Master == true).Longitude),
                            latitude, longitude),
                        x.Book,
                        x.Contact,
                        x.id,
                        x.Images,
                        x.Types,
                        x.Name
                    })
                    .OrderBy(x => x.Distance);

                    return Ok(new ResponseViewModel
                    {
                        Result = booksMap,
                        Status = Constants.Sucess
                    });
                }

                var login = Util
                    .GetClaim(_httpContext,
                        Constants.UserClaimIdentifier);

                var user = await _userMananger
                    .GetByLogin(login);
                
                // retorna livros da biblioteca do usuario
                if (string.IsNullOrEmpty(type) && idLibrary == 0)
                {
                    var library = await _libraryMananger
                        .GetLibraryByPerson(user.Person.Id);

                    var libraryBookDb = await _libraryBookMananger
                        .GetByIdLibrary(library.Id, title, offset, limit);

                    var librariesBooksMap = _mapper
                        .Map<ICollection<LibraryBookViewModel>>(libraryBookDb);

                    return Ok(new ResponseViewModel
                    {
                        Result = librariesBooksMap,
                        Status = Constants.Sucess
                    });
                }

                if (!string.IsNullOrEmpty(type))
                {
                    // retorna todos os livros de todas as bibliotecas que 
                    // forem diferente do usuario da requisição de acordo com tipo 
                    var libraryBookDb = await
                        GetByType(type, user.Person.Library.Id, title, offset, limit);

                    if (libraryBookDb.Count <= 0)
                        return NotFound(new ResponseErrorViewModel
                        {
                            Status = Constants.Error,
                            Errors = new List<object>
                            {
                                new { Message = Constants.BooksNotFound }
                            }
                        });

                    if (!type.Equals("interesse"))
                    {
                        // TRAZER LIVROS RELACIONADOS POR CATEGORIA
                        var categories = libraryBookDb
                            .Select(x => x.Book)
                                .Select(x => x.CategoryBooks
                                    .Select(x => x.Category)
                                        .Select(x => x.Name)
                                            .Single())
                                            .Distinct().ToList();

                        var associateds = new List<LibraryBook>();

                        foreach (var category in categories)
                        {
                            var assosiated = await GetByAssociated(category);

                            associateds.AddRange(assosiated);
                        }

                        // adiciona livros associados
                        libraryBookDb.AddRange(associateds);
                    }

                    var librariesBooksMap = _mapper
                        .Map<ICollection<LibraryBookViewModel>>(libraryBookDb)
                        .Select(x => new
                        {
                            Distance = Util.Distance(
                                Double.Parse(x.Addresses.SingleOrDefault(x => x.Master == true).Latitude),
                                Double.Parse(x.Addresses.SingleOrDefault(x => x.Master == true).Longitude),
                                latitude, longitude),
                            x.Book,
                            x.Contact,
                            x.id,
                            x.Images,
                            x.Types,
                            x.Name
                        })
                        .OrderBy(x => x.Distance);

                    return Ok(new ResponseViewModel
                    {
                        Result = librariesBooksMap,
                        Status = Constants.Sucess
                    });
                }

                // retorna todos os livros de todas as bibliotecas de acordo com título
                var typeDb = await _typeMananger
                    .GetByDescriptionAsync(type);

                if (typeDb == null)
                    return NotFound(new ResponseErrorViewModel
                    {
                        Status = Constants.Error,
                        Errors = new List<object>
                        {
                            new { Message = "Tipo não localizado!" }
                        }
                    });

                var libraryBooks = await
                    GetByBookTitle(title, user.Person.Library.Id, typeDb, offset, limit);

                // verificar, não está mapeando os autores, categorias, e tipos
                var libraryBooksMaps = _mapper
                    .Map<ICollection<LibraryBookViewModel>>(libraryBooks)
                    .Select(x => new
                    {
                        Distance =
                            Util.Distance(
                                Double.Parse(x.Addresses.Single(x => x.Master == true).Latitude),
                                Double.Parse(x.Addresses.Single(x => x.Master == true).Longitude),
                                latitude, longitude),
                        x.Book,
                        x.Contact,
                        x.id,
                        x.Images,
                        x.Types,
                        x.Name
                    })
                    .OrderBy(x => x.Distance);

                return Ok(new ResponseViewModel
                {
                    Result = libraryBooksMaps,
                    Status = Constants.Sucess
                });
            }
            catch (ArgumentNullException)
            {
                // gerar log
                return NotFound(new
                {
                    Status = Constants.Error,
                    Errors = new List<object>
                    {
                        new { Message = "Nenhum livro localizado!" }
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
                var typeDb = await _typeMananger
                    .GetByDescriptionAsync(type);

                if (typeDb == null)
                    return NotFound(new ResponseErrorViewModel
                    {
                        Status = Constants.Error,
                        Errors = new List<object>
                        {
                            new { Message = "Tipo não localizado!" }
                        }
                    });

                var libraryBooks = await
                    GetByBookTitle(title, -1, typeDb, offset, limit);

                // verificar, não está mapeando os autores, categorias, e tipos
                var libraryBooksMaps = _mapper
                    .Map<ICollection<LibraryBookViewModel>>(libraryBooks)
                    .Select(x => new
                    {
                        Distance = Util.Distance(
                            Double.Parse(x.Addresses.Single(x => x.Master == true).Latitude),
                            Double.Parse(x.Addresses.Single(x => x.Master == true).Longitude),
                            latitude, longitude),
                        x.Book,
                        x.Contact,
                        x.id,
                        x.Images,
                        x.Types,
                        x.Name
                    })
                    .OrderBy(x => x.Distance);

                return Ok(new ResponseViewModel
                {
                    Result = libraryBooksMaps,
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

        private async Task<List<LibraryBook>> Combination(long idLibraryRequest, int offset, int limit)
        {
            // trazer todos os livros de todos os tipos para realizar validação
            var libraryBooks = await
                GetByType("all", idLibraryRequest, "", offset, limit);

            if (libraryBooks.Count <= 0)
                throw new ArgumentNullException();

            var libraryBooksInteresses = await
                GetByType("interesse", idLibraryRequest, "", offset, limit);

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
        private Task<List<LibraryBook>> GetByBookTitle(string title, long idLibraryRequest, Models.Type type, int offset, int limit)
        {
            return _libraryBookMananger
                .GetByBookTitle(
                    Util.RemoveSpecialCharacter(title), idLibraryRequest, type, offset, limit);
        }
        private async Task<List<LibraryBook>> GetByType(string type, long idLibraryRequest, string title, int offset, int limit)
        {
            // TROCAR
            // EMPRESTAR
            // DOAR
            // VENDA
            // EMPRESA
            // COMBINACAO            

            if (type.ToLower().Equals("combinacao"))
                return await Combination(idLibraryRequest, offset, limit);

            var typeDb = new Models.Type();

            if (!type.ToLower().Equals("all"))
            {
                typeDb = await _typeMananger.GetByDescriptionAsync(type);

                if (typeDb == null) throw new ArgumentNullException();

                idLibraryRequest = (typeDb
                    .Description.ToLower()
                        .Equals("interesse")) ? idLibraryRequest: -1;
            }
            else
            {
                typeDb = null;
            }

            return await _libraryBookMananger
                .GetByTypeNoTracking(typeDb, idLibraryRequest, offset, limit);
        }
        private Task<List<LibraryBook>> GetByAssociated(string category)
        {
            return _libraryBookMananger
                .GetByAssociated(category);
        }

        private List<object> CalculateDistance(ICollection<LibraryBookViewModel> notCalculate, double latitude, double longitude)
        {
            return (List<object>)notCalculate
            .Select(x => new
            {
                Distance = Util.Distance(
                            Double.Parse(x.Addresses.SingleOrDefault(x => x.Master == true).Latitude),
                            Double.Parse(x.Addresses.SingleOrDefault(x => x.Master == true).Longitude),
                            latitude, longitude),
                x.Book,
                x.Contact,
                x.id,
                x.Images,
                x.Types,
                x.Name
            });
        }

    }
}