using System.Text.Json.Serialization;
using WebMesaGestor.Domain.Entities;
namespace WebMesaGestor.Application.DTO.Output
{
    public class UsuarioDTO
    {
        public Guid? Id { get; set; }
        public string UsuNome { get; set; }
        public string UsuEmail { get; set; }
        public string UsuTelefone { get; set; }
        public UsuarioTipo UsuTipo { get; set; }
        public DateTime DataCriacao { get; set; }
        //[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual EmpresaDTO Empresa { get; set; }
    }
}
