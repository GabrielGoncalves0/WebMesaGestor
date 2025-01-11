namespace WebMesaGestor.Domain.Entities
{
    public class Empresa
    {
        public Guid Id { get; set; }
        public string Emp_nome { get; set; }
        public string Emp_cnpj { get; set; }
        public string Criacao_data { get; set; }
    }
}
