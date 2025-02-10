using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Domain.Interfaces
{
    public interface IProPedRepository
    {
        Task<IEnumerable<ProdutoPedido>> ObterProdutosPorPedId(Guid id);
        Task<ProdutoPedido> ObterProPedId(Guid id);
        Task<ProdutoPedido> CriarProPed(ProdutoPedido produtoPedido);
        Task<ProdutoPedido> AtualizarProPed(ProdutoPedido produtoPedido);
        Task<bool> DeletarProPed(Guid id);
    }
}
