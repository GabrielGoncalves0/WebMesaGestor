using WebMesaGestor.Application.DTO.Input.ProdutoPedido;
using WebMesaGestor.Application.DTO.Input.USuario;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.Map
{
    public static class ProdutoPedidoMap
    {
        public static ProPedOutputDTO MapProPed(this ProdutoPedido produtoPedido)
        {
            return new ProPedOutputDTO
            {
                Id = produtoPedido.Id,
                PedQuant = produtoPedido.PedQuant,
                PedDesconto = produtoPedido.PedDesconto,
                CriacaoData = produtoPedido.CriacaoData,
                StatusProPed = produtoPedido.StatusProPed,
                Produto = produtoPedido.Produto != null ? ProdutoMap.MapProduto(produtoPedido.Produto) : null,
                Pedido = produtoPedido.Pedido != null ? PedidoMap.MapPedido(produtoPedido.Pedido) : null
            };
        }

        public static IEnumerable<ProPedOutputDTO> MapProPed(this IEnumerable<ProdutoPedido> produtoPedido)
        {
            return produtoPedido.Select(x => x.MapProPed()).ToList();
        }

        public static ProdutoPedido MapProPed(this ProPedCriacaoDTO proPedCriacaoDTO)
        {
            return new ProdutoPedido
            {
                Id = Guid.NewGuid(),
                PedQuant = proPedCriacaoDTO.ProPedQuant,
                PedDesconto = proPedCriacaoDTO.ProPedDesconto,
                ProdutoId = proPedCriacaoDTO.ProdutoId,
                PedidoId = proPedCriacaoDTO.PedidoId,
                StatusProPed = StatusProPed.Nao_pago,
                CriacaoData = DateTime.UtcNow,
            };
        }

        public static IEnumerable<ProdutoPedido> MapProPed(this IEnumerable<ProPedCriacaoDTO> proPedCriacaoDTO)
        {
            return proPedCriacaoDTO.Select(x => x.MapProPed()).ToList();
        }

        public static ProdutoPedido MapProPed(this ProPedEdicaoDTO proPedEdicaoDTO)
        {
            return new ProdutoPedido
            {
                PedQuant = proPedEdicaoDTO.ProPedQuant,
                PedDesconto = proPedEdicaoDTO.ProPedDesconto,
                StatusProPed = proPedEdicaoDTO.StatusProPed,
                ProdutoId = proPedEdicaoDTO.ProdutoId,
                PedidoId = proPedEdicaoDTO.PedidoId,
            };
        }

        public static IEnumerable<ProdutoPedido> MapProPed(this IEnumerable<ProPedEdicaoDTO> proPedEdicaoDTO)
        {
            return proPedEdicaoDTO.Select(x => x.MapProPed()).ToList();
        }
    }
}
