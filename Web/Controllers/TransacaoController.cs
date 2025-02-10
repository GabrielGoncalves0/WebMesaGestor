//using Microsoft.AspNetCore.Mvc;
//using WebMesaGestor.Application.DTO.Input.Transacao;
//using WebMesaGestor.Application.DTO.Input.USuario;
//using WebMesaGestor.Application.Services;

//namespace WebMesaGestor.Web.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class TransacaoController : ControllerBase
//    {
//        private readonly TransacaoService _transacaoService;

//        public TransacaoController(TransacaoService transacaoService)
//        {
//            _transacaoService = transacaoService;
//        }

//        [HttpGet]
//        [Route("buscarTodos")]
//        public async Task<IActionResult> Get()
//        {
//            var transacao = await _transacaoService.ListarTrasacoes();
//            return Ok(transacao);
//        }

//        [HttpPost]
//        [Route("cadastrar")]
//        public async Task<IActionResult> Post([FromBody] TraCriacaoDTO traCriacaoDTO)
//        {
//            var transacao = await _transacaoService.CriarTransacao(traCriacaoDTO);
//            return Ok(transacao);
//        }

//        [HttpGet]
//        [Route("buscar/{id}")]
//        public async Task<IActionResult> Get([FromRoute] Guid id)
//        {
//            var transacao = await _transacaoService.TransacaoPorId(id);
//            return Ok(transacao);
//        }

//        [HttpDelete]
//        [Route("deletar/{id}")]
//        public async Task<IActionResult> Delete([FromRoute] Guid id)
//        {
//            var transacao = await _transacaoService.DeletarTransacao(id);
//            return Ok(transacao);
//        }
//    }
//}
