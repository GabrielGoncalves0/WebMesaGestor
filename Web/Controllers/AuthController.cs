//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using WebMesaGestor.Application.DTO.Input.Auth;
//using WebMesaGestor.Domain.Interfaces;

//namespace WebMesaGestor.Web.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
//        private readonly ITokenRepository _tokenRepository;

//        public AuthController(ITokenRepository tokenRepository)
//        {
//            _tokenRepository = tokenRepository;
//        }

//        [HttpPost]
//        [Route("login")]
//        public async Task<IActionResult> Login(LoginDTO login)
//        {
//            var token = await _tokenRepository.GenerateToken(login);

//            if (string.IsNullOrEmpty(token))
//            {
//                return Unauthorized("Usuário não autorizado");
//            }
//            return Ok(token);
//        }
//    }
//}
