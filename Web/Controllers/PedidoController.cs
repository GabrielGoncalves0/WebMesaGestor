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
        public async Task<IActionResult> Get()
        {
            return Ok(await _pedidoservice.ListarPedidos());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            return Ok(await _pedidoservice.PedidoPorId(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PedCriacaoDTO pedido)
        {
            return Ok(await _pedidoservice.CriarPedido(pedido));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PedEdicaoDTO pedido)
        {
            return Ok(await _pedidoservice.AtualizarPedido(pedido));
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            return Ok(await _pedidoservice.DeletarPedido(id));
        }
    }
}
