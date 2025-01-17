using Microsoft.AspNetCore.Mvc;
using WebMesaGestor.Application.DTO.Input.Transacao;
using WebMesaGestor.Application.DTO.Input.USuario;
using WebMesaGestor.Application.Services;

namespace WebMesaGestor.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransacaoController : ControllerBase
    {
        private readonly TransacaoService _transacaoService;

        public TransacaoController(TransacaoService transacaoService)
        {
            _transacaoService = transacaoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _transacaoService.ListarTrasacoes());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TraCriacaoDTO transacao)
        {
            return Ok(await _transacaoService.CriarTransacao(transacao));
        }


        [HttpPut]
        public async Task<IActionResult> Put([FromBody] TraEdicaoDTO transacao)
        {
            return Ok(await _transacaoService.AtualizarTransacao(transacao));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            return Ok(await _transacaoService.TransacaoPorId(id));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            return Ok(await _transacaoService.DeletarTransacao(id));
        }
    }
}
