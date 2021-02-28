using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bibi.Application.ArquivoAggregate;
using Bibi.Application.ArquivoAggregate.Dto;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.IO;

namespace Bibi.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArquivoController : ControllerBase
    {

        [HttpPost("")]
        public async Task<IActionResult> Post(
            [FromServices] IArquivoService arquivoService,
            [FromForm] IFormCollection file
        )
        {
            IFormFile myFile = file.Files[0];

            using (var ms = new MemoryStream())
            {
                await myFile.CopyToAsync(ms);
                var fileBytes = ms.ToArray();
                string s = Convert.ToBase64String(fileBytes);

                var stringFile = $"data:{myFile.ContentType};name={myFile.FileName};base64,{s}";
                var result = await arquivoService.AnalisarArquivo(stringFile, myFile.FileName);

                return Created(string.Empty, result);
            }

        }


        [HttpGet("")]
        public async Task<IActionResult> GetAll(
            [FromServices] IArquivoService arquivoService
        )
        {
            var result = await arquivoService.ObterTodasAnalises();

            return Ok(result);
        }
    }
}
