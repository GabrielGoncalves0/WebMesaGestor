//using WebMesaGestor.Application.Common;
//using WebMesaGestor.Application.DTO.Input.Transacao;
//using WebMesaGestor.Application.DTO.Output;
//using WebMesaGestor.Domain.Entities;
//using WebMesaGestor.Domain.Interfaces;

//namespace WebMesaGestor.Application.Services
//{
//    public class TransacaoService
//    {
//        private readonly ITransacaoRepository _transacaoRepository;
//        private readonly IUsuarioRepository _usuarioRepository;
//        private readonly ICaixaRepository _caixaRepository;
//        private readonly IPedidoRepository _pedidoRepository;

//        public TransacaoService(ITransacaoRepository transacaoRepository, IUsuarioRepository usuarioRepository,
//            ICaixaRepository caixaRepository, IPedidoRepository pedidoRepository)
//        {
//            _transacaoRepository = transacaoRepository;
//            _usuarioRepository = usuarioRepository;
//            _caixaRepository = caixaRepository;
//            _pedidoRepository = pedidoRepository;
//        }

//        public async Task<Response<IEnumerable<TransacaoDTO>>> ListarTrasacoes()
//        {
//            Response<IEnumerable<TransacaoDTO>> resposta = new Response<IEnumerable<TransacaoDTO>>();
//            try
//            {
//                IEnumerable<Transacao> transacoes = await _transacaoRepository.ListarTransacoes();
//                if (transacoes == null || !transacoes.Any())
//                {
//                    resposta.Mensagem = "Transações não encontrada.";
//                    resposta.Status = false;
//                    return resposta;
//                }
//                await PreencherTransacoes(transacoes);

//                resposta.Dados = TransacaoMap.MapTransacao(transacoes);
//                resposta.Mensagem = "Transacões listadas com sucesso";
//                return resposta;
//            }
//            catch (Exception ex)
//            {
//                resposta.Mensagem = ex.Message;
//                resposta.Status = false;
//                return resposta;
//            }
//        }

//        public async Task<Response<TransacaoDTO>> TransacaoPorId(Guid id)
//        {
//            Response<TransacaoDTO> resposta = new Response<TransacaoDTO>();
//            try
//            {
//                Transacao transacao = await _transacaoRepository.TransacaoPorId(id);
//                if (transacao == null)
//                {
//                    resposta.Mensagem = "Transação não encontrada.";
//                    resposta.Status = false;
//                    return resposta;
//                }

//                await PreencherTransacao(transacao);
//                resposta.Dados = TransacaoMap.MapTransacao(transacao);
//                resposta.Mensagem = "Transação encontrada com sucesso";
//                return resposta;
//            }
//            catch (Exception ex)
//            {
//                resposta.Mensagem = ex.Message;
//                resposta.Status = false;
//                return resposta;
//            }
//        }

//        public async Task<Response<TransacaoDTO>> CriarTransacao(TraCriacaoDTO transacao)
//        {
//            Response<TransacaoDTO> resposta = new Response<TransacaoDTO>();
//            try
//            {
//                validarTransacaoCriacao(transacao);
//                await ValidarUsuario(transacao.UsuarioId);
//                await ValidarCaixa(transacao.CaixaId);
//                await ValidarPedido(transacao.PedidoId);

//                Transacao map = TransacaoMap.MapTransacao(transacao);
//                Transacao retorno = await _transacaoRepository.CriarTransacao(map);

//                await ProcessarTransacao(map, transacao);

//                await PreencherTransacao(retorno);

//                resposta.Dados = TransacaoMap.MapTransacao(retorno);
//                resposta.Mensagem = "Transação criada com sucesso";
//                return resposta;
//            }
//            catch (Exception ex)
//            {
//                resposta.Mensagem = ex.Message;
//                resposta.Status = false;
//                return resposta;
//            }
//        }

