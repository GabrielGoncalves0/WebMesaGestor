using static WebMesaGestor.Domain.Entities.Pedido;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMesaGestor.Application.DTO.Input.Pedido
{
    public class PedEdicaoDTO
    {
        public Guid Id { get; set; }
        public decimal PedValor { get; set; }
        public StatusPedido PedStatus { get; set; }
        public TipoPagPedido PedTipoPag { get; set; }
        public Guid? UsuarioId { get; set; }
        public Guid? MesaId { get; set; }

    }
}
