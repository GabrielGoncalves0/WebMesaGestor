using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;

namespace WebMesaGestor.Domain.Entities
{
    public enum TranStatus { Entrada, Saida, Sangria, Suprimento }
    public class Transacao
    {
        public Guid Id { get; set; }
        [StringLength(100)]
        public string TraDescricao { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal TraValor { get; set; }
        public TranStatus TransacaoStatus { get; set; }
        public DateTime CriacaoData { get; set; }
        public Guid? UsuarioId { get; set; }
        public virtual Usuario? Usuario { get; set; }
        public Guid? CaixaId { get; set; }
        public virtual Caixa? Caixa { get; set; }
        public Guid? PedidoId { get; set; }
        public virtual Pedido? Pedido { get; set; }
    }
}
