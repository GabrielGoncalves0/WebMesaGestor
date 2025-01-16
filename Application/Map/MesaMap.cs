using WebMesaGestor.Application.DTO.Input.Mesa;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.Map
{
    public static class MesaMap
    {
        public static MesOutputDTO MapMesa(this Mesa mesa)
        {
            return new MesOutputDTO
            {
                Id = mesa.Id,
                MesaNumero = mesa.MesaNumero,
                MesaStatus = mesa.MesaStatus,
                CriacaoData = mesa.CriacaoData
            };
        }

        public static IEnumerable<MesOutputDTO> MapMesa(this IEnumerable<Mesa> mesa)
        {
            return mesa.Select(x => x.MapMesa()).ToList();
        }

        public static Mesa MapMesa(this MesCriacaoDTO mesa)
        {
            return new Mesa
            {
                Id = Guid.NewGuid(),
                MesaNumero = mesa.MesaNumero,
                MesaStatus = mesa.MesaStatus,
                CriacaoData = DateTime.UtcNow
            };
        }

        public static IEnumerable<Mesa> MapMesa(this IEnumerable<MesCriacaoDTO> mesa)
        {
            return mesa.Select(x => x.MapMesa()).ToList();
        }

        public static Mesa MapMesa(this MesEdicaoDTO mesa)
        {
            return new Mesa
            {
                MesaNumero = mesa.MesaNumero,
                MesaStatus = mesa.MesaStatus
            };
        }

        public static IEnumerable<Mesa> MapMesa(this IEnumerable<MesEdicaoDTO> mesa)
        {
            return mesa.Select(x => x.MapMesa()).ToList();
        }
    }
}
