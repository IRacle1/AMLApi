using System.Text.Json.Serialization;

namespace AMLApi.Core.Data.MaxModes
{
    public class ShortMaxModeData
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("videoID")]
        public string VideoId { get; set; } = string.Empty;

        [JsonPropertyName("game")]
        public string GameName { get; set; } = string.Empty;

        [JsonPropertyName("top")]
        public int Top { get; set; }
    }
}
