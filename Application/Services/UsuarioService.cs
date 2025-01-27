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
            Response<IEnumerable<UsuOutputDTO>> resposta = new Response<IEnumerable<UsuOutputDTO>>();
            try
            {
                IEnumerable<Usuario> usuarios = await _usuarioRepository.ListarUsuarios();
                if (usuarios == null)
                {
                    resposta.Mensagem = "Usuarios não encontrado.";
                    resposta.Status = false;
                    return resposta;
                }

                await PreencherEmpresas(usuarios);
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
                if (usuario == null)
                {
                    resposta.Mensagem = "Usuario não encontrada.";
                    resposta.Status = false;
                    return resposta;
                }

                await PreencherEmpresa(usuario);
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
                ValidarUsuarioCriacao(usuario);
                await ValidarEmpresa(usuario.EmpresaId);

                Usuario map = UsuarioMap.MapUsuario(usuario);
                Usuario retorno = await _usuarioRepository.CriarUsuario(map);
                await PreencherEmpresa(retorno);

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
                ValidarUsuarioEdicao(usuario);
                await ValidarEmpresa(usuario.EmpresaId);
                Usuario buscarUsuario = await _usuarioRepository.UsuarioPorId(usuario.Id);
                if (buscarUsuario == null)
                {
                    resposta.Mensagem = "Usuário não encontrado.";
                    resposta.Status = false;
                    return resposta;
                }
                AtualizarDadosUsuario(buscarUsuario, usuario);
                Usuario retorno = await _usuarioRepository.AtualizarUsuario(buscarUsuario);
                await PreencherEmpresa(retorno);

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
                Usuario usuario = await _usuarioRepository.UsuarioPorId(id);
                if (usuario == null)
                {
                    resposta.Mensagem = "Usuario não encontrada para deleção.";
                    resposta.Status = false;
                    return resposta;
                }
                Usuario retorno = await _usuarioRepository.DeletarUsuario(id);

                await PreencherEmpresa(retorno);
                resposta.Dados = UsuarioMap.MapUsuario(retorno);
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

        private async Task PreencherEmpresa(Usuario usuario)
        {
            if (usuario.EmpresaId != null)
            {
                usuario.Empresa = await _empresaRepository.EmpresaPorId((Guid)usuario.EmpresaId);
            }
        }

        private async Task ValidarEmpresa(Guid? empresaId)
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
        }

        private void ValidarUsuarioCriacao(UsuCriacaoDTO usuario)
        {
            ValidadorUtils.ValidarSeVazioOuNulo(usuario.UsuNome, "Nome é obrigatório");
            ValidadorUtils.ValidarMaximo(usuario.UsuNome, 50, "Nome deve conter no máximo 50 caracteres");
            ValidadorUtils.ValidarMinimo(usuario.UsuNome, 3, "Nome deve conter no minimo 3 caracteres");

            ValidadorUtils.ValidarSeVazioOuNulo(usuario.UsuEmail, "Email é obrigatório");
            ValidadorUtils.ValidarMaximo(usuario.UsuEmail, 50, "Telefone deve conter no máximo 50 caracteres");
            ValidadorUtils.ValidarEmail(usuario.UsuEmail, "Email deve seguir o padrão de e-mail");

            ValidadorUtils.ValidarSeVazioOuNulo(usuario.UsuTelefone, "Telefone é obrigatório");
            ValidadorUtils.ValidarMaximo(usuario.UsuTelefone, 16, "Telefone deve conter no máximo 16 caracteres");
            ValidadorUtils.ValidarNumeroTel(usuario.UsuTelefone, "Insira um numero de telefone valido");

            ValidadorUtils.ValidarSeVazioOuNulo(usuario.UsuSenha, "Senha é obrigatório");
            ValidadorUtils.ValidarSenha(usuario.UsuSenha, "Senha deve conter pelo menos 8 caracteres, uma letra maiúscula, uma letra minúscula, um número e um caracter especial");
            ValidadorUtils.ValidarMaximo(usuario.UsuSenha, 30, "Senha deve conter no máximo 30 caracteres");
            ValidadorUtils.ValidarMinimo(usuario.UsuSenha, 5, "Senha deve conter no minimo 5 caracteres");

            if (!Enum.IsDefined(typeof(UsuarioTipo), usuario.UsuTipo))
            {
                throw new Exception("Tipo de usuário é obrigatório");
            }
        }

        private void ValidarUsuarioEdicao(UsuEdicaoDTO usuario)
        {
            ValidadorUtils.ValidarSeVazioOuNulo(usuario.UsuNome, "Nome é obrigatório");
            ValidadorUtils.ValidarMaximo(usuario.UsuNome, 50, "Nome deve conter no máximo 50 caracteres");
            ValidadorUtils.ValidarMinimo(usuario.UsuNome, 3, "Nome deve conter no minimo 3 caracteres");

            ValidadorUtils.ValidarSeVazioOuNulo(usuario.UsuEmail, "Email é obrigatório");
            ValidadorUtils.ValidarMaximo(usuario.UsuEmail, 50, "Telefone deve conter no máximo 50 caracteres");
            ValidadorUtils.ValidarEmail(usuario.UsuEmail, "Email deve seguir o padrão de e-mail");

            ValidadorUtils.ValidarSeVazioOuNulo(usuario.UsuTelefone, "Telefone é obrigatório");
            ValidadorUtils.ValidarMaximo(usuario.UsuTelefone, 16, "Telefone deve conter no máximo 16 caracteres");
            ValidadorUtils.ValidarNumeroTel(usuario.UsuTelefone, "Insira um numero de telefone valido");

            ValidadorUtils.ValidarSeVazioOuNulo(usuario.UsuSenha, "Senha é obrigatório");
            ValidadorUtils.ValidarSenha(usuario.UsuSenha, "Senha deve conter pelo menos 8 caracteres, uma letra maiúscula, uma letra minúscula, um número e um caracter especial");
            ValidadorUtils.ValidarMaximo(usuario.UsuSenha, 30, "Senha deve conter no máximo 30 caracteres");
            ValidadorUtils.ValidarMinimo(usuario.UsuSenha, 5, "Senha deve conter no minimo 5 caracteres");

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
