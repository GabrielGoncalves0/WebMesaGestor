using WebMesaGestor.Application.DTO.Input.Marca;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.Map
{
    public static class MarcaMap
    {
        public static MarOutputDTO MapMarca(this Marca marca)
        {
            return new MarOutputDTO
            {
                Id = marca.Id,
                MarNome = marca.MarNome,
                CriacaoData = marca.CriacaoData
            };
        }

        public static IEnumerable<MarOutputDTO> MapMarca(this IEnumerable<Marca> marca)
        {
            return marca.Select(x => x.MapMarca()).ToList();
        }

        public static Marca MapMarca(this MarCriacaoDTO marca)
        {
            return new Marca
            {
                Id = Guid.NewGuid(),
                MarNome = marca.MarNome,
                CriacaoData = DateTime.UtcNow
            };
        }

        public static IEnumerable<Marca> MapMarca(this IEnumerable<MarCriacaoDTO> marca)
        {
            return marca.Select(x => x.MapMarca()).ToList();
        }

        public static Marca MapMarca(this MarEdicaoDTO marca)
        {
            return new Marca
            {
                MarNome = marca.MarNome,
            };
        }

        public static IEnumerable<Marca> MapMarca(this IEnumerable<MarEdicaoDTO> marca)
        {
            return marca.Select(x => x.MapMarca()).ToList();
        }
    }
}
