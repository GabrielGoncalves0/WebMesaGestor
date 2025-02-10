using System.Text.Json.Serialization;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Application.DTO.Auxiliar;

namespace WebMesaGestor.Application.DTO.Output
{
    public class TransacaoDTO
    {
        public Guid Id { get; set; }
        public string TraDescricao { get; set; }
        [JsonConverter(typeof(DecimalConverter))]
        public decimal TraValor { get; set; }
        public TranStatus TransacaoStatus { get; set; }
        public DateTime DataCriacao { get; set; }
        public virtual UsuarioDTO Usuario { get; set; }
        public virtual CaixaDTO Caixa { get; set; }
        public virtual PedidoDTO Pedido { get; set; }
    }
}
