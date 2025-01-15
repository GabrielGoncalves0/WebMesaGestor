using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMesaGestor.Domain.Entities
{
    public enum CaixaStatus { Aberto, Fechado }
    public class Caixa
    {
        public Caixa()
        {
        }
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Cai_Val_Inicial { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Cai_Val_Fechamento { get; set; }
        //[Column(TypeName = "decimal(18,2)")]
        //public decimal Cai_Val_Total { get; set; }
        public DateTime Abertura_data { get; set; }
        public DateTime? Fechamento_data { get; set; }
        public CaixaStatus Cai_status { get; set; }
        public Guid? UsuarioId { get; set; }
        public virtual Usuario? Usuario { get; set; }
    }
}
