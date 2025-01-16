using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> ListarProdutos();
        Task<Produto> ProdutoPorId(Guid id);
        Task<Produto> CriarProduto(Produto produto);
        Task<Produto> AtualizarProduto(Produto produto);
        Task<Produto> DeletarProduto(Guid id);
    }
}
