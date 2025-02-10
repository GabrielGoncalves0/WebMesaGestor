//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using WebMesaGestor.Application.DTO.Input.OpcaoProPed;
//using WebMesaGestor.Application.DTO.Input.ProdutoPedido;
//using WebMesaGestor.Application.Services;

//namespace WebMesaGestor.Web.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ProdutoPedidoController : ControllerBase
//    {
//        private readonly ProPedService _produtoPedidoService;

//        public ProdutoPedidoController(ProPedService proPedService)
//        {
//            _produtoPedidoService = proPedService;
//        }

//        [HttpGet]
//        [Route("buscarProdutosPorPed/{id}")]
//        public async Task<IActionResult> GetAll([FromRoute] Guid id)
//        {
//            var proPed = await _produtoPedidoService.ListarProdutosPorPedId(id);
//            return Ok(proPed);
//        }

//        [HttpPost]
//        [Route("cadastrar")]
//        public async Task<IActionResult> Post([FromBody] ProPedCriacaoDTO proPedCriacaoDTO)
//        {
//            var result = await _produtoPedidoService.CriarProPed(proPedCriacaoDTO);
//            return Ok(result);
//        }


//        [HttpPut]
//        [Route("atualizar")]
//        public async Task<IActionResult> Put([FromBody] ProPedEdicaoDTO proPedEdicaoDTO)
//        {
//            var result = await _produtoPedidoService.AtualizarProPed(proPedEdicaoDTO);
//            return Ok(result);
//        }

//        [HttpGet]
//        [Route("buscar/{id}")]
//        public async Task<IActionResult> Get([FromRoute] Guid id)
//        {
//            var proPed = await _produtoPedidoService.ProPedId(id);
//            return Ok(proPed);
//        }

//        [HttpDelete]
//        [Route("deletar/{id}")]
//        public async Task<IActionResult> Delete([FromRoute] Guid id)
//        {
//            var proPed = await _produtoPedidoService.DeletarProPed(id);
//            return Ok(proPed);
//        }
//    }
//}
