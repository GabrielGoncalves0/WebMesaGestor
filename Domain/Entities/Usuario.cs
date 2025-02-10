using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebMesaGestor.Domain.Entities
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UsuarioTipo { Administrador, Supervisor }
    public class Usuario
    {
        [Key]
        public Guid Id { get; set; }
        [StringLength(50)]
        public string UsuNome { get; set; }
        [StringLength(50)]
        public string UsuEmail { get; set; }
        [StringLength(16)]
        public string UsuTelefone { get; set; }
        [StringLength(60)]
        public string UsuSenha { get; set; }
        public UsuarioTipo UsuTipo { get; set; }
        public DateTime DataCriacao { get; set; }
        public Guid? EmpresaId { get; set; }
        public virtual Empresa? Empresa { get; set; }
    }
}
