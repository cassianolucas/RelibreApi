using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RelibreApi.Services;
using RelibreApi.Utils;
using RelibreApi.ViewModel;

namespace RelibreApi.Controllers
{
    [Route("api/v1/[controller]"), ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;    

        public NotificationController(
            [FromServices] IUnitOfWork uow,
            [FromServices] IMapper mapper
            )
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpPost, Route(""), Authorize(Policy = "adiminstrator")]
        public async Task<IActionResult> RegisterAsync(
            [FromBody] NotificationViewModel notification
            )
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {                
                return BadRequest(Util.ReturnException(ex));
            }            
        }

        [HttpPut, Route(""), Authorize(Policy = "administrator")]
        public async Task<IActionResult> UpdateAsync(
            [FromBody] NotificationViewModel notification
            )
        {
            try
            {


                return Ok();
            }
            catch (Exception ex)
            {                
                return BadRequest(Util.ReturnException(ex));
            }            
        }

        [HttpDelete, Route(""), Authorize(Policy = "administrator")]
        public async Task<IActionResult> RemoveAsync(
            [FromQuery(Name = "id")] int id
            )
        {
            try
            {


                return Ok();
            }
            catch (Exception ex)
            {                
                return BadRequest(Util.ReturnException(ex));
            }            
        }

        [HttpGet, Route(""), Authorize]
        public async Task<IActionResult> GetAsync(
            [FromQuery(Name = "id")] int id
            )
        {
            try
            {


                return Ok();
            }
            catch (Exception ex)
            {                
                return BadRequest(Util.ReturnException(ex));
            }            
        }
    }
}