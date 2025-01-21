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

        public async Task<Response<IEnumerable<CaiOutputDTO>>> ListarCaixas()
        {
            Response<IEnumerable<CaiOutputDTO>> resposta = new Response<IEnumerable<CaiOutputDTO>>();
            try
            {
                IEnumerable<Caixa> caixas = await _caixaRepository.ListarCaixas();
                foreach (var caixa in caixas)
                {
                    if (caixa.UsuarioId != null)
                    {
                        caixa.Usuario = await _usuarioRepository.UsuarioPorId((Guid)caixa.UsuarioId);
                    }
                }
                resposta.Dados = CaixaMap.MapCaixa(caixas);
                resposta.Mensagem = "Caixas listados com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
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
