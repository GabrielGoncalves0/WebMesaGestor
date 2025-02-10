using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Domain.Interfaces
{
    public interface IOpcProPedRepository
    {
        Task<IEnumerable<OpcaoProPed>> ObterOpcoesPorProPedId(Guid id);
        Task<OpcaoProPed> ObterOpcaoProPedId(Guid id);
        Task<OpcaoProPed> CriarOpcaoProdPed(OpcaoProPed opcaoProPed);
        Task<OpcaoProPed> AtualizarOpcaoProdPed(OpcaoProPed opcaoProPed);
        Task<bool> DeletarOpcaoProdPed(Guid id);
    }
}
