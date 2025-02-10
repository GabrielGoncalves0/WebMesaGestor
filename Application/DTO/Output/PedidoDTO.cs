using System.Text.Json.Serialization;
using WebMesaGestor.Application.DTO.Auxiliar;
using static WebMesaGestor.Domain.Entities.Pedido;

namespace WebMesaGestor.Application.DTO.Output
{
    public class PedidoDTO
    {
        public Guid? Id { get; set; }
        [JsonConverter(typeof(DecimalConverter))]
        public decimal PedValor { get; set; }
        public StatusPedido PedStatus { get; set; }
        public TipoPagPedido PedTipoPag { get; set; }
        public DateTime DataCriacao { get; set; }
        public virtual UsuarioDTO Usuario { get; set; }
        public virtual MesaDTO Mesa { get; set; }
    }
}
