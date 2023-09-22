using System.Text.Json;
using System.Text.Json.Serialization;

namespace Paradigm.WindowsAppSDK.Services.LegacyConfiguration.JsonConverters
{
    internal class DictionaryStringObjectJsonConverter : JsonConverter<Dictionary<string, object>>
    {
        public override Dictionary<string, object> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException($"JsonTokenType was of type {reader.TokenType}, only objects are supported");

            var dictionary = new Dictionary<string, object>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                    return dictionary;

                if (reader.TokenType != JsonTokenType.PropertyName)
                    throw new JsonException("JsonTokenType was not PropertyName");

                var propertyName = reader.GetString();

                if (string.IsNullOrWhiteSpace(propertyName))
                    throw new JsonException("Failed to get property name");

                reader.Read();

                var value = ExtractValue(ref reader, options);
                if (value is not null)
                    dictionary.Add(propertyName, value);
            }

            return dictionary;
        }

        public override void Write(Utf8JsonWriter writer, Dictionary<string, object> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }

        private object? ExtractValue(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    if (reader.TryGetDateTime(out var date))
                        return date;

                    return reader.GetString();

                case JsonTokenType.False:
                    return false;

                case JsonTokenType.True:
                    return true;

                case JsonTokenType.Null:
                    return null;

                case JsonTokenType.Number:
                    return reader.TryGetInt64(out var result) ? result : reader.GetDouble();

                case JsonTokenType.StartObject:
                    return Read(ref reader, typeof(object), options);

                case JsonTokenType.StartArray:
                    var list = new List<object>();
                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        var value = ExtractValue(ref reader, options);
                        if (value is not null) list.Add(value);
                    }
                    return list;

                default:
                    throw new JsonException($"'{reader.TokenType}' is not supported");
            }
        }
    }
}
