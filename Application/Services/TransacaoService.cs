using WebMesaGestor.Application.DTO.Input.Transacao;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.Map;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;

namespace WebMesaGestor.Application.Services
{
    public class TransacaoService
    {
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ICaixaRepository _caixaRepository;
        private readonly IPedidoRepository _pedidoRepository;

        public TransacaoService(ITransacaoRepository transacaoRepository, IUsuarioRepository usuarioRepository,
            ICaixaRepository caixaRepository, IPedidoRepository pedidoRepository)
        {
            _transacaoRepository = transacaoRepository;
            _usuarioRepository = usuarioRepository;
            _caixaRepository = caixaRepository;
            _pedidoRepository = pedidoRepository;
        }

        public async Task<IEnumerable<TraOutputDTO>> ListarTrasacoes()
        {
            IEnumerable<Transacao> transacaos = await _transacaoRepository.ListarTransacoes();
            foreach (var transacao in transacaos)
            {
                if (transacao.UsuarioId != null)
                {
                    transacao.Usuario = await _usuarioRepository.UsuarioPorId((Guid)transacao.UsuarioId);
                }
                if (transacao.CaixaId != null)
                {
                    transacao.Caixa = await _caixaRepository.CaixaPorId((Guid)transacao.CaixaId);
                }
                if (transacao.PedidoId != null)
                {
                    transacao.Pedido = await _pedidoRepository.PedidoPorId((Guid)transacao.PedidoId);
                }
            }
            return TransacaoMap.MapTransacao(transacaos);
        }

        public async Task<TraOutputDTO> TransacaoPorId(Guid id)
        {
            Transacao transacao = await _transacaoRepository.TransacaoPorId(id);
            if (transacao.UsuarioId != null)
            {
                transacao.Usuario = await _usuarioRepository.UsuarioPorId((Guid)transacao.UsuarioId);
            }
            if (transacao.CaixaId != null)
            {
                transacao.Caixa = await _caixaRepository.CaixaPorId((Guid)transacao.CaixaId);
            }
            if (transacao.PedidoId != null)
            {
                transacao.Pedido = await _pedidoRepository.PedidoPorId((Guid)transacao.PedidoId);
            }
            return TransacaoMap.MapTransacao(transacao);
        }

        public async Task<TraOutputDTO> CriarTransacao(TraCriacaoDTO transacao)
        {
            Transacao map = TransacaoMap.MapTransacao(transacao);
            Transacao retorno = await _transacaoRepository.CriarTransacao(map);
            return TransacaoMap.MapTransacao(retorno);
        }

        public async Task<TraOutputDTO> AtualizarTransacao(TraEdicaoDTO transacao)
        {
            Transacao buscarTransacao = await _transacaoRepository.TransacaoPorId(transacao.Id);

            buscarTransacao.TraDescricao = transacao.TraDescricao;
            buscarTransacao.TraValor = transacao.TraValor;
            buscarTransacao.TransactionStatus = transacao.TransactionStatus;
            buscarTransacao.UsuarioId = transacao.UsuarioId;
            buscarTransacao.CaixaId = transacao.CaixaId;
            buscarTransacao.PedidoId = transacao.PedidoId;

            Transacao retorno = await _transacaoRepository.AtualizarTransacao(buscarTransacao);
            return TransacaoMap.MapTransacao(retorno);
        }

        public async Task<TraOutputDTO> DeletarTransacao(Guid id)
        {
            Transacao retorno = await _transacaoRepository.DeletarTransacao(id);
            return TransacaoMap.MapTransacao(retorno);
        }
    }
}
