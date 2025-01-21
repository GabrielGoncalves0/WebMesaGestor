using Microsoft.AspNetCore.Mvc;
using WebMesaGestor.Application.DTO.Input.Setor;
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
        public async Task<IActionResult> Get()
        {
            var setor = await _setorService.ListarSetors();
            return Ok(setor);
        }

        [HttpPost]
        [Route("cadastrar")]
        public async Task<IActionResult> Post([FromBody] SetCriacaoDTO setCriacaoDTO)
        {
            var setor = await _setorService.CriarSetor(setCriacaoDTO);
            return Ok(setor);
        }


        [HttpPut]
        [Route("atualizar")]
        public async Task<IActionResult> Put([FromBody] SetEdicaoDTO setEdicaoDTO)
        {
            var setor = await _setorService.AtualizarSetor(setEdicaoDTO);
            return Ok(setor);
        }

        [HttpGet]
        [Route("buscar/{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var setor = await _setorService.SetorPorId(id);
            return Ok(setor);
        }

        [HttpDelete]
        [Route("deletar/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var setor = await _setorService.DeletarSetor(id);
            return Ok(setor);
        }
    }
}
