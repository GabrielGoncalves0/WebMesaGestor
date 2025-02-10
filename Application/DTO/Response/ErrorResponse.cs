using System.Text.Json.Serialization;

namespace WebMesaGestor.Application.DTO.Response
{
    public class ErrorResponse
    {
        public string Mensagem { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        private List<string> _erros = new List<string>();
    }
}
