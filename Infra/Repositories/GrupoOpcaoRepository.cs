using Microsoft.EntityFrameworkCore;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Infra.Data;

namespace WebMesaGestor.Infra.Repositories
{
    public class GrupoOpcaoRepository : IGrupoOpcaoRepository
    {
        private readonly AppDbContext _context;

        public GrupoOpcaoRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<IEnumerable<GrupoOpcoes>> ObterGrupoOpcoes()
        {
            return await _context.GrupoOpcoes
                .Include(go => go.Produto)
                .ToListAsync();
        }

        public async Task<GrupoOpcoes> ObterGrupoOpcaoPorId(Guid id)
        {
            return await _context.GrupoOpcoes
                .Include(go => go.Produto)
                .FirstOrDefaultAsync(go => go.Id == id);
        }

        public async Task<GrupoOpcoes> CriarGrupoOpcao(GrupoOpcoes grupoOpcao)
        {
            _context.GrupoOpcoes.AddAsync(grupoOpcao);
            await _context.SaveChangesAsync();
            return grupoOpcao;

        }

        public async Task<GrupoOpcoes> AtualizarGrupoOpcao(GrupoOpcoes grupoOpcao)
        {
            _context.GrupoOpcoes.Update(grupoOpcao);
            await _context.SaveChangesAsync();
            return grupoOpcao;
        }

        public async Task<bool> DeletarGrupoOpcao(Guid id)
        {
            var grupoOpcao = await _context.GrupoOpcoes.FindAsync(id);
            if (grupoOpcao == null)
            {
                return false;
            }
            _context.GrupoOpcoes.Remove(grupoOpcao);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
