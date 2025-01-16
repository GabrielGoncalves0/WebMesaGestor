using WebMesaGestor.Application.DTO.Input.Categoria;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.Map
{
    public static class CategoriaMap
    {
        public static CatOutputDTO MapCategoria(this Categoria categoria)
        {
            return new CatOutputDTO
            {
                Id = categoria.Id,
                CatDesc = categoria.CatDesc,
                CriacaoData = categoria.CriacaoData
            };
        }

        public static IEnumerable<CatOutputDTO> MapCategoria(this IEnumerable<Categoria> categoria)
        {
            return categoria.Select(x => x.MapCategoria()).ToList();
        }

        public static Categoria MapCategoria(this CatCriacaoDTO categoria)
        {
            return new Categoria
            {
                Id = Guid.NewGuid(),
                CatDesc = categoria.CatDesc,
                CriacaoData = DateTime.UtcNow
            };
        }

        public static IEnumerable<Categoria> MapCategoria(this IEnumerable<CatCriacaoDTO> categoria)
        {
            return categoria.Select(x => x.MapCategoria()).ToList();
        }

        public static Categoria MapCategoria(this CatEdicaoDTO categoria)
        {
            return new Categoria
            {
                CatDesc = categoria.CatDesc,
            };
        }

        public static IEnumerable<Categoria> MapCategoria(this IEnumerable<CatEdicaoDTO> categoria)
        {
            return categoria.Select(x => x.MapCategoria()).ToList();
        }
    }
}
