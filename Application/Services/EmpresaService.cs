using AutoMapper;
using WebMesaGestor.Application.Common;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.DTO.Input.Empresa;
using WebMesaGestor.Application.Validations.Empresa;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Infra.Repositories;

namespace WebMesaGestor.Application.Services
{
    public class EmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IMapper _mapper;
        public EmpresaService(IEmpresaRepository empresaRepository, IMapper mapper)
        {
            _empresaRepository = empresaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmpresaDTO>> ObterTodasEmpresas()
        {
            var empresas = await _empresaRepository.ObterTodasEmpresas();
            return _mapper.Map<IEnumerable<EmpresaDTO>>(empresas);
        }

        public async Task<EmpresaDTO> ObterEmpresaPorId(Guid id)
        {
            var empresa = await _empresaRepository.ObterEmpresaPorId(id);

            if (empresa == null)
            {
                return null;
            }

            return _mapper.Map<EmpresaDTO>(empresa);
        }

        public async Task<Response<EmpresaDTO>> CriarEmpresa(EmpCriacaoDTO empresaDTO)
        {
            var validationResult = new EmpresaCriacaoValidator().Validate(empresaDTO);
            if (!validationResult.IsValid)
            {
                return new Response<EmpresaDTO>
                {
                    Sucesso = false,
                    Erros = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var empresa = _mapper.Map<Empresa>(empresaDTO);
            await _empresaRepository.CriarEmpresa(empresa);

            return new Response<EmpresaDTO>
            {
                Sucesso = true,
                Id = empresa.Id,
                Data = _mapper.Map<EmpresaDTO>(empresa)
            };
        }

        public async Task<Response<EmpresaDTO>> AtualizarEmpresa(EmpEdicaoDTO empresaDTO)
        {
            var validationResult = new EmpresaEdicaoValidator().Validate(empresaDTO);
            if (!validationResult.IsValid)
            {
                return new Response<EmpresaDTO>()
                {
                    Sucesso = false,
                    Erros = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var empresaExistente = await _empresaRepository.ObterEmpresaPorId(empresaDTO.Id);
            if (empresaExistente == null)
            {
                return new Response<EmpresaDTO>()
                {
                    Sucesso = false,
                    Erros = new List<string> { EmpresaMessages.EmpresaNaoEncontrada }
                };
            }

            var empresa = _mapper.Map(empresaDTO, empresaExistente);
            await _empresaRepository.AtualizarEmpresa(empresa);

            return new Response<EmpresaDTO>()
            {
                Sucesso = true,
                Id = empresa.Id,
                Data = _mapper.Map<EmpresaDTO>(empresa)
            };
        }

        public async Task<Response<EmpresaDTO>> DeletarEmpresa(Guid id)
        {
            var empresaExistente = await _empresaRepository.ObterEmpresaPorId(id);

            if (empresaExistente == null)
            {
                return new Response<EmpresaDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { EmpresaMessages.EmpresaNaoEncontrada }
                };
            }

            var sucessoDelecao = await _empresaRepository.DeletarEmpresa(id);
            if (!sucessoDelecao)
            {
                return new Response<EmpresaDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { EmpresaMessages.ErroAoDeletarEmpresa }
                };
            }

            var dados = _mapper.Map<EmpresaDTO>(empresaExistente);
            return new Response<EmpresaDTO>
            {
                Sucesso = true,
                Data = dados,
                Erros = new List<string> { EmpresaMessages.EmpresaDeletadaComSucesso }
            };
        }
    }
}
