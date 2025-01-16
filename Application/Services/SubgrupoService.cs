using WebMesaGestor.Application.DTO.Input.Subgrupo;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.Map;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;

namespace WebMesaGestor.Application.Services
{
    public class SubgrupoService
    {
        private readonly ISubgrupoRepository _subgrupoRepository;

        public SubgrupoService(ISubgrupoRepository subgrupoRepository)
        {
            _subgrupoRepository = subgrupoRepository;
        }

        public async Task<IEnumerable<SubgrupOutputDTO>> ListarSubgrupos()
        {
            IEnumerable<Subgrupo> subgrupos = await _subgrupoRepository.ListarSubgrupos();
            return SubgrupoMap.MapSubgrupo(subgrupos);
        }

        public async Task<SubgrupOutputDTO> SubgrupoPorId(Guid id)
        {
            Subgrupo subgrupo = await _subgrupoRepository.SubgrupoPorId(id);
            return SubgrupoMap.MapSubgrupo(subgrupo);
        }

        public async Task<SubgrupOutputDTO> CriarSubgrupo(SubgrupCriacaoDTO subgrupo)
        {
            Subgrupo map = SubgrupoMap.MapSubgrupo(subgrupo);
            Subgrupo retorno = await _subgrupoRepository.CriarSubgrupo(map);
            return SubgrupoMap.MapSubgrupo(retorno);
        }

        public async Task<SubgrupOutputDTO> AtualizarSubgrupo(SubgrupEdicaoDTO subgrupo)
        {
            Subgrupo buscarSubgrupo = await _subgrupoRepository.SubgrupoPorId(subgrupo.Id);

            buscarSubgrupo.SubgruDesc = subgrupo.SubgruDesc;

            Subgrupo retorno = await _subgrupoRepository.AtualizarSubgrupo(buscarSubgrupo);
            return SubgrupoMap.MapSubgrupo(retorno);
        }

        public async Task<SubgrupOutputDTO> DeletarSubgrupo(Guid id)
        {
            Subgrupo retorno = await _subgrupoRepository.DeletarSubgrupo(id);
            return SubgrupoMap.MapSubgrupo(retorno);
        }
    }
}
