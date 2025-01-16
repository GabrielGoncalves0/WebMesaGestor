using WebMesaGestor.Application.DTO.Input.Opcoes;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.Map
{
    public static class OpcoesMap
    {
        public static OpcOutputDTO MapOpcoes(this Opcoes opcoes)
        {
            return new OpcOutputDTO
            {
                Id = opcoes.Id,
                OpcaoDesc = opcoes.OpcaoDesc,
                OpcaoValor = opcoes.OpcaoValor,
                OpcaoQuantMax = opcoes.OpcaoQuantMax,
                CriacaoData = opcoes.CriacaoData,
                GrupoOpcoes = GrupoOpcaoMap.MapGrupoOpcao(opcoes.GrupoOpcoes),
            };
        }

        public static IEnumerable<OpcOutputDTO> MapOpcoes(this IEnumerable<Opcoes> opcoes)
        {
            return opcoes.Select(x => x.MapOpcoes()).ToList();
        }

        public static Opcoes MapOpcoes(this OpcCriacaoDTO opcoes)
        {
            return new Opcoes
            {
                Id = Guid.NewGuid(),
                OpcaoDesc = opcoes.OpcaoDesc,
                OpcaoValor = opcoes.OpcaoValor,
                OpcaoQuantMax = opcoes.OpcaoQuantMax,
                GrupoOpcoesId = opcoes.GrupoOpcoesId,
                CriacaoData = DateTime.UtcNow
            };
        }

        public static IEnumerable<Opcoes> MapOpcoes(this IEnumerable<OpcCriacaoDTO> opcoes)
        {
            return opcoes.Select(x => x.MapOpcoes()).ToList();
        }

        public static Opcoes MapOpcoes(this OpcEdicaoDTO opcoes)
        {
            return new Opcoes
            {
                OpcaoDesc = opcoes.OpcaoDesc,
                OpcaoValor = opcoes.OpcaoValor,
                OpcaoQuantMax = opcoes.OpcaoQuantMax,
                GrupoOpcoesId = opcoes.GrupoOpcoesId,
            };
        }

        public static IEnumerable<Opcoes> MapOpcoes(this IEnumerable<OpcEdicaoDTO> opcoes)
        {
            return opcoes.Select(x => x.MapOpcoes()).ToList();
        }
    }
}
