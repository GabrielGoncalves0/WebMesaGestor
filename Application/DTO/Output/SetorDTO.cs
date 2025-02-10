using System.Text.Json.Serialization;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.DTO.Output
{
    public class SetorDTO
    {
        public Guid? Id { get; set; }
        public string SetDesc { get; set; }
        public SetorStatus SetStatus { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
