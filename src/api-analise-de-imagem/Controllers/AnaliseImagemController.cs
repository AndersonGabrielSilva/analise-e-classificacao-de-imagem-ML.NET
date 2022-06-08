using IdentificarCodigoDeBarras.Models;
using IdentificarCodigoDeBarras.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IdentificarCodigoDeBarras.Controllers
{
    [Route("api/analiseimagem")]
    [ApiController]
    public class AnaliseImagemController : ControllerBase
    {
        public AnaliseImageService AnaliseImg { get; }
        public AnaliseImagemController([FromServices] AnaliseImageService analiseImg)
        {
            AnaliseImg = analiseImg;
        }

        /* - endpoint
         *   api/analiseimagem/verifica-codigo-barras
         */
        [RequestSizeLimit(40000000)] // 40 mb size
        [HttpPost("verifica-codigo-barras")]
        public async Task<IActionResult> VerificaImagem([FromForm] IFormFile arquivo)
        {
            try
            {
                var result = await AnaliseImg.VerificaCodigoDeBarras(arquivo);
                if (AnaliseImg.PossuiErros())
                {
                    return BadRequest(new ImagemResponse() { Erros = AnaliseImg.ObterErros() });
                }

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
