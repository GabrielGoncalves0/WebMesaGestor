using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Domain.Interfaces
{
    public interface IOpcProPedRepository
    {
        Task<IEnumerable<OpcaoProPed>> ListarOpcoesPorProPedId(Guid id);
        Task<OpcaoProPed> OpcaoProPedId(Guid id);
        Task<OpcaoProPed> CriarOpcaoProdPed(OpcaoProPed opcaoProPed);
        Task<OpcaoProPed> AtualizarOpcaoProdPed(OpcaoProPed opcaoProPed);
        Task<OpcaoProPed> DeletarOpcaoProdPed(Guid id);
    }
}
