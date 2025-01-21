using WebMesaGestor.Application.DTO.Input.Grupo;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.Map;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;

namespace WebMesaGestor.Application.Services
{
    public class GrupoOpcaoService
    {
        private readonly IGrupoOpcaoRepository _grupoOpcaoRepository;

        public GrupoOpcaoService(IGrupoOpcaoRepository grupoRepository)
        {
            _grupoOpcaoRepository = grupoRepository;
        }

        public async Task<Response<IEnumerable<GrupOpcOutputDTO>>> ListarGrupoOpcoes()
        {
            Response<IEnumerable<GrupOpcOutputDTO>> resposta = new Response<IEnumerable<GrupOpcOutputDTO>>();
            try
            {
                IEnumerable<GrupoOpcoes> grupos = await _grupoOpcaoRepository.ListarGrupoOpcoes();
                resposta.Dados = GrupoOpcaoMap.MapGrupoOpcao(grupos);
                resposta.Mensagem = "Grupos listados com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<GrupOpcOutputDTO>> GrupoOpcaoPorId(Guid id)
        {
            Response<GrupOpcOutputDTO> resposta = new Response<GrupOpcOutputDTO>();
            try
            {
                GrupoOpcoes grupoOpcao = await _grupoOpcaoRepository.GrupoOpcaoPorId(id);

                resposta.Dados = GrupoOpcaoMap.MapGrupoOpcao(grupoOpcao);
                resposta.Mensagem = "Grupo encontrado com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<GrupOpcOutputDTO>> CriarGrupoOpcao(GrupOpcCriacaoDTO grupoOpcao)
        {
            Response<GrupOpcOutputDTO> resposta = new Response<GrupOpcOutputDTO>();
            try
            {
                GrupoOpcoes map = GrupoOpcaoMap.MapGrupoOpcao(grupoOpcao);
                GrupoOpcoes retorno = await _grupoOpcaoRepository.CriarGrupoOpcao(map);

                resposta.Dados = GrupoOpcaoMap.MapGrupoOpcao(retorno);
                resposta.Mensagem = "Grupo criado com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<GrupOpcOutputDTO>> AtualizarGrupoOpcao(GrupOpcEdicaoDTO grupoOpcao)
        {
            Response<GrupOpcOutputDTO> resposta = new Response<GrupOpcOutputDTO>();
            try
            {
                GrupoOpcoes buscarGrupoOpcao = await _grupoOpcaoRepository.GrupoOpcaoPorId(grupoOpcao.Id);
                buscarGrupoOpcao.GrupOpcDesc = grupoOpcao.GrupOpcDesc;
                buscarGrupoOpcao.GrupOpcTipo = grupoOpcao.GrupOpcTipo;
                buscarGrupoOpcao.GrupOpcMax = grupoOpcao.GrupOpcMax;
                buscarGrupoOpcao.ProdutoId = grupoOpcao.ProdutoId;
                GrupoOpcoes retorno = await _grupoOpcaoRepository.AtualizarGrupoOpcao(buscarGrupoOpcao);

                resposta.Dados = GrupoOpcaoMap.MapGrupoOpcao(retorno);
                resposta.Mensagem = "Grupo atualizado com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<GrupOpcOutputDTO>> DeletarGrupoOpcao(Guid id)
        {
            Response<GrupOpcOutputDTO> resposta = new Response<GrupOpcOutputDTO>();
            try
            {
                GrupoOpcoes retorno = await _grupoOpcaoRepository.DeletarGrupoOpcao(id);
                resposta.Dados = GrupoOpcaoMap.MapGrupoOpcao(retorno);
                resposta.Mensagem = "Grupo deletado com sucesso";
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
