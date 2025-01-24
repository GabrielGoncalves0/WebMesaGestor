using WebMesaGestor.Application.DTO.Input.Produto;
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

        public async Task<Response<IEnumerable<ProOutputDTO>>> ListarProdutos()
        {
            Response<IEnumerable<ProOutputDTO>> resposta = new Response<IEnumerable<ProOutputDTO>>();
            try
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
                resposta.Dados = ProdutoMap.MapProduto(produtos);
                resposta.Mensagem = "Produtos listados com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<ProOutputDTO>> ProdutoPorId(Guid id)
        {
            Response<ProOutputDTO> resposta = new Response<ProOutputDTO>();
            try
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
                resposta.Dados = ProdutoMap.MapProduto(produto);
                resposta.Mensagem = "Produto encontrado com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<ProOutputDTO>> CriarProduto(ProCriacaoDTO produto)
        {
            Response<ProOutputDTO> resposta = new Response<ProOutputDTO>();
            try
            {
                Produto map = ProdutoMap.MapProduto(produto);
                Produto retorno = await _produtoRepository.CriarProduto(map);

                resposta.Dados = ProdutoMap.MapProduto(retorno);
                resposta.Mensagem = "Produto criado com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<ProOutputDTO>> AtualizarProduto(ProEdicaoDTO produto)
        {
            Response<ProOutputDTO> resposta = new Response<ProOutputDTO>();
            try
            {
                Produto buscarProduto = await _produtoRepository.ProdutoPorId(produto.Id);

                buscarProduto.ProCodigo = produto.ProCodigo;
                buscarProduto.ProDescricao = produto.ProDescricao;
                buscarProduto.ProUnidade = produto.ProUnidade;
                buscarProduto.ProPreco = produto.ProPreco;
                buscarProduto.CategoriaId = produto.CategoriaId;
                buscarProduto.SetorId = produto.SetorId;

                Produto retorno = await _produtoRepository.AtualizarProduto(buscarProduto);

                resposta.Dados = ProdutoMap.MapProduto(retorno);
                resposta.Mensagem = "Produto atualizado com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<ProOutputDTO>> DeletarProduto(Guid id)
        {
            Response<ProOutputDTO> resposta = new Response<ProOutputDTO>();
            try
            {
                Produto retorno = await _produtoRepository.DeletarProduto(id);
                resposta.Dados = ProdutoMap.MapProduto(retorno);
                resposta.Mensagem = "Produto deletado com sucesso";
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
