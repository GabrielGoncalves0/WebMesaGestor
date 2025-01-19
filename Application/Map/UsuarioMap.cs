using WebMesaGestor.Application.DTO.Input.USuario;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.Map
{
    public static class UsuarioMap
    {
        public static UsuOutputDTO MapUsuario(this Usuario usuario)
        {
            return new UsuOutputDTO
            {
                Id = usuario.Id,
                UsuNome = usuario.UsuNome,
                UsuEmail = usuario.UsuEmail,
                UsuTelefone = usuario.UsuTelefone,
                UsuTipo = usuario.UsuTipo,
                CriacaoData = usuario.CriacaoData,
                Empresa = usuario.Empresa != null ? EmpresaMap.MapEmpresa(usuario.Empresa) : null
            };
        }

        public static IEnumerable<UsuOutputDTO> MapUsuario(this IEnumerable<Usuario> usuario)
        {
            return usuario.Select(x => x.MapUsuario()).ToList();
        }

        public static Usuario MapUsuario(this UsuCriacaoDTO usuario)
        {
            return new Usuario
            {
                Id = Guid.NewGuid(),
                UsuNome = usuario.UsuNome,
                UsuEmail = usuario.UsuEmail,
                UsuTelefone = usuario.UsuTelefone,
                UsuSenha = usuario.UsuSenha,
                UsuTipo = usuario.UsuTipo,
                CriacaoData = DateTime.UtcNow,
                EmpresaId = usuario.EmpresaId
            };
        }

        public static IEnumerable<Usuario> MapUsuario(this IEnumerable<UsuCriacaoDTO> usuario)
        {
            return usuario.Select(x => x.MapUsuario()).ToList();
        }

        public static Usuario MapUsuario(this UsuEdicaoDTO usuario)
        {
            return new Usuario
            {
                UsuNome = usuario.UsuNome,
                UsuEmail = usuario.UsuEmail,
                UsuTelefone = usuario.UsuTelefone,
                UsuSenha = usuario.UsuSenha,
                UsuTipo = usuario.UsuTipo,
                EmpresaId = usuario.EmpresaId
            };
        }

        public static IEnumerable<Usuario> MapUsuario(this IEnumerable<UsuEdicaoDTO> usuario)
        {
            return usuario.Select(x => x.MapUsuario()).ToList();
        }
    }
}
