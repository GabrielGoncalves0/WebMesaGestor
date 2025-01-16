using Microsoft.AspNetCore.Mvc;
using WebMesaGestor.Application.DTO.Input.Produto;
using WebMesaGestor.Application.Services;

namespace WebMesaGestor.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        public readonly ProdutoService _produtoservice;
        public ProdutoController(ProdutoService produtoservice)
        {
            _produtoservice = produtoservice;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _produtoservice.ListarProdutos());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            return Ok(await _produtoservice.ProdutoPorId(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProCriacaoDTO produto)
        {
            return Ok(await _produtoservice.CriarProduto(produto));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ProEdicaoDTO produto)
        {
            return Ok(await _produtoservice.AtualizarProduto(produto));
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            return Ok(await _produtoservice.DeletarProduto(id));
        }
    }
}
