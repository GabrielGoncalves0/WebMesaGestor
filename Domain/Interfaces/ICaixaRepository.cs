using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Domain.Interfaces
{
    public interface ICaixaRepository
    {
        Task<IEnumerable<Caixa>> ObterTodosCaixas();
        Task<Caixa> ObterCaixaPorId(Guid id);
        Task<Caixa> AbrirCaixa(Caixa caixa);
        Task<Caixa> AtualizarCaixa(Caixa caixa);
        Task<bool> DeletarCaixa(Guid id);
        Task<Caixa> UltimoCaixa();
    }
}
