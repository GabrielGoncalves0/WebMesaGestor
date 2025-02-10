namespace WebMesaGestor.Application.Common
{
    public class Response<T>
    {
        public bool Sucesso { get; set; }
        public List<string> Erros { get; set; } = new List<string>();
        public T Data { get; set; }
        public Guid Id { get; set; }
    }
}


