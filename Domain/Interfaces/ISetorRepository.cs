using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Domain.Interfaces
{
    public interface ISetorRepository
    {
        Task<IEnumerable<Setor>> ObterTodosSetores();
        Task<Setor> ObterSetorPorId(Guid id);
        Task<Setor> CriarSetor(Setor setor);
        Task<Setor> AtualizarSetor(Setor setor);
        Task<bool> DeletarSetor(Guid id);
    }
}
