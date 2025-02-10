using System.ComponentModel.DataAnnotations;

namespace WebMesaGestor.Application.DTO.Output
{
    public class CategoriaDTO
    {
        public Guid? Id { get; set; }
        public string CatDesc { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
