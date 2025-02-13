using AutoMapper;
using WebMesaGestor.Application.Common;
using WebMesaGestor.Application.DTO.Input.Produto;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.Validations.Produto;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Infra.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebMesaGestor.Application.Services
{
    public class ProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ISetorRepository _setorRepository;
        private readonly IMapper _mapper;

        public ProdutoService(IProdutoRepository produtoRepository, ICategoriaRepository categoriaRepository, 
            ISetorRepository setorRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
            _setorRepository = setorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProdutoDTO>> ObterTodosProdutos()
        {
            var produtos = await _produtoRepository.ObterTodosProdutos();
            return _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
        }

        public async Task<ProdutoDTO> ObterProdutoPorId(Guid id)
        {
            var produto = await _produtoRepository.ObterProdutoPorId(id);

            if (produto == null)
            {
                return null;
            }

            return _mapper.Map<ProdutoDTO>(produto);
        }

        public async Task<Response<ProdutoDTO>> CriarProduto(ProCriacaoDTO produtoDTO)
        {
            var validationResult = new ProdutoCriacaoValidator().Validate(produtoDTO);
            if(!validationResult.IsValid)
            {
                return new Response<ProdutoDTO>
                {
                    Sucesso = false,
                    Erros = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var categoria = await _categoriaRepository.ObterCategoriaPorId(produtoDTO.CategoriaId);
            if (categoria == null)
            {
                return new Response<ProdutoDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { CategoriaMessages.CategoriaNaoEncontrada }
                };
            }

            var setor = await _setorRepository.ObterSetorPorId(produtoDTO.SetorId);
            if (setor == null)
            {
                return new Response<ProdutoDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { SetorMessages.SetorNaoEncontrado }
                };
            }

            var produto = _mapper.Map<Produto>(produtoDTO);
            produto.Categoria = categoria;
            produto.Setor = setor;

            await _produtoRepository.CriarProduto(produto);
            return new Response<ProdutoDTO>
            {
                Sucesso = true,
                Id = produto.Id,
                Data = _mapper.Map<ProdutoDTO>(produto),
            };
        }

        public async Task<Response<ProdutoDTO>> AtualizarProduto(ProEdicaoDTO produtoDTO)
        {
            var validationResult = new ProdutoEdicaoValidator().Validate(produtoDTO);
            if (!validationResult.IsValid)
            {
                return new Response<ProdutoDTO>
                {
                    Sucesso = false,
                    Erros = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var produtoExistente = await _produtoRepository.ObterProdutoPorId(produtoDTO.Id);
            if (produtoExistente == null)
            {
                return new Response<ProdutoDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { ProdutoMessages.ProdutoNaoEncontrado }
                };
            }

            var categoria = await _categoriaRepository.ObterCategoriaPorId(produtoDTO.CategoriaId);
            if (categoria == null)
            {
                return new Response<ProdutoDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { CategoriaMessages.CategoriaNaoEncontrada }
                };
            }

            var setor = await _setorRepository.ObterSetorPorId(produtoDTO.SetorId);
            if (setor == null)
            {
                return new Response<ProdutoDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { SetorMessages.SetorNaoEncontrado }
                };
            }

            var produto = _mapper.Map(produtoDTO, produtoExistente);

            await _produtoRepository.AtualizarProduto(produto);

            return new Response<ProdutoDTO>
            {
                Sucesso = true,
                Id = produto.Id,
                Data = _mapper.Map<ProdutoDTO>(produto)
            };
        }

        public async Task<Response<ProdutoDTO>> DeletarProduto(Guid id)
        {
            var produtoExistente = await _produtoRepository.ObterProdutoPorId(id);
            if (produtoExistente == null)
            {
                return new Response<ProdutoDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { ProdutoMessages.ProdutoNaoEncontrado }
                };
            }

            var sucessoDelecao = await _produtoRepository.DeletarProduto(id);
            if(!sucessoDelecao)
            {
                return new Response<ProdutoDTO>()
                {
                    Sucesso = false,
                    Erros = new List<string> { ProdutoMessages.ErroAoDeletarProduto }
                };
            }

            var dados = _mapper.Map<ProdutoDTO>(produtoExistente);
            return new Response<ProdutoDTO>
            {
                Sucesso = true,
                Data = dados,
                Erros = new List<string> { ProdutoMessages.ProdutoDeletadoComSucesso }
            };
        }
    }
}
