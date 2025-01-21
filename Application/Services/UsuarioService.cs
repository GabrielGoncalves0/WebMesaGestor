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

        public async Task<Response<IEnumerable<UsuOutputDTO>>> ListarUsuarios()
        {
            Response<IEnumerable<UsuOutputDTO>> resposta = new Response<IEnumerable<UsuOutputDTO>>();
            try
            {
                IEnumerable<Usuario> usuarios = await _usuarioRepository.ListarUsuarios();
                foreach(var usuario in usuarios)
                {
                    if (usuario.EmpresaId != null)
                    {
                        usuario.Empresa = await _empresaRepository.EmpresaPorId((Guid)usuario.EmpresaId);
                    }
                }
                resposta.Dados = UsuarioMap.MapUsuario(usuarios);
                resposta.Mensagem = "Usuários listados com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<UsuOutputDTO>> UsuarioPorId(Guid id)
        {
            Response<UsuOutputDTO> resposta = new Response<UsuOutputDTO>();
            try
            {
                Usuario usuario = await _usuarioRepository.UsuarioPorId(id);
                if (usuario.EmpresaId != null)
                {
                    usuario.Empresa = await _empresaRepository.EmpresaPorId((Guid)usuario.EmpresaId);
                }

                resposta.Dados = UsuarioMap.MapUsuario(usuario);
                resposta.Mensagem = "Usuário encontrado com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<UsuOutputDTO>> CriarUsuario(UsuCriacaoDTO usuario)
        {
            Response<UsuOutputDTO> resposta = new Response<UsuOutputDTO>();
            try
            {
                Usuario map = UsuarioMap.MapUsuario(usuario);
                Usuario retorno = await _usuarioRepository.CriarUsuario(map);
                if (retorno.EmpresaId != null)
                {
                    retorno.Empresa = await _empresaRepository.EmpresaPorId((Guid)retorno.EmpresaId);
                }

                resposta.Dados = UsuarioMap.MapUsuario(retorno);
                resposta.Mensagem = "Usuário criado com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<UsuOutputDTO>> AtualizarUsuario(UsuEdicaoDTO usuario)
        {
            Response<UsuOutputDTO> resposta = new Response<UsuOutputDTO>();
            try
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
                resposta.Dados = UsuarioMap.MapUsuario(retorno);
                resposta.Mensagem = "Usuário atualizado com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }
        
        public async Task<Response<UsuOutputDTO>> DeletarUsuario(Guid id)
        {
            Response<UsuOutputDTO> resposta = new Response<UsuOutputDTO>();
            try
            {
                Usuario usuario = await _usuarioRepository.DeletarUsuario(id);
                resposta.Dados = UsuarioMap.MapUsuario(usuario);
                resposta.Mensagem = "Usuário deletado com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }
    }
}
