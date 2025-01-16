using System.ComponentModel.DataAnnotations;

namespace WebMesaGestor.Application.DTO.Output
{
    public class SubgrupOutputDTO
    {
        public Guid? Id { get; set; }
        public string SubgruDesc { get; set; }
        public DateTime CriacaoData { get; set; }
    }
}
