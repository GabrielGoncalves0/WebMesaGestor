using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMesaGestor.Domain.Entities
{
    public class Opcoes
    {
        public Opcoes()
        {
        }
        [Key]
        public Guid Id { get; set; }
        [StringLength(100)]
        public string OpcaoDesc { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal OpcaoValor { get; set; }
        public int OpcaoQuantMax { get; set; }
        public DateTime CriacaoData { get; set; }
        public Guid? GrupoOpcoesId { get; set; }
        public GrupoOpcoes? GrupoOpcoes { get; set; }
    }
}
