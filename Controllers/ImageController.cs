using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RelibreApi.Utils;
using RelibreApi.ViewModel;

namespace RelibreApi.Controllers
{
    [Route("api/v1/[controller]"), ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ImageController(
            [FromServices] IConfiguration configuration
        )
        {
            _configuration = configuration;
        }

        [HttpPost, Route(""), Authorize]
        public async Task<IActionResult> UploadImage(
            [FromForm(Name = "file")] IFormFile file
        )
        {
            try
            {
                if (file == null || file.Length <= 0)
                    return BadRequest(new ResponseErrorViewModel
                    {
                        Status = Constants.Error,
                        Errors = new List<object>
                            {
                                new { Message = Constants.InvalidImage }
                            }
                    });

                var identifier = Util.GenerateGuid();

                await Util.UploadImage(_configuration, file, identifier);

                var endPoint = string.Concat(_configuration
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
    }
}