using WebMesaGestor.Application.DTO.Input.OpcaoProPed;
using WebMesaGestor.Application.DTO.Input.USuario;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.Map
{
    public static class OpcaoProPedMap
    {
        public static OpcProPedOutputDTO MapOpcProPed(this OpcaoProPed opcaoProPed)
        {
            return new OpcProPedOutputDTO
            {
                Id = opcaoProPed.Id,
                CriacaoData = opcaoProPed.CriacaoData,
                Opcao = opcaoProPed.Opcao != null ? OpcaoMap.MapOpcao(opcaoProPed.Opcao) : null,
                ProPed = opcaoProPed.ProdutoPedido != null ? ProdutoPedidoMap.MapProPed(opcaoProPed.ProdutoPedido) : null
            };
        }

        public static IEnumerable<OpcProPedOutputDTO> MapOpcProPed(this IEnumerable<OpcaoProPed> opcaoProPed)
        {
            return opcaoProPed.Select(x => x.MapOpcProPed()).ToList();
        }

        public static OpcaoProPed MapOpcProPed(this OpcProPedCriacaoDTO opcProPedCriacaoDTO)
        {
            return new OpcaoProPed
            {
                Id = Guid.NewGuid(),
                CriacaoData = DateTime.UtcNow,
                OpcaoId = opcProPedCriacaoDTO.OpcaoId,
                ProdutoPedidoId = opcProPedCriacaoDTO.ProdutoPedidoId,
            };
        }

        public static IEnumerable<OpcaoProPed> MapOpcProPed(this IEnumerable<OpcProPedCriacaoDTO> opcProPedCriacaoDTO)
        {
            return opcProPedCriacaoDTO.Select(x => x.MapOpcProPed()).ToList();
        }

        public static OpcaoProPed MapOpcProPed(this OpcProPedEdicaoDTO opcProPedEdicaoDTO)
        {
            return new OpcaoProPed
            {
                OpcaoId = opcProPedEdicaoDTO.OpcaoId,
                ProdutoPedidoId = opcProPedEdicaoDTO.ProdutoPedidoId,
            };
        }

        public static IEnumerable<OpcaoProPed> MapOpcProPed(this IEnumerable<OpcProPedEdicaoDTO> opcProPedEdicaoDTO)
        {
            return opcProPedEdicaoDTO.Select(x => x.MapOpcProPed()).ToList();
        }
    }
}
