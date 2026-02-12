using System.Text.Json.Serialization;

using AMLApi.Core.Data.MaxModes;
using AMLApi.Core.Data.Players;

namespace AMLApi.Core.Data
{
    public class RecordData
    {
        [JsonPropertyName("userid")]
        public Guid UId { get; set; }

        [JsonPropertyName("levelid")]
        public int MaxModeId { get; set; }

        [JsonPropertyName("videoLink")]
        public string VideoLink { get; set; } = null!;

        [JsonPropertyName("skillValue")]
        public int SkillValue { get; set; }

        [JsonPropertyName("rngValue")]
        public int RngValue { get; set; }

        [JsonPropertyName("progress")]
        public int? Progress { get; set; }

        [JsonPropertyName("timestamp")]
        public long? DateUtc { get; set; }

        [JsonPropertyName("timetaken")]
        public long? TimeTaken { get; set; }

        [JsonPropertyName("mobile")]
        public int IsMobile { get; set; }

        [JsonPropertyName("isChecked")]
        public bool IsChecked { get; set; }

        [JsonPropertyName("comment")]
        public string? Comment { get; set; }

        [JsonPropertyName("refreshRate")]
        public int? FPS { get; set; }

        [JsonPropertyName("isNotificationSent")]
        public bool IsNotificationSent { get; set; }

        [JsonPropertyName("players")]
        public PlayerData? Player { get; set; }

        [JsonPropertyName("levels")]
        public ShortMaxModeData? MaxMode { get; set; }
    }
}
