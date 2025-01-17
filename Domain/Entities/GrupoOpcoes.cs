using System.ComponentModel.DataAnnotations;

namespace WebMesaGestor.Domain.Entities
{
    public enum GrupOpcTipo { unica, multipla }
    public class GrupoOpcoes
    {
        public GrupoOpcoes()
        {
        }
        [Key]
        public Guid Id { get; set; }
        [StringLength(100)]
        public string GrupOpcDesc { get; set; }
        public int GrupOpcMax { get; set; }
        public GrupOpcTipo GrupOpcTipo { get; set; }
        public DateTime CriacaoData { get; set; }
        public Guid? ProdutoId { get; set; }
        public virtual Produto? Produto { get; set; }
    }
}
