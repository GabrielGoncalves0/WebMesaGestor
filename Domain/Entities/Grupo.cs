using System.ComponentModel.DataAnnotations;

namespace WebMesaGestor.Domain.Entities
{
    public class Grupo
    {
        public Grupo()
        {
        }
        [Key]
        public Guid Id { get; set; }
        [StringLength(100)]
        public string GrupDesc { get; set; }
        public DateTime CriacaoData { get; set; }
    }
}
