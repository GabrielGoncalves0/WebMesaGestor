//using Microsoft.EntityFrameworkCore;
//using WebMesaGestor.Domain.Entities;
//using WebMesaGestor.Domain.Interfaces;
//using WebMesaGestor.Infra.Data;

//namespace WebMesaGestor.Infra.Repositories
//{
//    public class TransacaoRepository : ITransacaoRepository
//    {
//        private readonly AppDbContext _appDbContext;

//        public TransacaoRepository(AppDbContext appDbContext)
//        {
//            _appDbContext = appDbContext;
//        }

//        public async Task<IEnumerable<Transacao>> ListarTransacoes()
//        {
//            return await _appDbContext.Transacoes.ToListAsync();
//        }

//        public async Task<Transacao> TransacaoPorId(Guid id)
//        {
//            return await _appDbContext.Transacoes.FirstOrDefaultAsync(u => u.Id == new Guid(id.ToString()));
//        }

//        public async Task<Transacao> CriarTransacao(Transacao transacao)
//        {
//            try
//            {
//                await _appDbContext.Transacoes.AddAsync(transacao);
//                await _appDbContext.SaveChangesAsync();
//                return transacao;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.InnerException?.Message);
//                throw;
//            }
//        }

//        public async Task<Transacao> AtualizarTransacao(Transacao transacao)
//        {
//            _appDbContext.Transacoes.Update(transacao);
//            await _appDbContext.SaveChangesAsync();
//            return transacao;
//        }

//        public async Task<Transacao> DeletarTransacao(Guid id)
//        {
//            Transacao transacao = await TransacaoPorId(id);
//            _appDbContext.Transacoes.Remove(transacao);
//            await _appDbContext.SaveChangesAsync();
//            return transacao;
//        }
//    }
//}
