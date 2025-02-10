using AutoMapper;
using WebMesaGestor.Application.DTO.Input.Produto;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.Mapping
{
    public class ProdutoMapping : Profile
    {
        public ProdutoMapping()
        {
            CreateMap<Produto, ProdutoDTO>();
            CreateMap<Produto, ProCriacaoDTO>();
            CreateMap<Produto, ProEdicaoDTO>();
            CreateMap<ProCriacaoDTO, Produto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<ProEdicaoDTO, Produto>();
        }
    }
}
