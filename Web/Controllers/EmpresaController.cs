using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMesaGestor.Application.Common;
using WebMesaGestor.Application.DTO.Input.Empresa;
using WebMesaGestor.Application.DTO.Input.Usuario;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.DTO.Response;
using WebMesaGestor.Application.Services;

namespace WebMesaGestor.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly EmpresaService _empresaService;

        public EmpresaController(EmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        [HttpGet]
        [Route("buscarTodos")]
        public async Task<IActionResult> ObterTodasEmpresas()
        {
            var empresas = await _empresaService.ObterTodasEmpresas();

            if (empresas == null)
            {
                empresas = new List<EmpresaDTO>();
            }

            return Ok(empresas);
        }

        [HttpGet]
        [Route("buscar/{id}")]
        public async Task<IActionResult> ObterEmpresaPorId(Guid id)
        {
            var empresa = await _empresaService.ObterEmpresaPorId(id);

            if (empresa == null)
            {
                return NotFound(new ErrorResponse { Mensagem = EmpresaMessages.EmpresaNaoEncontrada });
            }

            return Ok(empresa);
        }

        [HttpPost]
        [Route("cadastrar")]
        public async Task<IActionResult> CriarEmpresa([FromBody] EmpCriacaoDTO empresaDTO)
        {
            var resultado = await _empresaService.CriarEmpresa(empresaDTO);

            if (resultado.Sucesso)
            {
                return CreatedAtAction(nameof(ObterEmpresaPorId), new { id = resultado.Id }, resultado);
            }

            return BadRequest(new ValidationErrorResponse { Errors = resultado.Erros });
        }

        [HttpPut]
        [Route("atualizar")]
        public async Task<IActionResult> AtualizarEmpresa([FromBody] EmpEdicaoDTO empresaDTO)
        {
            var empresaExistente = await _empresaService.ObterEmpresaPorId(empresaDTO.Id);

            if (empresaExistente == null)
            {
                return NotFound(new { mensagem = EmpresaMessages.EmpresaNaoEncontrada });
            }

            var resultado = await _empresaService.AtualizarEmpresa(empresaDTO);

            if (resultado.Sucesso)
            {
                return Ok(new { mensagem = EmpresaMessages.EmpresaAtualizadaComSucesso });
            }

            return BadRequest(new { mensagem = EmpresaMessages.ErroAoAtualizarEmpresa, erros = resultado.Erros });
        }

        [HttpDelete]
        [Route("deletar/{id}")]
        public async Task<IActionResult> DeletarEmpresa(Guid id)
        {
            var empresaExistente = await _empresaService.ObterEmpresaPorId(id);

            if (empresaExistente == null)
            {
                return NotFound(new { mensagem = EmpresaMessages.EmpresaNaoEncontrada });
            }

            var resultado = await _empresaService.DeletarEmpresa(id);

            if (resultado.Sucesso)
            {
                return NoContent();
            }

            return BadRequest(new { mensagem = EmpresaMessages.ErroAoDeletarEmpresa });
        }
    }
}
