using Microsoft.EntityFrameworkCore;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Infra.Data;

namespace WebMesaGestor.Infra.Repositories
{
    public class OpcProPedRepository : IOpcProPedRepository
    {
        private readonly AppDbContext _context;

        public OpcProPedRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<IEnumerable<OpcaoProPed>> ObterOpcoesPorProPedId(Guid id)
        {
            return await _context.OpcaoProPed.Where(opc => opc.ProdutoPedidoId == id)
                .Include(opc => opc.Opcao)
                .Include(opc => opc.ProdutoPedido)
                .ToListAsync();
        }

        public async Task<OpcaoProPed> ObterOpcaoProPedId(Guid id)
        {
            return await _context.OpcaoProPed
                .Include(opc => opc.Opcao)
                .Include(opc => opc.ProdutoPedido)
                .FirstOrDefaultAsync(opc => opc.Id == id);
        }

        public async Task<OpcaoProPed> CriarOpcaoProdPed(OpcaoProPed opcaoProPed)
        {
            _context.OpcaoProPed.AddAsync(opcaoProPed);
            await _context.SaveChangesAsync();
            return opcaoProPed;
        }

        public async Task<OpcaoProPed> AtualizarOpcaoProdPed(OpcaoProPed opcaoProPed)
        {
            _context.OpcaoProPed.Update(opcaoProPed);
            await _context.SaveChangesAsync();
            return opcaoProPed;
        }

        public async Task<bool> DeletarOpcaoProdPed(Guid id)
        {
            var opcaoProPed = await _context.OpcaoProPed.FindAsync(id);
            if (opcaoProPed == null)
            {
                return false;
            }
            _context.OpcaoProPed.Remove(opcaoProPed);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
