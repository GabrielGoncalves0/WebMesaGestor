using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Domain.Interfaces
{
    public interface IPedidoRepository
    {
        Task<IEnumerable<Pedido>> ListarPedidos();
        Task<Pedido> PedidoPorId(Guid id);
        Task<Pedido> CriarPedido(Pedido pedido);
        Task<Pedido> AtualizarPedido(Pedido pedido);
        Task<Pedido> DeletarPedido(Guid id);
    }
}
