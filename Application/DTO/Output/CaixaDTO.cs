using System.Text.Json.Serialization;
using WebMesaGestor.Application.DTO.Auxiliar;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.DTO.Output
{
    public class CaixaDTO
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
        public CaixaStatus? CaiStatus { get; set; }
        public virtual UsuarioDTO Usuario { get; set; }
    }
}
