using Microsoft.EntityFrameworkCore;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Infra.Data;

namespace WebMesaGestor.Infra.Repositories
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly AppDbContext _context;

        public TransacaoRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<IEnumerable<Transacao>> ObterTransacoes()
        {
            return await _context.Transacoes
                .Include(t => t.Usuario)
                .Include(t => t.Caixa)
                .Include(t => t.Pedido)
                .ToListAsync();
        }

        public async Task<Transacao> ObterTransacaoPorId(Guid id)
        {
            return await _context.Transacoes
               .Include(t => t.Usuario)
               .Include(t => t.Caixa)
               .Include(t => t.Pedido)
               .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Transacao> CriarTransacao(Transacao transacao)
        {
            _context.Transacoes.Add(transacao);
            await _context.SaveChangesAsync();
            return transacao;
        }

        public async Task<Transacao> AtualizarTransacao(Transacao transacao)
        {
            _context.Transacoes.Update(transacao);
            await _context.SaveChangesAsync();
            return transacao;
        }

        public async Task<bool> DeletarTransacao(Guid id)
        {
            var transacao = await _context.Transacoes.FindAsync(id);
            if (transacao == null)
            {
                return false;
            }

            _context.Transacoes.Remove(transacao);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
