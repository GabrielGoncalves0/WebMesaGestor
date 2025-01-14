using System.Data;
using WebMesaGestor.Application.DTO.Input.Criacao;
using WebMesaGestor.Application.DTO.Input.Edicao;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.Map
{
    public static class EmpresaMap
    {
        public static EmpOutputDTO MapEmpresa(this Empresa empresa)
        {
            return new EmpOutputDTO
            {
                Id = empresa.Id,
                Emp_nome = empresa.Emp_nome,
                Emp_cnpj = empresa.Emp_cnpj,
                Criacao_data = empresa.Criacao_data
            };
        }

        public static IEnumerable<EmpOutputDTO> MapEmpresa(this IEnumerable<Empresa> empresa)
        {
            return empresa.Select(x => x.MapEmpresa()).ToList();
        }

        public static Empresa MapEmpresa(this EmpCriacaoDTO empresa)
        {
            return new Empresa
            {
                Id = Guid.NewGuid(),
                Emp_nome = empresa.Emp_nome,
                Emp_cnpj = empresa.Emp_cnpj,
                Criacao_data = DateTime.UtcNow
            };
        }

        public static IEnumerable<Empresa> MapEmpresa(this IEnumerable<EmpCriacaoDTO> empresa)
        {
            return empresa.Select(x => x.MapEmpresa()).ToList();
        }

        public static Empresa MapEmpresa(this EmpEdicaoDTO empresa)
        {
            return new Empresa
            {
                Emp_nome = empresa.Emp_nome,
                Emp_cnpj = empresa.Emp_cnpj
            };
        }

        public static IEnumerable<Empresa> MapEmpresa(this IEnumerable<EmpEdicaoDTO> empresa)
        {
            return empresa.Select(x => x.MapEmpresa()).ToList();
        }
    }
}
