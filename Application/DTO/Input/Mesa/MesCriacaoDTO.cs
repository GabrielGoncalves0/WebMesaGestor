using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.DTO.Input.Mesa
{
    public class MesCriacaoDTO
    {
        public int MesaNumero { get; set; }
        public MesaStatus MesaStatus { get; set; }
    }
}
