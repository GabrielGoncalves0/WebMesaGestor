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
        public DateTime AberturaData { get; set; }
        public DateTime? FechamentoData { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CaixaStatus? CaiStatus { get; set; }
        public virtual UsuOutputDTO? Usuario { get; set; }
    }
}
