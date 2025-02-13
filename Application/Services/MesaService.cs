using AutoMapper;
using WebMesaGestor.Application.Common;
using WebMesaGestor.Application.DTO.Input.Mesa;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.Validations.Mesa;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;

namespace WebMesaGestor.Application.Services
{
    public class MesaService
    {
        private readonly IMesaRepository _mesaRepository;
        private readonly IMapper _mapper;
        public MesaService(IMesaRepository mesaRepository, IMapper mapper)
        {
            _mesaRepository = mesaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MesaDTO>> ObterTodasMesas()
        {
            var mesas = await _mesaRepository.ObterTodasMesas();
            return _mapper.Map<IEnumerable<MesaDTO>>(mesas);
        }

        public async Task<MesaDTO> ObterMesaPorId(Guid id)
        {
            var mesa = await _mesaRepository.ObterMesaPorId(id);

            if (mesa == null)
            {
                return null;
            }

            return _mapper.Map<MesaDTO>(mesa);
        }

        public async Task<Response<MesaDTO>> CriarMesa(MesCriacaoDTO mesaDTO)
        {
            var validationResult = new MesaCriacaoValidator().Validate(mesaDTO);
            if (!validationResult.IsValid)
            {
                return new Response<MesaDTO>
                {
                    Sucesso = false,
                    Erros = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var mesa = _mapper.Map<Mesa>(mesaDTO);
            await _mesaRepository.CriarMesa(mesa);

            return new Response<MesaDTO>
            {
                Sucesso = true,
                Id = mesa.Id,
                Data = _mapper.Map<MesaDTO>(mesa)
            };
        }

        public async Task<Response<MesaDTO>> AtualizarMesa(MesEdicaoDTO mesaDTO)
        {
            var validationResult = new MesaEdicaoValidator().Validate(mesaDTO);
            if (!validationResult.IsValid)
            {
                return new Response<MesaDTO>()
                {
                    Sucesso = false,
                    Erros = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var mesaExistente = await _mesaRepository.ObterMesaPorId(mesaDTO.Id);
            if (mesaExistente == null)
            {
                return new Response<MesaDTO>()
                {
                    Sucesso = false,
                    Erros = new List<string> { MesaMessages.MesaNaoEncontrada }
                };
            }

            var mesa = _mapper.Map(mesaDTO, mesaExistente);
            await _mesaRepository.AtualizarMesa(mesa);

            return new Response<MesaDTO>()
            {
                Sucesso = true,
                Id = mesa.Id,
                Data = _mapper.Map<MesaDTO>(mesa)
            };
        }

        public async Task<Response<MesaDTO>> DeletarMesa(Guid id)
        {
            var mesaExistente = await _mesaRepository.ObterMesaPorId(id);

            if (mesaExistente == null)
            {
                return new Response<MesaDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { MesaMessages.MesaNaoEncontrada }
                };
            }

            var sucessoDelecao = await _mesaRepository.DeletarMesa(id);
            if (!sucessoDelecao)
            {
                return new Response<MesaDTO>
                {
                    Sucesso = false,
                    Erros = new List<string> { MesaMessages.ErroAoDeletarMesa }
                };
            }

            var dados = _mapper.Map<MesaDTO>(mesaExistente);
            return new Response<MesaDTO>
            {
                Sucesso = true,
                Data = dados,
                Erros = new List<string> { MesaMessages.MesaDeletadaComSucesso }
            };
        }
    }
}
