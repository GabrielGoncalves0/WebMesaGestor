using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Domain.Interfaces
{
    public interface IEmpresaRepository
    {
        Task<IEnumerable<Empresa>> ObterTodasEmpresas();
        Task<Empresa> ObterEmpresaPorId(Guid id);
        Task<Empresa> CriarEmpresa(Empresa empresa);
        Task<Empresa> AtualizarEmpresa(Empresa empresa);
        Task<bool> DeletarEmpresa(Guid id);
    }
}
