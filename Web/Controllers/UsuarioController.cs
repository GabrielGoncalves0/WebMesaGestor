using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebMesaGestor.Application.Common;
using WebMesaGestor.Application.DTO.Input.Usuario;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.DTO.Response;
using WebMesaGestor.Application.Services;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("buscarTodos")]
        public async Task<IActionResult> ObterTodosUsuarios()
        {
            var usuarios = await _usuarioService.ObterTodosUsuarios();

            if (usuarios == null)
            {
                usuarios = new List<UsuarioDTO>();
            }

            return Ok(usuarios);
        }

        [HttpGet]
        [Route("buscar/{id}")]
        public async Task<IActionResult> ObterUsuarioPorId(Guid id)
        {
            var usuario = await _usuarioService.ObterUsuarioPorId(id);

            if (usuario == null)
            {
                return NotFound(new ErrorResponse { Mensagem = UsuarioMessages.UsuarioNaoEncontrado });
            }

            return Ok(usuario);
        }

        [HttpPost]
        [Route("cadastrar")]
        public async Task<IActionResult> CriarUsuario([FromBody] UsuCriacaoDTO usuarioDTO)
        {
            var resultado = await _usuarioService.CriarUsuario(usuarioDTO);

            if (resultado.Sucesso)
            {
                return CreatedAtAction(nameof(ObterUsuarioPorId), new { id = resultado.Id }, resultado);
            }

            return BadRequest(new ValidationErrorResponse { Errors = resultado.Erros });
        }

        [HttpPut]
        [Route("atualizar")]
        public async Task<IActionResult> AtualizarUsuario([FromBody] UsuEdicaoDTO usuarioDTO)
        {
            var usuarioExistente = await _usuarioService.ObterUsuarioPorId(usuarioDTO.Id);

            if (usuarioExistente == null)
            {
                return NotFound(new { mensagem = UsuarioMessages.UsuarioNaoEncontrado });
            }

            var resultado = await _usuarioService.AtualizarUsuario(usuarioDTO);

            if (resultado.Sucesso)
            {
                return Ok(new { mensagem = UsuarioMessages.UsuarioAtualizadoComSucesso });
            }

            return BadRequest(new { mensagem = UsuarioMessages.ErroAoAtualizarUsuario, erros = resultado.Erros });
        }

        [HttpDelete]
        [Route("deletar/{id}")]
        public async Task<IActionResult> DeletarUsuario(Guid id)
        {
            var usuarioExistente = await _usuarioService.ObterUsuarioPorId(id);

            if (usuarioExistente == null)
            {
                return NotFound(new { mensagem = UsuarioMessages.UsuarioNaoEncontrado });
            }   

            var resultado = await _usuarioService.DeletarUsuario(id);

            if (resultado.Sucesso)
            {
                return NoContent();
            }

            return BadRequest(new { mensagem = UsuarioMessages.ErroAoDeletarUsuario});
        }
    }
}
