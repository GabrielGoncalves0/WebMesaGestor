using Microsoft.AspNetCore.Mvc;
using WebMesaGestor.Application.DTO.Input.OpcaoProPed;
using WebMesaGestor.Application.DTO.Input.USuario;
using WebMesaGestor.Application.Services;

namespace WebMesaGestor.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpcaoProPedController : ControllerBase
    {
        private readonly OpcaoProPedService _opcaoProPedService;

        public OpcaoProPedController(OpcaoProPedService opcaoProPedService)
        {
            _opcaoProPedService = opcaoProPedService;
        }

        [HttpGet]
        [Route("buscarOpcProdPed/{id}")]
        public async Task<IActionResult> GetAll([FromRoute] Guid id)
        {
            var opcaoProPed = await _opcaoProPedService.ListarOpcoesPorProPedId(id);
            return Ok(opcaoProPed);
        }

        [HttpPost]
        [Route("cadastrar")]
        public async Task<IActionResult> Post([FromBody] OpcProPedCriacaoDTO opcProPed)
        {
            var result = await _opcaoProPedService.CriarOpcaoProPed(opcProPed);
            return Ok(result);
        }


        [HttpPut]
        [Route("atualizar")]
        public async Task<IActionResult> Put([FromBody] OpcProPedEdicaoDTO opcProPed)
        {
            var result = await _opcaoProPedService.AtualizarOpcaoProPed(opcProPed);
            return Ok(result);
        }

        [HttpGet]
        [Route("buscar/{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var opcProPed = await _opcaoProPedService.OpcaoProPedId(id);
            return Ok(opcProPed);
        }

        [HttpDelete]
        [Route("deletar/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var opcProPed = await _opcaoProPedService.DeletarOpcProPed(id);
            return Ok(opcProPed);
        }
    }
}
