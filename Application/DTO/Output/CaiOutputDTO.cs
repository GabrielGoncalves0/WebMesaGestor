using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.DTO.Output
{
    public class CaiOutputDTO
    {
        public Guid Id { get; set; }
        public decimal Cai_Val_Inicial { get; set; }
        public decimal? Cai_Val_Fechamento { get; set; }
        public DateTime Abertura_data { get; set; }
        public DateTime? Fechamento_data { get; set; }
        public CaixaStatus Cai_status { get; set; }
        public virtual UsuOutputDTO Usuario { get; set; }
    }
}
