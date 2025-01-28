using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.DTO.Input.ProdutoPedido
{
    public class ProPedEdicaoDTO
    {
        public Guid Id { get; set; }
        public int PedQuant { get; set; }
        public int PedDesconto { get; set; }
        public StatusProPed statusProPed { get; set; }
        public Guid ProdutoId { get; set; }
        public Guid PedidoId { get; set; }
    }
}
