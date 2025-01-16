using Microsoft.EntityFrameworkCore;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Infra.Data;

namespace WebMesaGestor.Infra.Repositories
{
    public class MarcaRepository : IMarcaRepository
    {
        private readonly AppDbContext _appDbContext;

        public MarcaRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Marca>> ListarMarcas()
        {
            return await _appDbContext.Marcas.ToListAsync();
        }

        public async Task<Marca> MarcaPorId(Guid id)
        {
            return await _appDbContext.Marcas.FirstOrDefaultAsync(u => u.Id == new Guid(id.ToString()));
        }

        public async Task<Marca> CriarMarca(Marca marca)
        {
            try
            {
                await _appDbContext.Marcas.AddAsync(marca);
                await _appDbContext.SaveChangesAsync();
                return marca;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }
        }

        public async Task<Marca> AtualizarMarca(Marca marca)
        {
            _appDbContext.Marcas.Update(marca);
            await _appDbContext.SaveChangesAsync();
            return marca;
        }

        public async Task<Marca> DeletarMarca(Guid id)
        {
            Marca marca = await MarcaPorId(id);
            _appDbContext.Marcas.Remove(marca);
            await _appDbContext.SaveChangesAsync();
            return marca;
        }
    }
}
