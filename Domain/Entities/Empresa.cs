using System.ComponentModel.DataAnnotations;

namespace WebMesaGestor.Domain.Entities
{
    public class Empresa
    {
        public Empresa() {}
        [Key]
        public Guid Id { get; set; }
        [StringLength(100)]
        public string EmpNome { get; set; }
        [StringLength(18)]
        public string EmpCnpj { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
