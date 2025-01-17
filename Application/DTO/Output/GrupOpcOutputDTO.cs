using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Application.DTO.Output
{
    public class GrupOpcOutputDTO
    {
        public Guid? Id { get; set; }
        public string GrupOpcDesc { get; set; }
        public int GrupOpcMax { get; set; }
        public GrupOpcTipo GrupOpcTipo { get; set; }
        public DateTime CriacaoData { get; set; }
        public ProOutputDTO? produto { get; set; }
    }
}
