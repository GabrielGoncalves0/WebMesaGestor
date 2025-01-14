using Microsoft.AspNetCore.Mvc;
using WebMesaGestor.Application.DTO.Input.Criacao;
using WebMesaGestor.Application.DTO.Input.Edicao;
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
        public async Task<IActionResult> Post([FromBody] CaiCriacaoDTO caixa)
        {
            return Ok(await _caixaService.CriarCaixa(caixa));
        }


        [HttpPut]
        public async Task<IActionResult> Put([FromBody] CaiEdicaoDTO caixa)
        {
            return Ok(await _caixaService.AtualizarCaixa(caixa));
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
