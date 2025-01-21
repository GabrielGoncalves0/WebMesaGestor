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

        public async Task<Response<IEnumerable<CatOutputDTO>>> ListarCategorias()
        {
            Response<IEnumerable<CatOutputDTO>> resposta = new Response<IEnumerable<CatOutputDTO>>();
            try
            {
                IEnumerable<Categoria> categorias = await _categoriaRepository.ListarCategorias();
                resposta.Dados = CategoriaMap.MapCategoria(categorias);
                resposta.Mensagem = "Categorias listadas com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
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
