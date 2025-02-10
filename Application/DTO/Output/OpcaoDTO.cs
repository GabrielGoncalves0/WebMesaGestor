using System.Text.Json.Serialization;
using WebMesaGestor.Application.DTO.Auxiliar;

namespace WebMesaGestor.Application.DTO.Output
{
    public class OpcaoDTO
    {
        public Guid Id { get; set; }
        public string OpcaoDesc { get; set; }
        [JsonConverter(typeof(DecimalConverter))]
        public decimal OpcaoValor { get; set; }
        public int OpcaoQuantMax { get; set; }
        public DateTime DataCriacao { get; set; }
        public GrupoOpcoesDTO GrupoOpcoes { get; set; }
    }
}
