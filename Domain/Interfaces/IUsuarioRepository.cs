using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> ObterTodosUsuarios();
        Task<Usuario> ObterUsuarioPorId(Guid id);
        Task<Usuario> CriarUsuario(Usuario usuario);
        Task<Usuario> AtualizarUsuario(Usuario usuario);
        Task<bool> DeletarUsuario(Guid id);
    }
}
