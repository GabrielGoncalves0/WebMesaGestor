using System.Reflection.Emit;

namespace WebMesaGestor.Application.DTO.Input.Opcoes
{
    public class OpcCriacaoDTO
    {
            public string OpcaoDesc { get; set; }
            public decimal OpcaoValor { get; set; }
            public int OpcaoQuantMax { get; set; }
            public Guid? GrupoOpcoesId { get; set; }
    }
}
