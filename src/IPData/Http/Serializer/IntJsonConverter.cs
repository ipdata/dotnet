using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace IPData.Http.Serializer
{
    internal class IntJsonConverter : JsonConverter<int>
    {
        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var stringValue = reader.GetString();
                if (int.TryParse(stringValue, out var value))
                {
                    return value;
                }

                throw new JsonException($"Unable to convert \"{stringValue}\" to System.Int32.");
            }

            return reader.GetInt32();
        }

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value);
        }
    }
}
