using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Domain.Interfaces
{
    public interface IMarcaRepository
    {
        Task<IEnumerable<Marca>> ListarMarcas();
        Task<Marca> MarcaPorId(Guid id);
        Task<Marca> CriarMarca(Marca marca);
        Task<Marca> AtualizarMarca(Marca marca);
        Task<Marca> DeletarMarca(Guid id);
    }
}
