using WebMesaGestor.Application.DTO.Input.Auth;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Domain.Interfaces
{
    public interface ITokenRepository
    {
        Task<string> GenerateToken(LoginDTO login);
    }
}
