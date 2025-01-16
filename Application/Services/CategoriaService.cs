using WebMesaGestor.Application.DTO.Input.Categoria;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.Map;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;

namespace WebMesaGestor.Application.Services
{
    public class CategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<IEnumerable<CatOutputDTO>> ListarCategorias()
        {
            IEnumerable<Categoria> categorias = await _categoriaRepository.ListarCategorias();
            return CategoriaMap.MapCategoria(categorias);
        }

        public async Task<CatOutputDTO> CategoriaPorId(Guid id)
        {
            Categoria categoria = await _categoriaRepository.CategoriaPorId(id);
            return CategoriaMap.MapCategoria(categoria);
        }

        public async Task<CatOutputDTO> CriarCategoria(CatCriacaoDTO categoria)
        {
            Categoria map = CategoriaMap.MapCategoria(categoria);
            Categoria retorno = await _categoriaRepository.CriarCategoria(map);
            return CategoriaMap.MapCategoria(retorno);
        }

        public async Task<CatOutputDTO> AtualizarCategoria(CatEdicaoDTO categoria)
        {
            Categoria buscarCategoria = await _categoriaRepository.CategoriaPorId(categoria.Id);

            buscarCategoria.CatDesc = categoria.CatDesc;

            Categoria retorno = await _categoriaRepository.AtualizarCategoria(buscarCategoria);
            return CategoriaMap.MapCategoria(retorno);
        }

        public async Task<CatOutputDTO> DeletarCategoria(Guid id)
        {
            Categoria retorno = await _categoriaRepository.DeletarCategoria(id);
            return CategoriaMap.MapCategoria(retorno);
        }
    }

}
