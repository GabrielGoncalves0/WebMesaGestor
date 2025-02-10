using AutoMapper;
using WebMesaGestor.Application.DTO.Input.Caixa;
using WebMesaGestor.Application.DTO.Input.Categoria;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.Mapping
{
    public class CaixaMapping : Profile
    {
        public CaixaMapping()
        {
            CreateMap<Caixa, CaixaDTO>();
            CreateMap<Caixa, CaiAbrirDTO>();
            CreateMap<Caixa, CaiFecharDTO>();
            CreateMap<Caixa, CaiAtualizarDTO>();
            CreateMap<CaiAbrirDTO, Caixa>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.AberturaData, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<CaiFecharDTO, Caixa>()
                .ForMember(dest => dest.FechamentoData, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<CaiAtualizarDTO, Caixa>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
