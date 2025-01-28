namespace WebMesaGestor.Application.DTO.Output
{
    public class OpcProPedOutputDTO
    {
        public Guid? Id { get; set; }
        public DateTime CriacaoData { get; set; }
        public virtual OpcOutputDTO? Opcao { get; set; }
        public virtual ProPedOutputDTO? ProPed { get; set; }
    }
}
