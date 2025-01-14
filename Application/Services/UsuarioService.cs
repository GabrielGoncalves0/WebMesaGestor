using WebMesaGestor.Application.DTO.Input;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.Map;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Infra.Repositories;

namespace WebMesaGestor.Application.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEmpresaRepository _empresaRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository, IEmpresaRepository empresaRepository)
        {
            _usuarioRepository = usuarioRepository;
            _empresaRepository = empresaRepository;
        }

        public async Task<IEnumerable<UsuOutputDTO>> ListarUsuarios()
        {
            IEnumerable<Usuario> usuarios = await _usuarioRepository.ListarUsuarios();
            foreach(var usuario in usuarios)
            {
                if (usuario.EmpresaId != null)
                {
                    usuario.Empresa = await _empresaRepository.EmpresaPorId((Guid)usuario.EmpresaId);
                }
            }
            return UsuarioMap.MapUsuario(usuarios);
        }

        public async Task<UsuOutputDTO> UsuarioPorId(Guid id)
        {
            Usuario usuario = await _usuarioRepository.UsuarioPorId(id);
            if (usuario.EmpresaId != null)
            {
                usuario.Empresa = await _empresaRepository.EmpresaPorId((Guid)usuario.EmpresaId);
            }
            return UsuarioMap.MapUsuario(usuario);
        }

        public async Task<UsuOutputDTO> CriarUsuario(UsuCriacaoDTO usuario)
        {
            Usuario map = UsuarioMap.MapUsuario(usuario);
            Usuario retorno = await _usuarioRepository.CriarUsuario(map);
            return UsuarioMap.MapUsuario(retorno);
        }

        public async Task<UsuOutputDTO> AtualizarUsuario(UsuEdicaoDTO usuario)
        {
            Usuario buscarUsuario = await _usuarioRepository.UsuarioPorId(usuario.Id);

            buscarUsuario.Usu_nome = usuario.Usu_nome;
            buscarUsuario.Usu_email = usuario.Usu_email;
            buscarUsuario.Usu_telefone = usuario.Usu_telefone;
            buscarUsuario.Usu_senha = usuario.Usu_senha;
            buscarUsuario.Usu_tipo = usuario.Usu_tipo;
            buscarUsuario.EmpresaId = usuario.EmpresaId;

            Usuario retorno = await _usuarioRepository.AtualizarUsuario(buscarUsuario);
            return UsuarioMap.MapUsuario(retorno);
        }

        public async Task<UsuOutputDTO> DeletarUsuario(Guid id)
        {
            Usuario retorno = await _usuarioRepository.DeletarUsuario(id);
            return UsuarioMap.MapUsuario(retorno);
        }
    }
}
