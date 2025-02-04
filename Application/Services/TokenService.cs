using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebMesaGestor.Application.DTO.Input.Auth;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.Map;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;

namespace WebMesaGestor.Application.Services
{
    public class TokenService : ITokenRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioRepository _usuarioRepository;

        public TokenService(IConfiguration configuration, IUsuarioRepository usuarioRepository)
        {
            _configuration = configuration;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<string> GenerateToken(LoginDTO login)
        {
            var user = await _usuarioRepository.UsuarioPorEmail(login.Email);

            if (user.UsuEmail != login.Email || login.Password != user.UsuSenha)
            {
                return String.Empty;
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"] ?? string.Empty));
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UsuNome),
                    new Claim(ClaimTypes.Email, user.UsuEmail),
                    new Claim(ClaimTypes.Role, user.UsuTipo.ToString())
                },
                expires: DateTime.Now.AddHours(8),
                signingCredentials: signingCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return token;
        }
    }
}
