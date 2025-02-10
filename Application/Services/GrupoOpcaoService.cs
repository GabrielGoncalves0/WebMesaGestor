//using WebMesaGestor.Application.Common;
//using WebMesaGestor.Application.DTO.Input.Grupo;
//using WebMesaGestor.Application.DTO.Input.Opcoes;
//using WebMesaGestor.Application.DTO.Output;
//using WebMesaGestor.Application.Map;
//using WebMesaGestor.Domain.Entities;
//using WebMesaGestor.Domain.Interfaces;
//using WebMesaGestor.Utils;

//namespace WebMesaGestor.Application.Services
//{
//    public class GrupoOpcaoService
//    {
//        private readonly IGrupoOpcaoRepository _grupoOpcaoRepository;
//        private readonly IProdutoRepository _produtoRepository;

//        public GrupoOpcaoService(IGrupoOpcaoRepository grupoRepository, IProdutoRepository produtoRepository)
//        {
//            _grupoOpcaoRepository = grupoRepository;
//            _produtoRepository = produtoRepository;
//        }

//        public async Task<Response<IEnumerable<GrupoOpcaoDTO>>> ListarGrupoOpcoes()
//        {
//            Response<IEnumerable<GrupoOpcaoDTO>> resposta = new Response<IEnumerable<GrupoOpcaoDTO>>();
//            try
//            {
//                IEnumerable<GrupoOpcoes> grupoOpcao = await _grupoOpcaoRepository.ListarGrupoOpcoes();
//                if(grupoOpcao == null || !grupoOpcao.Any())
//                {
//                    resposta.Mensagem = "Grupo de opcões não encontrado.";
//                    resposta.Status = false;
//                    return resposta;
//                }
//                await PreencherGrupoOpcoes(grupoOpcao);
//                resposta.Dados = GrupoOpcaoMap.MapGrupoOpcao(grupoOpcao);
//                resposta.Mensagem = "Grupo de opções listados com sucesso";
//                return resposta;
//            }
//            catch (Exception ex)
//            {
//                resposta.Mensagem = ex.Message;
//                resposta.Status = false;
//                return resposta;
//            }
//        }

//        public async Task<Response<GrupoOpcaoDTO>> GrupoOpcaoPorId(Guid id)
//        {
//            Response<GrupoOpcaoDTO> resposta = new Response<GrupoOpcaoDTO>();
//            try
//            {
//                GrupoOpcoes grupoOpcao = await _grupoOpcaoRepository.GrupoOpcaoPorId(id);
//                if (grupoOpcao == null)
//                {
//                    resposta.Mensagem = "Grupos não encontrado.";
//                    resposta.Status = false;
//                    return resposta;
//                }
//                await PreencherGrupoOpcao(grupoOpcao);
//                resposta.Dados = GrupoOpcaoMap.MapGrupoOpcao(grupoOpcao);
//                resposta.Mensagem = "Grupo encontrado com sucesso";
//                return resposta;
//            }
//            catch (Exception ex)
//            {
//                resposta.Mensagem = ex.Message;
//                resposta.Status = false;
//                return resposta;
//            }
//        }

//        public async Task<Response<GrupoOpcaoDTO>> CriarGrupoOpcao(GrupOpcCriacaoDTO grupoOpcao)
//        {
//            Response<GrupoOpcaoDTO> resposta = new Response<GrupoOpcaoDTO>();
//            try
//            {
//                ValidarGrupoOpcaoCriacao(grupoOpcao);
//                await ValidarProduto(grupoOpcao.ProdutoId);

//                GrupoOpcoes map = GrupoOpcaoMap.MapGrupoOpcao(grupoOpcao);
//                GrupoOpcoes retorno = await _grupoOpcaoRepository.CriarGrupoOpcao(map);
//                await PreencherGrupoOpcao(retorno);

//                resposta.Dados = GrupoOpcaoMap.MapGrupoOpcao(retorno);
//                resposta.Mensagem = "Grupo de opções criado com sucesso";
//                return resposta;
//            }
//            catch (Exception ex)
//            {
//                resposta.Mensagem = ex.Message;
//                resposta.Status = false;
//                return resposta;
//            }
//        }

//        public async Task<Response<GrupoOpcaoDTO>> AtualizarGrupoOpcao(GrupOpcEdicaoDTO grupoOpcao)
//        {
//            Response<GrupoOpcaoDTO> resposta = new Response<GrupoOpcaoDTO>();
//            try
//            {
//                ValidarGrupoOpcaoEdicao(grupoOpcao);
//                await ValidarProduto(grupoOpcao.ProdutoId);
//                GrupoOpcoes buscarGrupoOpcao = await _grupoOpcaoRepository.GrupoOpcaoPorId(grupoOpcao.Id);
//                if (buscarGrupoOpcao == null)
//                {
//                    resposta.Mensagem = "Grupo de opções não encontrado.";
//                    resposta.Status = false;
//                    return resposta;
//                }
//                AtualizarDadosGrupoOpcao(buscarGrupoOpcao, grupoOpcao);
//                GrupoOpcoes retorno = await _grupoOpcaoRepository.AtualizarGrupoOpcao(buscarGrupoOpcao);
//                await PreencherGrupoOpcao(retorno);

//                resposta.Dados = GrupoOpcaoMap.MapGrupoOpcao(retorno);
//                resposta.Mensagem = "Grupo atualizado com sucesso";
//                return resposta;
//            }
//            catch (Exception ex)
//            {
//                resposta.Mensagem = ex.Message;
//                resposta.Status = false;
//                return resposta;
//            }
//        }

