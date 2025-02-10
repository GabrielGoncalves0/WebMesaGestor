//using Microsoft.EntityFrameworkCore;
//using WebMesaGestor.Domain.Entities;
//using WebMesaGestor.Domain.Interfaces;
//using WebMesaGestor.Infra.Data;

//namespace WebMesaGestor.Infra.Repositories
//{
//    public class ProPedRepository : IProPedRepository
//    {
//        private readonly AppDbContext _appDbContext;

//        public ProPedRepository(AppDbContext appDbContext)
//        {
//            _appDbContext = appDbContext;
//        }
        
//        public async Task<IEnumerable<ProdutoPedido>> ListarProdutosPorPedId(Guid id)
//        {
//            return await _appDbContext.ProdutoPedido.Where(pp => pp.PedidoId == id).ToListAsync();
//        }

//        public async Task<ProdutoPedido> ProPedId(Guid id)
//        {
//            return await _appDbContext.ProdutoPedido.FirstOrDefaultAsync(pp => pp.Id == new Guid(id.ToString()));
//        }

//        public async Task<ProdutoPedido> CriarProPed(ProdutoPedido produtoPedido)
//        {
//            await _appDbContext.ProdutoPedido.AddAsync(produtoPedido);
//            await _appDbContext.SaveChangesAsync();
//            return produtoPedido;
//        }

//        public async Task<ProdutoPedido> AtualizarProPed(ProdutoPedido produtoPedido)
//        {
//            _appDbContext.ProdutoPedido.Update(produtoPedido);
//            await _appDbContext.SaveChangesAsync();
//            return produtoPedido;
//        }

//        public async Task<ProdutoPedido> DeletarProPed(Guid id)
//        {
//            ProdutoPedido produtoPed = await ProPedId(id);
//            _appDbContext.ProdutoPedido.Remove(produtoPed);
//            await _appDbContext.SaveChangesAsync();
//            return produtoPed;
//        }
//    }
//}
