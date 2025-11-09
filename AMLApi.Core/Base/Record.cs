using AMLApi.Core.Data;

namespace AMLApi.Core.Base
{
    /// <summary>
    /// Base abstract object for records.
    /// </summary>
    public abstract class Record : IEquatable<Record>
    {
        protected readonly RecordData recordData;

        protected Record(RecordData data)
        {
            recordData = data;
            CompletionDate = DateOnly.FromDateTime(DateTimeOffset.FromUnixTimeMilliseconds(data.DateUtc).UtcDateTime);
            if (data.TimeTaken is not null)
                TimeTaken = TimeSpan.FromMilliseconds(data.TimeTaken.Value);
        }

        /// <summary>
        /// Gets a target player guid.
        /// </summary>
        public Guid PlayerGuid => recordData.UId;

        /// <summary>
        /// Gets a target maxmode id.
        /// </summary>
        public int MaxModeId => recordData.MaxModeId;

        /// <summary>
        /// Gets a records video link.
        /// </summary>
        public string VideoLink => recordData.VideoLink;

        /// <summary>
        /// Gets a record 'progress'.
        /// </summary>
        /// <remarks>
        /// Will be 100 in 100% times.
        /// </remarks>
        public int Progress => recordData.Progress ?? 100;

        /// <summary>
        /// Gets a record completion date.
        /// </summary>
        public DateOnly CompletionDate { get; }

        /// <summary>
        /// Gets a time taken idk.
        /// </summary>
        /// <remarks>
        /// Idk
        /// </remarks>
        public TimeSpan? TimeTaken { get; }

        /// <summary>
        /// Gets a value that indicates whether the record was on mobile.
        /// </summary>
        public bool IsMobile => recordData.IsMobile != 0;

        /// <summary>
        /// Gets a value that indicates whether the record was checked.
        /// </summary>
        public bool IsChecked => recordData.IsChecked;

        /// <summary>
        /// Gets a record comment.
        /// </summary>
        public string? Comment => recordData.Comment;

        /// <summary>
        /// Gets a record video FPS.
        /// </summary>
        public int? FPS => recordData.FPS;

        /// <summary>
        /// Gets a value that indicates whether the notification was sent to a target player.
        /// </summary>
        public bool IsNotificationSent => recordData.IsNotificationSent;

        /// <inheritdoc/>
        public bool Equals(Record? other)
        {
            if (other == null)
                return false;

            return MaxModeId == other.MaxModeId &&
                PlayerGuid == other.PlayerGuid;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return Equals(obj as Record);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(MaxModeId, PlayerGuid);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return MaxModeId.ToString() + " + " + PlayerGuid.ToString();
        }
    }
}
