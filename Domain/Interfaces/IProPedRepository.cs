using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Domain.Interfaces
{
    public interface IProPedRepository
    {
        Task<IEnumerable<ProdutoPedido>> ListarProdutosPorPedId(Guid id);
        Task<ProdutoPedido> ProPedId(Guid id);
        Task<ProdutoPedido> CriarProPed(ProdutoPedido produtoPedido);
        Task<ProdutoPedido> AtualizarProPed(ProdutoPedido produtoPedido);
        Task<ProdutoPedido> DeletarProPed(Guid id);
    }
}
