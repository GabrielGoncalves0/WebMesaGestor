using System.ComponentModel.DataAnnotations.Schema;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.DTO.Input.Criacao
{
    public class CaiCriacaoDTO
    {
        public decimal Cai_Val_Inicial { get; set; }
        public Guid? UsuarioId { get; set; }
    }
}
