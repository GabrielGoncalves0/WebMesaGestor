using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Domain.Interfaces
{
    public interface IPedidoRepository
    {
        Task<IEnumerable<Pedido>> ObterPedidos();
        Task<Pedido> ObterPedidoPorId(Guid id);
        Task<Pedido> CriarPedido(Pedido pedido);
        Task<Pedido> AtualizarPedido(Pedido pedido);
        Task<bool> DeletarPedido(Guid id);
    }
}
