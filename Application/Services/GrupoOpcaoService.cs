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

        public async Task<IEnumerable<GrupOpcOutputDTO>> ListarGrupoOpcoes()
        {
            IEnumerable<GrupoOpcoes> grupos = await _grupoOpcaoRepository.ListarGrupoOpcoes();
            return GrupoOpcaoMap.MapGrupoOpcao(grupos);
        }

        public async Task<GrupOpcOutputDTO> GrupoOpcaoPorId(Guid id)
        {
            GrupoOpcoes grupoOpcao = await _grupoOpcaoRepository.GrupoOpcaoPorId(id);
            return GrupoOpcaoMap.MapGrupoOpcao(grupoOpcao);
        }

        public async Task<GrupOpcOutputDTO> CriarGrupoOpcao(GrupOpcCriacaoDTO grupoOpcao)
        {
            GrupoOpcoes map = GrupoOpcaoMap.MapGrupoOpcao(grupoOpcao);
            GrupoOpcoes retorno = await _grupoOpcaoRepository.CriarGrupoOpcao(map);
            return GrupoOpcaoMap.MapGrupoOpcao(retorno);
        }

        public async Task<GrupOpcOutputDTO> AtualizarGrupoOpcao(GrupOpcEdicaoDTO grupoOpcao)
        {
            GrupoOpcoes buscarGrupoOpcao = await _grupoOpcaoRepository.GrupoOpcaoPorId(grupoOpcao.Id);

            buscarGrupoOpcao.GrupOpcDesc = grupoOpcao.GrupOpcDesc;
            buscarGrupoOpcao.GrupOpcTipo = grupoOpcao.GrupOpcTipo;
            buscarGrupoOpcao.GrupOpcMax = grupoOpcao.GrupOpcMax;
            buscarGrupoOpcao.ProdutoId = grupoOpcao.ProdutoId;


            GrupoOpcoes retorno = await _grupoOpcaoRepository.AtualizarGrupoOpcao(buscarGrupoOpcao);
            return GrupoOpcaoMap.MapGrupoOpcao(retorno);
        }

        public async Task<GrupOpcOutputDTO> DeletarGrupoOpcao(Guid id)
        {
            GrupoOpcoes retorno = await _grupoOpcaoRepository.DeletarGrupoOpcao(id);
            return GrupoOpcaoMap.MapGrupoOpcao(retorno);
        }
    }
}
