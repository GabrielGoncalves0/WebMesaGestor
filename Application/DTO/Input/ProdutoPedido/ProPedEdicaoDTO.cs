using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.DTO.Input.ProdutoPedido
{
    public class ProPedEdicaoDTO
    {
        public Guid Id { get; set; }
        public int ProPedQuant { get; set; }
        public int ProPedDesconto { get; set; }
        public StatusProPed StatusProPed { get; set; }
        public Guid ProdutoId { get; set; }
        public Guid PedidoId { get; set; }
    }
}
