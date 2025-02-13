using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Domain.Interfaces
{
    public interface IGrupoOpcaoRepository
    {
        Task<IEnumerable<GrupoOpcoes>> ObterTodosGrupoOpcoes();
        Task<GrupoOpcoes> ObterGrupoOpcaoPorId(Guid id);
        Task<GrupoOpcoes> CriarGrupoOpcao(GrupoOpcoes GrupoOpcao);
        Task<GrupoOpcoes> AtualizarGrupoOpcao(GrupoOpcoes GrupoOpcao);
        Task<bool> DeletarGrupoOpcao(Guid id);
    }
}
