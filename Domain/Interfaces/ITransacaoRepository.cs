using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Domain.Interfaces
{
    public interface ITransacaoRepository
    {
        Task<IEnumerable<Transacao>> ListarTransacoes();
        Task<Transacao> TransacaoPorId(Guid id);
        Task<Transacao> CriarTransacao(Transacao transacao);
        Task<Transacao> DeletarTransacao(Guid id);
    }
}
