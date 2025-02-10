using Microsoft.EntityFrameworkCore;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Infra.Data;

namespace WebMesaGestor.Infra.Repositories
{
    public class SetorRepository : ISetorRepository
    {
        private readonly AppDbContext _context;

        public SetorRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<IEnumerable<Setor>> ObterTodosSetores()
        {
            return await _context.Setores.ToListAsync();
        }

        public async Task<Setor> ObterSetorPorId(Guid id)
        {
            return await _context.Setores.FirstOrDefaultAsync(u => u.Id == new Guid(id.ToString()));
        }

        public async Task<Setor> CriarSetor(Setor setor)
        {
            _context.Setores.Add(setor);
            await _context.SaveChangesAsync();
            return setor;
        }

        public async Task<Setor> AtualizarSetor(Setor setor)
        {
            _context.Setores.Update(setor);
            await _context.SaveChangesAsync();
            return setor;
        }

        public async Task<bool> DeletarSetor(Guid id)
        {
            var setor = await _context.Setores.FindAsync(id);
            if (setor == null)
            {
                return false;
            }
            _context.Setores.Remove(setor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
