using System.ComponentModel.DataAnnotations.Schema;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.DTO.Input.Caixa
{
    public class CaiAbrirDTO
    {
        public decimal CaiValInicial { get; set; }
        public Guid? UsuarioId { get; set; }
    }
}
