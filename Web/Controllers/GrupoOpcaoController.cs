using Microsoft.AspNetCore.Mvc;
using WebMesaGestor.Application.DTO.Input.Grupo;
using WebMesaGestor.Application.Services;

namespace WebMesaGestor.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GrupoOpcaoController : ControllerBase
    {
        private readonly GrupoOpcaoService _grupoOpcaoService;

        public GrupoOpcaoController(GrupoOpcaoService grupoService)
        {
            _grupoOpcaoService = grupoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _grupoOpcaoService.ListarGrupoOpcoes());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GrupOpcCriacaoDTO grupo)
        {
            return Ok(await _grupoOpcaoService.CriarGrupoOpcao(grupo));
        }


        [HttpPut]
        public async Task<IActionResult> Put([FromBody] GrupOpcEdicaoDTO grupo)
        {
            return Ok(await _grupoOpcaoService.AtualizarGrupoOpcao(grupo));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            return Ok(await _grupoOpcaoService.GrupoOpcaoPorId(id));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            return Ok(await _grupoOpcaoService.DeletarGrupoOpcao(id));
        }
    }
}
