using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.DTO.Input.Caixa
{
    public class CaiFecharDTO
    {
        public Guid Id { get; set; }
        public decimal? Cai_Val_Fechamento { get; set; }
    }
}
