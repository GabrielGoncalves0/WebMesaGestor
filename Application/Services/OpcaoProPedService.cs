using AutoMapper;
using FluentValidation;
using WebMesaGestor.Application.Common;
using WebMesaGestor.Application.DTO.Input.OpcaoProPed;
using WebMesaGestor.Application.DTO.Input.ProdutoPedido;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.Validations.OpcaoProPed;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Infra.Repositories;

namespace WebMesaGestor.Application.Services
{
    public class OpcaoProPedService
    {
        private readonly IOpcProPedRepository _opcProPedRepository;
        private readonly IProPedRepository _proPedRepository;
        private readonly IOpcaoRepository _opcaoRepository;
        private readonly IMapper _mapper;

        public OpcaoProPedService(IOpcProPedRepository opcProPedRepository, IProPedRepository proPedRepository,
            IOpcaoRepository opcaoRepository, IMapper mapper)
        {
            _opcProPedRepository = opcProPedRepository;
            _proPedRepository = proPedRepository;
            _opcaoRepository = opcaoRepository;
            _mapper = mapper;
        }



        public async Task<OpcaoProPedDTO> OpcaoProPedId(Guid id)
        {
            var opcaoProPed = await _opcProPedRepository.ObterOpcaoProPedId(id);
            return _mapper.Map<OpcaoProPedDTO>(opcaoProPed);
        }

        public async Task<IEnumerable<OpcaoProPedDTO>> ObterTodasOpcoesPorProPedId(Guid id)
        {
            var opcaoProPed = await _opcProPedRepository.ObterTodasOpcoesPorProPedId(id);
            return _mapper.Map<IEnumerable<OpcaoProPedDTO>>(opcaoProPed);
        }

        public async Task<Response<OpcaoProPedDTO>> CriarOpcaoProPed(OpcProPedCriacaoDTO opcProPedDTO)
        {
            var validationResult = new OpcProPedCriacaoValidator().Validate(opcProPedDTO);

            if (!validationResult.IsValid)
            {
                return new Response<OpcaoProPedDTO>
                {
                    Sucesso = false,
                    Erros = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var opcao = await _opcaoRepository.ObterOpcaoPorId(opcProPedDTO.OpcaoId);
            if (opcao == null)
            {
                return new Response<OpcaoProPedDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { OpcaoMessages.OpcaoNaoEncontrada }
                };
            }

            var proPed = await _proPedRepository.ObterProPedId(opcProPedDTO.ProdutoPedidoId);
            if (proPed == null)
            {
                return new Response<OpcaoProPedDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { ProdutoPedidoMessages.ProdutoPedidoNaoEncontrado }
                };
            }

            var opcaoProPed = _mapper.Map<OpcaoProPed>(opcProPedDTO);
            opcaoProPed.Opcao = opcao;
            opcaoProPed.ProdutoPedido = proPed;

            await _opcProPedRepository.CriarOpcaoProdPed(opcaoProPed);
            return new Response<OpcaoProPedDTO>
            {
                Sucesso = true,
                Id = opcaoProPed.Id,
                Data = _mapper.Map<OpcaoProPedDTO>(opcaoProPed)
            };
        }

        public async Task<Response<OpcaoProPedDTO>> AtualizarOpcaoProPed(OpcProPedEdicaoDTO opcProPedDTO)
        {
            var validationResult = new OpcProPedEdicaoValidator().Validate(opcProPedDTO);

            if (!validationResult.IsValid)
            {
                return new Response<OpcaoProPedDTO>
                {
                    Sucesso = false,
                    Erros = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var opcaoProPedExiste = await _opcProPedRepository.ObterOpcaoProPedId(opcProPedDTO.Id);
            if (opcaoProPedExiste == null)
            {
                return new Response<OpcaoProPedDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { OpcaoProPedMessages.OpcaoProPedNaoEncontrado }
                };
            }

            var opcaoExiste = await _opcaoRepository.ObterOpcaoPorId(opcProPedDTO.OpcaoId);
            if (opcaoExiste == null)
            {
                return new Response<OpcaoProPedDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { OpcaoMessages.OpcaoNaoEncontrada }
                };
            }

            var proPedExiste = await _proPedRepository.ObterProPedId(opcProPedDTO.ProdutoPedidoId);
            if (proPedExiste == null)
            {
                return new Response<OpcaoProPedDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { ProdutoPedidoMessages.ProdutoPedidoNaoEncontrado }
                };
            }

            var opcaoProPed = _mapper.Map(opcProPedDTO, opcaoProPedExiste);

            await _opcProPedRepository.AtualizarOpcaoProdPed(opcaoProPed);

            return new Response<OpcaoProPedDTO>
            {
                Sucesso = true,
                Id = opcaoProPed.Id,
                Data = _mapper.Map<OpcaoProPedDTO>(opcaoProPed)
            };
        }

        public async Task<Response<OpcaoProPedDTO>> DeletarOpcProPed(Guid id)
        {
            var opcaoProPed = await _opcProPedRepository.ObterOpcaoProPedId(id);

            if (opcaoProPed == null)
            {
                return new Response<OpcaoProPedDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { OpcaoProPedMessages.OpcaoProPedNaoEncontrado }
                };
            }

            var sucessoDelecao = await _opcProPedRepository.DeletarOpcaoProdPed(id);
            if (!sucessoDelecao)
            {
                return new Response<OpcaoProPedDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { OpcaoProPedMessages.ErroAoDeletarOpcaoProPed }
                };
            }

            var dados = _mapper.Map<OpcaoProPedDTO>(opcaoProPed);
            return new Response<OpcaoProPedDTO>
            {
                Sucesso = true,
                Data = dados,
                Erros = new List<string> { OpcaoProPedMessages.OpcaoProPedDeletadoComSucesso }
            };
        }
    }
}
