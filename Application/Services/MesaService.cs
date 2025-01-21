using WebMesaGestor.Application.DTO.Input.Mesa;
using WebMesaGestor.Application.DTO.Output;
using WebMesaGestor.Application.Map;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Domain.Interfaces;

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

        public async Task<MesOutputDTO> MesaPorId(Guid id)
        {
            Mesa mesa = await _mesaRepository.MesaPorId(id);
            return MesaMap.MapMesa(mesa);
        }

        public async Task<MesOutputDTO> CriarMesa(MesCriacaoDTO mesa)
        {
            Mesa map = MesaMap.MapMesa(mesa);
            Mesa retorno = await _mesaRepository.CriarMesa(map);
            return MesaMap.MapMesa(retorno);
        }

        public async Task<MesOutputDTO> AtualizarMesa(MesEdicaoDTO mesa)
        {
            Mesa buscarMesa = await _mesaRepository.MesaPorId(mesa.Id);

            buscarMesa.MesaNumero = mesa.MesaNumero;
            buscarMesa.MesaStatus = mesa.MesaStatus;

            Mesa retorno = await _mesaRepository.AtualizarMesa(buscarMesa);
            return MesaMap.MapMesa(retorno);
        }

        public async Task<MesOutputDTO> DeletarMesa(Guid id)
        {
            Mesa retorno = await _mesaRepository.DeletarMesa(id);
            return MesaMap.MapMesa(retorno);
        }
    }
}
