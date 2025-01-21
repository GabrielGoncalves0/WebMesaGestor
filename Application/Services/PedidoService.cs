using WebMesaGestor.Application.DTO.Input.Pedido;
using WebMesaGestor.Application.DTO.Input.USuario;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.Map;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Infra.Repositories;

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

        public async Task<PedOutputDTO> PedidoPorId(Guid id)
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
            return PedidoMap.MapPedido(pedido);
        }

        public async Task<PedOutputDTO> CriarPedido(PedCriacaoDTO pedido)
        {
            Pedido map = PedidoMap.MapPedido(pedido);
            Pedido retorno = await _pedidoRepository.CriarPedido(map);
            return PedidoMap.MapPedido(retorno);
        }

        public async Task<PedOutputDTO> AtualizarPedido(PedEdicaoDTO pedido)
        {
            Pedido buscarPedido = await _pedidoRepository.PedidoPorId(pedido.Id);

            buscarPedido.PedValor = pedido.PedValor;
            buscarPedido.PedStatus = pedido.PedStatus;
            buscarPedido.PedTipoPag = pedido.PedTipoPag;
            buscarPedido.UsuarioId = pedido.UsuarioId;
            buscarPedido.MesaId = pedido.MesaId;

            Pedido retorno = await _pedidoRepository.AtualizarPedido(buscarPedido);
            return PedidoMap.MapPedido(retorno);
        }

        public async Task<PedOutputDTO> DeletarPedido(Guid id)
        {
            Pedido retorno = await _pedidoRepository.DeletarPedido(id);
            return PedidoMap.MapPedido(retorno);
        }
    }
}
