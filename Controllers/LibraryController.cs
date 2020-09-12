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
    public class LibraryController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILibraryBook _libraryBookMananger;

        public LibraryController(
            [FromServices] IUnitOfWork uow,
            [FromServices] IMapper mapper,
            [FromServices] ILibraryBook libraryBookMananger
            )
        {
            _uow = uow;
            _mapper = mapper;
            _libraryBookMananger = libraryBookMananger;
        }

        [HttpPost, Route(""), Authorize]
        public async Task<IActionResult> RegisterAsync(
            [FromBody] LibraryBookViewModel libraryBook
            )
        {
            try
            {
                var libraryBookMap = _mapper.Map<LibraryBook>(libraryBook);




                // realizar validações

                // await _libraryBookMananger.CreateAsync(libraryBookMap);

                libraryBookMap.Active = true;    
                libraryBookMap.CreatedAt = Util.CurrentDateTime();
                libraryBookMap.UpdatedAt = libraryBookMap.CreatedAt;                

                _uow.Commit();

                // var libraryBookCreated = _mapper.Map<LibraryBook>(libraryBookMap);

                return Created(new Uri(Url.ActionLink("Register", "Library")), libraryBookMap);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut, Route(""), Authorize]
        public async Task<IActionResult> UpdateAsync(
            [FromBody] LibraryBookViewModel library
            )
        {
            try
            {
                await _libraryBookMananger.UpdateAsync(null);

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

                return Ok(await GetByBookTitle(title, offset, limit));
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