//        public async Task<Response<GrupoOpcaoDTO>> DeletarGrupoOpcao(Guid id)
//        {
//            Response<GrupoOpcaoDTO> resposta = new Response<GrupoOpcaoDTO>();
//            try
//            {
//                GrupoOpcoes grupoOpcao = await _grupoOpcaoRepository.GrupoOpcaoPorId(id);
//                if (grupoOpcao == null)
//                {
//                    resposta.Mensagem = "Grupo de opcoes não encontrado para deleção.";
//                    resposta.Status = false;
//                    return resposta;
//                }
//                GrupoOpcoes retorno = await _grupoOpcaoRepository.DeletarGrupoOpcao(id);
//                resposta.Dados = GrupoOpcaoMap.MapGrupoOpcao(retorno);
//                resposta.Mensagem = "Grupo deletado com sucesso";
//                return resposta;
//            }
//            catch (Exception ex)
//            {
//                resposta.Mensagem = ex.Message;
//                resposta.Status = false;
//                return resposta;
//            }
//        }

//        private async Task PreencherGrupoOpcoes(IEnumerable<GrupoOpcoes> grupoOpcoes)
//        {
//            foreach (var grupoOpcao in grupoOpcoes)
//            {
//                if (grupoOpcao.ProdutoId != null)
//                {
//                    grupoOpcao.Produto = await _produtoRepository.ProdutoPorId((Guid)grupoOpcao.ProdutoId);
//                }
//            }
//        }

//        private async Task PreencherGrupoOpcao(GrupoOpcoes grupoOpcao)
//        {
//            if (grupoOpcao.ProdutoId != null)
//            {
//                grupoOpcao.Produto = await _produtoRepository.ProdutoPorId((Guid)grupoOpcao.ProdutoId); 
//            }
//        }

//        private async Task ValidarProduto(Guid? produtoId)
//        {
//            if (produtoId == null || produtoId == Guid.Empty)
//            {
//                throw new Exception("Produto é obrigatório");
//            }

//            var produto = await _produtoRepository.ProdutoPorId((Guid)produtoId);

//            if (produto == null)
//            {
//                throw new Exception("Produto não encontrado");
//            }
//        }

//        private void ValidarGrupoOpcaoCriacao(GrupOpcCriacaoDTO grupoOpcao)
//        {
//            ValidadorUtils.ValidarSeVazioOuNulo(grupoOpcao.GrupOpcDesc, "Descricao é obrigatório");
//            ValidadorUtils.ValidarMaximo(grupoOpcao.GrupOpcDesc, 100, "Descricao deve conter no máximo 100 caracteres");
//            ValidadorUtils.ValidarMinimo(grupoOpcao.GrupOpcDesc, 3, "Descricao deve conter no minimo 3 caracteres");

//            ValidadorUtils.ValidarInteiroSeVazioOuNulo(grupoOpcao.GrupOpcMax, "Quantidade máxima é obrigatório");
//            ValidadorUtils.ValidarMaximo(grupoOpcao.GrupOpcMax, 9999999, "Quantidade máxima deve ser menor que 9999999");
//            ValidadorUtils.ValidarMinimo(grupoOpcao.GrupOpcMax, 0, "Quantidade máxima deve ser maior que 0");

//            if (!Enum.IsDefined(typeof(GrupOpcTipo), grupoOpcao.GrupOpcTipo))
//            {
//                throw new Exception("Multiplicidade do grupo de opções é obrigatório");
//            }
//        }

//        private void ValidarGrupoOpcaoEdicao(GrupOpcEdicaoDTO grupoOpcao)
//        {
//            ValidadorUtils.ValidarSeVazioOuNulo(grupoOpcao.GrupOpcDesc, "Descricao é obrigatório");
//            ValidadorUtils.ValidarMaximo(grupoOpcao.GrupOpcDesc, 100, "Descricao deve conter no máximo 100 caracteres");
//            ValidadorUtils.ValidarMinimo(grupoOpcao.GrupOpcDesc, 3, "Descricao deve conter no minimo 3 caracteres");

//            ValidadorUtils.ValidarInteiroSeVazioOuNulo(grupoOpcao.GrupOpcMax, "Quantidade máxima é obrigatório");
//            ValidadorUtils.ValidarMaximo(grupoOpcao.GrupOpcMax, 9999999, "Quantidade máxima deve ser menor que 9999999");
//            ValidadorUtils.ValidarMinimo(grupoOpcao.GrupOpcMax, 0, "Quantidade máxima deve ser maior que 0");

//            if (!Enum.IsDefined(typeof(GrupOpcTipo), grupoOpcao.GrupOpcTipo))
//            {
//                throw new Exception("Multiplicidade do grupo de opções é obrigatório");
//            }
//        }

//        private void AtualizarDadosGrupoOpcao(GrupoOpcoes buscarGrupoOpcao, GrupOpcEdicaoDTO grupoOpcao)
//        {
//            buscarGrupoOpcao.GrupOpcDesc = grupoOpcao.GrupOpcDesc;
//            buscarGrupoOpcao.GrupOpcTipo = grupoOpcao.GrupOpcTipo;
//            buscarGrupoOpcao.GrupOpcMax = grupoOpcao.GrupOpcMax;
//            buscarGrupoOpcao.ProdutoId = grupoOpcao.ProdutoId;
//        }
//    }
//}
