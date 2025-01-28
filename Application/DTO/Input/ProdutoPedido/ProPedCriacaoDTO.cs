using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.DTO.Input.ProdutoPedido
{
    public class ProPedCriacaoDTO
    {
        public int PedQuant { get; set; }
        public int PedDesconto { get; set; }
        public Guid ProdutoId { get; set; }
        public Guid PedidoId { get; set; }
    }
}
