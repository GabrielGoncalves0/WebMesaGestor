using System.ComponentModel.DataAnnotations;

namespace WebMesaGestor.Domain.Entities
{
    public class OpcaoProPed
    {
        public OpcaoProPed()
        {
        }
        [Key]
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public virtual ProdutoPedido ProdutoPedido { get; set; }
        public Guid ProdutoPedidoId { get; set; }
        public virtual Opcao Opcao { get; set; }
        public Guid OpcaoId { get; set; }
    }
}
