using System.Text.Json.Serialization;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Application.DTO.Auxiliar;

namespace WebMesaGestor.Application.DTO.Output
{
    public class TraOutputDTO
    {
        public Guid Id { get; set; }
        public string TraDescricao { get; set; }
        [JsonConverter(typeof(DecimalConverter))]
        public decimal TraValor { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TranStatus TransacaoStatus { get; set; }
        public DateTime CriacaoData { get; set; }
        public virtual UsuOutputDTO? Usuario { get; set; }
        public virtual CaiOutputDTO? Caixa { get; set; }
        public virtual PedOutputDTO? Pedido { get; set; }
    }
}
