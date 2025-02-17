using AutoMapper;
using WebMesaGestor.Application.Common;
using WebMesaGestor.Application.DTO.Input.Transacao;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.Validations.Transacao;
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
        private readonly IMapper _mapper;

        public TransacaoService(ITransacaoRepository transacaoRepository, IUsuarioRepository usuarioRepository,
            ICaixaRepository caixaRepository, IPedidoRepository pedidoRepository,IMapper mapper)
        {
            _transacaoRepository = transacaoRepository;
            _usuarioRepository = usuarioRepository;
            _caixaRepository = caixaRepository;
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TransacaoDTO>> ObterTodosUsuarios()
        {
            var transacoes = await _transacaoRepository.ObterTodasTransacoes();
            if (transacoes == null)
            {
                return null;
            }
            return _mapper.Map<IEnumerable<TransacaoDTO>>(transacoes);
        }

        public async Task<TransacaoDTO> TransacaoPorId(Guid id)
        {
            var transacao = await _transacaoRepository.ObterTransacaoPorId(id);
            if (transacao == null)
            {
                return null;
            }
            return _mapper.Map<TransacaoDTO>(transacao);
        }

        public async Task<Response<TransacaoDTO>> CriarTransacao(TraCriacaoDTO transacaoDTO)
        {
            var validationResult = new TransacaoCriacaoValidator().Validate(transacaoDTO);
            if (!validationResult.IsValid)
            {
                return new Response<TransacaoDTO>
                {
                    Sucesso = false,
                    Erros = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var usuario = await _usuarioRepository.ObterUsuarioPorId((Guid)transacaoDTO.UsuarioId);
            if (usuario == null)
            {
                return new Response<TransacaoDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { UsuarioMessages.UsuarioNaoEncontrado }
                };
            }

            var caixa = await _caixaRepository.ObterCaixaPorId((Guid)transacaoDTO.CaixaId);
            if (caixa == null)
            {
                return new Response<TransacaoDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { CaixaMessages.CaixaNaoEncontrado }
                };
            }

            var pedido = await _pedidoRepository.ObterPedidoPorId((Guid)transacaoDTO.PedidoId);
            if (pedido == null)
            {
                return new Response<TransacaoDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { PedidoMessages.PedidoNaoEncontrado }
                };
            }

            var transacao = _mapper.Map<Transacao>(transacaoDTO);
            transacao.Usuario = usuario;
            transacao.Caixa = caixa;
            transacao.Pedido = pedido;

            await _transacaoRepository.CriarTransacao(transacao);

            await ProcessarTransacao(transacaoDTO);

            return new Response<TransacaoDTO>
            {
                Sucesso = true,
                Id = transacao.Id,
                Data = _mapper.Map<TransacaoDTO>(transacao),
            };
        }

        public async Task<Response<TransacaoDTO>> DeletarTransacao(Guid id)
        {
            var transacaoExistente = await _transacaoRepository.ObterTransacaoPorId(id);
            if (transacaoExistente == null)
            {
                return new Response<TransacaoDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { TransacaoMessages.TransacaoNaoEncontrada }
                };
            }

            var caixa = await _caixaRepository.ObterCaixaPorId((Guid)transacaoExistente.CaixaId);
            if (caixa == null)
            {
                return new Response<TransacaoDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { CaixaMessages.CaixaNaoEncontrado }
                };
            }

            if (transacaoExistente.TransacaoStatus == TranStatus.Suprimento)
            {
                await ProcessarSuprimentoOuSangria((Guid)transacaoExistente.CaixaId, transacaoExistente.TraValor, false); // Reverter o suprimento
            }
            if (transacaoExistente.TransacaoStatus == TranStatus.Sangria)
            {
                await ProcessarSuprimentoOuSangria((Guid)transacaoExistente.CaixaId, transacaoExistente.TraValor, true); // Reverter a sangria
            }

            if (transacaoExistente.PedidoId != null)
            {
                var pedido = await _pedidoRepository.ObterPedidoPorId((Guid)transacaoExistente.PedidoId);
                if (pedido != null)
                {
                    await ProcessarEntradaNoCaixa((Guid)transacaoExistente.CaixaId, -pedido.PedValor); // Subtrair o valor do pedido
                }
            }

            var sucessoDelecao = await _transacaoRepository.DeletarTransacao(id);
            if (!sucessoDelecao)
            {
                return new Response<TransacaoDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { TransacaoMessages.ErroAoDeletarTransacao }
                };
            }

            await _caixaRepository.AtualizarCaixa(caixa);

            var dados = _mapper.Map<TransacaoDTO>(transacaoExistente);

            return new Response<TransacaoDTO>
            {
                Sucesso = true,
                Data = dados,
                Erros = new List<string> { TransacaoMessages.TransacaoDeletadaComSucesso }
            };
        }

        private async Task ProcessarSuprimentoOuSangria(Guid caixaId, decimal valor, bool isSuprimento)
        {
            Caixa caixa = await _caixaRepository.ObterCaixaPorId(caixaId);
            if (caixa == null)
            {
                throw new Exception("Caixa não encontrado.");
            }

            if (isSuprimento)
            {
                caixa.CaiValTotal += valor;
            }
            else
            {
                if (caixa.CaiValTotal < valor)
                {
                    throw new Exception("Valor de sangria é maior que o valor disponível no caixa.");
                }
                caixa.CaiValTotal -= valor;
            }

            await _caixaRepository.AtualizarCaixa(caixa);
        }
        private async Task ProcessarEntradaNoCaixa(Guid caixaId, decimal valorPedido)
        {
            Caixa caixa = await _caixaRepository.ObterCaixaPorId(caixaId);
            if (caixa == null)
            {
                throw new Exception("Caixa não encontrado.");
            }

            caixa.CaiValTotal += valorPedido;

            await _caixaRepository.AtualizarCaixa(caixa);
        }
        private async Task ProcessarTransacao(TraCriacaoDTO transacaoDTO)
        {
            if (transacaoDTO.TransacaoStatus == TranStatus.Suprimento)
            {
                await ProcessarSuprimentoOuSangria((Guid)transacaoDTO.CaixaId, transacaoDTO.TraValor, true);
            }
            if (transacaoDTO.TransacaoStatus == TranStatus.Sangria)
            {
                await ProcessarSuprimentoOuSangria((Guid)transacaoDTO.CaixaId, transacaoDTO.TraValor, false);
            }
            if (transacaoDTO.PedidoId != null)
            {
                var pedido = await _pedidoRepository.ObterPedidoPorId((Guid)transacaoDTO.PedidoId);
                if (pedido == null)
                {
                    throw new Exception( PedidoMessages.PedidoNaoEncontrado);
                }
                await ProcessarEntradaNoCaixa((Guid)transacaoDTO.CaixaId, pedido.PedValor);
            }
        }

    }
}
