using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMesaGestor.Application.DTO.Input;
using WebMesaGestor.Application.Services;

namespace WebMesaGestor.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly EmpresaService _empresaService;

        public EmpresaController(EmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _empresaService.ListarEmpresas());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmpCriacaoDTO empresa)
        {
            return Ok(await _empresaService.CriarEmpresa(empresa));
        }


        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EmpEdicaoDTO empresa)
        {
            return Ok(await _empresaService.AtualizarEmpresa(empresa));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            return Ok(await _empresaService.EmpresaPorId(id));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            return Ok(await _empresaService.DeletarEmpresa(id));
        }
    }
}
