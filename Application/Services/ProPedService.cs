using AutoMapper;
using WebMesaGestor.Application.Common;
using WebMesaGestor.Application.DTO.Input.ProdutoPedido;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.Validations.ProdutoPedido;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Infra.Repositories;

namespace WebMesaGestor.Application.Services
{
    public class ProPedService
    {
        private readonly IProPedRepository _produtoPedidoRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapper _mapper;

        public ProPedService(IProPedRepository produtoPedidoRepository, IProdutoRepository produtoRepository,
            IPedidoRepository pedidoRepository, IMapper mapper)
        {
            _produtoPedidoRepository = produtoPedidoRepository;
            _produtoRepository = produtoRepository;
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProdutoPedidoDTO>> ObterProdutosPorPedId(Guid id)
        {
            var produtoPedido = await _produtoPedidoRepository.ObterTodosProdutosPorPedId(id);
            return _mapper.Map<IEnumerable<ProdutoPedidoDTO>>(produtoPedido);
        }

        public async Task<ProdutoPedidoDTO> ObterProPedId(Guid id)
        {
            var produtoPedido = await _produtoPedidoRepository.ObterProPedId(id);
            return _mapper.Map<ProdutoPedidoDTO>(produtoPedido);
        }

        public async Task<Response<ProdutoPedidoDTO>> CriarProPed(ProPedCriacaoDTO proPedCriacaoDTO)
        {
            var validationResult = new ProdutoPedCriacaoValidator().Validate(proPedCriacaoDTO);
            if (!validationResult.IsValid)
            {
                return new Response<ProdutoPedidoDTO>()
                {
                    Sucesso = false,
                    Erros = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var produto = await _produtoRepository.ObterProdutoPorId(proPedCriacaoDTO.ProdutoId);
            if (produto == null)
            {
                return new Response<ProdutoPedidoDTO>()
                {
                    Sucesso = false,
                    Erros = new List<string> { ProdutoMessages.ProdutoNaoEncontrado }
                };
            }

            var pedido = await _pedidoRepository.ObterPedidoPorId(proPedCriacaoDTO.PedidoId);
            if (pedido == null)
            {
                return new Response<ProdutoPedidoDTO>()
                {
                    Sucesso = false,
                    Erros = new List<string> { PedidoMessages.PedidoNaoEncontrado }
                };
            }

            var produtoPedido = _mapper.Map<ProdutoPedido>(proPedCriacaoDTO);
            produtoPedido.Pedido = pedido;
            produtoPedido.Produto = produto;

            await _produtoPedidoRepository.CriarProPed(produtoPedido);
            return new Response<ProdutoPedidoDTO>()
            {
                Sucesso = true,
                Id = produtoPedido.Id,
                Data = _mapper.Map<ProdutoPedidoDTO>(produtoPedido)
            };
        }

        public async Task<Response<ProdutoPedidoDTO>> AtualizarProPed(ProPedEdicaoDTO proPedEdicaoDTO)
        {
            var validationResult = new ProdutoPedEdicaoValidator().Validate(proPedEdicaoDTO);
            if (!validationResult.IsValid)
            {
                return new Response<ProdutoPedidoDTO>()
                {
                    Sucesso = false,
                    Erros = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var produtoPedidoExiste = await _produtoPedidoRepository.ObterProPedId(proPedEdicaoDTO.Id);
            if (produtoPedidoExiste == null)
            {
                return new Response<ProdutoPedidoDTO>()
                {
                    Sucesso = false,
                    Erros = new List<string> { ProdutoPedidoMessages.ProdutoPedidoNaoEncontrado }
                };
            }

            var produtoPedido = _mapper.Map(proPedEdicaoDTO, produtoPedidoExiste);
            await _produtoPedidoRepository.AtualizarProPed(produtoPedido);

            return new Response<ProdutoPedidoDTO>()
            {
                Sucesso = true,
                Id = produtoPedido.Id,
                Data = _mapper.Map<ProdutoPedidoDTO>(produtoPedido)
            };
        }

        public async Task<Response<ProdutoPedidoDTO>> DeletarProPed(Guid id)
        {
            var produtoPedidoExiste = await _produtoPedidoRepository.ObterProPedId(id);
            if(produtoPedidoExiste == null)
            {
                return new Response<ProdutoPedidoDTO>()
                {
                    Sucesso = false,
                    Erros = new List<string> { ProdutoPedidoMessages.ProdutoPedidoNaoEncontrado }
                };
            }

            var sucessoDelecao = await _produtoPedidoRepository.DeletarProPed(id);
            if(!sucessoDelecao)
            {
                return new Response<ProdutoPedidoDTO>()
                {
                    Sucesso = false,
                    Erros = new List<string> { ProdutoPedidoMessages.ErroAoDeletarProdutosPedido }
                };
            }

            var dados =  _mapper.Map<ProdutoPedidoDTO>(produtoPedidoExiste);
            return new Response<ProdutoPedidoDTO>()
            {
                Sucesso = true,
                Data = dados,
                Erros = new List<string> {  ProdutoMessages.ProdutoDeletadoComSucesso} 
            };
        }
    }
}
