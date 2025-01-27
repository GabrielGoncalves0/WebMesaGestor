using System.Text.Json.Serialization;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.DTO.Output
{
    public class SetOutputDTO
    {
        public Guid? Id { get; set; }
        public string SetDesc { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SetorStatus SetStatus { get; set; }
        public DateTime CriacaoData { get; set; }
    }
}
