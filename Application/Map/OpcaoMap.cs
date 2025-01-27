using WebMesaGestor.Application.DTO.Input.Opcoes;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.Map
{
    public static class OpcaoMap
    {
        public static OpcOutputDTO MapOpcao(this Opcao opcao)
        {
            return new OpcOutputDTO
            {
                Id = opcao.Id,
                OpcaoDesc = opcao.OpcaoDesc,
                OpcaoValor = opcao.OpcaoValor,
                OpcaoQuantMax = opcao.OpcaoQuantMax,
                CriacaoData = opcao.CriacaoData,
                GrupoOpcoes = opcao.GrupoOpcoes != null ? GrupoOpcaoMap.MapGrupoOpcao(opcao.GrupoOpcoes) : null,
            };
        }

        public static IEnumerable<OpcOutputDTO> MapOpcao(this IEnumerable<Opcao> opcao)
        {
            return opcao.Select(x => x.MapOpcao()).ToList();
        }

        public static Opcao MapOpcao(this OpcCriacaoDTO opcao)
        {
            return new Opcao
            {
                Id = Guid.NewGuid(),
                OpcaoDesc = opcao.OpcaoDesc,
                OpcaoValor = opcao.OpcaoValor,
                OpcaoQuantMax = opcao.OpcaoQuantMax,
                GrupoOpcoesId = opcao.GrupoOpcoesId,
                CriacaoData = DateTime.UtcNow
            };
        }

        public static IEnumerable<Opcao> MapOpcao(this IEnumerable<OpcCriacaoDTO> opcao)
        {
            return opcao.Select(x => x.MapOpcao()).ToList();
        }

        public static Opcao MapOpcao(this OpcEdicaoDTO opcao)
        {
            return new Opcao
            {
                OpcaoDesc = opcao.OpcaoDesc,
                OpcaoValor = opcao.OpcaoValor,
                OpcaoQuantMax = opcao.OpcaoQuantMax,
                GrupoOpcoesId = opcao.GrupoOpcoesId,
            };
        }

        public static IEnumerable<Opcao> MapOpcao(this IEnumerable<OpcEdicaoDTO> opcao)
        {
            return opcao.Select(x => x.MapOpcao()).ToList();
        }
    }
}
