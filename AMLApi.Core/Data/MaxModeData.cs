using System.Text.Json.Serialization;

namespace AMLApi.Core.Data
{
    public class MaxModeData : ShortMaxModeData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("creator")]
        public string Creator { get; set; } = null!;

        [JsonPropertyName("mmlength")]
        public string Length { get; set; } = null!;

        [JsonPropertyName("link")]
        public string GameUrl { get; set; } = null!;

        [JsonPropertyName("skillValue")]
        public int SkillPoints { get; set; }

        [JsonPropertyName("rngValue")]
        public int RngPoints { get; set; }

        [JsonPropertyName("selfimposed")]
        public bool IsSelfImposed { get; set; }

        [JsonPropertyName("prepatch")]
        public bool IsPrePatch { get; set; }

        [JsonPropertyName("extra")]
        public bool IsExtra { get; set; }

        [JsonPropertyName("mmotm")]
        public bool IsMaxModeOfTheMonth { get; set; }

        [JsonPropertyName("aim")]
        public int AimSkillSet { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("greenrun")]
        public int GreenrunSkillSet { get; set; }

        [JsonPropertyName("speed")]
        public int SpeedSkillSet { get; set; }

        [JsonPropertyName("keyboard")]
        public int KeyboardSkillSet { get; set; }

        [JsonPropertyName("brain")]
        public int BrainSkillSet { get; set; }

        [JsonPropertyName("endurance")]
        public int EnduranceSkillSet { get; set; }
    }
}
