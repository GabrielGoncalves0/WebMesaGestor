using System.Text.Json.Serialization;
using System.Text.Json;

namespace WebMesaGestor.Application.DTO.Auxiliar
{
    public class DecimalConverter : JsonConverter<decimal>
    {
        public DecimalConverter() { }

        public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetDecimal();
        }

        public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("F2"));
        }
    }
}
