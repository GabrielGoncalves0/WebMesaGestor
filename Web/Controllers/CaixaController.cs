using Microsoft.AspNetCore.Mvc;
using WebMesaGestor.Application.DTO.Input.Caixa;
using WebMesaGestor.Application.Services;

namespace WebMesaGestor.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaixaController : ControllerBase
    {
        private readonly CaixaService _caixaService;

        public CaixaController(CaixaService caixaService)
        {
            _caixaService = caixaService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _caixaService.ListarCaixas());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CaiAbrirDTO caixa)
        {
            return Ok(await _caixaService.AbrirCaixa(caixa));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] CaiFecharDTO caixa)
        {
            return Ok(await _caixaService.FecharCaixa(caixa));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            return Ok(await _caixaService.CaixaPorId(id));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            return Ok(await _caixaService.DeletarCaixa(id));
        }
    }
}
