using AutoMapper;
using WebMesaGestor.Application.DTO.Input.Mesa;
using WebMesaGestor.Application.DTO.Input.Transacao;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.Mapping
{
    public class MesaMapping : Profile
    {
        public MesaMapping()
        {
            CreateMap<Mesa, MesaDTO>();
            CreateMap<Mesa, MesCriacaoDTO>();
            CreateMap<Mesa, MesEdicaoDTO>();
            CreateMap<MesCriacaoDTO, Mesa>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<MesEdicaoDTO, Mesa>();
        }
    }
}
