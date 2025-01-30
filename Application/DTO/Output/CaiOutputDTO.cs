using System.Text.Json.Serialization;
using WebMesaGestor.Application.DTO.Auxiliar;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.DTO.Output
{
    public class CaiOutputDTO
    {
        public Guid? Id { get; set; }
        [JsonConverter(typeof(DecimalConverter))]
        public decimal CaiValInicial { get; set; }
        [JsonConverter(typeof(DecimalConverter))]
        public decimal? CaiValFechamento { get; set; }
        [JsonConverter(typeof(DecimalConverter))]
        public decimal? CaiValTotal { get; set; }
        public DateTime AberturaData { get; set; }
        public DateTime? FechamentoData { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CaixaStatus? CaiStatus { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual UsuOutputDTO? Usuario { get; set; }
    }
}
