using WebMesaGestor.Application.DTO.Input.Opcoes;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.Map;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Utils;

namespace WebMesaGestor.Application.Services
{
    public class OpcaoService
    {
        private readonly IOpcaoRepository _opcaoRepository;
        private readonly IGrupoOpcaoRepository _grupoOpcoesRepository;

        public OpcaoService(IOpcaoRepository opcaoRepository, IGrupoOpcaoRepository grupoOpcoesRepository)
        {
            _opcaoRepository = opcaoRepository;
            _grupoOpcoesRepository = grupoOpcoesRepository;
        }
        public async Task<Response<IEnumerable<OpcOutputDTO>>> ListarOpcoes()
        {
            Response<IEnumerable<OpcOutputDTO>> resposta = new Response<IEnumerable<OpcOutputDTO>>();
            try
            {
                IEnumerable<Opcao> opcoes = await _opcaoRepository.ListarOpcoes();
                if (opcoes == null || !opcoes.Any())
                {
                    resposta.Mensagem = "Opções não encontrado.";
                    resposta.Status = false;
                    return resposta;
                }
                await PreencherOpcoes(opcoes);
                resposta.Dados = OpcaoMap.MapOpcao(opcoes);
                resposta.Mensagem = "Opções listadas com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<OpcOutputDTO>> OpcaoPorId(Guid id)
        {
            Response<OpcOutputDTO> resposta = new Response<OpcOutputDTO>();
            try
            {
                Opcao opcao = await _opcaoRepository.OpcaoPorId(id);
                if (opcao == null)
                {
                    resposta.Mensagem = "Opção não encontrada.";
                    resposta.Status = false;
                    return resposta;
                }
                await PreencherOpcao(opcao);
                resposta.Dados = OpcaoMap.MapOpcao(opcao);
                resposta.Mensagem = "Opções encontrada com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<OpcOutputDTO>> CriarOpcao(OpcCriacaoDTO opcao)
        {
            Response<OpcOutputDTO> resposta = new Response<OpcOutputDTO>();
            try
            {
                ValidarOpcaoCriacao(opcao);
                await ValidarGrupoOpcao(opcao.GrupoOpcoesId);

                Opcao map = OpcaoMap.MapOpcao(opcao);
                Opcao retorno = await _opcaoRepository.CriarOpcao(map);
                await PreencherOpcao(retorno);

                resposta.Dados = OpcaoMap.MapOpcao(retorno);
                resposta.Mensagem = "Opção criada com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<OpcOutputDTO>> AtualizarOpcao(OpcEdicaoDTO opcao)
        {
            Response<OpcOutputDTO> resposta = new Response<OpcOutputDTO>();
            try
            {
                ValidarOpcaoEdicao(opcao);
                await ValidarGrupoOpcao(opcao.GrupoOpcoesId);
                Opcao buscarOpcao = await _opcaoRepository.OpcaoPorId(opcao.Id);
                if (buscarOpcao == null)
                {
                    resposta.Mensagem = "Opção não encontrado.";
                    resposta.Status = false;
                    return resposta;
                }
                AtualizarDadosOpcao(buscarOpcao, opcao);
                Opcao retorno = await _opcaoRepository.AtualizarOpcao(buscarOpcao);
                await PreencherOpcao(retorno);

                resposta.Dados = OpcaoMap.MapOpcao(retorno);
                resposta.Mensagem = "Opção atualizada com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<OpcOutputDTO>> DeletarOpcao(Guid id)
        {
            Response<OpcOutputDTO> resposta = new Response<OpcOutputDTO>();
            try
            {
                Opcao opcao = await _opcaoRepository.OpcaoPorId(id);
                if(opcao == null)
                {
                    resposta.Mensagem = "Opcao não encontrada para deleção.";
                    resposta.Status = false;
                    return resposta;
                }
                Opcao retorno = await _opcaoRepository.DeletarOpcao(id);
                resposta.Dados = OpcaoMap.MapOpcao(retorno);
                resposta.Mensagem = "Opcao deletada com sucesso";
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
        private async Task PreencherOpcoes(IEnumerable<Opcao> opcoes)
        {
            foreach (var opcao in opcoes)
            {
                if (opcao.GrupoOpcoesId != null)
                {
                    opcao.GrupoOpcoes = await _grupoOpcoesRepository.GrupoOpcaoPorId((Guid)opcao.GrupoOpcoesId);
                }
            }
        }

        private async Task PreencherOpcao(Opcao opcao)
        {
            if (opcao.GrupoOpcoesId != null)
            {
                opcao.GrupoOpcoes = await _grupoOpcoesRepository.GrupoOpcaoPorId((Guid)opcao.GrupoOpcoesId);
            }
        }

        private async Task ValidarGrupoOpcao(Guid? grupoOpcaoId)
        {
            if (grupoOpcaoId == null || grupoOpcaoId == Guid.Empty)
            {
                throw new Exception("Grupo de opções é obrigatório");
            }

            var grupoOpcao = await _grupoOpcoesRepository.GrupoOpcaoPorId((Guid)grupoOpcaoId);

            if (grupoOpcao == null)
            {
                throw new Exception("Grupo de opções não encontrado");
            }
        }

        private void ValidarOpcaoCriacao(OpcCriacaoDTO opcao)
        {
            ValidadorUtils.ValidarSeVazioOuNulo(opcao.OpcaoDesc, "Descricao é obrigatório");
            ValidadorUtils.ValidarMaximo(opcao.OpcaoDesc, 100, "Descricao deve conter no máximo 100 caracteres");
            ValidadorUtils.ValidarMinimo(opcao.OpcaoDesc, 3, "Descricao deve conter no minimo 3 caracteres");

            ValidadorUtils.ValidarDecimalSeVazio(opcao.OpcaoValor, "Valor é obrigatório");
            ValidadorUtils.ValidarMaximo(opcao.OpcaoValor, 9999999, "Valor deve ser menor que 9999999");
            ValidadorUtils.ValidarMinimo(opcao.OpcaoValor, 0, "Valor deve ser maior que 0");

            ValidadorUtils.ValidarInteiroSeVazioOuNulo(opcao.OpcaoQuantMax, "Quantidade máxima é obrigatório");
            ValidadorUtils.ValidarMaximo(opcao.OpcaoQuantMax, 9999999, "Quantidade máxima deve ser menor que 9999999");
            ValidadorUtils.ValidarMinimo(opcao.OpcaoQuantMax, 0, "Quantidade máxima deve ser maior que 0");
        }

        private void ValidarOpcaoEdicao(OpcEdicaoDTO opcao)
        {
            ValidadorUtils.ValidarSeVazioOuNulo(opcao.OpcaoDesc, "Descricao é obrigatório");
            ValidadorUtils.ValidarMaximo(opcao.OpcaoDesc, 100, "Descricao deve conter no máximo 100 caracteres");
            ValidadorUtils.ValidarMinimo(opcao.OpcaoDesc, 3, "Descricao deve conter no minimo 3 caracteres");

            ValidadorUtils.ValidarDecimalSeVazio(opcao.OpcaoValor, "Valor é obrigatório");
            ValidadorUtils.ValidarMaximo(opcao.OpcaoValor, 9999999, "Valor deve ser menor que 9999999");
            ValidadorUtils.ValidarMinimo(opcao.OpcaoValor, 0, "Valor deve ser maior que 0");

            ValidadorUtils.ValidarInteiroSeVazioOuNulo(opcao.OpcaoQuantMax, "Quantidade máxima é obrigatório");
            ValidadorUtils.ValidarMaximo(opcao.OpcaoQuantMax, 9999999, "Quantidade máxima deve ser menor que 9999999");
            ValidadorUtils.ValidarMinimo(opcao.OpcaoQuantMax, 0, "Quantidade máxima deve ser maior que 0");
        }

        private void AtualizarDadosOpcao(Opcao buscarOpcao, OpcEdicaoDTO opcao)
        {
            buscarOpcao.OpcaoDesc = opcao.OpcaoDesc;
            buscarOpcao.OpcaoValor = opcao.OpcaoValor;
            buscarOpcao.OpcaoQuantMax = opcao.OpcaoQuantMax;
            buscarOpcao.GrupoOpcoesId = opcao.GrupoOpcoesId;
        }
    }
}
