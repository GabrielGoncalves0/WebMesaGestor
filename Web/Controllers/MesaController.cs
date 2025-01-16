using Microsoft.AspNetCore.Mvc;
using WebMesaGestor.Application.DTO.Input.Mesa;
using WebMesaGestor.Application.Services;

namespace WebMesaGestor.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MesaController : ControllerBase
    {
        private readonly MesaService _mesaService;

        public MesaController(MesaService mesaService)
        {
            _mesaService = mesaService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mesaService.ListarMesas());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MesCriacaoDTO mesa)
        {
            return Ok(await _mesaService.CriarMesa(mesa));
        }


        [HttpPut]
        public async Task<IActionResult> Put([FromBody] MesEdicaoDTO mesa)
        {
            return Ok(await _mesaService.AtualizarMesa(mesa));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            return Ok(await _mesaService.MesaPorId(id));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            return Ok(await _mesaService.DeletarMesa(id));
        }
    }
}
