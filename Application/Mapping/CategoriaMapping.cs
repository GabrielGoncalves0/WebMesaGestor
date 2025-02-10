using AutoMapper;
using WebMesaGestor.Application.DTO.Input.Categoria;
using WebMesaGestor.Application.DTO.Input.Grupo;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.Mapping
{
    public class CategoriaMapping : Profile
    {
        public CategoriaMapping()
        {
            CreateMap<Categoria, CategoriaDTO>();
            CreateMap<Categoria, CatCriacaoDTO>();
            CreateMap<Categoria, CatEdicaoDTO>();
            CreateMap<CatCriacaoDTO, Categoria>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<CatEdicaoDTO, Categoria>();
        }
    }
}
