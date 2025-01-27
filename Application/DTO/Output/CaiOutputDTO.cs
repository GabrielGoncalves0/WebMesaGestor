using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.DTO.Output
{
    public class CaiOutputDTO
    {
        public Guid? Id { get; set; }
        public decimal CaiValInicial { get; set; }
        public decimal? CaiValFechamento { get; set; }
        public DateTime AberturaData { get; set; }
        public DateTime? FechamentoData { get; set; }
        public CaixaStatus? CaiStatus { get; set; }
        public virtual UsuOutputDTO? Usuario { get; set; }
    }
}
