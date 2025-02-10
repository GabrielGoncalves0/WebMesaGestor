namespace WebMesaGestor.Application.DTO.Input.Opcoes
{
    public class OpcEdicaoDTO
    {
        public Guid Id { get; set; }
        public string OpcaoDesc { get; set; }
        public decimal OpcaoValor { get; set; }
        public int OpcaoQuantMax { get; set; }
        public Guid GrupoOpcoesId { get; set; }
    }
}
