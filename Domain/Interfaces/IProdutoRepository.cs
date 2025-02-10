using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> ObterProdutos();
        Task<Produto> ObterProdutoPorId(Guid id);
        Task<Produto> CriarProduto(Produto produto);
        Task<Produto> AtualizarProduto(Produto produto);
        Task<bool> DeletarProduto(Guid id);
    }
}
