using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Domain.Interfaces
{
    public interface IGrupoOpcaoRepository
    {
        Task<IEnumerable<GrupoOpcoes>> ListarGrupoOpcoes();
        Task<GrupoOpcoes> GrupoOpcaoPorId(Guid id);
        Task<GrupoOpcoes> CriarGrupoOpcao(GrupoOpcoes GrupoOpcao);
        Task<GrupoOpcoes> AtualizarGrupoOpcao(GrupoOpcoes GrupoOpcao);
        Task<GrupoOpcoes> DeletarGrupoOpcao(Guid id);
    }
}
