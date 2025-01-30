using System.Text.Json.Serialization;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.DTO.Output
{
    public class ProPedOutputDTO
    {
        public Guid? Id { get; set; }
        public int PedQuant { get; set; }
        public int PedDesconto { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public StatusProPed StatusProPed { get; set; }
        public DateTime CriacaoData { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual ProOutputDTO? Produto { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual PedOutputDTO? Pedido { get; set; }

    }
}
