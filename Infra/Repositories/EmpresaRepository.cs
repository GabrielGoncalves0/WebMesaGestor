using Microsoft.EntityFrameworkCore;
using System;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Infra.Data;

namespace WebMesaGestor.Infra.Repositories
{
    public class EmpresaRepository : IEmpresaRepository 
    {
        private readonly AppDbContext _appDbContext;

        public EmpresaRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Empresa>> ListarEmpresas()
        {
            return await _appDbContext.Empresas.ToListAsync();
        }

        public async Task<Empresa> EmpresaPorId(Guid id)
        {
            return await _appDbContext.Empresas.FirstOrDefaultAsync(u => u.Id == new Guid(id.ToString()));
        }

        public async Task<Empresa> CriarEmpresa(Empresa empresa)
        {
            try
            {
                await _appDbContext.Empresas.AddAsync(empresa);
                await _appDbContext.SaveChangesAsync();
                return empresa;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }
        }

        public async Task<Empresa> AtualizarEmpresa(Empresa empresa)
        {
            _appDbContext.Empresas.Update(empresa);
            await _appDbContext.SaveChangesAsync();
            return empresa;
        }

        public async Task<Empresa> DeletarEmpresa(Guid id)
        {
            Empresa empresa = await EmpresaPorId(id);
            _appDbContext.Empresas.Remove(empresa);
            await _appDbContext.SaveChangesAsync();
            return empresa;
        }
    }
}
