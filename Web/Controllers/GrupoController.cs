using Microsoft.AspNetCore.Mvc;
using WebMesaGestor.Application.DTO.Input.Grupo;
using WebMesaGestor.Application.Services;

namespace WebMesaGestor.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GrupoController : ControllerBase
    {
        private readonly GrupoService _grupoService;

        public GrupoController(GrupoService grupoService)
        {
            _grupoService = grupoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _grupoService.ListarGrupos());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GrupCriacaoDTO grupo)
        {
            return Ok(await _grupoService.CriarGrupo(grupo));
        }


        [HttpPut]
        public async Task<IActionResult> Put([FromBody] GrupEdicaoDTO grupo)
        {
            return Ok(await _grupoService.AtualizarGrupo(grupo));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            return Ok(await _grupoService.GrupoPorId(id));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            return Ok(await _grupoService.DeletarGrupo(id));
        }
    }
}
