using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Domain.Interfaces
{
    public interface ICaixaRepository
    {
        Task<IEnumerable<Caixa>> ListarCaixas();
        Task<Caixa> AbrirCaixa(Caixa caixa);
        Task<Caixa> FecharCaixa(Caixa caixa);
        Task<Caixa> AtualizarCaixa(Caixa caixa);
        Task<Caixa> DeletarCaixa(Guid id);
        Task<Caixa> CaixaPorId(Guid id);
        Task<Caixa> UltimoCaixa();
    }
}
