using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Domain.Interfaces
{
    public interface ICaixaRepository
    {
        Task<IEnumerable<Caixa>> ListarCaixas();
        Task<Caixa> CaixaPorId(Guid id);
        Task<Caixa> CriarCaixa(Caixa caixa);
        Task<Caixa> AtualizarCaixa(Caixa caixa);
        Task<Caixa> DeletarCaixa(Guid id);
    }
}
