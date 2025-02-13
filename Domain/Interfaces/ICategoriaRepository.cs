using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Domain.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> ObterTodasCategorias();
        Task<Categoria> ObterCategoriaPorId(Guid id);
        Task<Categoria> CriarCategoria(Categoria categoria);
        Task<Categoria> AtualizarCategoria(Categoria categoria);
        Task<bool> DeletarCategoria(Guid id);
    }
}
