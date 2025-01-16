using Microsoft.EntityFrameworkCore;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Infra.Data;

namespace WebMesaGestor.Infra.Repositories
{
    public class SetorRepository : ISetorRepository
    {
        private readonly AppDbContext _appDbContext;

        public SetorRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Setor>> ListarSetors()
        {
            return await _appDbContext.Setores.ToListAsync();
        }

        public async Task<Setor> SetorPorId(Guid id)
        {
            return await _appDbContext.Setores.FirstOrDefaultAsync(u => u.Id == new Guid(id.ToString()));
        }

        public async Task<Setor> CriarSetor(Setor setor)
        {
            try
            {
                await _appDbContext.Setores.AddAsync(setor);
                await _appDbContext.SaveChangesAsync();
                return setor;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }
        }

        public async Task<Setor> AtualizarSetor(Setor setor)
        {
            _appDbContext.Setores.Update(setor);
            await _appDbContext.SaveChangesAsync();
            return setor;
        }

        public async Task<Setor> DeletarSetor(Guid id)
        {
            Setor setor = await SetorPorId(id);
            _appDbContext.Setores.Remove(setor);
            await _appDbContext.SaveChangesAsync();
            return setor;
        }
    }
}
