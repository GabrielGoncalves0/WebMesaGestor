using System.ComponentModel.DataAnnotations;

namespace WebMesaGestor.Application.DTO.Output
{
    public class MarOutputDTO
    {
        public Guid? Id { get; set; }
        public string MarNome { get; set; }
        public DateTime CriacaoData { get; set; }
    }
}
