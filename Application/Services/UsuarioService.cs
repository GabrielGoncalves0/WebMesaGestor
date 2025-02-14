using AutoMapper;
using WebMesaGestor.Application.Common;
using WebMesaGestor.Application.DTO.Input.Usuario;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.Validations.Usuario;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Infra.Repositories;

namespace WebMesaGestor.Application.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, IEmpresaRepository empresaRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _empresaRepository = empresaRepository;
            _mapper = mapper;
        }

        public async Task<UsuarioDTO> ObterUsuarioPorId(Guid id)
        {
            var usuario = await _usuarioRepository.ObterUsuarioPorId(id);

            if (usuario == null)
            {
                return null;
            }

            return _mapper.Map<UsuarioDTO>(usuario);
        }

        public async Task<IEnumerable<UsuarioDTO>> ObterTodosUsuarios()
        {
            var usuarios = await _usuarioRepository.ObterTodosUsuarios();
            if (usuarios == null)
            {
                return null;
            }
            return _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
        }

        public async Task<Response<UsuarioDTO>> CriarUsuario(UsuCriacaoDTO usuarioDTO)
        {
            var validationResult = new UsuarioCriacaoValidator().Validate(usuarioDTO);

            if (!validationResult.IsValid)
            {
                return new Response<UsuarioDTO> 
                { 
                    Sucesso = false, 
                    Erros = validationResult.Errors.Select(e => e.ErrorMessage).ToList() 
                };
            }

            var empresa = await _empresaRepository.ObterEmpresaPorId(usuarioDTO.EmpresaId);
            if (empresa == null)
            {
                return new Response<UsuarioDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { EmpresaMessages.EmpresaNaoEncontrada }
                };
            }

            var usuario = _mapper.Map<Usuario>(usuarioDTO);
            usuario.Empresa = empresa;

            await _usuarioRepository.CriarUsuario(usuario);
            return new Response<UsuarioDTO> 
            { 
                Sucesso = true, 
                Id = usuario.Id, 
                Data = _mapper.Map<UsuarioDTO>(usuario),
            };
        }

        public async Task<Response<UsuarioDTO>> AtualizarUsuario(UsuEdicaoDTO usuarioDTO)
        {
            var validationResult = new UsuarioEdicaoValidator().Validate(usuarioDTO);

            if (!validationResult.IsValid)
            {
                return new Response<UsuarioDTO> 
                { 
                    Sucesso = false, 
                    Erros = validationResult.Errors.Select(e => e.ErrorMessage).ToList() 
                };
            }

            var usuarioExistente = await _usuarioRepository.ObterUsuarioPorId(usuarioDTO.Id);
            if (usuarioExistente == null)
            {
                return new Response<UsuarioDTO> 
                { 
                    Sucesso = false, 
                    Erros = new List<string> { UsuarioMessages.UsuarioNaoEncontrado } 
                };
            }

            var empresaExistente = await _empresaRepository.ObterEmpresaPorId((Guid)usuarioDTO.EmpresaId);
            if (empresaExistente == null)
            {
                return new Response<UsuarioDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { EmpresaMessages.EmpresaNaoEncontrada }
                };
            }

            var usuario = _mapper.Map(usuarioDTO, usuarioExistente);

            await _usuarioRepository.AtualizarUsuario(usuario);

            return new Response<UsuarioDTO>
            {
                Sucesso = true,
                Id = usuario.Id,
                Data = _mapper.Map<UsuarioDTO>(usuario)
            };
        }

        public async Task<Response<UsuarioDTO>> DeletarUsuario(Guid id)
        {
            var usuarioExistente = await _usuarioRepository.ObterUsuarioPorId(id);

            if (usuarioExistente == null)
            {
                return new Response<UsuarioDTO> 
                { 
                    Sucesso = false, 
                    Erros = new List<string> { UsuarioMessages.UsuarioNaoEncontrado } 
                };
            }

            var sucessoDelecao = await _usuarioRepository.DeletarUsuario(id);
            if (!sucessoDelecao)
            {
                return new Response<UsuarioDTO> { 
                    Sucesso = false, 
                    Erros = new List<string> { UsuarioMessages.ErroAoDeletarUsuario } 
                };
            }

            var dados = _mapper.Map<UsuarioDTO>(usuarioExistente);
            return new Response<UsuarioDTO> 
            { 
                Sucesso = true, 
                Data = dados, 
                Erros = new List<string> { UsuarioMessages.UsuarioDeletadoComSucesso } 
            };
        }
    }
}
