using System.Text.Json.Serialization;
using WebMesaGestor.Application.DTO.Auxiliar;
using static WebMesaGestor.Domain.Entities.Pedido;

namespace WebMesaGestor.Application.DTO.Output
{
    public class PedOutputDTO
    {
        public Guid? Id { get; set; }
        [JsonConverter(typeof(DecimalConverter))]
        public decimal PedValor { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public StatusPedido PedStatus { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TipoPagPedido PedTipoPag { get; set; }
        public DateTime CriacaoData { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual UsuOutputDTO? Usuario { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual MesOutputDTO? Mesa { get; set; }
    }
}
