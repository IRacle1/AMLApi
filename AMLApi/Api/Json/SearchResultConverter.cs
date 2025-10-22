using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using AMLApi.Api.Objects.Data;

namespace AMLApi.Api.Json
{
    internal class SearchResultConverter : JsonConverter<SearchResult>
    {
        public override SearchResult Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            reader.Read();
            var maxModes = JsonSerializer.Deserialize<MaxModeData[]>(ref reader, options);

            reader.Read();
            var players = JsonSerializer.Deserialize<ShortPlayerData[]>(ref reader, options);

            reader.Read();

            return new SearchResult
            {
                MaxModes = maxModes!,
                Players = players!
            };
        }

        public override void Write(Utf8JsonWriter writer, SearchResult value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            JsonSerializer.Serialize(writer, value.MaxModes, options);
            JsonSerializer.Serialize(writer, value.Players, options);
            writer.WriteEndArray();
        }
    }
}
