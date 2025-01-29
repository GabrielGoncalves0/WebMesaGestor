using System.ComponentModel.DataAnnotations;

namespace WebMesaGestor.Domain.Entities
{
    public enum StatusProPed { Pago, Nao_pago }
    public class ProdutoPedido
    {
        public ProdutoPedido()
        {
        }

        [Key]
        public Guid Id { get; set; }
        public int PedQuant { get; set; }
        public int PedDesconto { get; set; }
        public StatusProPed StatusProPed { get; set; }
        public DateTime CriacaoData { get; set; }
        public virtual Produto Produto { get; set; }
        public Guid ProdutoId { get; set; }
        public virtual Pedido Pedido { get; set; }
        public Guid PedidoId { get; set; }
    }
}
