using WebMesaGestor.Application.DTO.Input.Subgrupo;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.Map
{
    public static class SubgrupoMap
    {
        public static SubgrupOutputDTO MapSubgrupo(this Subgrupo subgrupo)
        {
            return new SubgrupOutputDTO
            {
                Id = subgrupo.Id,
                SubgruDesc = subgrupo.SubgruDesc,
                CriacaoData = subgrupo.CriacaoData
            };
        }

        public static IEnumerable<SubgrupOutputDTO> MapSubgrupo(this IEnumerable<Subgrupo> subgrupo)
        {
            return subgrupo.Select(x => x.MapSubgrupo()).ToList();
        }

        public static Subgrupo MapSubgrupo(this SubgrupCriacaoDTO subgrupo)
        {
            return new Subgrupo
            {
                Id = Guid.NewGuid(),
                SubgruDesc = subgrupo.SubgruDesc,
                CriacaoData = DateTime.UtcNow
            };
        }

        public static IEnumerable<Subgrupo> MapSubgrupo(this IEnumerable<SubgrupCriacaoDTO> subgrupo)
        {
            return subgrupo.Select(x => x.MapSubgrupo()).ToList();
        }

        public static Subgrupo MapSubgrupo(this SubgrupEdicaoDTO subgrupo)
        {
            return new Subgrupo
            {
                SubgruDesc = subgrupo.SubgruDesc
            };
        }

        public static IEnumerable<Subgrupo> MapSubgrupo(this IEnumerable<SubgrupEdicaoDTO> subgrupo)
        {
            return subgrupo.Select(x => x.MapSubgrupo()).ToList();
        }
    }
}
