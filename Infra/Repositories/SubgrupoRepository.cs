using Microsoft.EntityFrameworkCore;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Infra.Data;


namespace WebSubgrupoGestor.Infra.Repositories
{
    public class SubgrupoRepository : ISubgrupoRepository
    {
        private readonly AppDbContext _appDbContext;

        public SubgrupoRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Subgrupo>> ListarSubgrupos()
        {
            return await _appDbContext.Subgrupos.ToListAsync();
        }

        public async Task<Subgrupo> SubgrupoPorId(Guid id)
        {
            return await _appDbContext.Subgrupos.FirstOrDefaultAsync(u => u.Id == new Guid(id.ToString()));
        }

        public async Task<Subgrupo> CriarSubgrupo(Subgrupo subgrupo)
        {
            try
            {
                await _appDbContext.Subgrupos.AddAsync(subgrupo);
                await _appDbContext.SaveChangesAsync();
                return subgrupo;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }
        }

        public async Task<Subgrupo> AtualizarSubgrupo(Subgrupo subgrupo)
        {
            _appDbContext.Subgrupos.Update(subgrupo);
            await _appDbContext.SaveChangesAsync();
            return subgrupo;
        }

        public async Task<Subgrupo> DeletarSubgrupo(Guid id)
        {
            Subgrupo subgrupo = await SubgrupoPorId(id);
            _appDbContext.Subgrupos.Remove(subgrupo);
            await _appDbContext.SaveChangesAsync();
            return subgrupo;
        }
    }
}
