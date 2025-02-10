using System.Text.Json.Serialization;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.DTO.Output
{
    public class ProdutoPedidoDTO
    {
        public Guid? Id { get; set; }
        public int PedQuant { get; set; }
        public int PedDesconto { get; set; }
        public StatusProPed StatusProPed { get; set; }
        public DateTime DataCriacao { get; set; }
        public virtual ProdutoDTO Produto { get; set; }
        public virtual PedidoDTO Pedido { get; set; }

    }
}
