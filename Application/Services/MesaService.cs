using WebMesaGestor.Application.DTO.Input.Mesa;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.Map;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Utils;

namespace WebMesaGestor.Application.Services
{
    public class MesaService
    {
        private readonly IMesaRepository _mesaRepository;

        public MesaService(IMesaRepository mesaRepository)
        {
            _mesaRepository = mesaRepository;
        }

        public async Task<Response<IEnumerable<MesOutputDTO>>> ListarMesas()
        {
            Response<IEnumerable<MesOutputDTO>> resposta = new Response<IEnumerable<MesOutputDTO>>();
            try
            {
                IEnumerable<Mesa> mesas = await _mesaRepository.ListarMesas();
                if (mesas == null || !mesas.Any())
                {
                    resposta.Mensagem = "Nenhuma mesa encontrada.";
                    resposta.Status = false;
                    return resposta;
                }
                resposta.Dados = MesaMap.MapMesa(mesas);
                resposta.Mensagem = "Mesas listadas com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<MesOutputDTO>> MesaPorId(Guid id)
        {
            Response<MesOutputDTO> resposta = new Response<MesOutputDTO>();
            try
            {
                Mesa mesa = await _mesaRepository.MesaPorId(id);
                if (mesa == null)
                {
                    resposta.Mensagem = "Mesa não encontrada.";
                    resposta.Status = false;
                    return resposta;
                }
                resposta.Dados = MesaMap.MapMesa(mesa);
                resposta.Mensagem = "Mesa encontrada com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<MesOutputDTO>> CriarMesa(MesCriacaoDTO mesa)
        {
            Response<MesOutputDTO> resposta = new Response<MesOutputDTO>();
            try
            {
                ValidarMesaCriacao(mesa);
                Mesa map = MesaMap.MapMesa(mesa);
                Mesa retorno = await _mesaRepository.CriarMesa(map);

                resposta.Dados = MesaMap.MapMesa(retorno);
                resposta.Mensagem = "Mesa Criada com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<Response<MesOutputDTO>> AtualizarMesa(MesEdicaoDTO mesa)
        {
            Response<MesOutputDTO> resposta = new Response<MesOutputDTO>();
            try
            {
                ValidarCategoriaEdicao(mesa);
                Mesa buscarMesa = await _mesaRepository.MesaPorId(mesa.Id);
                if (buscarMesa == null)
                {
                    resposta.Mensagem = "Mesa não encontrada.";
                    resposta.Status = false;
                    return resposta;
                }
                AtualizarDadosCategoria(buscarMesa, mesa);
                Mesa retorno = await _mesaRepository.AtualizarMesa(buscarMesa);

                resposta.Dados = MesaMap.MapMesa(retorno);
                resposta.Mensagem = "Mesas atualizada com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }


        }

        public async Task<Response<MesOutputDTO>> DeletarMesa(Guid id)
        {
            Response<MesOutputDTO> resposta = new Response<MesOutputDTO>();
            try
            {
                Mesa mesa = await _mesaRepository.MesaPorId(id);
                if (mesa == null)
                {
                    resposta.Mensagem = "Mesa não encontrada.";
                    resposta.Status = false;
                    return resposta;
                }
                Mesa retorno = await _mesaRepository.DeletarMesa(id);
                resposta.Dados = MesaMap.MapMesa(retorno);
                resposta.Mensagem = "Mesas listadas com sucesso";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        private void ValidarMesaCriacao(MesCriacaoDTO mesa)
        {
            ValidadorUtils.ValidarInteiroSeVazioOuNulo(mesa.MesaNumero, "Numero da mesa é obrigatório");
            if (!Enum.IsDefined(typeof(MesaStatus), mesa.MesaStatus))
            {
                throw new Exception("Status da mesa é obrigatório");
            }
        }

        private void ValidarCategoriaEdicao(MesEdicaoDTO mesa)
        {
            ValidadorUtils.ValidarInteiroSeVazioOuNulo(mesa.MesaNumero, "Numero da mesa é obrigatório");
            if (!Enum.IsDefined(typeof(MesaStatus), mesa.MesaStatus))
            {
                throw new Exception("Status da mesa é obrigatório");
            }
        }

        private void AtualizarDadosCategoria(Mesa mesaExistente, MesEdicaoDTO mesa)
        {
            mesaExistente.MesaNumero = mesa.MesaNumero;
            mesaExistente.MesaStatus = mesa.MesaStatus;
        }
    }
}
