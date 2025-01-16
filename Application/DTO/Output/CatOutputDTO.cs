using System.ComponentModel.DataAnnotations;

namespace WebMesaGestor.Application.DTO.Output
{
    public class CatOutputDTO
    {
        public Guid? Id { get; set; }
        public string CatDesc { get; set; }
        public DateTime CriacaoData { get; set; }
    }
}
