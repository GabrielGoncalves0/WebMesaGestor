using System.ComponentModel.DataAnnotations;

namespace WebMesaGestor.Domain.Entities
{
    public enum UsuarioTipo { Administrador, Supervisor }
    public class Usuario
    {
        [Key]
        public Guid Id { get; set; }
        [StringLength(50)]
        public string Usu_nome { get; set; }
        [StringLength(50)]
        public string Usu_email { get; set; }
        [StringLength(50)]
        public string Usu_telefone { get; set; }
        [StringLength(60)]
        public string Usu_senha { get; set; }
        public UsuarioTipo Usu_tipo { get; set; }
        public DateTime Criacao_data { get; set; }
        public Guid? EmpresaId { get; set; }
        public virtual Empresa? Empresa { get; set; }
    }
}
