using AutoMapper;
using WebMesaGestor.Application.DTO.Input.Empresa;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.Mapping
{
    public class EmpresaMapping : Profile
    {
        public EmpresaMapping()
        {
            CreateMap<Empresa, EmpresaDTO>();

            CreateMap<Empresa, EmpCriacaoDTO>();

            CreateMap<Empresa, EmpEdicaoDTO>();

            CreateMap<EmpCriacaoDTO, Empresa>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<EmpEdicaoDTO, Empresa>();
        }
    }
}
