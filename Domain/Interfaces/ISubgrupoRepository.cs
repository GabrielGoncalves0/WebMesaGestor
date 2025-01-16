using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Domain.Interfaces
{
    public interface ISubgrupoRepository
    {
        Task<IEnumerable<Subgrupo>> ListarSubgrupos();
        Task<Subgrupo> SubgrupoPorId(Guid id);
        Task<Subgrupo> CriarSubgrupo(Subgrupo subgrup);
        Task<Subgrupo> AtualizarSubgrupo(Subgrupo subgrup);
        Task<Subgrupo> DeletarSubgrupo(Guid id);
    }
}
