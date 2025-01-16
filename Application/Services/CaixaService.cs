using WebMesaGestor.Application.DTO.Input.Caixa;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.Map;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Infra.Repositories;

namespace WebMesaGestor.Application.Services
{
    public class CaixaService
    {
        private readonly ICaixaRepository _caixaRepository; 
        private readonly IUsuarioRepository _usuarioRepository;


        public CaixaService(IUsuarioRepository usuarioRepository, ICaixaRepository caixaRepository)
        {
            _caixaRepository = caixaRepository;
            _usuarioRepository = usuarioRepository;

        }

        public async Task<IEnumerable<CaiOutputDTO>> ListarCaixas()
        {
            IEnumerable<Caixa> caixas = await _caixaRepository.ListarCaixas();
            foreach (var caixa in caixas)
            {
                if (caixa.UsuarioId != null)
                {
                    caixa.Usuario = await _usuarioRepository.UsuarioPorId((Guid)caixa.UsuarioId);
                }
            }
            return CaixaMap.MapCaixa(caixas);
        }

        public async Task<CaiOutputDTO> CaixaPorId(Guid id)
        {
            Caixa caixa = await _caixaRepository.CaixaPorId(id);
            if (caixa.UsuarioId != null)
            {
                caixa.Usuario = await _usuarioRepository.UsuarioPorId((Guid)caixa.UsuarioId);
            }
            return CaixaMap.MapCaixa(caixa);
        }

        public async Task<CaiOutputDTO> AbrirCaixa(CaiAbrirDTO caixa)
        {
            Caixa map = CaixaMap.MapCaixa(caixa);
            Caixa retorno = await _caixaRepository.AbrirCaixa(map);
            return CaixaMap.MapCaixa(retorno);
        }

        public async Task<CaiOutputDTO> FecharCaixa(CaiFecharDTO caixa)
        {
            Caixa buscarCaixa = await _caixaRepository.CaixaPorId(caixa.Id);

            buscarCaixa.CaiValFechamento = caixa.CaiValFechamento;
            
            Caixa retorno = await _caixaRepository.FecharCaixa(buscarCaixa);
            return CaixaMap.MapCaixa(retorno);
        }

        public async Task<CaiOutputDTO> DeletarCaixa(Guid id)
        {
            Caixa retorno = await _caixaRepository.DeletarCaixa(id);
            return CaixaMap.MapCaixa(retorno);
        }
    }
}
