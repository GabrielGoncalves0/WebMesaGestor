using Microsoft.AspNetCore.Mvc;
using WebMesaGestor.Application.DTO.Input.Caixa;
using WebMesaGestor.Application.Services;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaixaController : ControllerBase
    {
        private readonly CaixaService _caixaService;

        public CaixaController(CaixaService caixaService)
        {
            _caixaService = caixaService;
        }

        [HttpGet]
        [Route("buscar/{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var caixa = await _caixaService.CaixaPorId(id);
            return Ok(caixa);
        }

        [HttpGet]
        [Route("buscarTodos")]
        public async Task<IActionResult> Get()
        {
            var caixa = await _caixaService.ListarCaixas();
            return Ok(caixa);
        }

        [HttpPost]
        [Route("abrir")]
        public async Task<IActionResult> Post([FromBody] CaiAbrirDTO caiAbrirDTO)
        {
            var caixa = await _caixaService.AbrirCaixa(caiAbrirDTO);
            return Ok(caixa);
        }

        [HttpPut]
        [Route("fechar")]
        public async Task<IActionResult> Put([FromBody] CaiFecharDTO caiFecharDTO)
        {
            var caixa = await _caixaService.FecharCaixa(caiFecharDTO);
            return Ok(caixa);
        }

        [HttpPut]
        [Route("atualizar")]
        public async Task<IActionResult> PutValor([FromBody] CaiAtualizarDTO caiAtualizarDTO)
        {
            var caixa = await _caixaService.AtualizarCaixa(caiAtualizarDTO);
            return Ok(caixa);
        }

        [HttpDelete]
        [Route("deletar/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var caixa = await _caixaService.DeletarCaixa(id);
            return Ok(caixa );
        }

        [HttpGet]
        [Route("reabrirUltimo")]
        public async Task<IActionResult> GetUltimo()
        {
            var caixa = await _caixaService.ReabrirUltimoCaixa();
            return Ok(caixa);
        }

        [HttpPut]
        [Route("sangria/{id}")]
        public async Task<IActionResult> SangriaCaixa(Guid id, decimal valor) 
        {
            var caixa = await _caixaService.SangriaCaixa(id, valor);
            return Ok(caixa);
        }

        [HttpPut]
        [Route("suprimento/{id}")]
        public async Task<IActionResult> SuprimentoCaixa(Guid id, decimal valor)
        {
            var caixa = await _caixaService.SuprimentoCaixa(id, valor);
            return Ok(caixa);
        }

    }
}
