using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IBook _bookMananger;
        private readonly ICategory _categoryMananger;
        private readonly IAuthor _authorMananger;
        public BookController(
            [FromServices] IUnitOfWork uow,
            [FromServices] IMapper mapper,
            [FromServices] IBook bookMananger,
            [FromServices] ICategory categoryMananger,
            [FromServices] IAuthor authorMananger
            )
        {
            _uow = uow;
            _mapper = mapper;
            _bookMananger = bookMananger;
            _categoryMananger = categoryMananger;
            _authorMananger = authorMananger;
        }

        [HttpPost, Route(""), Authorize]
        public async Task<IActionResult> CreateAsync(
            [FromBody] BookViewModel book
            )
        {
            try
            {
                // campos necessários para cadastros 
                if (!ModelState.IsValid) throw new ArgumentNullException();

                var bookMap = _mapper.Map<Book>(book);

                var bookDb = await _bookMananger.GetByTitleAsync(bookMap.Title);

                // livro já existe
                if (bookDb != null) throw new ArgumentException();

                if (string.IsNullOrEmpty(bookMap.Title)) throw new ArgumentNullException();

                // verifica se existe categoria cadastrada no banco                                                                    
                foreach (var item in bookMap.CategoryBooks)
                {
                    var category = await _categoryMananger.GetByName(item.Category.Name);

                    if (category != null)
                    {
                        item.Category = category;
                    }
                    else
                    {
                        item.Category.CreatedAt = Util.CurrentDateTime();
                    }
                }

                // verifica se existe author cadastrado no banco
                foreach (var item in bookMap.AuthorBooks)
                {
                    var author = await _authorMananger.GetByName(item.Author.Name);

                    if (author != null)
                    {
                        item.Author = author;
                    }
                    else
                    {
                        item.Author.Active = true;
                        item.Author.CreatedAt = Util.CurrentDateTime();
                        item.Author.UpdatedAt = item.Author.CreatedAt;
                    }
                }

                bookMap.Title = bookMap.Title.Trim();
                bookMap.CreatedAt = Util.CurrentDateTime();

                await _bookMananger.CreateAsync(bookMap);

                _uow.Commit();

                return Created(new Uri(Url.ActionLink("Create", "Book")), book);
            }
            catch (ArgumentException ex)
            {
                if (ex is ArgumentNullException)
                {
                    return NoContent();
                }

                return Conflict(new
                {
                    Message = string.Format(Constants.MessageExceptionConflict, "")
                });
            }
            catch (Exception ex)
            {
                return BadRequest(Util.ReturnException(ex));
            }
        }

        [HttpPut, Route(""), Authorize]
        public async Task<IActionResult> UpdateAsync(
            [FromBody] BookViewModel book
            )
        {
            try
            {                
                var bookDb = await _bookMananger.GetByIdAsync(book.Id);
                
                _bookMananger.Update(null);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(Util.ReturnException(ex));
            }
        }

        [HttpDelete, Route(""), Authorize(Policy = "administrator")]
        public IActionResult RemoveAsync(
            [FromQuery(Name = "id")] int id
            )
        {
            try
            {
                _bookMananger.RemoveAsync(0);

                return Ok("");
            }
            catch (Exception ex)
            {
                return BadRequest(Util.ReturnException(ex));
            }
        }

        [HttpGet, Route(""), Authorize]
        public async Task<IActionResult> GetAsync(
            [FromQuery(Name = "id")] int id,
            [FromQuery(Name = "title")] string title,
            [FromQuery(Name = "offset")] int offset,
            [FromQuery(Name = "limit")] int limit
            )
        {
            try
            {
                // offset é a partir de qual registro você quer
                // limit é o valor máximo de registros a serem retornados
                if (id > 0) return Ok(await GetById(id));

                var booksDb = await GetByTitle(title, offset, limit);

                var bookViewModel = _mapper.Map<ICollection<Book>, ICollection<BookViewModel>>(booksDb);

                return Ok(bookViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(Util.ReturnException(ex));
            }
        }
        private Task<Book> GetById(int id)
        {
            return _bookMananger.GetByIdAsync(id);
        }
        private Task<List<Book>> GetByTitle(string title, int offset = 0, int limit = 0)
        {
            return _bookMananger.GetByTitleAsync(title, offset, limit);
        }




    }
}