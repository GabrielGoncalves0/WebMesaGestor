//using WebMesaGestor.Application.Common;
//using WebMesaGestor.Application.DTO.Input.Categoria;
//using WebMesaGestor.Application.DTO.Input.Empresa;
//using WebMesaGestor.Application.DTO.Output;
//using WebMesaGestor.Application.Map;
//using WebMesaGestor.Domain.Entities;
//using WebMesaGestor.Domain.Interfaces;
//using WebMesaGestor.Utils;

//namespace WebMesaGestor.Application.Services
//{
//    public class CategoriaService
//    {
//        private readonly ICategoriaRepository _categoriaRepository;

//        public CategoriaService(ICategoriaRepository categoriaRepository)
//        {
//            _categoriaRepository = categoriaRepository;
//        }

//        public async Task<Response<IEnumerable<CategoriaDTO>>> ListarCategorias()
//        {
//            Response<IEnumerable<CategoriaDTO>> resposta = new Response<IEnumerable<CategoriaDTO>>();
//            try
//            {
//                IEnumerable<Categoria> categorias = await _categoriaRepository.ListarCategorias();
//                if (categorias == null || !categorias.Any())
//                {
//                    resposta.Mensagem = "Nenhuma categoria encontrada.";
//                    resposta.Status = false;
//                    return resposta;
//                }
//                resposta.Dados = CategoriaMap.MapCategoria(categorias);
//                resposta.Mensagem = "Categorias listadas com sucesso";
//                return resposta;
//            }
//            catch (Exception ex)
//            {
//                resposta.Mensagem = ex.Message;
//                resposta.Status = false;
//                return resposta;
//            }
//        }

//        public async Task<Response<CategoriaDTO>> CategoriaPorId(Guid id)
//        {
//            Response<CategoriaDTO> resposta = new Response<CategoriaDTO>();
//            try
//            {
//                Categoria categoria = await _categoriaRepository.CategoriaPorId(id);
//                if (categoria == null)
//                {
//                    resposta.Mensagem = "Categoria não encontrada.";
//                    resposta.Status = false;
//                    return resposta;
//                }
//                resposta.Dados = CategoriaMap.MapCategoria(categoria);
//                resposta.Mensagem = "Categorias listadas com sucesso";
//                return resposta;
//            }
//            catch (Exception ex)
//            {
//                resposta.Mensagem = ex.Message;
//                resposta.Status = false;
//                return resposta;
//            }
//        }

//        public async Task<Response<CategoriaDTO>> CriarCategoria(CatCriacaoDTO categoria)
//        {
//            Response<CategoriaDTO> resposta = new Response<CategoriaDTO>();
//            try
//            {
//                ValidarCategoriaCriacao(categoria);
//                Categoria map = CategoriaMap.MapCategoria(categoria);
//                Categoria retorno = await _categoriaRepository.CriarCategoria(map);

//                resposta.Dados = CategoriaMap.MapCategoria(retorno);
//                resposta.Mensagem = "Categorias listadas com sucesso";
//                return resposta;
//            }
//            catch (Exception ex)
//            {
//                resposta.Mensagem = ex.Message;
//                resposta.Status = false;
//                return resposta;
//            }
//        }

//        public async Task<Response<CategoriaDTO>> AtualizarCategoria(CatEdicaoDTO categoria)
//        {
//            Response<CategoriaDTO> resposta = new Response<CategoriaDTO>();
//            try
//            {
//                ValidarCategoriaEdicao(categoria);
//                Categoria buscarCategoria = await _categoriaRepository.CategoriaPorId(categoria.Id);
//                if (buscarCategoria == null)
//                {
//                    resposta.Mensagem = "Categoria não encontrada.";
//                    resposta.Status = false;
//                    return resposta;
//                }
//                AtualizarDadosCategoria(buscarCategoria, categoria);
//                Categoria retorno = await _categoriaRepository.AtualizarCategoria(buscarCategoria);

//                resposta.Dados = CategoriaMap.MapCategoria(retorno);
//                resposta.Mensagem = "Categorias listadas com sucesso";
//                return resposta;
//            }
//            catch (Exception ex)
//            {
//                resposta.Mensagem = ex.Message;
//                resposta.Status = false;
//                return resposta;
//            }
//        }

//        public async Task<Response<CategoriaDTO>> DeletarCategoria(Guid id)
//        {
//            Response<CategoriaDTO> resposta = new Response<CategoriaDTO>();
//            try
//            {
//                Categoria categoria = await _categoriaRepository.CategoriaPorId(id);
//                if (categoria == null)
//                {
//                    resposta.Mensagem = "Categoria não encontrada.";
//                    resposta.Status = false;
//                    return resposta;
//                }
//                Categoria retorno = await _categoriaRepository.DeletarCategoria(id);

//                resposta.Dados = CategoriaMap.MapCategoria(retorno);
//                resposta.Mensagem = "Categorias listadas com sucesso";
//                return resposta;
//            }
//            catch (Exception ex)
//            {
//                resposta.Mensagem = ex.Message;
//                resposta.Status = false;
//                return resposta;
//            }
//        }

//        private void ValidarCategoriaCriacao(CatCriacaoDTO categoria)
//        {
//            ValidadorUtils.ValidarSeVazioOuNulo(categoria.CatDesc, "Categoria é obrigatório");
//            ValidadorUtils.ValidarMaximo(categoria.CatDesc, 100, "Nome deve conter no máximo 100 caracteres");
//        }

//        private void ValidarCategoriaEdicao(CatEdicaoDTO categoria)
//        {
//            ValidadorUtils.ValidarSeVazioOuNulo(categoria.CatDesc, "Categoria é obrigatório");
//            ValidadorUtils.ValidarMaximo(categoria.CatDesc, 100, "Nome deve conter no máximo 100 caracteres");
//        }

//        private void AtualizarDadosCategoria(Categoria categoriaExistente, CatEdicaoDTO categoria)
//        {
//            categoriaExistente.CatDesc = categoria.CatDesc;
//        }
//    }
//}
