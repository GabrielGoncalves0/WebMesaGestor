//using Microsoft.EntityFrameworkCore;
//using WebMesaGestor.Domain.Entities;
//using WebMesaGestor.Domain.Interfaces;
//using WebMesaGestor.Infra.Data;

//namespace WebMesaGestor.Infra.Repositories
//{
//    public class CategoriaRepository : ICategoriaRepository
//    {
//        private readonly AppDbContext _appDbContext;

//        public CategoriaRepository(AppDbContext appDbContext)
//        {
//            _appDbContext = appDbContext;
//        }

//        public async Task<IEnumerable<Categoria>> ListarCategorias()
//        {
//            return await _appDbContext.Categorias.ToListAsync();
//        }

//        public async Task<Categoria> CategoriaPorId(Guid id)
//        {
//            return await _appDbContext.Categorias.FirstOrDefaultAsync(u => u.Id == new Guid(id.ToString()));
//        }

//        public async Task<Categoria> CriarCategoria(Categoria categoria)
//        {
//            try
//            {
//                await _appDbContext.Categorias.AddAsync(categoria);
//                await _appDbContext.SaveChangesAsync();
//                return categoria;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.InnerException?.Message);
//                throw;
//            }
//        }

//        public async Task<Categoria> AtualizarCategoria(Categoria categoria)
//        {
//            _appDbContext.Categorias.Update(categoria);
//            await _appDbContext.SaveChangesAsync();
//            return categoria;
//        }

//        public async Task<Categoria> DeletarCategoria(Guid id)
//        {
//            Categoria categoria = await CategoriaPorId(id);
//            _appDbContext.Categorias.Remove(categoria);
//            await _appDbContext.SaveChangesAsync();
//            return categoria;
//        }
//    }
//}
