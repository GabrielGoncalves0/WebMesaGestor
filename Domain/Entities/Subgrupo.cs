using System.ComponentModel.DataAnnotations;

namespace WebMesaGestor.Domain.Entities
{
    public class Subgrupo
    {
        public Subgrupo()
        {
        }
        [Key]
        public Guid Id { get; set; }
        [StringLength(100)]
        public string SubgruDesc { get; set; }
        public DateTime CriacaoData { get; set; }
    }
}
