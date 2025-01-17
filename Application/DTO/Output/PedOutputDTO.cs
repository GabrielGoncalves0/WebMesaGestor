using static WebMesaGestor.Domain.Entities.Pedido;

namespace WebMesaGestor.Application.DTO.Output
{
    public class PedOutputDTO
    {
        public Guid? Id { get; set; }
        public decimal PedValor { get; set; }
        public StatusPedido PedStatus { get; set; }
        public TipoPagPedido PedTipoPag { get; set; }
        public DateTime CriacaoData { get; set; }
        public virtual UsuOutputDTO? Usuario { get; set; }
        public virtual MesOutputDTO? Mesa { get; set; }
    }
}
