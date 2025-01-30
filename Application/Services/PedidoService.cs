using WebMesaGestor.Application.DTO.Input.Pedido;
using WebMesaGestor.Application.DTO.Input.Produto;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.Map;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Infra.Repositories;
using WebMesaGestor.Utils;
using static WebMesaGestor.Domain.Entities.Pedido;

namespace WebMesaGestor.Application.Services
{
    public class PedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMesaRepository _mesaRepository;

        public PedidoService(IPedidoRepository pedidoRepository, IUsuarioRepository usuarioRepository, IMesaRepository mesaRepository)
        {
            _pedidoRepository = pedidoRepository;
            _usuarioRepository = usuarioRepository;
            _mesaRepository = mesaRepository;
        }

        public async Task<Response<IEnumerable<PedOutputDTO>>> ListarPedidos()
        {
            Response<IEnumerable<PedOutputDTO>> resposta = new Response<IEnumerable<PedOutputDTO>>();
            try
            {
                IEnumerable<Pedido> pedidos = await _pedidoRepository.ListarPedidos();
                if (pedidos == null)
                {
                    resposta.Mensagem = "Pedidos não encontrado.";
                    resposta.Status = false;
                    return resposta;
                }
                await PreencherPedidos(pedidos);
                resposta.Dados = PedidoMap.MapPedido(pedidos);
                resposta.Mensagem = "Pedidos listados com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<PedOutputDTO>> PedidoPorId(Guid id)
        {
            Response<PedOutputDTO> resposta = new Response<PedOutputDTO>();
            try
            {
                Pedido pedido = await _pedidoRepository.PedidoPorId(id);
                if (pedido == null)
                {
                    resposta.Mensagem = "Pedido não encontrada.";
                    resposta.Status = false;
                    return resposta;
                }

                await PreencherPedido(pedido);
                resposta.Dados = PedidoMap.MapPedido(pedido);
                resposta.Mensagem = "Pedido encontrado com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<PedOutputDTO>> CriarPedido(PedCriacaoDTO pedido)
        {
            Response<PedOutputDTO> resposta = new Response<PedOutputDTO>();
            try
            {
                await ValidarMesa(pedido.MesaId);
                await ValidarUsuario(pedido.UsuarioId);

                Pedido map = PedidoMap.MapPedido(pedido);
                Pedido retorno = await _pedidoRepository.CriarPedido(map);
                await PreencherPedido(retorno);

                resposta.Dados = PedidoMap.MapPedido(retorno);
                resposta.Mensagem = "Pedido criado com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<PedOutputDTO>> AtualizarPedido(PedEdicaoDTO pedido)
        {
            Response<PedOutputDTO> resposta = new Response<PedOutputDTO>();
            try
            {
                ValidarPedidoEdicao(pedido);
                await ValidarUsuario(pedido.UsuarioId);
                await ValidarMesa(pedido.MesaId);
                Pedido buscarPedido = await _pedidoRepository.PedidoPorId(pedido.Id);
                if(buscarPedido == null)
                {
                    resposta.Mensagem = "Pedido não encontrado para atualização";
                    resposta.Status = false;
                    return resposta;
                }
                AtualizarDadosPedido(buscarPedido, pedido);
                Pedido retorno = await _pedidoRepository.AtualizarPedido(buscarPedido);
                await PreencherPedido(retorno);

                resposta.Dados = PedidoMap.MapPedido(retorno);
                resposta.Mensagem = "Pedido atualizado com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<PedOutputDTO>> DeletarPedido(Guid id)
        {
            Response<PedOutputDTO> resposta = new Response<PedOutputDTO>();
            try
            {
                Pedido pedido = await _pedidoRepository.PedidoPorId(id);
                if(pedido == null)
                {
                    resposta.Mensagem ="Pedido não encontrado para deleção";
                    resposta.Status = false;
                    return resposta;
                }
                Pedido retorno = await _pedidoRepository.DeletarPedido(id);

                resposta.Dados = PedidoMap.MapPedido(retorno);
                resposta.Mensagem = "Pedido deletado com sucesso";
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
        private async Task PreencherPedidos(IEnumerable<Pedido> pedidos)
        {
            foreach (var pedido in pedidos)
            {
                if (pedido.UsuarioId != null)
                {
                    pedido.Usuario = await _usuarioRepository.UsuarioPorId((Guid)pedido.UsuarioId);
                }
                if (pedido.MesaId != null)
                {
                    pedido.Mesa = await _mesaRepository.MesaPorId((Guid)pedido.MesaId);
                }
            }
        }

        private async Task PreencherPedido(Pedido pedido)
        {
            if (pedido.UsuarioId != null)
            {
                pedido.Usuario = await _usuarioRepository.UsuarioPorId((Guid)pedido.UsuarioId);
            }
            if (pedido.MesaId != null)
            {
                pedido.Mesa = await _mesaRepository.MesaPorId((Guid)pedido.MesaId);
            }
        }

        private async Task ValidarUsuario(Guid? usuarioId)
        {
            if (usuarioId == null || usuarioId == Guid.Empty)
            {
                throw new Exception("Usuario é obrigatório");
            }

            var usuario = await _usuarioRepository.UsuarioPorId((Guid)usuarioId);

            if (usuario == null)
            {
                throw new Exception("Usuario não encontrado");
            }
        }

        private async Task ValidarMesa(Guid? mesaId)
        {
            if (mesaId == null || mesaId == Guid.Empty)
            {
                throw new Exception("Mesa é obrigatório");
            }

            var mesa = await _mesaRepository.MesaPorId((Guid)mesaId);

            if (mesa == null)
            {
                throw new Exception("Mesa não encontrada");
            }
        }

        private void ValidarPedidoEdicao(PedEdicaoDTO pedido)
        {
            ValidadorUtils.ValidarDecimalSeVazio(pedido.PedValor, "Valor é obrigatório");
            ValidadorUtils.ValidarMaximo(pedido.PedValor, 9999999, "Valor deve ser menor que 9999999");
            ValidadorUtils.ValidarMinimo(pedido.PedValor, 0, "Valor deve ser maior que 0");

            if (!Enum.IsDefined(typeof(StatusPedido), pedido.PedStatus))
            {
                throw new Exception("Status do pedido é obrigatório");
            }
            if (!Enum.IsDefined(typeof(TipoPagPedido), pedido.PedTipoPag))
            {
                throw new Exception("Tipo de pagamento é obrigatório");
            }
        }

        private void AtualizarDadosPedido(Pedido buscarPedido, PedEdicaoDTO pedido)
        {
            buscarPedido.PedValor = pedido.PedValor;
            buscarPedido.PedStatus = pedido.PedStatus;
            buscarPedido.PedTipoPag = pedido.PedTipoPag;
            buscarPedido.UsuarioId = pedido.UsuarioId;
            buscarPedido.MesaId = pedido.MesaId;
        }
    }
}
