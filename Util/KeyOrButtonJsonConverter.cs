using JoyMap.Windows;
using JoyMap.XBox;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JoyMap.Util
{
    public class KeyOrButtonJsonConverter : JsonConverter<KeyOrButton>
    {
        public override KeyOrButton Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Handle backwards compatibility: if it's a string/number, treat as Keys enum
            if (reader.TokenType == JsonTokenType.String)
            {
                var str = reader.GetString();
                if (Enum.TryParse<Keys>(str, out var key1))
                    return KeyOrButton.From(key1);
                return default;
            }
            if (reader.TokenType == JsonTokenType.Number)
            {
                var num = reader.GetInt32();
                return KeyOrButton.From((Keys)num);
            }

            // Modern format: object with Key and/or XBoxButton properties
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException("Expected object or string/number for KeyOrButton");

            Keys? key = null;
            XBoxButton? xboxButton = null;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                    break;

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propName = reader.GetString();
                    reader.Read();

                    if (string.Equals(propName, "Key", StringComparison.OrdinalIgnoreCase))
                    {
                        if (reader.TokenType == JsonTokenType.String)
                        {
                            var str = reader.GetString();
                            if (Enum.TryParse<Keys>(str, out var k))
                                key = k;
                        }
                        else if (reader.TokenType == JsonTokenType.Number)
                        {
                            key = (Keys)reader.GetInt32();
                        }
                    }
                    else if (string.Equals(propName, "XBoxButton", StringComparison.OrdinalIgnoreCase))
                    {
                        if (reader.TokenType == JsonTokenType.String)
                        {
                            var str = reader.GetString();
                            if (Enum.TryParse<XBoxButton>(str, out var xb))
                                xboxButton = xb;
                        }
                        else if (reader.TokenType == JsonTokenType.Number)
                        {
                            xboxButton = (XBoxButton)reader.GetInt32();
                        }
                    }
                }
            }

            return new KeyOrButton(Key: key, XBoxButton: xboxButton);
        }

        public override void Write(Utf8JsonWriter writer, KeyOrButton value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            if (value.Key is not null)
                writer.WriteString("Key", value.Key.Value.ToString());
            if (value.XBoxButton is not null)
                writer.WriteString("XBoxButton", value.XBoxButton.Value.ToString());
            writer.WriteEndObject();
        }
    }
}
