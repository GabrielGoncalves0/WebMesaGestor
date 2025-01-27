using WebMesaGestor.Application.DTO.Input.Grupo;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.Map
{
    public static class GrupoOpcaoMap
    {
        public static GrupOpcOutputDTO MapGrupoOpcao(this GrupoOpcoes grupoOpcao)
        {
            return new GrupOpcOutputDTO
            {
                Id = grupoOpcao.Id,
                GrupOpcDesc = grupoOpcao.GrupOpcDesc,
                GrupOpcMax = grupoOpcao.GrupOpcMax,
                GrupOpcTipo = grupoOpcao.GrupOpcTipo,
                CriacaoData = grupoOpcao.CriacaoData,
                Produto = grupoOpcao.Produto != null ? ProdutoMap.MapProduto(grupoOpcao.Produto) : null,
            };
        }
        
        public static IEnumerable<GrupOpcOutputDTO> MapGrupoOpcao(this IEnumerable<GrupoOpcoes> grupoOpcao)
        {
            return grupoOpcao.Select(x => x.MapGrupoOpcao()).ToList();
        }

        public static GrupoOpcoes MapGrupoOpcao(this GrupOpcCriacaoDTO grupoOpcao)
        {
            return new GrupoOpcoes
            {
                Id = Guid.NewGuid(),
                GrupOpcDesc = grupoOpcao.GrupOpcDesc,
                GrupOpcMax = grupoOpcao.GrupOpcMax,
                GrupOpcTipo = grupoOpcao.GrupOpcTipo,
                ProdutoId = grupoOpcao.ProdutoId,
                CriacaoData = DateTime.UtcNow
            };
        }

        public static IEnumerable<GrupoOpcoes> MapUsuario(this IEnumerable<GrupOpcCriacaoDTO> grupoOpcao)
        {
            return grupoOpcao.Select(x => x.MapGrupoOpcao()).ToList();
        }

        public static GrupoOpcoes MapGrupoOpcao(this GrupOpcEdicaoDTO grupoOpcao)
        {
            return new GrupoOpcoes
            {
                GrupOpcDesc = grupoOpcao.GrupOpcDesc,
                GrupOpcMax = grupoOpcao.GrupOpcMax,
                GrupOpcTipo = grupoOpcao.GrupOpcTipo,
                ProdutoId = grupoOpcao.ProdutoId
            };
        }

        public static IEnumerable<GrupoOpcoes> MapGrupoOpcao(this IEnumerable<GrupOpcEdicaoDTO> grupo)
        {
            return grupo.Select(x => x.MapGrupoOpcao()).ToList();
        }
    }
}
