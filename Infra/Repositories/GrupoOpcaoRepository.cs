//using Microsoft.EntityFrameworkCore;
//using WebMesaGestor.Domain.Entities;
//using WebMesaGestor.Domain.Interfaces;
//using WebMesaGestor.Infra.Data;

//namespace WebMesaGestor.Infra.Repositories
//{
//    public class GrupoOpcaoRepository : IGrupoOpcaoRepository
//    {
//        private readonly AppDbContext _appDbContext;

//        public GrupoOpcaoRepository(AppDbContext appDbContext)
//        {
//            _appDbContext = appDbContext;
//        }

//        public async Task<IEnumerable<GrupoOpcoes>> ListarGrupoOpcoes()
//        {
//            return await _appDbContext.GrupoOpcoes.ToListAsync();
//        }

//        public async Task<GrupoOpcoes> GrupoOpcaoPorId(Guid id)
//        {
//            return await _appDbContext.GrupoOpcoes.FirstOrDefaultAsync(u => u.Id == new Guid(id.ToString()));
//        }

//        public async Task<GrupoOpcoes> CriarGrupoOpcao(GrupoOpcoes grupoOpcao)
//        {
//            try
//            {
//                await _appDbContext.GrupoOpcoes.AddAsync(grupoOpcao);
//                await _appDbContext.SaveChangesAsync();
//                return grupoOpcao;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.InnerException?.Message);
//                throw;
//            }
//        }

//        public async Task<GrupoOpcoes> AtualizarGrupoOpcao(GrupoOpcoes grupoOpcao)
//        {
//            _appDbContext.GrupoOpcoes.Update(grupoOpcao);
//            await _appDbContext.SaveChangesAsync();
//            return grupoOpcao;
//        }

//        public async Task<GrupoOpcoes> DeletarGrupoOpcao(Guid id)
//        {
//            GrupoOpcoes grupoOpcao = await GrupoOpcaoPorId(id);
//            _appDbContext.GrupoOpcoes.Remove(grupoOpcao);
//            await _appDbContext.SaveChangesAsync();
//            return grupoOpcao;
//        }
//    }
//}
