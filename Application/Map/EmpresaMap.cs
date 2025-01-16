using System.Data;
using WebMesaGestor.Application.DTO.Input.Empresa;
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
                EmpNome = empresa.EmpNome,
                EmpCnpj = empresa.EmpCnpj,
                CriacaoData = empresa.CriacaoData
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
                EmpNome = empresa.EmpNome,
                EmpCnpj = empresa.EmpCnpj,
                CriacaoData = DateTime.UtcNow
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
                EmpNome = empresa.EmpNome,
                EmpCnpj = empresa.EmpCnpj
            };
        }

        public static IEnumerable<Empresa> MapEmpresa(this IEnumerable<EmpEdicaoDTO> empresa)
        {
            return empresa.Select(x => x.MapEmpresa()).ToList();
        }
    }
}
