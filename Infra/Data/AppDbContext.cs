using Microsoft.EntityFrameworkCore;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Caixa> Caixas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<Setor> Setores { get; set; }
        public DbSet<GrupoOpcoes> GrupoOpcoes { get; set; }
        public DbSet<Opcao> Opcoes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }
    }
}
