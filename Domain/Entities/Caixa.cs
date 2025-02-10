using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebMesaGestor.Domain.Entities
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CaixaStatus { Aberto, Fechado }
    public class Caixa
    {
        public Caixa()
        {
        }
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal CaiValInicial { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal? CaiValFechamento { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal? CaiValTotal { get; set; }
        public CaixaStatus? CaiStatus { get; set; }
        public DateTime AberturaData { get; set; }
        public DateTime? FechamentoData { get; set; }
        public Guid? UsuarioId { get; set; }
        public virtual Usuario? Usuario { get; set; }
    }
}
