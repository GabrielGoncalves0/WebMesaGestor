using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.DTO.Input.Criacao
{
    public class UsuCriacaoDTO
    {
        public string Usu_nome { get; set; }
        public string Usu_email { get; set; }
        public string Usu_telefone { get; set; }
        public string Usu_senha { get; set; }
        public UsuarioTipo Usu_tipo { get; set; }
        public Guid? EmpresaId { get; set; }
    }
}
