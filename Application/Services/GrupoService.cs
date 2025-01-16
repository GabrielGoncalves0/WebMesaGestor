using WebMesaGestor.Application.DTO.Input.Grupo;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.Map;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;

namespace WebMesaGestor.Application.Services
{
    public class GrupoService
    {
        private readonly IGrupoRepository _grupoRepository;

        public GrupoService(IGrupoRepository grupoRepository)
        {
            _grupoRepository = grupoRepository;
        }

        public async Task<IEnumerable<GrupOutputDTO>> ListarGrupos()
        {
            IEnumerable<Grupo> grupos = await _grupoRepository.ListarGrupos();
            return GrupoMap.MapGrupo(grupos);
        }

        public async Task<GrupOutputDTO> GrupoPorId(Guid id)
        {
            Grupo grupo = await _grupoRepository.GrupoPorId(id);
            return GrupoMap.MapGrupo(grupo);
        }

        public async Task<GrupOutputDTO> CriarGrupo(GrupCriacaoDTO grupo)
        {
            Grupo map = GrupoMap.MapGrupo(grupo);
            Grupo retorno = await _grupoRepository.CriarGrupo(map);
            return GrupoMap.MapGrupo(retorno);
        }

        public async Task<GrupOutputDTO> AtualizarGrupo(GrupEdicaoDTO grupo)
        {
            Grupo buscarGrupo = await _grupoRepository.GrupoPorId(grupo.Id);

            buscarGrupo.GrupDesc = grupo.GrupDesc;

            Grupo retorno = await _grupoRepository.AtualizarGrupo(buscarGrupo);
            return GrupoMap.MapGrupo(retorno);
        }

        public async Task<GrupOutputDTO> DeletarGrupo(Guid id)
        {
            Grupo retorno = await _grupoRepository.DeletarGrupo(id);
            return GrupoMap.MapGrupo(retorno);
        }
    }
}
