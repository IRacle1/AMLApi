
using AMLApi.Core.Data;

namespace AMLApi.Core.Objects
{
    public abstract class Record : IEquatable<Record>
    {
        protected readonly RecordData recordData;

        protected Record(RecordData data)
        {
            recordData = data;
            DateUtc = DateTimeOffset.FromUnixTimeMilliseconds(data.DateUtc).UtcDateTime;
            if (data.TimeTaken is not null)
                TimeTaken = TimeSpan.FromMilliseconds(data.TimeTaken.Value);
        }

        public Guid PlayerGuid => recordData.UId;
        public int MaxModeId => recordData.MaxModeId;

        public string VideoLink => recordData.VideoLink;

        public int Progress => recordData.Progress ?? 100;

        public DateTime DateUtc { get; }
        public TimeSpan? TimeTaken { get; }

        public bool IsMobile => recordData.IsMobile != 0;
        public bool IsChecked => recordData.IsChecked;

        public string? Comment => recordData.Comment;

        public int? FPS => recordData.FPS;

        public bool IsNotificationSent => recordData.IsNotificationSent;

        public bool Equals(Record? other)
        {
            if (other == null)
                return false;

            return MaxModeId == other.MaxModeId && 
                PlayerGuid == other.PlayerGuid;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Record);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(MaxModeId, PlayerGuid);
        }

        public override string ToString()
        {
            return MaxModeId.ToString() + " + " + PlayerGuid.ToString();
        }
    }
}
