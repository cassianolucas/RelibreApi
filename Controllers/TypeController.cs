using System;
using System.Collections.Generic;
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
    public class TypeController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IType _typeMananger;

        public TypeController(
            [FromServices] IUnitOfWork uow,
            [FromServices] IMapper mapper,
            [FromServices] IType typeMananger
        )
        {
            _uow = uow;
            _mapper = mapper;
            _typeMananger = typeMananger;
        }

        [HttpGet, Route(""), Authorize]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var typesDb = await _typeMananger.GetAllAsync();

                var typesMap = _mapper.Map<ICollection<TypeViewModel>>(typesDb);

                return Ok(typesDb);
            }
            catch (Exception ex)
            {                
                // gerar log
                return BadRequest(Util.ReturnException(ex));
            }
        }
    }
}