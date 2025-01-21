namespace WebMesaGestor.Domain.Entities
{
    public class Response<T>
    {
        //Diz que o dado pode ser nullo, e os dados que vai receber nele é generico
        public T? Dados { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public bool Status { get; set; } = true;

    }
}


