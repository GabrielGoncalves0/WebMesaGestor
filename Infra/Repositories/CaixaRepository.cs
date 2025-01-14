using Microsoft.EntityFrameworkCore;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Infra.Data;

namespace WebMesaGestor.Infra.Repositories
{
    public class CaixaRepository : ICaixaRepository
    {
        private readonly AppDbContext _appDbContext;

        public CaixaRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Caixa>> ListarCaixas()
        {
            return await _appDbContext.Caixas.ToListAsync();
        }

        public async Task<Caixa> CaixaPorId(Guid id)
        {
            return await _appDbContext.Caixas.FirstOrDefaultAsync(u => u.Id == new Guid(id.ToString()));
        }

        public async Task<Caixa> CriarCaixa(Caixa caixa)
        {
            try
            {
                await _appDbContext.Caixas.AddAsync(caixa);
                await _appDbContext.SaveChangesAsync();
                return caixa;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }
        }

        public async Task<Caixa> AtualizarCaixa(Caixa caixa)
        {
            _appDbContext.Caixas.Update(caixa);
            await _appDbContext.SaveChangesAsync();
            return caixa;
        }

        public async Task<Caixa> DeletarCaixa(Guid id)
        {
            Caixa caixa = await CaixaPorId(id);
            _appDbContext.Caixas.Remove(caixa);
            await _appDbContext.SaveChangesAsync();
            return caixa;
        }
    }
}
