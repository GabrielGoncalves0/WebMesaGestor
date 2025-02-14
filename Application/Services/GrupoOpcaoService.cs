using AutoMapper;
using WebMesaGestor.Application.Common;
using WebMesaGestor.Application.DTO.Input.Grupo;
using WebMesaGestor.Application.DTO.Input.Opcoes;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.Validations.Grupo;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;

namespace WebMesaGestor.Application.Services
{
    public class GrupoOpcaoService
    {
        private readonly IGrupoOpcaoRepository _grupoOpcaoRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public GrupoOpcaoService(IGrupoOpcaoRepository grupoRepository, IProdutoRepository produtoRepository, IMapper mapper)
        {
            _grupoOpcaoRepository = grupoRepository;
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GrupoOpcoesDTO>> ObterTodosGrupoOpcoes()
        {
            var grupoOpcoes = await _grupoOpcaoRepository.ObterTodosGrupoOpcoes();
            return _mapper.Map<IEnumerable<GrupoOpcoesDTO>>(grupoOpcoes);
        }

        public async Task<GrupoOpcoesDTO> GrupoOpcaoPorId(Guid id)
        {
            var grupoOpcoes = await _grupoOpcaoRepository.ObterGrupoOpcaoPorId(id);
            return _mapper.Map<GrupoOpcoesDTO>(grupoOpcoes);
        }

        public async Task<Response<GrupoOpcoesDTO>> CriarGrupoOpcao(GrupOpcCriacaoDTO grupoOpcao)
        {
            var validationResult = new GrupOpcCriacaoValidator().Validate(grupoOpcao);
            if (!validationResult.IsValid)
            {
                return new Response<GrupoOpcoesDTO>
                {
                    Sucesso = false,
                    Erros = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var produto = await _produtoRepository.ObterProdutoPorId(grupoOpcao.ProdutoId);
            if (produto == null)
            {
                return new Response<GrupoOpcoesDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { GrupoOpcoesMessages.GrupoOpcoesNaoEncontrado }
                };
            }

            var grupoOpcoes = _mapper.Map<GrupoOpcoes>(grupoOpcao);
            grupoOpcoes.Produto = produto;

            await _grupoOpcaoRepository.CriarGrupoOpcao(grupoOpcoes);
            return new Response<GrupoOpcoesDTO>
            {
                Sucesso = true,
                Id = grupoOpcoes.Id,
                Data = _mapper.Map<GrupoOpcoesDTO>(grupoOpcoes),
            };
        }

        public async Task<Response<GrupoOpcoesDTO>> AtualizarGrupoOpcao(GrupOpcEdicaoDTO grupoOpcaoDTO)
        {
            var validationResult = new GrupOpcEdicaoValidator().Validate(grupoOpcaoDTO);
            if(!validationResult.IsValid)
            {
                return new Response<GrupoOpcoesDTO>
                {
                    Sucesso = false,
                    Erros = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var grupoOpcoesExiste = await _grupoOpcaoRepository.ObterGrupoOpcaoPorId(grupoOpcaoDTO.Id);
            if(grupoOpcoesExiste == null)
            {
                return new Response<GrupoOpcoesDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { GrupoOpcoesMessages.GrupoOpcoesNaoEncontrado }
                };
            }

            var produtoExiste = await _produtoRepository.ObterProdutoPorId(grupoOpcaoDTO.ProdutoId);
            if (produtoExiste == null)
            {
                return new Response<GrupoOpcoesDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { ProdutoMessages.ProdutoNaoEncontrado }
                };
            }

            var grupoOpcoes = _mapper.Map(grupoOpcaoDTO, grupoOpcoesExiste);
            await _grupoOpcaoRepository.AtualizarGrupoOpcao(grupoOpcoes);
            return new Response<GrupoOpcoesDTO>
            {
                Sucesso = true,
                Id = grupoOpcoes.Id,
                Data = _mapper.Map<GrupoOpcoesDTO>(grupoOpcoes)
            };
        }

        public async Task<Response<GrupoOpcoesDTO>> DeletarGrupoOpcao(Guid id)
        {
            var grupoOpcoes = await _grupoOpcaoRepository.ObterGrupoOpcaoPorId(id);
            if(grupoOpcoes == null)
            {
                return new Response<GrupoOpcoesDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { GrupoOpcoesMessages.GrupoOpcoesNaoEncontrado }
                };
            }

            var sucessoDelecao = await _grupoOpcaoRepository.DeletarGrupoOpcao(id);
            if(!sucessoDelecao)
            {
                return new Response<GrupoOpcoesDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { GrupoOpcoesMessages.ErroAoDeletarGrupoOpcoes }
                };
            }

            var dados = _mapper.Map<GrupoOpcoesDTO>(grupoOpcoes);
            return new Response<GrupoOpcoesDTO>
            {
                Sucesso = true,
                Data = dados,
                Erros = new List<string> { GrupoOpcoesMessages.GrupoOpcoesDeletadoComSucesso }
            };
        }
    }
}
