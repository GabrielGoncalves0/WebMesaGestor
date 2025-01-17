namespace WebMesaGestor.Domain.Entities
{
    public class RespostaPadrao<T>
    {
        public string Mensagem { get; set; }
        public T Dados { get; set; }
        public int Codigo { get; set; }

        public RespostaPadrao(string mensagem, T dados, int codigo)
        {
            Mensagem = mensagem;
            Dados = dados;
            Codigo = codigo;
        }
    }
}


