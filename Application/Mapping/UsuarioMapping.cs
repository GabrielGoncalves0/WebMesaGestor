using AutoMapper;
using WebMesaGestor.Application.DTO.Input.Usuario;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.Mapping
{
    public class UsuarioMapping : Profile
    {
        public UsuarioMapping()
        {
            CreateMap<Usuario, UsuarioDTO>();
            CreateMap<Usuario, UsuCriacaoDTO>();
            CreateMap<Usuario, UsuEdicaoDTO>();
            CreateMap<UsuCriacaoDTO, Usuario>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<UsuEdicaoDTO, Usuario>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
