using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Domain.Interfaces
{
    public interface IGrupoRepository
    {
        Task<IEnumerable<Grupo>> ListarGrupos();
        Task<Grupo> GrupoPorId(Guid id);
        Task<Grupo> CriarGrupo(Grupo grupo);
        Task<Grupo> AtualizarGrupo(Grupo grupo);
        Task<Grupo> DeletarGrupo(Guid id);
    }
}
