using System.ComponentModel.DataAnnotations;

namespace WebMesaGestor.Domain.Entities
{
    public enum SetorStatus { Ativo, Inativo }
    public class Setor
    {
        public Setor()
        {
        }
        [Key]
        public Guid Id { get; set; }
        [StringLength(100)]
        public string SetDesc { get; set; }
        public SetorStatus SetStatus { get; set; }
        public DateTime CriacaoData { get; set; }
    }
}
