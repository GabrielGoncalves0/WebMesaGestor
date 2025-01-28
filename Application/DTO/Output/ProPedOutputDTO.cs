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
        public StatusProPed statusProPed { get; set; }
        public DateTime CriacaoData { get; set; }
        public virtual ProOutputDTO? Produto { get; set; }
        public virtual PedOutputDTO? Pedido { get; set; }

    }
}
