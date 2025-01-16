using Microsoft.EntityFrameworkCore;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Infra.Data;

namespace WebMesaGestor.Infra.Repositories
{
    public class GrupoRepository : IGrupoRepository
    {
        private readonly AppDbContext _appDbContext;

        public GrupoRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Grupo>> ListarGrupos()
        {
            return await _appDbContext.Grupos.ToListAsync();
        }

        public async Task<Grupo> GrupoPorId(Guid id)
        {
            return await _appDbContext.Grupos.FirstOrDefaultAsync(u => u.Id == new Guid(id.ToString()));
        }

        public async Task<Grupo> CriarGrupo(Grupo grupo)
        {
            try
            {
                await _appDbContext.Grupos.AddAsync(grupo);
                await _appDbContext.SaveChangesAsync();
                return grupo;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }
        }

        public async Task<Grupo> AtualizarGrupo(Grupo grupo)
        {
            _appDbContext.Grupos.Update(grupo);
            await _appDbContext.SaveChangesAsync();
            return grupo;
        }

        public async Task<Grupo> DeletarGrupo(Guid id)
        {
            Grupo grupo = await GrupoPorId(id);
            _appDbContext.Grupos.Remove(grupo);
            await _appDbContext.SaveChangesAsync();
            return grupo;
        }
    }
}
