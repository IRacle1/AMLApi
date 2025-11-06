using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AMLApi.Core.Data
{
    public class ShortMaxModeData
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("videoID")]
        public string? VideoId { get; set; }

        [JsonPropertyName("game")]
        public string GameName { get; set; } = string.Empty;

        [JsonPropertyName("top")]
        public int Top { get; set; }
    }
}
