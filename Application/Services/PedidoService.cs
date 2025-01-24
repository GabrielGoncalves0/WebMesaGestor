using WebMesaGestor.Application.DTO.Input.Pedido;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.Map;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;

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
                if (pedido.UsuarioId != null)
                {
                    pedido.Usuario = await _usuarioRepository.UsuarioPorId((Guid)pedido.UsuarioId);
                }
                if (pedido.MesaId != null)
                {
                    pedido.Mesa = await _mesaRepository.MesaPorId((Guid)pedido.MesaId);
                }
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
                Pedido map = PedidoMap.MapPedido(pedido);
                Pedido retorno = await _pedidoRepository.CriarPedido(map);

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
                Pedido buscarPedido = await _pedidoRepository.PedidoPorId(pedido.Id);
                buscarPedido.PedValor = pedido.PedValor;
                buscarPedido.PedStatus = pedido.PedStatus;
                buscarPedido.PedTipoPag = pedido.PedTipoPag;
                buscarPedido.UsuarioId = pedido.UsuarioId;
                buscarPedido.MesaId = pedido.MesaId;
                Pedido retorno = await _pedidoRepository.AtualizarPedido(buscarPedido);

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
    }
}
