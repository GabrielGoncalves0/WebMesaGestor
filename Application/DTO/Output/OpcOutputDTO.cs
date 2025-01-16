namespace WebMesaGestor.Application.DTO.Output
{
    public class OpcOutputDTO
    {
        public Guid Id { get; set; }
        public string OpcaoDesc { get; set; }
        public decimal OpcaoValor { get; set; }
        public int OpcaoQuantMax { get; set; }
        public DateTime CriacaoData { get; set; }
        public GrupOpcOutputDTO? GrupoOpcoes { get; set; }
    }
}
