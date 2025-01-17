using Microsoft.AspNetCore.Mvc;
using WebMesaGestor.Application.DTO.Input.Opcoes;
using WebMesaGestor.Application.Services;

namespace WebMesaGestor.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpcaoController : ControllerBase
    {
        private readonly OpcaoService _opcaoService;

        public OpcaoController(OpcaoService opcaoService)
        {
            _opcaoService = opcaoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _opcaoService.ListarOpcoes());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OpcCriacaoDTO opcao)
        {
            return Ok(await _opcaoService.CriarOpcao(opcao));
        }


        [HttpPut]
        public async Task<IActionResult> Put([FromBody] OpcEdicaoDTO opcao)
        {
            return Ok(await _opcaoService.AtualizarOpcao(opcao));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            return Ok(await _opcaoService.OpcaoPorId(id));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            return Ok(await _opcaoService.DeletarOpcao(id));
        }
    }
}
