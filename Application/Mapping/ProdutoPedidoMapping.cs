using AutoMapper;
using WebMesaGestor.Application.DTO.Input.ProdutoPedido;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.Mapping
{
    public class ProdutoPedidoMapping : Profile
    {
        public ProdutoPedidoMapping()
        {
            CreateMap<ProdutoPedido, ProdutoPedidoDTO>();
            CreateMap<ProdutoPedido, ProPedCriacaoDTO>();
            CreateMap<ProdutoPedido, ProPedEdicaoDTO>();
            CreateMap<ProPedCriacaoDTO, ProdutoPedido>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<ProPedEdicaoDTO, ProdutoPedido>();
        }
    }
}
