using System.ComponentModel.DataAnnotations;

namespace WebMesaGestor.Domain.Entities
{
    public class Marca
    {
        public Marca()
        {
        }
        [Key]
        public Guid Id { get; set; }
        [StringLength(100)]
        public string MarNome { get; set; }
        public DateTime CriacaoData { get; set; }
    }
}
