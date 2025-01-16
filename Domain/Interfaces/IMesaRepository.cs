using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Domain.Interfaces
{
    public interface IMesaRepository
    {
        Task<IEnumerable<Mesa>> ListarMesas();
        Task<Mesa> MesaPorId(Guid id);
        Task<Mesa> CriarMesa(Mesa mesa);
        Task<Mesa> AtualizarMesa(Mesa mesa);
        Task<Mesa> DeletarMesa(Guid id);
    }
}
