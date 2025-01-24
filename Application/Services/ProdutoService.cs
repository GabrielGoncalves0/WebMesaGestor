using WebMesaGestor.Application.DTO.Input.Produto;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.Map;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Utils;

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
                if (produtos == null)
                {
                    resposta.Mensagem = "Produtos não encontrado.";
                    resposta.Status = false;
                    return resposta;
                }
                await PreencherProdutos(produtos);
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
                if (produto == null)
                {
                    resposta.Mensagem = "Produto não encontrada.";
                    resposta.Status = false;
                    return resposta;
                }

                await PreencherProduto(produto);
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
                ValidarProdutoCriacao(produto);
                await ValidarCategoria(produto.CategoriaId);
                await ValidarSetor(produto.SetorId);

                Produto map = ProdutoMap.MapProduto(produto);
                Produto retorno = await _produtoRepository.CriarProduto(map);
                await PreencherProduto(retorno);

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
                ValidarProdutoEdicao(produto);
                await ValidarCategoria(produto.CategoriaId);
                await ValidarSetor(produto.SetorId);
                Produto buscarProduto = await _produtoRepository.ProdutoPorId(produto.Id);
                if (buscarProduto == null)
                {
                    resposta.Mensagem = "Produto não encontrado.";
                    resposta.Status = false;
                    return resposta;
                }
                
                AtualizarDadosProduto(buscarProduto, produto);
                Produto retorno = await _produtoRepository.AtualizarProduto(buscarProduto);
                await PreencherProduto(retorno);

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
                Produto produto = await _produtoRepository.ProdutoPorId(id);
                if(produto == null)
                {
                    resposta.Mensagem = "Produto não encontrado para delação.";
                    resposta.Status = false;
                    return resposta;
                }
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

        // Métodos auxiliares
        private async Task PreencherProdutos(IEnumerable<Produto> produtos)
        {
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
        }

        private async Task PreencherProduto(Produto produto)
        {
            produto.Categoria = await _categoriaRepository.CategoriaPorId((Guid)produto.CategoriaId);
            produto.Setor = await _setorRepository.SetorPorId((Guid)produto.SetorId);
        }

        private async Task ValidarSetor(Guid? setorId)
        {
            if (setorId == null || setorId == Guid.Empty)
            {
                throw new Exception("Setor é obrigatório");
            }

            var setor = await _setorRepository.SetorPorId((Guid)setorId);

            if (setor == null)
            {
                throw new Exception("Setor não encontrado");
            }
        }

        private async Task ValidarCategoria(Guid? categoriaId)
        {
            if (categoriaId == null || categoriaId == Guid.Empty)
            {
                throw new Exception("Categoria é obrigatório");
            }

            var categoria = await _categoriaRepository.CategoriaPorId((Guid)categoriaId);

            if (categoria == null)
            {
                throw new Exception("Categoria não encontrada");
            }
        }

        private void ValidarProdutoCriacao(ProCriacaoDTO produto)
        {
            ValidadorUtils.ValidarInteiroSeVazioOuNulo(produto.ProCodigo, "Código é obrigatório");
            ValidadorUtils.ValidarMaximo(produto.ProCodigo, 6, "Código deve conter no máximo 6 caracteres");
            ValidadorUtils.ValidarMinimo(produto.ProCodigo, 1, "Código deve conter no minimo 1 caracteres");

            ValidadorUtils.ValidarSeVazioOuNulo(produto.ProDescricao, "Descrição é obrigatório");
            ValidadorUtils.ValidarMaximo(produto.ProDescricao, 100, "Descrição deve conter no máximo 100 caracteres");
            ValidadorUtils.ValidarMinimo(produto.ProDescricao, 3, "Descrição deve conter no minimo 3 caracteres");
            
            ValidadorUtils.ValidarSeVazioOuNulo(produto.ProUnidade, "Unidade é obrigatório");
            ValidadorUtils.ValidarMaximo(produto.ProUnidade, 30, "Unidade deve conter no máximo 30 caracteres");
            ValidadorUtils.ValidarMinimo(produto.ProUnidade, 2, "Unidade deve conter no minimo 2 caracteres");

            ValidadorUtils.ValidarDecimalSeVazio(produto.ProPreco, "Valor é obrigatório");
            ValidadorUtils.ValidarMaximo(produto.ProPreco, 9999999, "Valor deve ser menor que 9999999");
            ValidadorUtils.ValidarMinimo(produto.ProPreco, 0, "Valor deve ser maior que 0");
        }

        private void ValidarProdutoEdicao(ProEdicaoDTO produto)
        {
            ValidadorUtils.ValidarInteiroSeVazioOuNulo(produto.ProCodigo, "Código é obrigatório");
            ValidadorUtils.ValidarMaximo(produto.ProCodigo, 6, "Código deve conter no máximo 6 caracteres");
            ValidadorUtils.ValidarMinimo(produto.ProCodigo, 1, "Código deve conter no minimo 1 caracteres");

            ValidadorUtils.ValidarSeVazioOuNulo(produto.ProDescricao, "Descrição é obrigatório");
            ValidadorUtils.ValidarMaximo(produto.ProDescricao, 100, "Descrição deve conter no máximo 100 caracteres");
            ValidadorUtils.ValidarMinimo(produto.ProDescricao, 3, "Descrição deve conter no minimo 3 caracteres");

            ValidadorUtils.ValidarSeVazioOuNulo(produto.ProUnidade, "Unidade é obrigatório");
            ValidadorUtils.ValidarMaximo(produto.ProUnidade, 30, "Unidade deve conter no máximo 30 caracteres");
            ValidadorUtils.ValidarMinimo(produto.ProUnidade, 2, "Unidade deve conter no minimo 2 caracteres");

            ValidadorUtils.ValidarDecimalSeVazio(produto.ProPreco, "Valor é obrigatório");
            ValidadorUtils.ValidarMaximo(produto.ProPreco, 9999999, "Valor deve ser menor que 9999999");
            ValidadorUtils.ValidarMinimo(produto.ProPreco, 0, "Valor deve ser maior que 0");
        }

        private void AtualizarDadosProduto(Produto produtoExistente, ProEdicaoDTO produto)
        {
            produtoExistente.ProCodigo = produto.ProCodigo;
            produtoExistente.ProDescricao = produto.ProDescricao;
            produtoExistente.ProUnidade = produto.ProUnidade;
            produtoExistente.ProPreco = produto.ProPreco;
            produtoExistente.CategoriaId = produto.CategoriaId;
            produtoExistente.SetorId = produto.SetorId;
        }
    }
}
