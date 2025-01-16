using Microsoft.AspNetCore.Mvc;
using WebMesaGestor.Application.DTO.Input.Marca;
using WebMesaGestor.Application.Services;

namespace WebMesaGestor.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarcaController : ControllerBase
    {
        private readonly MarcaService _marcaService;

        public MarcaController(MarcaService marcaService)
        {
            _marcaService = marcaService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _marcaService.ListarMarcas());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MarCriacaoDTO marca)
        {
            return Ok(await _marcaService.CriarMarca(marca));
        }


        [HttpPut]
        public async Task<IActionResult> Put([FromBody] MarEdicaoDTO marca)
        {
            return Ok(await _marcaService.AtualizarMarca(marca));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            return Ok(await _marcaService.MarcaPorId(id));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            return Ok(await _marcaService.DeletarMarca(id));
        }
    }
}
