using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMesaGestor.Application.DTO.Input.Empresa;
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
        [Route("buscarTodos")]
        public async Task<IActionResult> Get()
        {
            var empresa = await _empresaService.ListarEmpresas();
            return Ok(empresa);
        }

        [HttpPost]
        [Route("cadastrar")]
        public async Task<IActionResult> Post([FromBody] EmpCriacaoDTO empCriacaoDTO)
        {
            var empresa = await _empresaService.CriarEmpresa(empCriacaoDTO);
            return Ok(empresa);
        }


        [HttpPut]
        [Route("atualizar")]
        public async Task<IActionResult> Put([FromBody] EmpEdicaoDTO empEdicaoDTO)
        {
            var empresa = await _empresaService.AtualizarEmpresa(empEdicaoDTO);
            return Ok(empresa);
        }

        [HttpGet]
        [Route("buscar/{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var empresa = await _empresaService.EmpresaPorId(id);
            return Ok(empresa);
        }

        [HttpDelete]
        [Route("deletar/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var empresa = await _empresaService.DeletarEmpresa(id);
            return Ok(empresa);
        }
    }
}
