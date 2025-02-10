//using Microsoft.AspNetCore.Mvc;
//using WebMesaGestor.Application.DTO.Input.Produto;
//using WebMesaGestor.Application.Services;

//namespace WebMesaGestor.Web.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class ProdutoController : ControllerBase
//    {
//        public readonly ProdutoService _produtoservice;
//        public ProdutoController(ProdutoService produtoservice)
//        {
//            _produtoservice = produtoservice;
//        }

//        [HttpGet]
//        [Route("buscarTodos")]
//        public async Task<IActionResult> Get()
//        {
//            var produto = await _produtoservice.ListarProdutos();
//            return Ok(produto);
//        }


//        [HttpPost]
//        [Route("cadastrar")]
//        public async Task<IActionResult> Post([FromBody] ProCriacaoDTO proCriacaoDTO)
//        {
//            var produto = await _produtoservice.CriarProduto(proCriacaoDTO);
//            return Ok(produto);
//        }

//        [HttpPut]
//        [Route("atualizar")]
//        public async Task<IActionResult> Put([FromBody] ProEdicaoDTO proEdicaoDTO)
//        {
//            var produto = await _produtoservice.AtualizarProduto(proEdicaoDTO);
//            return Ok(produto);
//        }

//        [HttpGet]
//        [Route("buscar/{id}")]
//        public async Task<IActionResult> Get([FromRoute] Guid id)
//        {
//            var produto = await _produtoservice.ProdutoPorId(id);
//            return Ok(produto);
//        }

//        [HttpDelete]
//        [Route("deletar/{id}")]
//        public async Task<IActionResult> Delete([FromRoute] Guid id)
//        {
//            var produto = await _produtoservice.DeletarProduto(id);
//            return Ok(produto);
//        }
//    }
//}
