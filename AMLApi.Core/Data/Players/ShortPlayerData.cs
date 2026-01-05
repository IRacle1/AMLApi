using System.Text.Json.Serialization;

namespace AMLApi.Core.Data.Players
{
    public class ShortPlayerData
    {
        [JsonPropertyName("id")]
        public Guid Guid { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;
    }
}
