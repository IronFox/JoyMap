using System.Text.Json;
using System.Text.Json.Serialization;

namespace JoyMap.Util
{
    public static class JsonUtil
    {
        // Shared serializer options: enums as strings, indented output.
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        };

        public static string Serialize<T>(T item)
        {
            return JsonSerializer.Serialize(item, JsonOptions);
        }

        public static T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, JsonOptions)
                ?? throw new JsonException("Expected deserialized JSON to not produce null: " + json);
        }
    }
}
