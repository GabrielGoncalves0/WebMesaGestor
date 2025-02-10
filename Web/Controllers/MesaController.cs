//using Microsoft.AspNetCore.Mvc;
//using WebMesaGestor.Application.DTO.Input.Mesa;
//using WebMesaGestor.Application.Services;

//namespace WebMesaGestor.Web.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class MesaController : ControllerBase
//    {
//        private readonly MesaService _mesaService;

//        public MesaController(MesaService mesaService)
//        {
//            _mesaService = mesaService;
//        }

//        [HttpGet]
//        [Route("buscarTodos")]
//        public async Task<IActionResult> Get()
//        {
//            var mesa = await _mesaService.ListarMesas();
//            return Ok(mesa);
//        }

//        [HttpPost]
//        [Route("cadastrar")]
//        public async Task<IActionResult> Post([FromBody] MesCriacaoDTO mesCriacaoDTO)
//        {
//            var mesa = await _mesaService.CriarMesa(mesCriacaoDTO);
//            return Ok(mesa);
//        }


//        [HttpPut]
//        [Route("atualizar")]
//        public async Task<IActionResult> Put([FromBody] MesEdicaoDTO mesEdicaoDTO)
//        {
//            var mesa = await _mesaService.AtualizarMesa(mesEdicaoDTO);
//            return Ok(mesa);
//        }

//        [HttpGet]
//        [Route("buscar/{id}")]
//        public async Task<IActionResult> Get([FromRoute] Guid id)
//        {
//            var mesa = await _mesaService.MesaPorId(id);
//            return Ok(mesa);
//        }

//        [HttpDelete]
//        [Route("deletar/{id}")]
//        public async Task<IActionResult> Delete([FromRoute] Guid id)
//        {
//            var mesa = await _mesaService.DeletarMesa(id);
//            return Ok(mesa);
//        }
//    }
//}
