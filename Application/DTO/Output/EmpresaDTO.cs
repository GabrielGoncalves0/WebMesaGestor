namespace WebMesaGestor.Application.DTO.Output
{
    public class EmpresaDTO
    {
        public Guid? Id { get; set; }
        public string EmpNome { get; set; }
        public string EmpCnpj { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
