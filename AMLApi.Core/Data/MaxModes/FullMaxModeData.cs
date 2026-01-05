using System.Text.Json.Serialization;

namespace AMLApi.Core.Data.MaxModes
{
    public class FullMaxModeData
    {
        [JsonPropertyName("data")]
        public MaxModeData Data { get; set; } = null!;

        [JsonPropertyName("records")]
        public RecordData[] Records { get; set; } = null!;
    }
}
