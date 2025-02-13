using Microsoft.EntityFrameworkCore;
using WebMesaGestor.Application.DTO.Input.Opcoes;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Infra.Data;

namespace WebMesaGestor.Infra.Repositories
{
    public class OpcaoRepository : IOpcaoRepository
    {
        private readonly AppDbContext _context;
        public OpcaoRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<IEnumerable<Opcao>> ObterTodasOpcoes()
        {
            return await _context.Opcoes
                .Include(p => p.GrupoOpcoes)
                .ToListAsync();
        }

        public async Task<Opcao> ObterOpcaoPorId(Guid id)
        {
            return await _context.Opcoes
                .Include(p => p.GrupoOpcoes)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Opcao> CriarOpcao(Opcao Opcao)
        {
            _context.Opcoes.AddAsync(Opcao);
            await _context.SaveChangesAsync();
            return Opcao;
        }

        public async Task<Opcao> AtualizarOpcao(Opcao Opcao)
        {
            _context.Opcoes.Update(Opcao);
            await _context.SaveChangesAsync();
            return Opcao;
        }

        public async Task<bool> DeletarOpcao(Guid id)
        {
            var opcao = await _context.Opcoes.FindAsync(id);
            if(opcao == null)
            {
                return false;
            } 

            _context.Opcoes.Remove(opcao);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
