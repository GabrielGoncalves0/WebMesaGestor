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
        [Route("buscarTodos")]
        public async Task<IActionResult> Get()
        {
            var categoria = await _categoriaService.ListarCategorias();
            return Ok(categoria);
        }

        [HttpPost]
        [Route("cadastrar")]
        public async Task<IActionResult> Post([FromBody] CatCriacaoDTO catCriacaoDTO)
        {
            var categoria = await _categoriaService.CriarCategoria(catCriacaoDTO);
            return Ok(categoria);
        }


        [HttpPut]
        [Route("atualizar")]
        public async Task<IActionResult> Put([FromBody] CatEdicaoDTO catEdicaoDTO)
        {
            var categoria = await _categoriaService.AtualizarCategoria(catEdicaoDTO);
            return Ok(categoria);
        }

        [HttpGet]
        [Route("buscar/{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var categoria = await _categoriaService.CategoriaPorId(id);
            return Ok(categoria);
        }

        [HttpDelete]
        [Route("deletar/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var categoria = await _categoriaService.DeletarCategoria(id);
            return Ok(categoria);
        }
    }
}
