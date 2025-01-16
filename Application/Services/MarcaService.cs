using WebMesaGestor.Application.DTO.Input.Marca;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.Map;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;

namespace WebMesaGestor.Application.Services
{
    public class MarcaService
    {
        private readonly IMarcaRepository _marcaRepository;

        public MarcaService(IMarcaRepository marcaRepository)
        {
            _marcaRepository = marcaRepository;
        }

        public async Task<IEnumerable<MarOutputDTO>> ListarMarcas()
        {
            IEnumerable<Marca> marcas = await _marcaRepository.ListarMarcas();
            return MarcaMap.MapMarca(marcas);
        }

        public async Task<MarOutputDTO> MarcaPorId(Guid id)
        {
            Marca marca = await _marcaRepository.MarcaPorId(id);
            return MarcaMap.MapMarca(marca);
        }

        public async Task<MarOutputDTO> CriarMarca(MarCriacaoDTO marca)
        {
            Marca map = MarcaMap.MapMarca(marca);
            Marca retorno = await _marcaRepository.CriarMarca(map);
            return MarcaMap.MapMarca(retorno);
        }

        public async Task<MarOutputDTO> AtualizarMarca(MarEdicaoDTO marca)
        {
            Marca buscarMarca = await _marcaRepository.MarcaPorId(marca.Id);

            buscarMarca.MarNome = marca.MarNome;

            Marca retorno = await _marcaRepository.AtualizarMarca(buscarMarca);
            return MarcaMap.MapMarca(retorno);
        }

        public async Task<MarOutputDTO> DeletarMarca(Guid id)
        {
            Marca retorno = await _marcaRepository.DeletarMarca(id);
            return MarcaMap.MapMarca(retorno);
        }
    }
}
