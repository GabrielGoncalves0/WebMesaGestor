using System.Text.Json.Serialization;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.DTO.Input.Grupo
{
    public class GrupOpcCriacaoDTO
    {
        public string GrupOpcDesc { get; set; }
        public GrupOpcTipo GrupOpcTipo { get; set; }
        public int GrupOpcMax { get; set; }
        public Guid ProdutoId { get; set; }
    }
}
