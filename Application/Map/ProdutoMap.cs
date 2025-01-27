using WebMesaGestor.Application.DTO.Input.Produto;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.Map
{
    public static class ProdutoMap
    {
        public static ProOutputDTO MapProduto(this Produto produto)
        {
            return new ProOutputDTO
            {
                Id = produto.Id,
                ProCodigo = produto.ProCodigo,
                ProDescricao = produto.ProDescricao,
                ProUnidade = produto.ProUnidade,
                ProPreco = produto.ProPreco,
                CriacaoData = produto.CriacaoData,
                Categoria = produto.Categoria != null ? CategoriaMap.MapCategoria(produto.Categoria) : null,
                Setor = produto.Setor != null ? SetorMap.MapSetor(produto.Setor) : null
            };
        }

        public static IEnumerable<ProOutputDTO> MapProduto(this IEnumerable<Produto> produto)
        {
            return produto.Select(x => x.MapProduto()).ToList();
        }

        public static Produto MapProduto(this ProCriacaoDTO produto)
        {
            return new Produto
            {
                Id = Guid.NewGuid(),
                ProCodigo = produto.ProCodigo,
                ProDescricao = produto.ProDescricao,
                ProUnidade = produto.ProUnidade,
                ProPreco = produto.ProPreco,
                CategoriaId = produto.CategoriaId,
                SetorId =  produto.SetorId,
                CriacaoData = DateTime.UtcNow,
            };
        }

        public static IEnumerable<Produto> MapProduto(this IEnumerable<ProCriacaoDTO> produto)
        {
            return produto.Select(x => x.MapProduto()).ToList();
        }

        public static Produto MapProduto(this ProEdicaoDTO produto)
        {
            return new Produto
            {
                ProCodigo = produto.ProCodigo,
                ProDescricao = produto.ProDescricao,
                ProUnidade = produto.ProUnidade,
                ProPreco = produto.ProPreco,
                CategoriaId = produto.CategoriaId,
                SetorId = produto.SetorId,
            };
        }

        public static IEnumerable<Produto> MapProduto(this IEnumerable<ProEdicaoDTO> produto)
        {
            return produto.Select(x => x.MapProduto()).ToList();
        }
    }
}