using WebMesaGestor.Application.DTO.Input.Opcoes;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.Map;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;

namespace WebMesaGestor.Application.Services
{
    public class OpcaoService
    {
        private readonly IOpcaoRepository _opcaoRepository;
        private readonly IGrupoOpcaoRepository _grupoOpcoesRepository;

        public OpcaoService(IOpcaoRepository opcaoRepository, IGrupoOpcaoRepository grupoOpcoesRepository)
        {
            _opcaoRepository = opcaoRepository;
            _grupoOpcoesRepository = grupoOpcoesRepository;
        }

        public async Task<Response<IEnumerable<OpcOutputDTO>>> ListarOpcoes()
        {
            Response<IEnumerable<OpcOutputDTO>> resposta = new Response<IEnumerable<OpcOutputDTO>>();
            try
            {
                IEnumerable<Opcao> opcoes = await _opcaoRepository.ListarOpcoes();
                foreach (var opcao in opcoes)
                {
                    if (opcao.GrupoOpcoesId != null)
                    {
                        opcao.GrupoOpcoes = await _grupoOpcoesRepository.GrupoOpcaoPorId((Guid)opcao.GrupoOpcoesId);
                    }
                }
                resposta.Dados = OpcaoMap.MapOpcao(opcoes);
                resposta.Mensagem = "Opções listadas com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<OpcOutputDTO> OpcaoPorId(Guid id)
        {
            Opcao opcao = await _opcaoRepository.OpcaoPorId(id);
            if (opcao.GrupoOpcoesId != null)
            {
                opcao.GrupoOpcoes = await _grupoOpcoesRepository.GrupoOpcaoPorId((Guid)opcao.GrupoOpcoesId);
            }
            return OpcaoMap.MapOpcao(opcao);
        }

        public async Task<OpcOutputDTO> CriarOpcao(OpcCriacaoDTO opcao)
        {
            Opcao map = OpcaoMap.MapOpcao(opcao);
            Opcao retorno = await _opcaoRepository.CriarOpcao(map);
            return OpcaoMap.MapOpcao(retorno);
        }

        public async Task<OpcOutputDTO> AtualizarOpcao(OpcEdicaoDTO opcao)
        {
            Opcao buscarOpcao = await _opcaoRepository.OpcaoPorId(opcao.Id);

            buscarOpcao.OpcaoDesc = opcao.OpcaoDesc;
            buscarOpcao.OpcaoValor = opcao.OpcaoValor;
            buscarOpcao.OpcaoQuantMax = opcao.OpcaoQuantMax;
            buscarOpcao.GrupoOpcoesId = opcao.GrupoOpcoesId;

            Opcao retorno = await _opcaoRepository.AtualizarOpcao(buscarOpcao);
            return OpcaoMap.MapOpcao(retorno);
        }

        public async Task<OpcOutputDTO> DeletarOpcao(Guid id)
        {
            Opcao retorno = await _opcaoRepository.DeletarOpcao(id);
            return OpcaoMap.MapOpcao(retorno);
        }
    }
}
