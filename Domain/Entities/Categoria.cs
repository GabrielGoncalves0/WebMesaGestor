using System.ComponentModel.DataAnnotations;

namespace WebMesaGestor.Domain.Entities
{
    public class Categoria
    {
        public Categoria()
        {
        }
        [Key]
        public Guid Id { get; set; }
        [StringLength(100)]
        public string CatDesc { get; set; }
        public DateTime CriacaoData { get; set; }
    }
}
