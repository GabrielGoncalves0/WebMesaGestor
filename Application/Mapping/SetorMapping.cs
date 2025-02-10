using AutoMapper;
using WebMesaGestor.Application.DTO.Input.Setor;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.Mapping
{
    public class SetorMapping : Profile
    {
        public SetorMapping()
        {
            CreateMap<Setor, SetorDTO>();
            CreateMap<Setor, SetCriacaoDTO>();
            CreateMap<Setor, SetEdicaoDTO>();
            CreateMap<SetCriacaoDTO, Setor>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<SetEdicaoDTO, Setor>();
        }
    }
}
