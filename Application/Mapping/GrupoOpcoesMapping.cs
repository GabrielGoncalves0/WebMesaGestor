using AutoMapper;
using WebMesaGestor.Application.DTO.Input.Grupo;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.Mapping
{
    public class GrupoOpcoesMapping : Profile
    {
        public GrupoOpcoesMapping()
        {
            CreateMap<GrupoOpcoes, GrupoOpcoesDTO>();
            CreateMap<GrupoOpcoes, GrupOpcCriacaoDTO>();
            CreateMap<GrupoOpcoes, GrupOpcEdicaoDTO>();
            CreateMap<GrupOpcCriacaoDTO, GrupoOpcoes>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<GrupOpcEdicaoDTO, GrupoOpcoes>();
        }
    }
}
