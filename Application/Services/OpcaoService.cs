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

        public async Task<Response<OpcOutputDTO>> OpcaoPorId(Guid id)
        {
            Response<OpcOutputDTO> resposta = new Response<OpcOutputDTO>();
            try
            {
                Opcao opcao = await _opcaoRepository.OpcaoPorId(id);
                if (opcao.GrupoOpcoesId != null)
                {
                    opcao.GrupoOpcoes = await _grupoOpcoesRepository.GrupoOpcaoPorId((Guid)opcao.GrupoOpcoesId);
                }
                resposta.Dados = OpcaoMap.MapOpcao(opcao);
                resposta.Mensagem = "Opcao encontrada com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<OpcOutputDTO>> CriarOpcao(OpcCriacaoDTO opcao)
        {
            Response<OpcOutputDTO> resposta = new Response<OpcOutputDTO>();
            try
            {
                Opcao map = OpcaoMap.MapOpcao(opcao);
                Opcao retorno = await _opcaoRepository.CriarOpcao(map);

                resposta.Dados = OpcaoMap.MapOpcao(retorno);
                resposta.Mensagem = "Opcao criada com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<OpcOutputDTO>> AtualizarOpcao(OpcEdicaoDTO opcao)
        {
            Response<OpcOutputDTO> resposta = new Response<OpcOutputDTO>();
            try
            {
                Opcao buscarOpcao = await _opcaoRepository.OpcaoPorId(opcao.Id);
                buscarOpcao.OpcaoDesc = opcao.OpcaoDesc;
                buscarOpcao.OpcaoValor = opcao.OpcaoValor;
                buscarOpcao.OpcaoQuantMax = opcao.OpcaoQuantMax;
                buscarOpcao.GrupoOpcoesId = opcao.GrupoOpcoesId;
                Opcao retorno = await _opcaoRepository.AtualizarOpcao(buscarOpcao);

                resposta.Dados = OpcaoMap.MapOpcao(retorno);
                resposta.Mensagem = "Opcao atualizada com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<OpcOutputDTO>> DeletarOpcao(Guid id)
        {
            Response<OpcOutputDTO> resposta = new Response<OpcOutputDTO>();
            try
            {
                Opcao retorno = await _opcaoRepository.DeletarOpcao(id);

                resposta.Dados = OpcaoMap.MapOpcao(retorno);
                resposta.Mensagem = "Opcao deletada com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }
    }
}
