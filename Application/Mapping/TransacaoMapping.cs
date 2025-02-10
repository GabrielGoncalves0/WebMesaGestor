using AutoMapper;
using WebMesaGestor.Application.DTO.Input.Transacao;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.Mapping
{
    public class TransacaoMapping : Profile
    {
        public TransacaoMapping()
        {
            CreateMap<Transacao, TransacaoDTO>();
            CreateMap<Transacao, TraCriacaoDTO>();
            CreateMap<Transacao, TraEdicaoDTO>();
            CreateMap<TraCriacaoDTO, Transacao>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<TraEdicaoDTO, Transacao>();
        }
    }
}
