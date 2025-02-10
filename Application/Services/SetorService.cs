using AutoMapper;
using WebMesaGestor.Application.Common;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.DTO.Input.Setor;
using WebMesaGestor.Application.Validations.Setor;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Infra.Repositories;


namespace WebMesaGestor.Application.Services
{
    public class SetorService
    {
        private readonly ISetorRepository _setorRepository;
        private readonly IMapper _mapper;

        public SetorService(ISetorRepository setorRepository, IMapper mapper)
        {
            _setorRepository = setorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SetorDTO>> ObterTodosSetores()
        {
            var setores = await _setorRepository.ObterTodosSetores();
            return _mapper.Map<IEnumerable<SetorDTO>>(setores);
        }

        public async Task<SetorDTO> ObterSetorPorId(Guid id)
        {
            var setor = await _setorRepository.ObterSetorPorId(id);

            if (setor == null)
            {
                return null;
            }

            return _mapper.Map<SetorDTO>(setor);
        }

        public async Task<Response<SetorDTO>> CriarSetor(SetCriacaoDTO setorDTO)
        {
            var validationResult = new SetorCriacaoValidator().Validate(setorDTO);
            if (!validationResult.IsValid)
            {
                return new Response<SetorDTO>
                {
                    Sucesso = false,
                    Erros = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var setor = _mapper.Map<Setor>(setorDTO);
            await _setorRepository.CriarSetor(setor);

            return new Response<SetorDTO>
            {
                Sucesso = true,
                Id = setor.Id,
                Data = _mapper.Map<SetorDTO>(setor)
            };
        }

        public async Task<Response<SetorDTO>> AtualizarSetor(SetEdicaoDTO setorDTO)
        {
            var validationResult = new SetorEdicaoValidator().Validate(setorDTO);
            if (!validationResult.IsValid)
            {
                return new Response<SetorDTO>()
                {
                    Sucesso = false,
                    Erros = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var setorExistente = await _setorRepository.ObterSetorPorId(setorDTO.Id);
            if (setorExistente == null)
            {
                return new Response<SetorDTO>()
                {
                    Sucesso = false,
                    Erros = new List<string> { SetorMessages.SetorNaoEncontrado }
                };
            }

            var setor = _mapper.Map(setorDTO, setorExistente);
            await _setorRepository.AtualizarSetor(setor);

            return new Response<SetorDTO>()
            {
                Sucesso = true,
                Id = setor.Id,
                Data = _mapper.Map<SetorDTO>(setor)
            };
        }

        public async Task<Response<SetorDTO>> DeletarSetor(Guid id)
        {
            var setorExistente = await _setorRepository.ObterSetorPorId(id);

            if (setorExistente == null)
            {
                return new Response<SetorDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { SetorMessages.SetorNaoEncontrado }
                };
            }

            var sucessoDelecao = await _setorRepository.DeletarSetor(id);
            if (!sucessoDelecao)
            {
                return new Response<SetorDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { SetorMessages.ErroAoDeletarSetor }
                };
            }

            var dados = _mapper.Map<SetorDTO>(setorExistente);
            return new Response<SetorDTO>
            {
                Sucesso = true,
                Data = dados,
                Erros = new List<string> { SetorMessages.SetorDeletadoComSucesso }
            };
        }
    }
}
