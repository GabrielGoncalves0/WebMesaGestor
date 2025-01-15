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
                Cai_Val_Inicial = caixa.Cai_Val_Inicial,
                Cai_Val_Fechamento = caixa.Cai_Val_Fechamento,
                Abertura_data = caixa.Abertura_data,
                Fechamento_data = caixa.Fechamento_data,
                Cai_status = caixa.Cai_status,
                Usuario = UsuarioMap.MapUsuario(caixa.Usuario)
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
                Cai_Val_Inicial = caixa.Cai_Val_Inicial,
                UsuarioId = caixa.UsuarioId,
                Abertura_data = DateTime.UtcNow,
                Cai_status = CaixaStatus.Aberto
            };
        }

        public static IEnumerable<Caixa> MapCaixa(this IEnumerable<CaiAbrirDTO> caixa)
        {
            return caixa.Select(x => x.MapCaixa()).ToList();
        }

        public static Caixa MapCaixa(this CaiFecharDTO caixa)
        {
            return new Caixa
            {
                Cai_Val_Fechamento = caixa.Cai_Val_Fechamento,
                Fechamento_data = DateTime.UtcNow,
                Cai_status = CaixaStatus.Fechado
            };
        }

        public static IEnumerable<Caixa> MapCaixa(this IEnumerable<CaiFecharDTO> caixa)
        {
            return caixa.Select(x => x.MapCaixa()).ToList();
        }
    }
}
