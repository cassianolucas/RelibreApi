using System;
using System.Collections.Generic;
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
    public class NotificationController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;    
        private readonly HttpContext _httpContext;
        private readonly INotification _notificationMananger;
        private readonly INotificationPerson _notificationPersonMananger;
        private readonly IUser _userMananger;

        public NotificationController(
            [FromServices] IUnitOfWork uow,
            [FromServices] IMapper mapper,
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromServices] INotification notificationMananger,
            [FromServices] IUser userMananger,
            [FromServices] INotificationPerson notificationPersonMananger
            )
        {
            _uow = uow;
            _mapper = mapper;
            _httpContext = httpContextAccessor.HttpContext;
            _notificationMananger = notificationMananger;
            _userMananger = userMananger;
            _notificationPersonMananger = notificationPersonMananger;
        }

        // [HttpPost, Route(""), Authorize(Policy = "adiminstrator")]
        // public async Task<IActionResult> RegisterAsync(
        //     [FromBody] NotificationViewModel notification
        //     )
        // {
        //     try
        //     {
        //         return Ok();
        //     }
        //     catch (Exception ex)
        //     {                
        //         return BadRequest(Util.ReturnException(ex));
        //     }            
        // }

        /// <summary>
        /// Método atualiza notificações de acordo com id 
        /// </summary>
        /// <param name=""id"">id da notifiação a ser atualizada</param>
        /// <returns></returns>
        [HttpPut, Route(""), Authorize]
        public async Task<IActionResult> UpdateAsync(
            [FromForm(Name = "id")] long idNotification
            )
        {
            try
            {
                if (idNotification <= 0) return NoContent();
                
                var notificationPersonDb = await _notificationPersonMananger
                    .GetByIdAsync(idNotification);

                if (notificationPersonDb == null) return NoContent();

                notificationPersonDb.Active = false;

                _notificationPersonMananger.Update(notificationPersonDb);

                _uow.Commit();
                
                return Ok("");
            }
            catch (Exception ex)
            {                
                return BadRequest(Util.ReturnException(ex));
            }            
        }

        // [HttpDelete, Route(""), Authorize(Policy = "administrator")]
        // public async Task<IActionResult> RemoveAsync(
        //     [FromQuery(Name = "id")] int id
        //     )
        // {
        //     try
        //     {


        //         return Ok();
        //     }
        //     catch (Exception ex)
        //     {                
        //         return BadRequest(Util.ReturnException(ex));
        //     }            
        // }
        
        /// <summary>
        /// Método retorna a lista de notificações que ainda não foram verificadas do usuario logado
        /// </summary>
        /// <param name=""limit"">Parametro limita a quantidade de resultados (Não obrigatório)</param>
        /// <param name=""offset"">Parametro deve capturar os registros a partir de valor (Não obrigatório)</param>
        /// <returns></returns>
        [HttpGet, Route(""), Authorize]
        public async Task<IActionResult> GetAsync(
            [FromQuery(Name = "limit")] int limit, 
            [FromQuery(Name = "offset")] int offset
        )
        {
            try
            {
                var login = Util.GetClaim(_httpContext,
                    Constants.UserClaimIdentifier);

                var userDb = await _userMananger.GetByLogin(login);

                if (userDb == null) return NoContent();

                // capturar usuario da requisição para listar apenas as notificações dele
                var notificationsDb = await GetByPerson(userDb.Person.Id, offset, limit);

                var notificationsMap = _mapper
                    .Map<ICollection<NotificationPersonViewModel>>(notificationsDb);
                
                return Ok(notificationsMap);
            }
            catch (Exception ex)
            {                
                return BadRequest(Util.ReturnException(ex));
            }            
        }

        private Task<List<NotificationPerson>> GetByPerson(long idPerson, int offset, int limit)
        {
            return _notificationMananger.GetByPersonAsyncNoTracking(idPerson, offset, limit);
        }
    }
}