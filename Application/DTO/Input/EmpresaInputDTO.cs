namespace WebMesaGestor.Application.DTO.Input
{
    public class EmpresaInputDTO
    {
        public Guid? Id { get; set; }
        public string Emp_nome { get; set; }
        public string Emp_cnpj { get; set; }
        public string Criacao_data { get; set; }
    }
}
