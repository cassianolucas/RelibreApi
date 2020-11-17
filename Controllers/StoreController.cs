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
    public class StoreController : ControllerBase
    {
        private readonly IUser _userMananger;
        private readonly HttpContext _httpContext;
        private readonly IMapper _mapper;
        public StoreController(
            [FromServices] IUser userMananger,
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromServices] IMapper mapper
        )
        {
            _userMananger = userMananger;
            _httpContext = httpContextAccessor.HttpContext;
            _mapper = mapper;
        }

        [HttpGet, Route(""), AllowAnonymous]
        public async Task<IActionResult> GetBussiness(
            [FromQuery(Name = "latitude")] double latidude,
            [FromQuery(Name = "longitude")] double longitude,
            [FromQuery(Name = "id")] long id
        )
        {
            try
            {                
                if (id > 0)
                {
                    var userBusinessDb = await _userMananger
                        .GetBusinessByPersonNoTracking(id);

                    var userMap = _mapper
                        .Map<UserBusinessViewModel>(userBusinessDb);

                    return Ok(new ResponseViewModel
                    {
                        Result = new
                        {
                            userMap.Id,
                            userMap.Name,
                            Legal_Name = userMap.LastName,
                            userMap.Login,
                            userMap.Document,
                            userMap.WebSite,
                            userMap.UrlImage,
                            userMap.Description,
                            userMap.Addresses,
                            userMap.Phone,
                            Distance = Util.Distance(
                            Double.Parse(userMap.Addresses.Single(x => x.Master = true).Latitude),
                            Double.Parse(userMap.Addresses.Single(x => x.Master = true).Longitude),
                            latidude, longitude)
                        },
                        Status = Constants.Sucess
                    });
                }
                else
                {
                    var usersDb = await _userMananger
                        .GetAllBusinessNoTracking();

                    var usersMap = _mapper
                        .Map<List<UserBusinessViewModel>>(usersDb);

                    return Ok(new ResponseViewModel
                    {
                        Result = usersMap
                    .Select(x => new
                    {
                        x.Id,
                        x.Name,
                        x.LastName,
                        x.Document,
                        x.Description,
                        x.Addresses,
                        x.Phone,
                        x.UrlImage,
                        x.WebSite,
                        Distance = Util.Distance(
                            Double.Parse(x.Addresses.Single(x => x.Master = true).Latitude),
                            Double.Parse(x.Addresses.Single(x => x.Master = true).Longitude),
                        latidude, longitude)
                    }),
                        Status = Constants.Sucess
                    });
                }
            }
            catch (Exception ex)
            {
                // gerar log
                return BadRequest(new ResponseErrorViewModel
                {
                    Status = Constants.Error,
                    Errors = new List<object> { Util.ReturnException(ex) }
                });
            }
        }
    }
}