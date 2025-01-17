using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebMesaGestor.Application.DTO.Input.USuario;
using WebMesaGestor.Application.Services;

namespace WebMesaGestor.Web.Controllers
{
    [ApiController]
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
            return Ok(await _usuarioService.ListarUsuarios());
        }

        [HttpPost]
        [Route("cadastrar")]
        public async Task<IActionResult> Post([FromBody] UsuCriacaoDTO usuario)
        {
            return Ok(await _usuarioService.CriarUsuario(usuario));
        }


        [HttpPut]
        [Route("atualizar")]
        public async Task<IActionResult> Put([FromBody] UsuEdicaoDTO usuario)
        {
            return Ok(await _usuarioService.AtualizarUsuario(usuario));
        }

        [HttpGet]
        [Route("buscar/{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            return Ok(await _usuarioService.UsuarioPorId(id));
        }

        [HttpDelete]
        [Route("deletar/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            return Ok(await _usuarioService.DeletarUsuario(id));
        }
    }
}
