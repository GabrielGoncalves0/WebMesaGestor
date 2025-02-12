using Microsoft.EntityFrameworkCore;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Infra.Data;

namespace WebMesaGestor.Infra.Repositories
{
    public class ProPedRepository : IProPedRepository
    {
        private readonly AppDbContext _context;

        public ProPedRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<IEnumerable<ProdutoPedido>> ObterProdutosPorPedId(Guid id)
        {
            return await _context.ProdutoPedido
                .Include(pp => pp.Pedido)
                .Include(pp => pp.Produto)
                .Where(pp => pp.PedidoId == id)
                .ToListAsync();
        }


        public async Task<ProdutoPedido> ObterProPedId(Guid id)
        {
            return await _context.ProdutoPedido
                .Include(pp => pp.Pedido)
                .Include(pp => pp.Produto)
                .FirstOrDefaultAsync(pp => pp.Id == id);
        }

        public async Task<ProdutoPedido> CriarProPed(ProdutoPedido produtoPedido)
        {
            _context.ProdutoPedido.Add(produtoPedido);
            await _context.SaveChangesAsync();
            return produtoPedido;
        }

        public async Task<ProdutoPedido> AtualizarProPed(ProdutoPedido produtoPedido)
        {
            _context.ProdutoPedido.Update(produtoPedido);
            await _context.SaveChangesAsync();
            return produtoPedido;
        }

        public async Task<bool> DeletarProPed(Guid id)
        {
            var produtoPed = await _context.ProdutoPedido.FindAsync(id);
            if (produtoPed == null) {
                return false;
            }
            _context.ProdutoPedido.Remove(produtoPed);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
