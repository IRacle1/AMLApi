using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AMLApi.Core.Data
{
    public class MaxModeData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("creator")]
        public string Creator { get; set; } = string.Empty;

        [JsonPropertyName("mmlength")]
        public string Length { get; set; } = string.Empty;

        [JsonPropertyName("videoID")]
        public string? VideoId { get; set; }

        [JsonPropertyName("game")]
        public string GameName { get; set; } = string.Empty;

        [JsonPropertyName("link")]
        public string GameUrl { get; set; } = string.Empty;

        [JsonPropertyName("skillValue")]
        public int SkillPoints { get; set; }

        [JsonPropertyName("rngValue")]
        public int RngPoints { get; set; }

        [JsonPropertyName("top")]
        public int Top { get; set; }

        [JsonPropertyName("selfimposed")]
        public bool IsSelfImposed { get; set; }

        [JsonPropertyName("prepatch")]
        public bool IsPrePatch { get; set; }

        [JsonPropertyName("extra")]
        public bool IsExtra { get; set; }

        [JsonPropertyName("mmotm")]
        public bool IsMaxModeOfTheMonth { get; set; }
    }
}
