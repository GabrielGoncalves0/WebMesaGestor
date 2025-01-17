using WebMesaGestor.Application.DTO.Input.Pedido;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.Map
{
    public static class PedidoMap
    {
        public static PedOutputDTO MapPedido(this Pedido pedido)
        {
            return new PedOutputDTO
            {
                Id = pedido.Id,
                PedStatus = pedido.PedStatus,
                PedTipoPag = pedido.PedTipoPag,
                Usuario = UsuarioMap.MapUsuario(pedido.Usuario),
                Mesa = MesaMap.MapMesa(pedido.Mesa),
                CriacaoData = pedido.CriacaoData,
            };
        }

        public static IEnumerable<PedOutputDTO> MapPedido(this IEnumerable<Pedido> pedido)
        {
            return pedido.Select(x => x.MapPedido()).ToList();
        }

        public static Pedido MapPedido(this PedCriacaoDTO pedido)
        {
            return new Pedido
            {
                Id = Guid.NewGuid(),
                PedStatus = pedido.PedStatus,
                PedTipoPag = pedido.PedTipoPag,
                UsuarioId = pedido.UsuarioId,
                MesaId = pedido.MesaId,
                CriacaoData = DateTime.UtcNow
            };
        }

        public static IEnumerable<Pedido> MapPedido(this IEnumerable<PedCriacaoDTO> pedido)
        {
            return pedido.Select(x => x.MapPedido()).ToList();
        }

        public static Pedido MapPedido(this PedEdicaoDTO pedido)
        {
            return new Pedido
            {
                PedStatus = pedido.PedStatus,
                PedTipoPag = pedido.PedTipoPag,
                UsuarioId = pedido.UsuarioId,
                MesaId = pedido.MesaId,
            };
        }

        public static IEnumerable<Pedido> MapPedido(this IEnumerable<PedEdicaoDTO> pedido)
        {
            return pedido.Select(x => x.MapPedido()).ToList();
        }
    }
}
