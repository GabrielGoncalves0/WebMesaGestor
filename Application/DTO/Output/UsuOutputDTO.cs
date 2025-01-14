using System.ComponentModel.DataAnnotations;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.DTO.Output
{
    public class UsuOutputDTO
    {
        public Guid? Id { get; set; }
        public string Usu_nome { get; set; }
        public string Usu_email { get; set; }
        public string Usu_telefone { get; set; }
        public UsuarioTipo Usu_tipo { get; set; }
        public DateTime Criacao_data { get; set; }
        public virtual EmpOutputDTO Empresa { get; set; }
    }
}
