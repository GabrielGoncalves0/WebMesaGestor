using WebMesaGestor.Application.DTO.Input.Grupo;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.Map
{
    public static class GrupoMap
    {
        public static GrupOutputDTO MapGrupo(this Grupo grupo)
        {
            return new GrupOutputDTO
            {
                Id = grupo.Id,
                GrupDesc = grupo.GrupDesc,
                CriacaoData = grupo.CriacaoData
            };
        }

        public static IEnumerable<GrupOutputDTO> MapGrupo(this IEnumerable<Grupo> grupo)
        {
            return grupo.Select(x => x.MapGrupo()).ToList();
        }

        public static Grupo MapGrupo(this GrupCriacaoDTO grupo)
        {
            return new Grupo
            {
                Id = Guid.NewGuid(),
                GrupDesc = grupo.GrupDesc,
                CriacaoData = DateTime.UtcNow
            };
        }

        public static IEnumerable<Grupo> MapGrupo(this IEnumerable<GrupCriacaoDTO> grupo)
        {
            return grupo.Select(x => x.MapGrupo()).ToList();
        }

        public static Grupo MapGrupo(this GrupEdicaoDTO grupo)
        {
            return new Grupo
            {
                GrupDesc = grupo.GrupDesc,
            };
        }

        public static IEnumerable<Grupo> MapGrupo(this IEnumerable<GrupEdicaoDTO> grupo)
        {
            return grupo.Select(x => x.MapGrupo()).ToList();
        }
    }
}
