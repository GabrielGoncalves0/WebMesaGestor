using Microsoft.EntityFrameworkCore;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Infra.Data;

namespace WebMesaGestor.Infra.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _appDbContext;

        public UsuarioRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Usuario>> ListarUsuarios()
        {
            return await _appDbContext.Usuarios.ToListAsync();
        }

        public async Task<Usuario> UsuarioPorId(Guid id)
        {
            return await _appDbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == new Guid(id.ToString()));
        }

        public async Task<Usuario> CriarUsuario(Usuario usuario)
        {
            try
            {
                await _appDbContext.Usuarios.AddAsync(usuario);
                await _appDbContext.SaveChangesAsync();
                return usuario;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }
        }

        public async Task<Usuario> AtualizarUsuario(Usuario usuario)
        {
            _appDbContext.Usuarios.Update(usuario);
            await _appDbContext.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> DeletarUsuario(Guid id)
        {
            Usuario usuario = await UsuarioPorId(id);
            _appDbContext.Usuarios.Remove(usuario);
            await _appDbContext.SaveChangesAsync();
            return usuario;
        }
    }
}
