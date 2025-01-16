using Microsoft.AspNetCore.Mvc;
using WebMesaGestor.Application.DTO.Input.Categoria;
using WebMesaGestor.Application.Services;

namespace WebMesaGestor.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaService _categoriaService;

        public CategoriaController(CategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _categoriaService.ListarCategorias());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CatCriacaoDTO categoria)
        {
            return Ok(await _categoriaService.CriarCategoria(categoria));
        }


        [HttpPut]
        public async Task<IActionResult> Put([FromBody] CatEdicaoDTO categoria)
        {
            return Ok(await _categoriaService.AtualizarCategoria(categoria));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            return Ok(await _categoriaService.CategoriaPorId(id));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            return Ok(await _categoriaService.DeletarCategoria(id));
        }
    }
}
