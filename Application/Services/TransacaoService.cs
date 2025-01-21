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

        public async Task<Response<IEnumerable<TraOutputDTO>>> ListarTrasacoes()
        {
            Response<IEnumerable<TraOutputDTO>> resposta = new Response<IEnumerable<TraOutputDTO>>();
            try
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
                resposta.Dados = TransacaoMap.MapTransacao(transacaos);
                resposta.Mensagem = "Transacões listadas com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<TraOutputDTO>> TransacaoPorId(Guid id)
        {
            Response<TraOutputDTO> resposta = new Response<TraOutputDTO>();
            try
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
                resposta.Dados = TransacaoMap.MapTransacao(transacao);
                resposta.Mensagem = "Transação encontrada com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<TraOutputDTO>> CriarTransacao(TraCriacaoDTO transacao)
        {
            Response<TraOutputDTO> resposta = new Response<TraOutputDTO>();
            try
            {
                Transacao map = TransacaoMap.MapTransacao(transacao);
                Transacao retorno = await _transacaoRepository.CriarTransacao(map);
                resposta.Dados = TransacaoMap.MapTransacao(retorno);
                resposta.Mensagem = "Transação criada com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<TraOutputDTO>> AtualizarTransacao(TraEdicaoDTO transacao)
        {
            Response<TraOutputDTO> resposta = new Response<TraOutputDTO>();
            try
            {
                Transacao buscarTransacao = await _transacaoRepository.TransacaoPorId(transacao.Id);

                buscarTransacao.TraDescricao = transacao.TraDescricao;
                buscarTransacao.TraValor = transacao.TraValor;
                buscarTransacao.TransactionStatus = transacao.TransactionStatus;
                buscarTransacao.UsuarioId = transacao.UsuarioId;
                buscarTransacao.CaixaId = transacao.CaixaId;
                buscarTransacao.PedidoId = transacao.PedidoId;

                Transacao retorno = await _transacaoRepository.AtualizarTransacao(buscarTransacao);

                resposta.Dados = TransacaoMap.MapTransacao(retorno);
                resposta.Mensagem = "Transação atualizada com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }

        }

        public async Task<Response<TraOutputDTO>> DeletarTransacao(Guid id)
        {
            Response<TraOutputDTO> resposta = new Response<TraOutputDTO>();
            try
            {
                Transacao retorno = await _transacaoRepository.DeletarTransacao(id);
                resposta.Dados = TransacaoMap.MapTransacao(retorno);
                resposta.Mensagem = "Transação deletada com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }

        }
    }
}
