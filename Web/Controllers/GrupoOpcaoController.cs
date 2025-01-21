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
        [Route("buscarTodos")]
        public async Task<IActionResult> Get()
        {
            var grupoOpcao = await _grupoOpcaoService.ListarGrupoOpcoes();
            return Ok(grupoOpcao);
        }

        [HttpPost]
        [Route("cadastrar")]
        public async Task<IActionResult> Post([FromBody] GrupOpcCriacaoDTO grupOpcCriacaoDTO)
        {
            var grupoOpcao = await _grupoOpcaoService.CriarGrupoOpcao(grupOpcCriacaoDTO);
            return Ok(grupoOpcao);
        }


        [HttpPut]
        [Route("atualizar")]
        public async Task<IActionResult> Put([FromBody] GrupOpcEdicaoDTO grupOpcEdicaoDTO)
        {
            var grupoOpcao = await _grupoOpcaoService.AtualizarGrupoOpcao(grupOpcEdicaoDTO);
            return Ok(grupoOpcao);
        }

        [HttpGet]
        [Route("buscar/{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var grupoOpcao = await _grupoOpcaoService.GrupoOpcaoPorId(id);
            return Ok(grupoOpcao);
        }

        [HttpDelete]
        [Route("deletar/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var grupoOpcao = await _grupoOpcaoService.DeletarGrupoOpcao(id);
            return Ok(grupoOpcao);
        }
    }
}
