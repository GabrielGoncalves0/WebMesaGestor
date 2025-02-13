using Microsoft.EntityFrameworkCore;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Infra.Data;

namespace WebMesaGestor.Infra.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;

        public PedidoRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<IEnumerable<Pedido>> ObterTodosPedidos()
        {
            return await _context.Pedidos
                .Include(p => p.Usuario)
                .Include(p => p.Mesa)
                .ToListAsync();
        }

        public async Task<Pedido> ObterPedidoPorId(Guid id)
        {
            return await _context.Pedidos
                .Include(p => p.Usuario)
                .Include(p => p.Mesa)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Pedido> CriarPedido(Pedido pedido)
        {
            _context.Pedidos.AddAsync(pedido);
            await _context.SaveChangesAsync();
            return pedido;
        }

        public async Task<Pedido> AtualizarPedido(Pedido pedido)
        {
            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();
            return pedido;
        }

        public async Task<bool> DeletarPedido(Guid id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return false;
            }
            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
