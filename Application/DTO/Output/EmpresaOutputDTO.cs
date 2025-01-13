namespace WebMesaGestor.Application.DTO.Output
{
    public class EmpOutputDTO
    {
        public Guid? Id { get; set; }
        public string Emp_nome { get; set; }
        public string Emp_cnpj { get; set; }
        public DateTime Criacao_data { get; set; }
    }
}
