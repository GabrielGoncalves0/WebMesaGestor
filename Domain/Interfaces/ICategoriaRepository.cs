using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Domain.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> ListarCategorias();
        Task<Categoria> CategoriaPorId(Guid id);
        Task<Categoria> CriarCategoria(Categoria categoria);
        Task<Categoria> AtualizarCategoria(Categoria categoria);
        Task<Categoria> DeletarCategoria(Guid id);
    }
}
