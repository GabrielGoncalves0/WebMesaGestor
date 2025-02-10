using System.Text.Json.Serialization;
using WebMesaGestor.Domain.Entities;
using WebMesaGestor.Application.DTO.Auxiliar;


namespace WebMesaGestor.Application.DTO.Output
{
    public class ProdutoDTO
    {
        public Guid? Id { get; set; }
        public int ProCodigo { get; set; }
        public string ProDescricao { get; set; }
        public string ProUnidade { get; set; }
        [JsonConverter(typeof(DecimalConverter))]
        public decimal ProPreco { get; set; }
        public DateTime DataCriacao { get; set; }
        public virtual CategoriaDTO Categoria { get; set; }
        public virtual SetorDTO Setor { get; set; }
    }
}
