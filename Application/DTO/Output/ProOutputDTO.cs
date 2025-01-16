namespace WebMesaGestor.Application.DTO.Output
{
    public class ProOutputDTO
    {
        public Guid? Id { get; set; }
        public int ProCodigo { get; set; }
        public string ProDescricao { get; set; }
        public string ProUnidade { get; set; }
        public decimal ProPreco { get; set; }
        public DateTime CriacaoData { get; set; }
        public virtual CatOutputDTO? Categoria { get; set; }
        public virtual SetOutputDTO? Setor { get; set; }
    }
}
