using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebMesaGestor.Application.DTO.Input.USuario;
using WebMesaGestor.Application.Services;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Web.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("buscarTodos")]
        public async Task<IActionResult> Get()
        {
            var usuarios = await _usuarioService.ListarUsuarios();
            return Ok(usuarios);
        }

        [HttpPost]
        [Route("cadastrar")]
        public async Task<IActionResult> Post([FromBody] UsuCriacaoDTO usuario)
        {
            var result = await _usuarioService.CriarUsuario(usuario);
            return Ok(result);
        }


        [HttpPut]
        [Route("atualizar")]
        public async Task<IActionResult> Put([FromBody] UsuEdicaoDTO usuario)
        {
            var result = await _usuarioService.AtualizarUsuario(usuario);
            return Ok(result);
        }

        [HttpGet]
        [Route("buscar/{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var usuario = await _usuarioService.UsuarioPorId(id);
            return Ok(usuario);
        }

        [HttpDelete]
        [Route("deletar/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var usuario = await _usuarioService.DeletarUsuario(id);
            return Ok(usuario);
        }
    }
}
