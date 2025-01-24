using WebMesaGestor.Application.DTO.Input.Transacao;
using WebMesaGestor.Application.DTO.Input.USuario;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.Map
{
    public static class TransacaoMap
    {
        public static TraOutputDTO MapTransacao(this Transacao transacao)
        {
            return new TraOutputDTO
            {
                Id = transacao.Id,
                TraDescricao = transacao.TraDescricao,
                TraValor = transacao.TraValor,
                TransacaoStatus = transacao.TransacaoStatus,
                CriacaoData = transacao.CriacaoData,
                Usuario = UsuarioMap.MapUsuario(transacao.Usuario),
                Caixa = CaixaMap.MapCaixa(transacao.Caixa),
                Pedido = PedidoMap.MapPedido(transacao.Pedido)
            };
        }

        public static IEnumerable<TraOutputDTO> MapTransacao(this IEnumerable<Transacao> transacao)
        {
            return transacao.Select(x => x.MapTransacao()).ToList();
        }

        public static Transacao MapTransacao(this TraCriacaoDTO transacao)
        {
            return new Transacao
            {
                Id = Guid.NewGuid(),
                TraDescricao = transacao.TraDescricao,
                TraValor = transacao.TraValor,
                TransacaoStatus = transacao.TransacaoStatus,
                CriacaoData = DateTime.UtcNow,
                UsuarioId = transacao.UsuarioId,
                CaixaId = transacao.CaixaId,
                PedidoId = transacao.PedidoId
            };
        }

        public static IEnumerable<Transacao> MapTransacao(this IEnumerable<TraCriacaoDTO> transacao)
        {
            return transacao.Select(x => x.MapTransacao()).ToList();
        }

        public static Transacao MapTransacao(this TraEdicaoDTO transacao)
        {
            return new Transacao
            {
                TraDescricao = transacao.TraDescricao,
                TraValor = transacao.TraValor,
                TransacaoStatus = transacao.TransacaoStatus,
                UsuarioId = transacao.UsuarioId,
                CaixaId = transacao.CaixaId,
                PedidoId = transacao.PedidoId
            };
        }

        public static IEnumerable<Transacao> MapTransacao(this IEnumerable<TraEdicaoDTO> transacao)
        {
            return transacao.Select(x => x.MapTransacao()).ToList();
        }
    }
}
