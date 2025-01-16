using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Domain.Interfaces
{
    public interface ISetorRepository
    {
        Task<IEnumerable<Setor>> ListarSetors();
        Task<Setor> SetorPorId(Guid id);
        Task<Setor> CriarSetor(Setor setor);
        Task<Setor> AtualizarSetor(Setor setor);
        Task<Setor> DeletarSetor(Guid id);
    }
}
