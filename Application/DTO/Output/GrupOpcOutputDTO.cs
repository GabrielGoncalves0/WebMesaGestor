using System.Text.Json.Serialization;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.DTO.Output
{
    public class GrupOpcOutputDTO
    {
        public Guid? Id { get; set; }
        public string GrupOpcDesc { get; set; }
        public int GrupOpcMax { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public GrupOpcTipo GrupOpcTipo { get; set; }
        public DateTime CriacaoData { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ProOutputDTO? Produto { get; set; }
    }
}
