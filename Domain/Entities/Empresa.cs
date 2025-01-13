using System.ComponentModel.DataAnnotations;

namespace WebMesaGestor.Domain.Entities
{
    public class Empresa
    {
        public Empresa() {}
        [Key]
        public Guid Id { get; set; }
        [StringLength(100)]
        public string Emp_nome { get; set; }
        [StringLength(100)]
        public string Emp_cnpj { get; set; }
        public DateTime Criacao_data { get; set; }
    }
}
