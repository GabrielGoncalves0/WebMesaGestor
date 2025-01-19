using WebMesaGestor.Application.DTO.Input.USuario;
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
            if (retorno.EmpresaId != null)
            {
                retorno.Empresa = await _empresaRepository.EmpresaPorId((Guid)retorno.EmpresaId);
            }
            return UsuarioMap.MapUsuario(retorno);
        }

        public async Task<UsuOutputDTO> AtualizarUsuario(UsuEdicaoDTO usuario)
        {
            Usuario buscarUsuario = await _usuarioRepository.UsuarioPorId(usuario.Id);

            buscarUsuario.UsuNome = usuario.UsuNome;
            buscarUsuario.UsuEmail = usuario.UsuEmail;
            buscarUsuario.UsuTelefone = usuario.UsuTelefone;
            buscarUsuario.UsuSenha = usuario.UsuSenha;
            buscarUsuario.UsuTipo = usuario.UsuTipo;
            buscarUsuario.EmpresaId = usuario.EmpresaId;

            Usuario retorno = await _usuarioRepository.AtualizarUsuario(buscarUsuario);
            if (retorno.EmpresaId != null)
            {
                retorno.Empresa = await _empresaRepository.EmpresaPorId((Guid)retorno.EmpresaId);
            }
            return UsuarioMap.MapUsuario(retorno);
        }

        public async Task<UsuOutputDTO> DeletarUsuario(Guid id)
        {
            Usuario retorno = await _usuarioRepository.DeletarUsuario(id);
            return UsuarioMap.MapUsuario(retorno);
        }
    }
}
