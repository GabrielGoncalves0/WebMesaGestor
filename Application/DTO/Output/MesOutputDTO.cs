using System.Text.Json.Serialization;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.DTO.Output
{
    public class MesOutputDTO
    {
        public Guid? Id { get; set; }
        public int MesaNumero { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MesaStatus MesaStatus { get; set; }
        public DateTime CriacaoData { get; set; }
    }
}
