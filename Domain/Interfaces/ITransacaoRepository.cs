using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Domain.Interfaces
{
    public interface ITransacaoRepository
    {
        Task<IEnumerable<Transacao>> ObterTodasTransacoes();
        Task<Transacao> ObterTransacaoPorId(Guid id);
        Task<Transacao> CriarTransacao(Transacao transacao);
        Task<bool> DeletarTransacao(Guid id);
    }
}
