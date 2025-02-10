using System.Text.Json.Serialization;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.DTO.Output
{
    public class GrupoOpcoesDTO
    {
        public Guid? Id { get; set; }
        public string GrupOpcDesc { get; set; }
        public int GrupOpcMax { get; set; }
        public GrupOpcTipo GrupOpcTipo { get; set; }
        public DateTime DataCriacao { get; set; }
        public ProdutoDTO Produto { get; set; }
    }
}
