namespace WebMesaGestor.Application.DTO.Output
{
    public class OpcaoProPedDTO
    {
        public Guid? Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public virtual OpcaoDTO Opcao { get; set; }
        public virtual ProdutoPedidoDTO ProPed { get; set; }
    }
}
