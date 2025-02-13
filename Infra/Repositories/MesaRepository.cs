using Microsoft.EntityFrameworkCore;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Infra.Data;

namespace WebMesaGestor.Infra.Repositories
{
    public class MesaRepository : IMesaRepository
    {
        private readonly AppDbContext _appDbContext;

        public MesaRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Mesa>> ObterTodasMesas()
        {
            return await _appDbContext.Mesas.ToListAsync();
        }

        public async Task<Mesa> ObterMesaPorId(Guid id)
        {
            return await _appDbContext.Mesas.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Mesa> CriarMesa(Mesa mesa)
        {
            _appDbContext.Mesas.AddAsync(mesa);
            await _appDbContext.SaveChangesAsync();
            return mesa;
        }

        public async Task<Mesa> AtualizarMesa(Mesa mesa)
        {
            _appDbContext.Mesas.Update(mesa);
            await _appDbContext.SaveChangesAsync();
            return mesa;
        }

        public async Task<bool> DeletarMesa(Guid id)
        {
            var mesa = await _appDbContext.Mesas.FindAsync(id);
            if (mesa == null)
            {
                return false;
            }
            _appDbContext.Mesas.Remove(mesa);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
    }
}
