using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebMesaGestor.Domain.Entities
{
    public class Pedido
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum StatusPedido { aberto, pago }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum TipoPagPedido { pix, cartao, avista, nenhum }
        public Pedido()
        {
        }
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal PedValor { get; set; }
        public StatusPedido PedStatus { get; set; }
        public TipoPagPedido PedTipoPag { get; set; }
        public DateTime DataCriacao { get; set; }
        public Guid? UsuarioId { get; set; }
        public virtual Usuario? Usuario { get; set; }
        public Guid? MesaId { get; set; }
        public virtual Mesa? Mesa { get; set; }
    }
}
