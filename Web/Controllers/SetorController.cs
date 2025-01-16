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
        public async Task<IActionResult> Get()
        {
            return Ok(await _setorService.ListarSetors());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SetCriacaoDTO setor)
        {
            return Ok(await _setorService.CriarSetor(setor));
        }


        [HttpPut]
        public async Task<IActionResult> Put([FromBody] SetEdicaoDTO setor)
        {
            return Ok(await _setorService.AtualizarSetor(setor));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            return Ok(await _setorService.SetorPorId(id));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            return Ok(await _setorService.DeletarSetor(id));
        }
    }
}
