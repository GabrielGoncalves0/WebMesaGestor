//using WebMesaGestor.Application.Common;
//using WebMesaGestor.Application.DTO.Input.Grupo;
//using WebMesaGestor.Application.DTO.Input.OpcaoProPed;
//using WebMesaGestor.Application.DTO.Input.ProdutoPedido;
//using WebMesaGestor.Application.DTO.Input.USuario;
//using WebMesaGestor.Application.DTO.Output;
//using WebMesaGestor.Application.Map;
//using WebMesaGestor.Domain.Entities;
//using WebMesaGestor.Domain.Interfaces;
//using WebMesaGestor.Infra.Repositories;
//using WebMesaGestor.Utils;

//namespace WebMesaGestor.Application.Services
//{
//    public class ProPedService
//    {
//        private readonly IProPedRepository _produtoPedidoRepository;
//        private readonly IProdutoRepository _produtoRepository;
//        private readonly IPedidoRepository _pedidoRepository;

//        public ProPedService(IProPedRepository produtoPedidoRepository, IProdutoRepository produtoRepository,
//            IPedidoRepository pedidoRepository)
//        {
//            _produtoPedidoRepository = produtoPedidoRepository;
//            _produtoRepository = produtoRepository;
//            _pedidoRepository = pedidoRepository;
//        }

//        public async Task<Response<IEnumerable<ProdutoPedidoDTO>>> ListarProdutosPorPedId(Guid id)
//        {
//            Response<IEnumerable<ProdutoPedidoDTO>> resposta = new Response<IEnumerable<ProdutoPedidoDTO>>();
//            try
//            {
//                IEnumerable<ProdutoPedido> produtosPed = await _produtoPedidoRepository.ListarProdutosPorPedId(id);
//                if (produtosPed == null || !produtosPed.Any())
//                {
//                    resposta.Mensagem = "Produtos por pedido não encontrado.";
//                    resposta.Status = false;
//                    return resposta;
//                }

//                await PreencherProdutoPed(produtosPed);
//                resposta.Dados = ProdutoPedidoMap.MapProPed(produtosPed);
//                resposta.Mensagem = "Produtos por pedido listados com sucesso";
//                return resposta;
//            }
//            catch (Exception ex)
//            {
//                resposta.Mensagem = ex.Message;
//                resposta.Status = false;
//                return resposta;
//            }
//        }

//        public async Task<Response<ProdutoPedidoDTO>> ProPedId(Guid id)
//        {
//            Response<ProdutoPedidoDTO> resposta = new Response<ProdutoPedidoDTO>();
//            try
//            {
//                ProdutoPedido produtoPed = await _produtoPedidoRepository.ProPedId(id);
//                if (produtoPed == null)
//                {
//                    resposta.Mensagem = "Produto do pedido não encontrado.";
//                    resposta.Status = false;
//                    return resposta;
//                }

//                await PreencherProdutoPed(produtoPed);
//                resposta.Dados = ProdutoPedidoMap.MapProPed(produtoPed);
//                resposta.Mensagem = "Produto do pedido encontrado com sucesso";
//                return resposta;
//            }
//            catch (Exception ex)
//            {
//                resposta.Mensagem = ex.Message;
//                resposta.Status = false;
//                return resposta;
//            }
//        }

//        public async Task<Response<IEnumerable<ProdutoPedidoDTO>>> CriarProPed(ProPedCriacaoDTO proPedCriacaoDTO)
//        {
//            Response<IEnumerable<ProdutoPedidoDTO>> resposta = new Response<IEnumerable<ProdutoPedidoDTO>>();
//            try
//            {
//                var listaProdutoPedido = new List<ProdutoPedido>();

//                await ValidarPedido(proPedCriacaoDTO.PedidoId);
//                await ValidarProduto(proPedCriacaoDTO.ProdutoId);

//                for (int i = 0; i < proPedCriacaoDTO.ProPedQuant; i++)
//                {
//                    ProdutoPedido map = ProdutoPedidoMap.MapProPed(proPedCriacaoDTO);
//                    map.PedQuant = 1;
//                    ProdutoPedido retorno = await _produtoPedidoRepository.CriarProPed(map);
//                    await PreencherProdutoPed(retorno);
//                    await AtualizarValorCriacao(retorno);
//                    listaProdutoPedido.Add(retorno);
//                }

//                resposta.Dados = ProdutoPedidoMap.MapProPed(listaProdutoPedido);
//                resposta.Mensagem = "Produtos do pedido criados com sucesso";
//                return resposta;
//            }
//            catch (Exception ex)
//            {
//                resposta.Mensagem = ex.Message;
//                resposta.Status = false;
//                return resposta;
//            }
//        }

//        public async Task<Response<ProdutoPedidoDTO>> AtualizarProPed(ProPedEdicaoDTO proPedEdicaoDTO)
//        {
//            Response<ProdutoPedidoDTO> resposta = new Response<ProdutoPedidoDTO>();
//            try
//            {
//                ProdutoPedido buscarProPed = await _produtoPedidoRepository.ProPedId(proPedEdicaoDTO.Id);
//                if (buscarProPed == null)
//                {
//                    resposta.Mensagem = "Produto do pedido não encontrado.";
//                    resposta.Status = false;
//                    return resposta;
//                }
//                buscarProPed.StatusProPed = proPedEdicaoDTO.StatusProPed;
//                ProdutoPedido retorno = await _produtoPedidoRepository.AtualizarProPed(buscarProPed);

