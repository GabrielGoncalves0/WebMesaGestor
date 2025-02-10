using Microsoft.AspNetCore.Mvc;
using WebMesaGestor.Application.Common;
using WebMesaGestor.Application.DTO.Input.Setor;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.DTO.Response;
using WebMesaGestor.Application.Services;

namespace WebMesaGestor.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SetorController : ControllerBase
    {
        private readonly SetorService _setorService;

        public SetorController(SetorService setorService)
        {
            _setorService = setorService;
        }

        [HttpGet]
        [Route("buscarTodos")]
        public async Task<IActionResult> ObterTodosSetores()
        {
            var setors = await _setorService.ObterTodosSetores();

            if (setors == null)
            {
                setors = new List<SetorDTO>();
            }

            return Ok(setors);
        }

        [HttpGet]
        [Route("buscar/{id}")]
        public async Task<IActionResult> ObterSetorPorId(Guid id)
        {
            var setor = await _setorService.ObterSetorPorId(id);

            if (setor == null)
            {
                return NotFound(new ErrorResponse { Mensagem = SetorMessages.SetorNaoEncontrado });
            }

            return Ok(setor);
        }

        [HttpPost]
        [Route("cadastrar")]
        public async Task<IActionResult> CriarSetor([FromBody] SetCriacaoDTO setorDTO)
        {
            var resultado = await _setorService.CriarSetor(setorDTO);

            if (resultado.Sucesso)
            {
                return CreatedAtAction(nameof(ObterSetorPorId), new { id = resultado.Id }, resultado);
            }

            return BadRequest(new ValidationErrorResponse { Errors = resultado.Erros });
        }

        [HttpPut]
        [Route("atualizar")]
        public async Task<IActionResult> AtualizarSetor([FromBody] SetEdicaoDTO setorDTO)
        {
            var setorExistente = await _setorService.ObterSetorPorId(setorDTO.Id);

            if (setorExistente == null)
            {
                return NotFound(new { mensagem = SetorMessages.SetorNaoEncontrado });
            }

            var resultado = await _setorService.AtualizarSetor(setorDTO);

            if (resultado.Sucesso)
            {
                return Ok(new { mensagem = SetorMessages.SetorAtualizadoComSucesso });
            }

            return BadRequest(new { mensagem = SetorMessages.ErroAoAtualizarSetor, erros = resultado.Erros });
        }

        [HttpDelete]
        [Route("deletar/{id}")]
        public async Task<IActionResult> DeletarSetor(Guid id)
        {
            var setorExistente = await _setorService.ObterSetorPorId(id);

            if (setorExistente == null)
            {
                return NotFound(new { mensagem = SetorMessages.SetorNaoEncontrado });
            }

            var resultado = await _setorService.DeletarSetor(id);

            if (resultado.Sucesso)
            {
                return NoContent();
            }

            return BadRequest(new { mensagem = SetorMessages.ErroAoDeletarSetor });
        }
    }
}
