using Microsoft.EntityFrameworkCore;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Infra.Data;

namespace WebMesaGestor.Infra.Repositories
{
    public class CaixaRepository : ICaixaRepository
    {
        private readonly AppDbContext _context;

        public CaixaRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<IEnumerable<Caixa>> ObterTodosCaixas()
        {
            return await _context.Caixas
                .Include(c => c.Usuario)
                .ToListAsync();
        }

        public async Task<Caixa> ObterCaixaPorId(Guid id)
        {
            return await _context.Caixas
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Caixa> UltimoCaixa()
        {
            return await _context.Caixas.Where(c => c.CaiStatus == CaixaStatus.Fechado)
            .OrderByDescending(c => c.FechamentoData).FirstOrDefaultAsync();
        }

        public async Task<Caixa> AbrirCaixa(Caixa caixa)
        {
           _context.Caixas.Add(caixa);
            await _context.SaveChangesAsync();
            return caixa;
        }

        public async Task<Caixa> AtualizarCaixa(Caixa caixa)
        {
            _context.Caixas.Update(caixa);
            await _context.SaveChangesAsync();
            return caixa;
        }

        public async Task<bool> DeletarCaixa(Guid id)
        {
            var caixa = await _context.Caixas.FindAsync(id);
            if (caixa == null)
            {
                return false;
            }
            _context.Caixas.Remove(caixa);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
