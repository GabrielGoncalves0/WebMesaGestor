using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Domain.Interfaces
{
    public interface IOpcaoRepository
    {
        Task<IEnumerable<Opcao>> ObterOpcoes();
        Task<Opcao> ObterOpcaoPorId(Guid id);
        Task<Opcao> CriarOpcao(Opcao opcao);
        Task<Opcao> AtualizarOpcao(Opcao opcao);
        Task<bool> DeletarOpcao(Guid id);
    }
}
