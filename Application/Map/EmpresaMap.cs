using WebMesaGestor.Application.DTO.Input;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.Map
{
    public static class EmpresaMap
    {
        public static EmpresaOutputDTO MapEmpresa(this Empresa empresa)
        {
            return new EmpresaOutputDTO
            {
                Id = empresa.Id,
                Emp_nome = empresa.Emp_nome,
                Emp_cnpj = empresa.Emp_cnpj
            };
        }

        public static IEnumerable<EmpresaOutputDTO> MapEmpresa(this IEnumerable<Empresa> empresa)
        {
            return empresa.Select(x => x.MapEmpresa()).ToList();
        }

        public static Empresa MapEmpresa(this EmpresaInputDTO empresa)
        {
            return new Empresa
            {
                Emp_nome = empresa.Emp_nome,
                Emp_cnpj = empresa.Emp_cnpj
            };
        }

        public static IEnumerable<Empresa> MapEmpresa(this IEnumerable<EmpresaInputDTO> empresa)
        {
            return empresa.Select(x => x.MapEmpresa()).ToList();
        }
    }
}
