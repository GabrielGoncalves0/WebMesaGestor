using WebMesaGestor.Application.DTO.Input.Transacao;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.Map;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Utils;

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
                IEnumerable<Transacao> transacoes = await _transacaoRepository.ListarTransacoes();
                if (transacoes == null)
                {
                    resposta.Mensagem = "Transações não encontrada.";
                    resposta.Status = false;
                    return resposta;
                }
                await PreencherTransacoes(transacoes);

                resposta.Dados = TransacaoMap.MapTransacao(transacoes);
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
                if (transacao == null)
                {
                    resposta.Mensagem = "Transação não encontrada.";
                    resposta.Status = false;
                    return resposta;
                }

                await PreencherTransacao(transacao);
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
                validarTransacaoCriacao(transacao);
                await ValidarUsuario(transacao.UsuarioId);
                await ValidarCaixa(transacao.CaixaId);
                await ValidarPedido(transacao.PedidoId);

                Transacao map = TransacaoMap.MapTransacao(transacao);
                Transacao retorno = await _transacaoRepository.CriarTransacao(map);
                await PreencherTransacao(retorno);

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
                validarTransacaoEdicao(transacao);
                await ValidarUsuario(transacao.UsuarioId);
                await ValidarCaixa(transacao.CaixaId);
                await ValidarPedido(transacao.PedidoId);
                Transacao buscarTransacao = await _transacaoRepository.TransacaoPorId(transacao.Id);
                if (buscarTransacao == null)
                {
                    resposta.Mensagem = "Transação não encontrada.";
                    resposta.Status = false;
                    return resposta;
                }
                AtualizarDadosTransacao(buscarTransacao, transacao);
                Transacao retorno = await _transacaoRepository.AtualizarTransacao(buscarTransacao);
                await PreencherTransacao(retorno);

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
                Transacao transacao = await _transacaoRepository.TransacaoPorId(id);
                if(transacao == null)
                {
                    resposta.Mensagem = "Transação não encontrada para deleção.";
                    resposta.Status = false;
                    return resposta;
                }
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

        // Métodos auxiliares
        private async Task PreencherTransacoes(IEnumerable<Transacao> transacoes)
        {
            foreach (var transacao in transacoes)
            {
                if (transacao.UsuarioId != null)
                {
                    transacao.Usuario = await _usuarioRepository.UsuarioPorId((Guid)transacao.UsuarioId);
                }
                if (transacao.CaixaId != null)
                {
                    transacao.Caixa = await _caixaRepository.CaixaPorId((Guid)transacao.CaixaId); ;
                }
                if (transacao.PedidoId != null)
                {
                    transacao.Pedido = await _pedidoRepository.PedidoPorId((Guid)transacao.PedidoId);
                }
            }
        }

        private async Task PreencherTransacao(Transacao transacao)
        {
            transacao.Usuario = await _usuarioRepository.UsuarioPorId((Guid)transacao.UsuarioId);
            transacao.Caixa = await _caixaRepository.CaixaPorId((Guid)transacao.CaixaId);
            transacao.Pedido = await _pedidoRepository.PedidoPorId((Guid)transacao.PedidoId);
        }

        private async Task ValidarUsuario(Guid? usuarioId)
        {
            if (usuarioId == null || usuarioId == Guid.Empty)
            {
                throw new Exception("Empresa é obrigatória");
            }

            var usuario = await _usuarioRepository.UsuarioPorId((Guid)usuarioId);

            if (usuario == null)
            {
                throw new Exception("Empresa não encontrada");
            }
        }

        private async Task ValidarCaixa(Guid? caixaId)
        {
            if (caixaId == null || caixaId == Guid.Empty)
            {
                throw new Exception("Empresa é obrigatória");
            }

            var caixa = await _caixaRepository.CaixaPorId((Guid)caixaId);

            if (caixa == null)
            {
                throw new Exception("Empresa não encontrada");
            }
        }

        private async Task ValidarPedido(Guid? pedidoId)
        {
            if (pedidoId == null || pedidoId == Guid.Empty)
            {
                throw new Exception("Pedido é obrigatória");
            }

            var pedido = await _pedidoRepository.PedidoPorId((Guid)pedidoId);

            if (pedido == null)
            {
                throw new Exception("Empresa não encontrada");
            }
        }

        public void validarTransacaoCriacao(TraCriacaoDTO transacao)
        {
            ValidadorUtils.ValidarSeVazioOuNulo(transacao.TraDescricao, "Descricao é obrigatório");
            ValidadorUtils.ValidarMaximo(transacao.TraDescricao, 100, "Descricao deve conter no máximo 100 caracteres");
            ValidadorUtils.ValidarMinimo(transacao.TraDescricao, 3, "Descricao deve conter no minimo 3 caracteres");
            ValidadorUtils.ValidarDecimalSeVazio(transacao.TraValor, "Valor é obrigatório");
            ValidadorUtils.ValidarMaximo(transacao.TraValor, 9999999, "Valor deve ser menor que 9999999");
            ValidadorUtils.ValidarMinimo(transacao.TraValor, 0, "Valor deve ser maior que 0");
            if (!Enum.IsDefined(typeof(TranStatus), transacao.TransacaoStatus))
            {
                throw new Exception("Status de transação é obrigatório");
            }
        }

        public void validarTransacaoEdicao(TraEdicaoDTO transacao)
        {
            ValidadorUtils.ValidarSeVazioOuNulo(transacao.TraDescricao, "Descricao é obrigatório");
            ValidadorUtils.ValidarMaximo(transacao.TraDescricao, 100, "Descricao deve conter no máximo 100 caracteres");
            ValidadorUtils.ValidarMinimo(transacao.TraDescricao, 3, "Descricao deve conter no minimo 3 caracteres");
            ValidadorUtils.ValidarDecimalSeVazio(transacao.TraValor, "Descricao é obrigatório");
            ValidadorUtils.ValidarMaximo(transacao.TraValor, 9999999, "Valor deve ser menor que 9999999");
            ValidadorUtils.ValidarMinimo(transacao.TraValor, 0, "Valor deve ser maior que 0");
            if (!Enum.IsDefined(typeof(TranStatus), transacao.TransacaoStatus))
            {
                throw new Exception("Status de transação é obrigatório");
            }
        }

        private void AtualizarDadosTransacao(Transacao transacaoExistente, TraEdicaoDTO transacao)
        {
            transacaoExistente.TraDescricao = transacao.TraDescricao;
            transacaoExistente.TraValor = transacao.TraValor;
            transacaoExistente.TransacaoStatus = transacao.TransacaoStatus;
            transacaoExistente.UsuarioId = transacao.UsuarioId;
            transacaoExistente.CaixaId = transacao.CaixaId;
            transacaoExistente.PedidoId = transacao.PedidoId;
        }
    }
}
