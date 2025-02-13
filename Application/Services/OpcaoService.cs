using AutoMapper;
using FluentValidation;
using WebMesaGestor.Application.Common;
using WebMesaGestor.Application.DTO.Input.Opcoes;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.Validations.Opcoes;
using WebMesaGestor.Application.Validations.Produto;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Infra.Repositories;

namespace WebMesaGestor.Application.Services
{
    public class OpcaoService
    {
        private readonly IOpcaoRepository _opcaoRepository;
        private readonly IGrupoOpcaoRepository _grupoOpcoesRepository;
        private readonly IMapper _mapper;

        public OpcaoService(IOpcaoRepository opcaoRepository, IGrupoOpcaoRepository grupoOpcoesRepository, IMapper mapper)
        {
            _opcaoRepository = opcaoRepository;
            _grupoOpcoesRepository = grupoOpcoesRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<OpcaoDTO>> ObterTodasOpcoes()
        {
            var opcoes = await _opcaoRepository.ObterTodasOpcoes();
            return _mapper.Map<IEnumerable<OpcaoDTO>>(opcoes);
        }

        public async Task<OpcaoDTO> ObterOpcaoPorId(Guid id)
        {
            var opcao = await _opcaoRepository.ObterOpcaoPorId(id);

            if (opcao == null)
            {
                return null;
            }

            return _mapper.Map<OpcaoDTO>(opcao);
        }

        public async Task<Response<OpcaoDTO>> CriarOpcao(OpcCriacaoDTO opcaoDTO)
        {
            var validationResult = new OpcaoCriacaoValidator().Validate(opcaoDTO);
            if (!validationResult.IsValid)
            {
                return new Response<OpcaoDTO>
                {
                    Sucesso = false,
                    Erros = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var grupoOpcao = await _grupoOpcoesRepository.ObterGrupoOpcaoPorId(opcaoDTO.GrupoOpcoesId);
            if (grupoOpcao == null)
            {
                return new Response<OpcaoDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { GrupoOpcoesMessages.GrupoOpcoesNaoEncontrado }
                };
            }

            var opcao = _mapper.Map<Opcao>(opcaoDTO);
            opcao.GrupoOpcoes = grupoOpcao;

            await _opcaoRepository.CriarOpcao(opcao);

            return new Response<OpcaoDTO>
            {
                Sucesso = true,
                Id = opcao.Id,
                Data = _mapper.Map<OpcaoDTO>(opcao)
            };
        }

        public async Task<Response<OpcaoDTO>> AtualizarOpcao(OpcEdicaoDTO opcaoDTO)
        {
            var validationResult = new OpcaoEdicaoValidator().Validate(opcaoDTO);
            if (!validationResult.IsValid)
            {
                return new Response<OpcaoDTO>
                {
                    Sucesso = false,
                    Erros = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var opcaoExistente = await _opcaoRepository.ObterOpcaoPorId(opcaoDTO.Id);
            if (opcaoExistente == null)
            {
                return new Response<OpcaoDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { OpcaoMessages.OpcaoNaoEncontrada }
                };
            }

            var grupoOpcao = await _grupoOpcoesRepository.ObterGrupoOpcaoPorId(opcaoDTO.GrupoOpcoesId);
            if (grupoOpcao == null)
            {
                return new Response<OpcaoDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { GrupoOpcoesMessages.GrupoOpcoesNaoEncontrado }
                };
            }

            var opcao = _mapper.Map(opcaoDTO, opcaoExistente);

            await _opcaoRepository.AtualizarOpcao(opcao);

            return new Response<OpcaoDTO>
            {
                Sucesso = true,
                Id = opcao.Id,
                Data = _mapper.Map<OpcaoDTO>(opcao)
            };
        }

        public async Task<Response<OpcaoDTO>> DeletarOpcao(Guid id)
        {
            var opcaoExistente = await _opcaoRepository.ObterOpcaoPorId(id);
            if (opcaoExistente == null)
            {
                return new Response<OpcaoDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { OpcaoMessages.OpcaoNaoEncontrada }
                };
            }

            var sucessoDelecao = await _opcaoRepository.DeletarOpcao(id);
            if (!sucessoDelecao)
            {
                return new Response<OpcaoDTO>()
                {
                    Sucesso = false,
                    Erros = new List<string> { OpcaoMessages.ErroAoDeletarOpcao }
                };
            }

            var dados = _mapper.Map<OpcaoDTO>(opcaoExistente);
            return new Response<OpcaoDTO>
            {
                Sucesso = true,
                Data = dados,
                Erros = new List<string> { OpcaoMessages.OpcaoDeletadaComSucesso }
            };
        }
    }
}
