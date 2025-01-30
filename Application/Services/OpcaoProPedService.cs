using WebMesaGestor.Application.DTO.Input.OpcaoProPed;
using WebMesaGestor.Application.DTO.Input.ProdutoPedido;
using WebMesaGestor.Application.DTO.Input.USuario;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.Map;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Infra.Repositories;
using WebMesaGestor.Utils;

namespace WebMesaGestor.Application.Services
{
    public class OpcaoProPedService
    {
        private readonly IOpcProPedRepository _opcProPedRepository;
        private readonly IProPedRepository _proPedRepository;
        private readonly IOpcaoRepository _opcaoRepository;

        public OpcaoProPedService(IOpcProPedRepository opcProPedRepository, IProPedRepository proPedRepository,
            IOpcaoRepository opcaoRepository)
        {
            _opcProPedRepository = opcProPedRepository;
            _proPedRepository = proPedRepository;
            _opcaoRepository = opcaoRepository;
        }

        public async Task<Response<IEnumerable<OpcProPedOutputDTO>>> ListarOpcoesPorProPedId(Guid id)
        {
            Response<IEnumerable<OpcProPedOutputDTO>> resposta = new Response<IEnumerable<OpcProPedOutputDTO>>();
            try
            {
                IEnumerable<OpcaoProPed> opcaoProPed = await _opcProPedRepository.ListarOpcoesPorProPedId(id);
                if (opcaoProPed == null || !opcaoProPed.Any())
                {
                    resposta.Mensagem = "Opções por produto não encontrado.";
                    resposta.Status = false;
                    return resposta;
                }

                await PreencherOpcoesProPed(opcaoProPed);
                resposta.Dados = OpcaoProPedMap.MapOpcProPed(opcaoProPed);
                resposta.Mensagem = "Opções por produto listados com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<OpcProPedOutputDTO>> OpcaoProPedId(Guid id)
        {
            Response<OpcProPedOutputDTO> resposta = new Response<OpcProPedOutputDTO>();
            try
            {
                OpcaoProPed opcaoProPed = await _opcProPedRepository.OpcaoProPedId(id);
                if (opcaoProPed == null)
                {
                    resposta.Mensagem = "Opção por produto do pedido não encontrado.";
                    resposta.Status = false;
                    return resposta;
                }

                await PreencherOpcaoProPed(opcaoProPed);
                resposta.Dados = OpcaoProPedMap.MapOpcProPed(opcaoProPed);
                resposta.Mensagem = "Opção por produto do pedido encontrado com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<OpcProPedOutputDTO>> CriarOpcaoProPed(OpcProPedCriacaoDTO opcProPed)
        {
            Response<OpcProPedOutputDTO> resposta = new Response<OpcProPedOutputDTO>();
            try
            {
                await ValidarOpcao(opcProPed.OpcaoId);
                await ValidarProPed(opcProPed.ProdutoPedidoId);

                OpcaoProPed map = OpcaoProPedMap.MapOpcProPed(opcProPed);
                OpcaoProPed retorno = await _opcProPedRepository.CriarOpcaoProdPed(map);
                await PreencherOpcaoProPed(retorno);

                resposta.Dados = OpcaoProPedMap.MapOpcProPed(retorno);
                resposta.Mensagem = "Opção por produto do pedido criado com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<OpcProPedOutputDTO>> AtualizarOpcaoProPed(OpcProPedEdicaoDTO opcProPed)
        {
            Response<OpcProPedOutputDTO> resposta = new Response<OpcProPedOutputDTO>();
            try
            {
                await ValidarOpcao(opcProPed.OpcaoId);
                await ValidarProPed(opcProPed.ProdutoPedidoId);
                OpcaoProPed buscarOpcaoProPed = await _opcProPedRepository.OpcaoProPedId(opcProPed.Id);
                if (buscarOpcaoProPed == null)
                {
                    resposta.Mensagem = "Opção por produto do pedido não encontrado.";
                    resposta.Status = false;
                    return resposta;
                }
                AtualizarDadosUsuario(buscarOpcaoProPed, opcProPed);
                OpcaoProPed retorno = await _opcProPedRepository.AtualizarOpcaoProdPed(buscarOpcaoProPed);
                await PreencherOpcaoProPed(retorno);

                resposta.Dados = OpcaoProPedMap.MapOpcProPed(retorno);
                resposta.Mensagem = "Opção por produto do pedido atualizado com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<OpcProPedOutputDTO>> DeletarOpcProPed(Guid id)
        {
            Response<OpcProPedOutputDTO> resposta = new Response<OpcProPedOutputDTO>();
            try
            {
                OpcaoProPed opcaoProPed = await _opcProPedRepository.OpcaoProPedId(id);
                if (opcaoProPed  == null)
                {
                    resposta.Mensagem = "Opção por produto do pedido não encontrada para deleção.";
                    resposta.Status = false;
                    return resposta;
                }
                OpcaoProPed retorno = await _opcProPedRepository.DeletarOpcaoProdPed(id);

                await PreencherOpcaoProPed(retorno);
                resposta.Dados = OpcaoProPedMap.MapOpcProPed(retorno);
                resposta.Mensagem = "Opção por produto do pedido deletado com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        // Métodos auxiliares
        private async Task PreencherOpcoesProPed(IEnumerable<OpcaoProPed> opcoesProPed)
        {
            foreach (var opcaoProPed in opcoesProPed)
            {
                if (opcaoProPed.OpcaoId != null)
                {
                    opcaoProPed.Opcao = await _opcaoRepository.OpcaoPorId((Guid)opcaoProPed.OpcaoId);
                }
                if (opcaoProPed.ProdutoPedidoId != null)
                {
                    opcaoProPed.ProdutoPedido = await _proPedRepository.ProPedId((Guid)opcaoProPed.ProdutoPedidoId);
                }
            }
        }

        private async Task PreencherOpcaoProPed(OpcaoProPed opcaoProPed)
        {
            if (opcaoProPed.OpcaoId != null)
            {
                opcaoProPed.Opcao = await _opcaoRepository.OpcaoPorId((Guid)opcaoProPed.OpcaoId);
            }
            if (opcaoProPed.ProdutoPedidoId != null)
            {
                opcaoProPed.ProdutoPedido = await _proPedRepository.ProPedId((Guid)opcaoProPed.ProdutoPedidoId);
            }
        }

        private async Task ValidarOpcao(Guid? opcaoId)
        {
            if (opcaoId == null || opcaoId == Guid.Empty)
            {
                throw new Exception("Opção é obrigatório");
            }

            var opcao = await _opcaoRepository.OpcaoPorId((Guid)opcaoId);

            if (opcao == null)
            {
                throw new Exception("Opção não encontrada");
            }
        }

        private async Task ValidarProPed(Guid? propedId)
        {
            if (propedId == null || propedId == Guid.Empty)
            {
                throw new Exception("Produto do pedido é obrigatório");
            }

            var proped = await _proPedRepository.ProPedId((Guid)propedId);

            if (proped == null)
            {
                throw new Exception("Produto do pedido não encontrado");
            }
        }

        private void AtualizarDadosUsuario(OpcaoProPed opcaoProPedExistente, OpcProPedEdicaoDTO opcaoProPed)
        {
            opcaoProPedExistente.OpcaoId = opcaoProPed.OpcaoId;
            opcaoProPedExistente.ProdutoPedidoId = opcaoProPed.ProdutoPedidoId;
        }
    }
}
