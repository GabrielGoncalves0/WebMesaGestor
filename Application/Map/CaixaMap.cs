using WebMesaGestor.Application.DTO.Input.Caixa;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.Map
{
    public static class CaixaMap
    {
        public static CaiOutputDTO MapCaixa(this Caixa caixa)
        {
            return new CaiOutputDTO
            {
                Id = caixa.Id,
                CaiValInicial = caixa.CaiValInicial,
                CaiValFechamento = caixa.CaiValFechamento,
                CaiValTotal = caixa.CaiValTotal,
                AberturaData = caixa.AberturaData,
                FechamentoData = caixa.FechamentoData,
                CaiStatus = caixa.CaiStatus,
                Usuario = caixa.Usuario != null ? UsuarioMap.MapUsuario(caixa.Usuario) : null
            };
        }

        public static IEnumerable<CaiOutputDTO> MapCaixa(this IEnumerable<Caixa> caixa)
        {
            return caixa.Select(x => x.MapCaixa()).ToList();
        }

        public static Caixa MapCaixa(this CaiAbrirDTO caixa)
        {
            return new Caixa
            {
                Id = Guid.NewGuid(),
                CaiValInicial = caixa.CaiValInicial,
                UsuarioId = caixa.UsuarioId,
                AberturaData = DateTime.UtcNow,
                CaiStatus = CaixaStatus.Aberto
            };
        }

        public static IEnumerable<Caixa> MapCaixa(this IEnumerable<CaiAbrirDTO> caixa)
        {
            return caixa.Select(x => x.MapCaixa()).ToList();
        }

        public static Caixa MapCaixa(this CaiAtualizarDTO caixa)
        {
            return new Caixa
            {
                CaiValTotal = caixa.CaiValTotal,
                CaiStatus = caixa.CaiStatus
            };
        }

        public static IEnumerable<Caixa> MapCaixa(this IEnumerable<CaiAtualizarDTO> caixa)
        {
            return caixa.Select(x => x.MapCaixa()).ToList();
        }
    }
}
