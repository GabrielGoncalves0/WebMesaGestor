using Microsoft.EntityFrameworkCore;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Infra.Data;

namespace WebMesaGestor.Infra.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<IEnumerable<Produto>> ObterTodosProdutos()
        {
            return await _context.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.Setor)
                .ToListAsync();
        }

        public async Task<Produto> ObterProdutoPorId(Guid id)
        {
            return await _context.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.Setor)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Produto> CriarProduto(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        public async Task<Produto> AtualizarProduto(Produto produto)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        public async Task<bool> DeletarProduto(Guid id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return false;
            }
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
