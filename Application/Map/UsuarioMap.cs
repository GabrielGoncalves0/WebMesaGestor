using WebMesaGestor.Application.DTO.Input.Criacao;
using WebMesaGestor.Application.DTO.Input.Edicao;
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
                Usu_nome = usuario.Usu_nome,
                Usu_email = usuario.Usu_email,
                Usu_telefone = usuario.Usu_telefone,
                Usu_tipo = usuario.Usu_tipo,
                Criacao_data = usuario.Criacao_data,
                Empresa = EmpresaMap.MapEmpresa(usuario.Empresa)
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
                Usu_nome = usuario.Usu_nome,
                Usu_email = usuario.Usu_email,
                Usu_telefone = usuario.Usu_telefone,
                Usu_senha = usuario.Usu_senha,
                Usu_tipo = usuario.Usu_tipo,
                Criacao_data = DateTime.UtcNow,
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
                Usu_nome = usuario.Usu_nome,
                Usu_email = usuario.Usu_email,
                Usu_telefone = usuario.Usu_telefone,
                Usu_senha = usuario.Usu_senha,
                Usu_tipo = usuario.Usu_tipo,
                EmpresaId = usuario.EmpresaId
            };
        }

        public static IEnumerable<Usuario> MapUsuario(this IEnumerable<UsuEdicaoDTO> usuario)
        {
            return usuario.Select(x => x.MapUsuario()).ToList();
        }
    }
}
