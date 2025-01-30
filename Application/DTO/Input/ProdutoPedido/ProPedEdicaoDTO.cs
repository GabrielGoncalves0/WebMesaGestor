using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.DTO.Input.ProdutoPedido
{
    public class ProPedEdicaoDTO
    {
        public Guid Id { get; set; }
        public StatusProPed StatusProPed { get; set; }
    }
}
