using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Domain.Interfaces
{
    public interface IEmpresaRepository
    {
        Task<IEnumerable<Empresa>> ListarEmpresas();
        Task<Empresa> EmpresaPorId(Guid id);
        Task<Empresa> CriarEmpresa(Empresa empresa);
        Task<Empresa> AtualizarEmpresa(Empresa empresa);
        Task<Empresa> DeletarEmpresa(Guid id);
    }
}
