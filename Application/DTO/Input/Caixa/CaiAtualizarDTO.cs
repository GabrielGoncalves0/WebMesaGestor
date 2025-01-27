using System.ComponentModel.DataAnnotations.Schema;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.DTO.Input.Caixa
{
    public class CaiAtualizarDTO
    {
        public Guid Id { get; set; }
        public decimal? CaiValTotal { get; set; }
        public CaixaStatus? CaiStatus { get; set; }
    }
}
