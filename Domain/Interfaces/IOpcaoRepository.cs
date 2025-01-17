using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Domain.Interfaces
{
    public interface IOpcaoRepository
    {
        Task<IEnumerable<Opcao>> ListarOpcoes();
        Task<Opcao> OpcaoPorId(Guid id);
        Task<Opcao> CriarOpcao(Opcao opcao);
        Task<Opcao> AtualizarOpcao(Opcao opcao);
        Task<Opcao> DeletarOpcao(Guid id);
    }
}
