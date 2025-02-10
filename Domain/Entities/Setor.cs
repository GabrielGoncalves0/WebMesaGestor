using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebMesaGestor.Domain.Entities
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SetorStatus { Ativo, Inativo }
    public class Setor
    {
        public Setor()
        {
        }
        [Key]
        public Guid Id { get; set; }
        [StringLength(100)]
        public string SetDesc { get; set; }
        public SetorStatus SetStatus { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
