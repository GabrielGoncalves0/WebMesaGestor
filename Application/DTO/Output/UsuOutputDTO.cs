using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.DTO.Output
{
    public class UsuOutputDTO
    {
        public Guid? Id { get; set; }
        public string UsuNome { get; set; }
        public string UsuEmail { get; set; }
        public string UsuTelefone { get; set; }
        public UsuarioTipo UsuTipo { get; set; }
        public DateTime CriacaoData { get; set; }
        public virtual EmpOutputDTO? Empresa { get; set; }
    }
}
