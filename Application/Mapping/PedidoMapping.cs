using AutoMapper;
using WebMesaGestor.Application.DTO.Input.Pedido;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.Mapping
{
    public class PedidoMapping : Profile
    {
        public PedidoMapping()
        {
            CreateMap<Pedido, PedidoDTO>();
            CreateMap<Pedido, PedCriacaoDTO>();
            CreateMap<Pedido, PedEdicaoDTO>();
            CreateMap<PedCriacaoDTO, Pedido>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<PedEdicaoDTO, Pedido>();
        }
    }
}
