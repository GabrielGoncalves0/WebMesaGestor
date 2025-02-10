using AutoMapper;
using WebMesaGestor.Application.DTO.Input.OpcaoProPed;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.Mapping
{
    public class OpcaoProPedMapping : Profile
    {
        public OpcaoProPedMapping()
        {
            CreateMap<OpcaoProPed, OpcaoProPedDTO>();
            CreateMap<OpcaoProPed, OpcProPedCriacaoDTO>();
            CreateMap<OpcaoProPed, OpcProPedEdicaoDTO>();
            CreateMap<OpcProPedCriacaoDTO, OpcaoProPed>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<OpcProPedEdicaoDTO, OpcaoProPed>();
        }
    }
}
