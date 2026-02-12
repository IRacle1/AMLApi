using AMLApi.Core.Data;

namespace AMLApi.Core.Base.Instances
{
    /// <summary>
    /// Base abstract object for records.
    /// </summary>
    public abstract class AmlRecord : Record
    {
        protected readonly RecordData recordData;

        protected AmlRecord(RecordData data)
        {
            recordData = data;
            if (data.DateUtc is not null)
                CompletionDate = DateOnly.FromDateTime(DateTimeOffset.FromUnixTimeMilliseconds(data.DateUtc.Value).UtcDateTime);
            if (data.TimeTaken is not null)
                TimeTaken = TimeSpan.FromMilliseconds(data.TimeTaken.Value);
        }

        /// <inheritdoc/>
        public override Guid PlayerGuid => recordData.UId;

        /// <inheritdoc/>
        public override int MaxModeId => recordData.MaxModeId;

        /// <inheritdoc/>
        public override string VideoLink => recordData.VideoLink;

        /// <inheritdoc/>
        public override int Progress => recordData.Progress ?? 100;

        /// <inheritdoc/>
        public override DateOnly? CompletionDate { get; }

        /// <inheritdoc/>
        public override TimeSpan? TimeTaken { get; }

        /// <inheritdoc/>
        public override bool IsMobile => recordData.IsMobile != 0;

        /// <inheritdoc/>
        public override bool IsChecked => recordData.IsChecked;

        /// <inheritdoc/>
        public override string? Comment => recordData.Comment;

        /// <inheritdoc/>
        public override int? FPS => recordData.FPS;

        /// <inheritdoc/>
        public override bool IsNotificationSent => recordData.IsNotificationSent;
    }
}
