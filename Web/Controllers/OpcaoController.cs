using Microsoft.AspNetCore.Mvc;
using WebMesaGestor.Application.DTO.Input.Opcoes;
using WebMesaGestor.Application.Services;
using WebMesaGestor.Domain.Entities;

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
        [Route("buscarTodos")]
        public async Task<IActionResult> Get()
        {
            var opcao = await _opcaoService.ListarOpcoes();
            return Ok(opcao);
        }

        [HttpPost]
        [Route("cadastrar")]
        public async Task<IActionResult> Post([FromBody] OpcCriacaoDTO opcCriacaoDTO)
        {
            var opcao = await _opcaoService.CriarOpcao(opcCriacaoDTO);
            return Ok(opcao);
        }


        [HttpPut]
        [Route("atualizar")]
        public async Task<IActionResult> Put([FromBody] OpcEdicaoDTO opcEdicaoDTO)
        {
            var opcao = await _opcaoService.AtualizarOpcao(opcEdicaoDTO);
            return Ok(opcao);
        }

        [HttpGet]
        [Route("buscar/{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var opcao = await _opcaoService.OpcaoPorId(id);
            return Ok(opcao);
        }

        [HttpDelete]
        [Route("deletar/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var opcao = await _opcaoService.DeletarOpcao(id);
            return Ok(opcao);
        }
    }
}
