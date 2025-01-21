using Microsoft.AspNetCore.Mvc;
using WebMesaGestor.Application.DTO.Input.Pedido;
using WebMesaGestor.Application.Services;

namespace WebMesaGestor.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        public readonly PedidoService _pedidoservice;
        public PedidoController(PedidoService pedidoservice)
        {
            _pedidoservice = pedidoservice;
        }

        [HttpGet]
        [Route("buscarTodos")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _pedidoservice.ListarPedidos());
        }


        [HttpPost]
        [Route("cadastrar")]
        public async Task<IActionResult> Post([FromBody] PedCriacaoDTO pedCriacaoDTO)
        {
            var pedido = await _pedidoservice.CriarPedido(pedCriacaoDTO);
            return Ok(pedido);
        }

        [HttpPut]
        [Route("atualizar")]
        public async Task<IActionResult> Put([FromBody] PedEdicaoDTO pedEdicaoDTO)
        {
            var pedido = await _pedidoservice.AtualizarPedido(pedEdicaoDTO);
            return Ok(pedido);
        }

        [HttpGet]
        [Route("buscar/{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var pedido = await _pedidoservice.PedidoPorId(id);
            return Ok(pedido);
        }

        [HttpDelete]
        [Route("deletar/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var pedido = await _pedidoservice.DeletarPedido(id);
            return Ok(pedido);
        }
    }
}
