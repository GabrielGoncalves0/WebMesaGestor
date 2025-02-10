using System.Text.Json.Serialization;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.DTO.Output
{
    public class MesaDTO
    {
        public Guid? Id { get; set; }
        public int MesaNumero { get; set; }
        public MesaStatus MesaStatus { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
