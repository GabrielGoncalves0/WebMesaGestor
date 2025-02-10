using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebMesaGestor.Domain.Entities
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
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
        public DateTime DataCriacao { get; set; }
        public virtual Produto Produto { get; set; }
        public Guid ProdutoId { get; set; }
        public virtual Pedido Pedido { get; set; }
        public Guid PedidoId { get; set; }
    }
}