//                resposta.Dados = ProdutoPedidoMap.MapProPed(retorno);
//                resposta.Mensagem = "Produto do pedido atualizado com sucesso";
//                return resposta;
//            }
//            catch (Exception ex)
//            {
//                resposta.Mensagem = ex.Message;
//                resposta.Status = false;
//                return resposta;
//            }
//        }

//        public async Task<Response<ProdutoPedidoDTO>> DeletarProPed(Guid id)  
//        {
//            Response<ProdutoPedidoDTO> resposta = new Response<ProdutoPedidoDTO>();
//            try
//            {
//                ProdutoPedido produtoPed = await _produtoPedidoRepository.ProPedId(id);
//                if (produtoPed == null)
//                {
//                    resposta.Mensagem = "Produto do pedido não encontrado para deleção.";
//                    resposta.Status = false;
//                    return resposta;
//                }
//                await AtualizarValorDelete(produtoPed);

//                ProdutoPedido retorno = await _produtoPedidoRepository.DeletarProPed(id);

//                await PreencherProdutoPed(retorno);
//                resposta.Dados = ProdutoPedidoMap.MapProPed(retorno);
//                resposta.Mensagem = "Produto do pedido deletado com sucesso";
//                return resposta;
//            }
//            catch (Exception ex)
//            {
//                resposta.Mensagem = ex.Message;
//                resposta.Status = false;
//                return resposta;
//            }
//        }

//        // Métodos auxiliares
//        private async Task PreencherProdutoPed(IEnumerable<ProdutoPedido> produtosPed)
//        {
//            foreach (var produtoPed in produtosPed)
//            {
//                if (produtoPed.ProdutoId != null)
//                {
//                    produtoPed.Produto = await _produtoRepository.ProdutoPorId((Guid)produtoPed.ProdutoId);
//                }
//                if (produtoPed.PedidoId != null)
//                {
//                    produtoPed.Pedido = await _pedidoRepository.PedidoPorId((Guid)produtoPed.PedidoId);
//                }
//            }
//        }

//        private async Task PreencherProdutoPed(ProdutoPedido produtoPed)
//        {
//            if (produtoPed.ProdutoId != null)
//            {
//                produtoPed.Produto = await _produtoRepository.ProdutoPorId((Guid)produtoPed.ProdutoId);
//            }
//            if (produtoPed.PedidoId != null)
//            {
//                produtoPed.Pedido = await _pedidoRepository.PedidoPorId((Guid)produtoPed.PedidoId);
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
//                throw new Exception("Produto não encontrada");
//            }
//        }

//        private async Task ValidarPedido(Guid? pedidoId)
//        {
//            if (pedidoId == null || pedidoId == Guid.Empty)
//            {
//                throw new Exception("Pedido é obrigatório");
//            }

//            var pedido = await _pedidoRepository.PedidoPorId((Guid)pedidoId);

//            if (pedido == null)
//            {
//                throw new Exception("Pedido não encontrado");
//            }
//        }

//        private async Task AtualizarValorCriacao(ProdutoPedido produtoPedido)
//        {
//            Pedido pedido = await _pedidoRepository.PedidoPorId(produtoPedido.PedidoId);
//            Produto produto = await _produtoRepository.ProdutoPorId(produtoPedido.ProdutoId);

//            decimal valorTotalProdutoComDesconto = (produto.ProPreco * produtoPedido.PedQuant) - ((produto.ProPreco * produtoPedido.PedQuant) * (produtoPedido.PedDesconto / 100));
//            pedido.PedValor += valorTotalProdutoComDesconto;

//            await _pedidoRepository.AtualizarPedido(pedido);
//        }

//        private async Task AtualizarValorDelete(ProdutoPedido produtoPedido)
//        {
//            Pedido pedido = await _pedidoRepository.PedidoPorId(produtoPedido.PedidoId);
//            Produto produto = await _produtoRepository.ProdutoPorId(produtoPedido.ProdutoId);

//            pedido.PedValor -= produto.ProPreco;
//            await _pedidoRepository.AtualizarPedido(pedido);
//        }

//        private void ValidarProdutoPedCriacao(ProPedCriacaoDTO produtoPedido)
//        {
//            ValidadorUtils.ValidarInteiroSeVazioOuNulo(produtoPedido.ProPedQuant, "Quantidade é obrigatório");
//            ValidadorUtils.ValidarMaximo(produtoPedido.ProPedQuant, 10, "Quantidade deve conter no máximo 10 caracteres");
//            ValidadorUtils.ValidarMinimo(produtoPedido.ProPedQuant, 1, "Quantidade deve conter no minimo 3 caracteres");
//            ValidadorUtils.ValidarNegativo(produtoPedido.ProPedQuant, "Quantidade deve ser maior que 0");
            
//            ValidadorUtils.ValidarInteiroSeVazioOuNulo(produtoPedido.ProPedDesconto, "Desconto é obrigatório");
//            ValidadorUtils.ValidarMaximo(produtoPedido.ProPedDesconto, 3, "Desconto deve conter no máximo 3 caracteres");
//            ValidadorUtils.ValidarNegativo(produtoPedido.ProPedDesconto, "Desconto deve ser maior que 0");
//        }

//        private void ValidarProdutoPedEdicao(ProPedEdicaoDTO produtoPedido)
//        {
//            if (!Enum.IsDefined(typeof(StatusProPed), produtoPedido.StatusProPed))
//            {
//                throw new Exception("Status do produto do pedido é obrigatório");
//            }
//        }
//    }
//}