//        public async Task<Response<TransacaoDTO>> DeletarTransacao(Guid id)
//        {
//            Response<TransacaoDTO> resposta = new Response<TransacaoDTO>();
//            try
//            {
//                Transacao transacao = await _transacaoRepository.TransacaoPorId(id);
//                if (transacao == null)
//                {
//                    resposta.Mensagem = "Transação não encontrada para deleção.";
//                    resposta.Status = false;
//                    return resposta;
//                }
//                Caixa caixa = await _caixaRepository.CaixaPorId((Guid)transacao.CaixaId);
//                if (caixa == null)
//                {
//                    resposta.Mensagem = "Caixa não encontrado para atualização de valor";
//                    resposta.Status = false;
//                    return resposta;
//                }

//                if (transacao.TransacaoStatus == TranStatus.Suprimento)
//                {
//                    caixa.CaiValTotal -= transacao.TraValor;
//                }
//                else if (transacao.TransacaoStatus == TranStatus.Sangria)
//                {
//                    caixa.CaiValTotal += transacao.TraValor;
//                }
//                else if (transacao.PedidoId != null)
//                {
//                    var pedido = await _pedidoRepository.PedidoPorId((Guid)transacao.PedidoId);
//                    if (pedido != null)
//                    {
//                        caixa.CaiValTotal -= pedido.PedValor;
//                    }
//                }

//                await _caixaRepository.AtualizarCaixa(caixa);

//                Transacao retorno = await _transacaoRepository.DeletarTransacao(id);

//                await PreencherTransacao(retorno);
//                resposta.Dados = TransacaoMap.MapTransacao(retorno);
//                resposta.Mensagem = "Transação deletada com sucesso";
//                return resposta;
//            }
//            catch (Exception ex)
//            {
//                resposta.Mensagem = ex.Message;
//                resposta.Status = false;
//                return resposta;
//            }
//        }

//        private async Task ProcessarSuprimentoOuSangria(Guid caixaId, decimal valor, bool isSuprimento)
//        {
//            Caixa caixa = await _caixaRepository.ObterCaixaPorId(caixaId);
//            if (caixa == null)
//            {
//                throw new Exception("Caixa não encontrado.");
//            }

//            if (isSuprimento)
//            {
//                caixa.CaiValTotal += valor;
//            }
//            else
//            {
//                if (caixa.CaiValTotal < valor)
//                {
//                    throw new Exception("Valor de sangria é maior que o valor disponível no caixa.");
//                }
//                caixa.CaiValTotal -= valor;
//            }

//            await _caixaRepository.AtualizarCaixa(caixa);
//        }

//        private async Task ProcessarEntradaNoCaixa(Guid caixaId, decimal valorPedido)
//        {
//            Caixa caixa = await _caixaRepository.ObterCaixaPorId(caixaId);
//            if (caixa == null)
//            {
//                throw new Exception("Caixa não encontrado.");
//            }

//            caixa.CaiValTotal += valorPedido;

//            await _caixaRepository.AtualizarCaixa(caixa);
//        }

//        private async Task ProcessarTransacao(Transacao map, TraCriacaoDTO transacao)
//        {
//            if (transacao.TransacaoStatus == TranStatus.Suprimento)
//            {
//                await ProcessarSuprimentoOuSangria((Guid)transacao.CaixaId, transacao.TraValor, true);
//            }
//            if (transacao.TransacaoStatus == TranStatus.Sangria)
//            {
//                await ProcessarSuprimentoOuSangria((Guid)transacao.CaixaId, transacao.TraValor, false);
//            }
//            if (transacao.PedidoId != null)
//            {
//                var pedido = await _pedidoRepository.ObterPedidoPorId((Guid)transacao.PedidoId);
//                if (pedido == null)
//                {
//                    throw new Exception("Pedido não encontrado.");
//                }
//                await ProcessarEntradaNoCaixa((Guid)transacao.CaixaId, pedido.PedValor);
//            }
//        }
//    }
//}
