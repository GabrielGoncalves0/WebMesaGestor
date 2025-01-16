namespace WebMesaGestor.Application.DTO.Input.Produto
{
    public class ProEdicaoDTO
    {
        public Guid Id { get; set; }
        public int ProCodigo { get; set; }
        public string ProDescricao { get; set; }
        public string ProUnidade { get; set; }
        public decimal ProPreco { get; set; }
        public Guid CategoriaId { get; set; }
        public Guid SetorId { get; set; }
    }
}
