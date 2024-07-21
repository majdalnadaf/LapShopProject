using System.Text.Json;
using System.Text.Json.Serialization;

namespace LapShopProject.Filters
{
    public class Int32Converter : JsonConverter<int>
    {
        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number)
            {
                return reader.GetInt32();
            }
            else if (reader.TokenType == JsonTokenType.String && int.TryParse(reader.GetString(), out int intValue))
            {
                return intValue;
            }
            else
            {
                throw new JsonException($"Cannot convert {reader.TokenType} to int.");
            }
        }

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value);
        }
    }
}
