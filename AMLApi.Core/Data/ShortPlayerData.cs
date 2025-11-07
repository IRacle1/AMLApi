using System.Text.Json.Serialization;

namespace AMLApi.Core.Data
{
    public class ShortPlayerData
    {
        [JsonPropertyName("id")]
        public Guid Guid { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;
    }
}
