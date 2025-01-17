using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.DTO.Output
{
    public class TraOutputDTO
    {
        public Guid Id { get; set; }
        public string TraDescricao { get; set; }
        public decimal TraValor { get; set; }
        public TranStatus TransactionStatus { get; set; }
        public DateTime CriacaoData { get; set; }
        public virtual UsuOutputDTO? Usuario { get; set; }
        public virtual CaiOutputDTO? Caixa { get; set; }
        public virtual PedOutputDTO? Pedido { get; set; }
    }
}
