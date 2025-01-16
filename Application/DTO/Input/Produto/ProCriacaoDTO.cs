namespace WebMesaGestor.Application.DTO.Input.Produto
{
    public class ProCriacaoDTO
    {
        public int ProCodigo { get; set; }
        public string ProDescricao { get; set; }
        public string ProUnidade { get; set; }
        public decimal ProPreco { get; set; }
        public Guid CategoriaId { get; set; }
        public Guid SetorId { get; set; }
    }
}
