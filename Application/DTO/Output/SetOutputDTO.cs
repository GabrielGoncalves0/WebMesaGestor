using System.ComponentModel.DataAnnotations;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.DTO.Output
{
    public class SetOutputDTO
    {
        public Guid? Id { get; set; }
        public string SetDesc { get; set; }
        public SetorStatus SetStatus { get; set; }
        public DateTime CriacaoData { get; set; }
    }
}
