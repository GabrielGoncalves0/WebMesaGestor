using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebMesaGestor.Domain.Entities
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MesaStatus { Livre, Ocupada, SemMovimento, Pagamento }
    public class Mesa
    {
        public Mesa()
        {
        }
        [Key]
        public Guid Id { get; set; }
        public int MesaNumero { get; set; }
        public MesaStatus MesaStatus { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
