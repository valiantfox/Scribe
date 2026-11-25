using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Scribe
{
    public class Vector2Converter : JsonConverter<Vector2>
    {
        public override void WriteJson(JsonWriter writer, Vector2 value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("x");
            writer.WriteValue(value.x);
            writer.WritePropertyName("y");
            writer.WriteValue(value.y);
            writer.WriteEndObject();
        }

        public override Vector2 ReadJson(JsonReader reader, Type objectType, Vector2 existingValue,
            bool hasExistingValue, JsonSerializer serializer)
        {
            float x = 0, y = 0;
            if (reader.TokenType == JsonToken.Null)
                throw new JsonSerializationException("Cannot read null as Vector2.");

            while (reader.Read())
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    var propertyName = (string)reader.Value;
                    if (!reader.Read()) continue;
                    switch (propertyName)
                    {
                        case "x": x = Convert.ToSingle(reader.Value); break;
                        case "y": y = Convert.ToSingle(reader.Value); break;
                    }
                }
                else if (reader.TokenType == JsonToken.EndObject)
                    return new Vector2(x, y);

            throw new JsonSerializationException("Unexpected end when reading Vector2.");
        }
    }
}
