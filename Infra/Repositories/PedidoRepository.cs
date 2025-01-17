using Microsoft.EntityFrameworkCore;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Infra.Data;

namespace WebMesaGestor.Infra.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _appDbContext;

        public PedidoRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Pedido>> ListarPedidos()
        {
            return await _appDbContext.Pedidos.ToListAsync();
        }

        public async Task<Pedido> PedidoPorId(Guid id)
        {
            return await _appDbContext.Pedidos.FirstOrDefaultAsync(u => u.Id == new Guid(id.ToString()));
        }

        public async Task<Pedido> CriarPedido(Pedido pedido)
        {
            try
            {
                await _appDbContext.Pedidos.AddAsync(pedido);
                await _appDbContext.SaveChangesAsync();
                return pedido;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }
        }

        public async Task<Pedido> AtualizarPedido(Pedido pedido)
        {
            _appDbContext.Pedidos.Update(pedido);
            await _appDbContext.SaveChangesAsync();
            return pedido;
        }

        public async Task<Pedido> DeletarPedido(Guid id)
        {
            Pedido pedido = await PedidoPorId(id);
            _appDbContext.Pedidos.Remove(pedido);
            await _appDbContext.SaveChangesAsync();
            return pedido;
        }
    }
}
