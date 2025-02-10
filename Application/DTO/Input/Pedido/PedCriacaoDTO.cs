using static WebMesaGestor.Domain.Entities.Pedido;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMesaGestor.Application.DTO.Input.Pedido
{
    public class PedCriacaoDTO
    {
        public decimal PedValor { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid MesaId { get; set; }
    }
}
