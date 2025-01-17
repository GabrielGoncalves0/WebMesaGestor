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
        private readonly AppDbContext _appDbContext;
        public OpcaoRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Opcao>> ListarOpcoes()
        {
            return await _appDbContext.Opcoes.ToListAsync();
        }

        public async Task<Opcao> OpcaoPorId(Guid id)
        {
            return await _appDbContext.Opcoes.FirstOrDefaultAsync(u => u.Id == new Guid(id.ToString()));
        }

        public async Task<Opcao> CriarOpcao(Opcao Opcao)
        {
            await _appDbContext.Opcoes.AddAsync(Opcao);
            await _appDbContext.SaveChangesAsync();
            return Opcao;
        }

        public async Task<Opcao> AtualizarOpcao(Opcao Opcao)
        {
            _appDbContext.Opcoes.Update(Opcao);
            await _appDbContext.SaveChangesAsync();
            return Opcao;
        }

        public async Task<Opcao> DeletarOpcao(Guid id)
        {
            Opcao Opcao = await OpcaoPorId(id);
            _appDbContext.Opcoes.Remove(Opcao);
            await _appDbContext.SaveChangesAsync();
            return Opcao;
        }
    }
}
