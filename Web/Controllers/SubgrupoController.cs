using Microsoft.AspNetCore.Mvc;
using WebMesaGestor.Application.DTO.Input.Subgrupo;
using WebMesaGestor.Application.Services;

namespace WebMesaGestor.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubgrupoController : ControllerBase
    {
        private readonly SubgrupoService _subgrupoService;

        public SubgrupoController(SubgrupoService subgrupoService)
        {
            _subgrupoService = subgrupoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _subgrupoService.ListarSubgrupos());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SubgrupCriacaoDTO subgrupo)
        {
            return Ok(await _subgrupoService.CriarSubgrupo(subgrupo));
        }


        [HttpPut]
        public async Task<IActionResult> Put([FromBody] SubgrupEdicaoDTO subgrupo)
        {
            return Ok(await _subgrupoService.AtualizarSubgrupo(subgrupo));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            return Ok(await _subgrupoService.SubgrupoPorId(id));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            return Ok(await _subgrupoService.DeletarSubgrupo(id));
        }
    }
}
