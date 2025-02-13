using Microsoft.EntityFrameworkCore;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Infra.Data;

namespace WebMesaGestor.Infra.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _appDbContext;

        public CategoriaRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Categoria>> ObterTodasCategorias()
        {
            return await _appDbContext.Categorias.ToListAsync();
        }

        public async Task<Categoria> ObterCategoriaPorId(Guid id)
        {
            return await _appDbContext.Categorias.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Categoria> CriarCategoria(Categoria categoria)
        {
            _appDbContext.Categorias.AddAsync(categoria);
            await _appDbContext.SaveChangesAsync();
            return categoria;

        }

        public async Task<Categoria> AtualizarCategoria(Categoria categoria)
        {
            _appDbContext.Categorias.Update(categoria);
            await _appDbContext.SaveChangesAsync();
            return categoria;
        }

        public async Task<bool> DeletarCategoria(Guid id)
        {
            var categoria = await _appDbContext.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return false;
            }
            _appDbContext.Categorias.Remove(categoria);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
    }
}
