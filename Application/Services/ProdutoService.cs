using WebMesaGestor.Application.DTO.Input.Produto;
using WebMesaGestor.Application.DTO.Input.USuario;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.Map;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;

namespace WebMesaGestor.Application.Services
{
    public class ProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ISetorRepository _setorRepository;

        public ProdutoService(IProdutoRepository produtoRepository, ICategoriaRepository categoriaRepository, ISetorRepository setorRepository)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
            _setorRepository = setorRepository;
        }

        public async Task<IEnumerable<ProOutputDTO>> ListarProdutos()
        {
            IEnumerable<Produto> produtos = await _produtoRepository.ListarProdutos();
            foreach (var produto in produtos)
            {
                if (produto.CategoriaId != null)
                {
                    produto.Categoria = await _categoriaRepository.CategoriaPorId((Guid)produto.CategoriaId);
                }
                if (produto.SetorId != null)
                {
                    produto.Setor = await _setorRepository.SetorPorId((Guid)produto.SetorId);
                }
            }
            return ProdutoMap.MapProduto(produtos);
        }

        public async Task<ProOutputDTO> ProdutoPorId(Guid id)
        {
            Produto produto = await _produtoRepository.ProdutoPorId(id);
            if (produto.CategoriaId != null)
            {
                produto.Categoria = await _categoriaRepository.CategoriaPorId((Guid)produto.CategoriaId);
            }
            if (produto.SetorId != null)
            {
                produto.Setor = await _setorRepository.SetorPorId((Guid)produto.SetorId);
            }
            return ProdutoMap.MapProduto(produto);
        }

        public async Task<ProOutputDTO> CriarProduto(ProCriacaoDTO produto)
        {
            Produto map = ProdutoMap.MapProduto(produto);
            Produto retorno = await _produtoRepository.CriarProduto(map);
            return ProdutoMap.MapProduto(retorno);
        }

        public async Task<ProOutputDTO> AtualizarProduto(ProEdicaoDTO produto)
        {
            Produto buscarProduto = await _produtoRepository.ProdutoPorId(produto.Id);

            buscarProduto.ProCodigo = produto.ProCodigo;
            buscarProduto.ProDescricao = produto.ProDescricao;
            buscarProduto.ProUnidade = produto.ProUnidade;
            buscarProduto.ProPreco = produto.ProPreco;
            buscarProduto.CategoriaId = produto.CategoriaId;
            buscarProduto.SetorId = produto.SetorId;

            Produto retorno = await _produtoRepository.AtualizarProduto(buscarProduto);
            return ProdutoMap.MapProduto(retorno);
        }

        public async Task<ProOutputDTO> DeletarProduto(Guid id)
        {
            Produto retorno = await _produtoRepository.DeletarProduto(id);
            return ProdutoMap.MapProduto(retorno);
        }
    }
}
