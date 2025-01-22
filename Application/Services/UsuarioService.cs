using WebMesaGestor.Application.DTO.Input.USuario;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.Map;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Utils;

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
            var resposta = new Response<IEnumerable<UsuOutputDTO>>();
            try
            {
                var usuarios = await _usuarioRepository.ListarUsuarios();
                await PreencherEmpresas(usuarios);
                resposta.Dados = UsuarioMap.MapUsuario(usuarios);
                resposta.Mensagem = "Usuários listados com sucesso";
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
            }
            return resposta;
        }

        public async Task<Response<UsuOutputDTO>> UsuarioPorId(Guid id)
        {
            var resposta = new Response<UsuOutputDTO>();
            try
            {
                var usuario = await _usuarioRepository.UsuarioPorId(id);
                await PreencherDados(usuario);
                resposta.Dados = UsuarioMap.MapUsuario(usuario);
                resposta.Mensagem = "Usuário encontrado com sucesso";
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
            }
            return resposta;
        }

        public async Task<Response<UsuOutputDTO>> CriarUsuario(UsuCriacaoDTO usuario)
        {
            var resposta = new Response<UsuOutputDTO>();
            try
            {
                ValidarUsuarioCriacao(usuario);
                var empresa = await BuscarEmpresa(usuario.EmpresaId);

                Usuario map = UsuarioMap.MapUsuario(usuario);
                var retorno = await _usuarioRepository.CriarUsuario(map);
                await PreencherDados(retorno);

                retorno.Empresa = await _empresaRepository.EmpresaPorId(usuario.EmpresaId);
                resposta.Dados = UsuarioMap.MapUsuario(retorno);
                resposta.Mensagem = "Usuário criado com sucesso";
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
            }
            return resposta;
        }

        public async Task<Response<UsuOutputDTO>> AtualizarUsuario(UsuEdicaoDTO usuario)
        {
            var resposta = new Response<UsuOutputDTO>();
            try
            {
                ValidarUsuarioEdicao(usuario);
                var empresa = await BuscarEmpresa(usuario.EmpresaId);

                var buscarUsuario = await _usuarioRepository.UsuarioPorId(usuario.Id);
                AtualizarDadosUsuario(buscarUsuario, usuario);

                var retorno = await _usuarioRepository.AtualizarUsuario(buscarUsuario);
                await PreencherDados(retorno);

                resposta.Dados = UsuarioMap.MapUsuario(retorno);
                resposta.Mensagem = "Usuário atualizado com sucesso";
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
            }
            return resposta;
        }

        public async Task<Response<UsuOutputDTO>> DeletarUsuario(Guid id)
        {
            var resposta = new Response<UsuOutputDTO>();
            try
            {
                var usuario = await _usuarioRepository.DeletarUsuario(id);
                await PreencherDados(usuario);
                resposta.Dados = UsuarioMap.MapUsuario(usuario);
                resposta.Mensagem = "Usuário deletado com sucesso";
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
            }
            return resposta;
        }

        // Métodos auxiliares
        private async Task PreencherEmpresas(IEnumerable<Usuario> usuarios)
        {
            foreach (var usuario in usuarios)
            {
                if (usuario.EmpresaId != null)
                {
                    usuario.Empresa = await _empresaRepository.EmpresaPorId((Guid)usuario.EmpresaId);
                }
            }
        }

        private async Task PreencherDados(Usuario usuario)
        {
            usuario.Empresa = await _empresaRepository.EmpresaPorId((Guid)usuario.EmpresaId);
        }

        private async Task<Empresa> BuscarEmpresa(Guid? empresaId)
        {
            if (empresaId == null || empresaId == Guid.Empty)
            {
                throw new Exception("Empresa é obrigatória");
            }

            var empresa = await _empresaRepository.EmpresaPorId((Guid)empresaId);

            if (empresa == null)
            {
                throw new Exception("Empresa não encontrada");
            }

            return empresa;
        }

        private void ValidarUsuarioCriacao(UsuCriacaoDTO usuario)
        {
            ValidadorUtils.ValidarSeVazioOuNulo(usuario.UsuNome, "Nome é obrigatório");
            ValidadorUtils.ValidarSeVazioOuNulo(usuario.UsuEmail, "Email é obrigatório");
            ValidadorUtils.ValidarSeVazioOuNulo(usuario.UsuTelefone, "Telefone é obrigatório");
            ValidadorUtils.ValidarSeVazioOuNulo(usuario.UsuSenha, "Senha é obrigatório");
            if (!Enum.IsDefined(typeof(UsuarioTipo), usuario.UsuTipo))
            {
                throw new Exception("Tipo de usuário é obrigatório");
            }
        }

        private void ValidarUsuarioEdicao(UsuEdicaoDTO usuario)
        {
            ValidadorUtils.ValidarSeVazioOuNulo(usuario.UsuNome, "Nome é obrigatório");
            ValidadorUtils.ValidarSeVazioOuNulo(usuario.UsuEmail, "Email é obrigatório");
            ValidadorUtils.ValidarSeVazioOuNulo(usuario.UsuTelefone, "Telefone é obrigatório");
            ValidadorUtils.ValidarSeVazioOuNulo(usuario.UsuSenha, "Senha é obrigatório");
            if (!Enum.IsDefined(typeof(UsuarioTipo), usuario.UsuTipo))
            {
                throw new Exception("Tipo de usuário é obrigatório");
            }
        }

        private void AtualizarDadosUsuario(Usuario usuarioExistente, UsuEdicaoDTO usuario)
        {
            usuarioExistente.UsuNome = usuario.UsuNome;
            usuarioExistente.UsuEmail = usuario.UsuEmail;
            usuarioExistente.UsuTelefone = usuario.UsuTelefone;
            usuarioExistente.UsuSenha = usuario.UsuSenha;
            usuarioExistente.UsuTipo = usuario.UsuTipo;
            usuarioExistente.EmpresaId = usuario.EmpresaId;
        }
    }
}
