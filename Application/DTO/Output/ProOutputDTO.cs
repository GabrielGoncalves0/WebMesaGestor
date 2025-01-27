using System.Text.Json.Serialization;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Application.DTO.Auxiliar;


namespace WebMesaGestor.Application.DTO.Output
{
    public class ProOutputDTO
    {
        public Guid? Id { get; set; }
        public int ProCodigo { get; set; }
        public string ProDescricao { get; set; }
        public string ProUnidade { get; set; }
        [JsonConverter(typeof(DecimalConverter))]
        public decimal ProPreco { get; set; }
        public DateTime CriacaoData { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual CatOutputDTO? Categoria { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual SetOutputDTO? Setor { get; set; }
    }
}
