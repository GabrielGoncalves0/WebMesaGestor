using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Domain.Interfaces
{
    public interface IMesaRepository
    {
        Task<IEnumerable<Mesa>> ObterTodasMesas();
        Task<Mesa> ObterMesaPorId(Guid id);
        Task<Mesa> CriarMesa(Mesa mesa);
        Task<Mesa> AtualizarMesa(Mesa mesa);
        Task<bool> DeletarMesa(Guid id);
    }
}
