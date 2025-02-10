//using Microsoft.EntityFrameworkCore;
//using WebMesaGestor.Domain.Entities;
//using WebMesaGestor.Domain.Interfaces;
//using WebMesaGestor.Infra.Data;

//namespace WebMesaGestor.Infra.Repositories
//{
//    public class OpcProPedRepository : IOpcProPedRepository
//    {
//        private readonly AppDbContext _appDbContext;

//        public OpcProPedRepository(AppDbContext appDbContext)
//        {
//            _appDbContext = appDbContext;
//        }

//        public async Task<IEnumerable<OpcaoProPed>> ListarOpcoesPorProPedId(Guid id)
//        {
//            return await _appDbContext.OpcaoProPed.Where(opc => opc.ProdutoPedidoId == id).ToListAsync();
//        }

//        public async Task<OpcaoProPed> OpcaoProPedId(Guid id)
//        {
//            return await _appDbContext.OpcaoProPed.FirstOrDefaultAsync(opc => opc.Id == new Guid(id.ToString()));
//        }

//        public async Task<OpcaoProPed> CriarOpcaoProdPed(OpcaoProPed opcaoProPed)
//        {
//            await _appDbContext.OpcaoProPed.AddAsync(opcaoProPed);
//            await _appDbContext.SaveChangesAsync();
//            return opcaoProPed;
//        }

//        public async Task<OpcaoProPed> AtualizarOpcaoProdPed(OpcaoProPed opcaoProPed)
//        {
//            _appDbContext.OpcaoProPed.Update(opcaoProPed);
//            await _appDbContext.SaveChangesAsync();
//            return opcaoProPed;
//        }

//        public async Task<OpcaoProPed> DeletarOpcaoProdPed(Guid id)
//        {
//            OpcaoProPed opcaoProPed = await OpcaoProPedId(id);
//            _appDbContext.OpcaoProPed.Remove(opcaoProPed);
//            await _appDbContext.SaveChangesAsync();
//            return opcaoProPed;
//        }
//    }
//}
