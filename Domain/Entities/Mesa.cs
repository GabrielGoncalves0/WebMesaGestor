using System.ComponentModel.DataAnnotations;

namespace WebMesaGestor.Domain.Entities
{
    public enum MesaStatus { Livre, Ocupada }
    public class Mesa
    {
        public Mesa()
        {
        }
        [Key]
        public Guid Id { get; set; }
        public int MesaNumero { get; set; }
        public MesaStatus MesaStatus { get; set; }
        public DateTime CriacaoData { get; set; }
    }
}
