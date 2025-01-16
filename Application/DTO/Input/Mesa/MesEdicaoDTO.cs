using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.DTO.Input.Mesa
{
    public class MesEdicaoDTO
    {
        public Guid Id { get; set; }
        public int MesaNumero { get; set; }
        public MesaStatus MesaStatus { get; set; }
    }
}
