//using Microsoft.EntityFrameworkCore;
//using WebMesaGestor.Domain.Entities;
//using WebMesaGestor.Domain.Interfaces;
//using WebMesaGestor.Infra.Data;

//namespace WebMesaGestor.Infra.Repositories
//{
//    public class MesaRepository : IMesaRepository
//    {
//        private readonly AppDbContext _appDbContext;

//        public MesaRepository(AppDbContext appDbContext)
//        {
//            _appDbContext = appDbContext;
//        }

//        public async Task<IEnumerable<Mesa>> ListarMesas()
//        {
//            return await _appDbContext.Mesas.ToListAsync();
//        }

//        public async Task<Mesa> MesaPorId(Guid id)
//        {
//            return await _appDbContext.Mesas.FirstOrDefaultAsync(u => u.Id == new Guid(id.ToString()));
//        }

//        public async Task<Mesa> CriarMesa(Mesa mesa)
//        {
//            try
//            {
//                await _appDbContext.Mesas.AddAsync(mesa);
//                await _appDbContext.SaveChangesAsync();
//                return mesa;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.InnerException?.Message);
//                throw;
//            }
//        }

//        public async Task<Mesa> AtualizarMesa(Mesa mesa)
//        {
//            _appDbContext.Mesas.Update(mesa);
//            await _appDbContext.SaveChangesAsync();
//            return mesa;
//        }

//        public async Task<Mesa> DeletarMesa(Guid id)
//        {
//            Mesa mesa = await MesaPorId(id);
//            _appDbContext.Mesas.Remove(mesa);
//            await _appDbContext.SaveChangesAsync();
//            return mesa;
//        }
//    }
//}
