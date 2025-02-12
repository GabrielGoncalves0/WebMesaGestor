using Microsoft.EntityFrameworkCore;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Infra.Data;

namespace WebMesaGestor.Infra.Repositories
{
    public class EmpresaRepository : IEmpresaRepository 
    {
        private readonly AppDbContext _context;

        public EmpresaRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<IEnumerable<Empresa>> ObterTodasEmpresas()
        {
            return await _context.Empresas.ToListAsync();
        }

        public async Task<Empresa> ObterEmpresaPorId(Guid id)
        {
            return await _context.Empresas.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Empresa> CriarEmpresa(Empresa empresa)
        {
            _context.Empresas.Add(empresa);
            await _context.SaveChangesAsync();
            return empresa;
        }

        public async Task<Empresa> AtualizarEmpresa(Empresa empresa)
        {
            _context.Empresas.Update(empresa);
            await _context.SaveChangesAsync();
            return empresa;
        }

        public async Task<bool> DeletarEmpresa(Guid id)
        {
            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa == null)
            {
                return false;
            }
            _context.Empresas.Remove(empresa);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
