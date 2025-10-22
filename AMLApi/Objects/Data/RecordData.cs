using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AMLApi.Core.Objects.Data
{
    public class RecordData
    {
        [JsonPropertyName("userid")]
        public Guid UId { get; set; }

        [JsonPropertyName("levelid")]
        public int MaxModeId { get; set; }

        [JsonPropertyName("videoLink")]
        public string VideoLink { get; set; } = null!;

        [JsonPropertyName("progress")]
        public int? Progress { get; set; }

        [JsonPropertyName("timestamp")]
        public long DateRaw { get; set; }

        [JsonIgnore]
        public DateTime DateUtc =>
            DateTimeOffset.FromUnixTimeMilliseconds(DateRaw).UtcDateTime;

        [JsonPropertyName("timetaken")]
        public long? TimeTakenRaw { get; set; }

        [JsonIgnore]
        public TimeSpan? TimeTaken => TimeTakenRaw is null ? null : TimeSpan.FromMilliseconds(TimeTakenRaw.GetValueOrDefault());

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
    }
}
