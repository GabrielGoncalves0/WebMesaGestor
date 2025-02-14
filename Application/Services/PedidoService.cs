using AutoMapper;
using WebMesaGestor.Application.Common;
using WebMesaGestor.Application.DTO.Input.Pedido;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.Validations.Pedido;
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
        private readonly IMapper _mapper;

        public PedidoService(IPedidoRepository pedidoRepository, IUsuarioRepository usuarioRepository, IMesaRepository mesaRepository, IMapper mapper)
        {
            _pedidoRepository = pedidoRepository;
            _usuarioRepository = usuarioRepository;
            _mesaRepository = mesaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PedidoDTO>> ObterTodosPedidos()
        {
            var pedido = await _pedidoRepository.ObterTodosPedidos();
            return _mapper.Map<IEnumerable<PedidoDTO>>(pedido);
        }

        public async Task<PedidoDTO> ObterPedidoPorId(Guid id)
        {
            var pedido = await _pedidoRepository.ObterPedidoPorId(id);
            return _mapper.Map<PedidoDTO>(pedido);
        }

        public async Task<Response<PedidoDTO>> CriarPedido(PedCriacaoDTO pedidoDTO)
        {
            var validationResult = new PedidoCriacaoValidator().Validate(pedidoDTO);
            if (!validationResult.IsValid)
            {
                return new Response<PedidoDTO>
                {
                    Sucesso = false,
                    Erros = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }
            var usuario = await _usuarioRepository.ObterUsuarioPorId(pedidoDTO.UsuarioId);
            if (usuario == null)
            {
                return new Response<PedidoDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { UsuarioMessages.UsuarioNaoEncontrado }
                };
            }
            var mesa = await _mesaRepository.ObterMesaPorId(pedidoDTO.MesaId);
            if (mesa == null)
            {
                return new Response<PedidoDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { MesaMessages.MesaNaoEncontrada }
                };
            }
            var pedido = _mapper.Map<Pedido>(pedidoDTO);
            pedido.Usuario = usuario;
            pedido.Mesa = mesa;
            await _pedidoRepository.CriarPedido(pedido);
            return new Response<PedidoDTO>
            {
                Sucesso = true,
                Id = pedido.Id,
                Data = _mapper.Map<PedidoDTO>(pedido)
            };
        }

        public async Task<Response<PedidoDTO>> AtualizarPedido(PedEdicaoDTO pedidoDTO)
        {
            var validationResult = new PedidoEdicaoValidator().Validate(pedidoDTO);
            if (!validationResult.IsValid)
            {
                return new Response<PedidoDTO>
                {
                    Sucesso = false,
                    Erros = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var pedidoExistente = await _pedidoRepository.ObterPedidoPorId(pedidoDTO.Id);
            if (pedidoExistente == null)
            {
                return new Response<PedidoDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { PedidoMessages.PedidoNaoEncontrado }
                };
            }

            var usuario = await _usuarioRepository.ObterUsuarioPorId(pedidoDTO.UsuarioId);
            if (usuario == null)
            {
                return new Response<PedidoDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { UsuarioMessages.UsuarioNaoEncontrado }
                };
            }

            var mesa = await _mesaRepository.ObterMesaPorId(pedidoDTO.MesaId);
            if (mesa == null)
            {
                return new Response<PedidoDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { MesaMessages.MesaNaoEncontrada }
                };
            }

            var pedido = _mapper.Map(pedidoDTO, pedidoExistente);
            await _pedidoRepository.AtualizarPedido(pedido);
            return new Response<PedidoDTO>
            {
                Sucesso = true,
                Id = pedido.Id,
                Data = _mapper.Map<PedidoDTO>(pedido)
            };
        }

        public async Task<Response<PedidoDTO>> DeletarPedido(Guid id)
        {
            var pedido = _pedidoRepository.ObterPedidoPorId(id);
            if (pedido == null)
            {
                return new Response<PedidoDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { PedidoMessages.PedidoNaoEncontrado }
                };
            }

            var sucessoDelecao = await _pedidoRepository.DeletarPedido(id);
            if (!sucessoDelecao)
            {
                return new Response<PedidoDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { PedidoMessages.ErroAoDeletarPedido }
                };

            }
            var dados = _mapper.Map<PedidoDTO>(pedido);
            return new Response<PedidoDTO>
            {
                Sucesso = true,
                Data = dados,
                Erros = new List<string> { PedidoMessages.PedidoDeletadoComSucesso }
            };
        }
    }
}
