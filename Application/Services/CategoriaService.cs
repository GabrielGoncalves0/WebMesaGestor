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

        public async Task<Response<CatOutputDTO>> CategoriaPorId(Guid id)
        {
            Response<CatOutputDTO> resposta = new Response<CatOutputDTO>();
            try
            {
                Categoria categoria = await _categoriaRepository.CategoriaPorId(id);

                resposta.Dados = CategoriaMap.MapCategoria(categoria);
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

        public async Task<Response<CatOutputDTO>> CriarCategoria(CatCriacaoDTO categoria)
        {
            Response<CatOutputDTO> resposta = new Response<CatOutputDTO>();
            try
            {
                Categoria map = CategoriaMap.MapCategoria(categoria);
                Categoria retorno = await _categoriaRepository.CriarCategoria(map);

                resposta.Dados = CategoriaMap.MapCategoria(retorno);
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

        public async Task<Response<CatOutputDTO>> AtualizarCategoria(CatEdicaoDTO categoria)
        {
            Response<CatOutputDTO> resposta = new Response<CatOutputDTO>();
            try
            {
                Categoria buscarCategoria = await _categoriaRepository.CategoriaPorId(categoria.Id);
                buscarCategoria.CatDesc = categoria.CatDesc;
                Categoria retorno = await _categoriaRepository.AtualizarCategoria(buscarCategoria);

                resposta.Dados = CategoriaMap.MapCategoria(retorno);
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

        public async Task<Response<CatOutputDTO>> DeletarCategoria(Guid id)
        {
            Response<CatOutputDTO> resposta = new Response<CatOutputDTO>();
            try
            {
                Categoria retorno = await _categoriaRepository.DeletarCategoria(id);

                resposta.Dados = CategoriaMap.MapCategoria(retorno);
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
    }

}
