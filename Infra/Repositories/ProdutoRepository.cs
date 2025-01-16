using Microsoft.EntityFrameworkCore;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Infra.Data;

namespace WebMesaGestor.Infra.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProdutoRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Produto>> ListarProdutos()
        {
            return await _appDbContext.Produtos.ToListAsync();
        }

        public async Task<Produto> ProdutoPorId(Guid id)
        {
            return await _appDbContext.Produtos.FirstOrDefaultAsync(u => u.Id == new Guid(id.ToString()));
        }

        public async Task<Produto> CriarProduto(Produto produto)
        {
            try
            {
                await _appDbContext.Produtos.AddAsync(produto);
                await _appDbContext.SaveChangesAsync();
                return produto;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }
        }

        public async Task<Produto> AtualizarProduto(Produto produto)
        {
            _appDbContext.Produtos.Update(produto);
            await _appDbContext.SaveChangesAsync();
            return produto;
        }

        public async Task<Produto> DeletarProduto(Guid id)
        {
            Produto produto = await ProdutoPorId(id);
            _appDbContext.Produtos.Remove(produto);
            await _appDbContext.SaveChangesAsync();
            return produto;
        }
    }
}
