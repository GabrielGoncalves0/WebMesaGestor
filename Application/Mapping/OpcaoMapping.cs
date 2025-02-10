using AutoMapper;
using WebMesaGestor.Application.DTO.Input.Opcoes;
using WebMesaGestor.Application.DTO.Input.Transacao;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.Mapping
{
    public class OpcaoMapping : Profile
    {
        public OpcaoMapping()
        {
            CreateMap<Opcao, OpcaoDTO>();
            CreateMap<Opcao, OpcCriacaoDTO>();
            CreateMap<Opcao, OpcEdicaoDTO>();
            CreateMap<OpcCriacaoDTO, Opcao>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<OpcEdicaoDTO, Opcao>();
        }
    }
}
