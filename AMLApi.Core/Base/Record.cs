using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Core.Base.Instances;
using AMLApi.Core.Data;

namespace AMLApi.Core.Base
{
    public abstract class Record : IEquatable<Record>
    {
        protected Record()
        {
        }

        /// <summary>
        /// Gets a target player guid.
        /// </summary>
        public abstract Guid PlayerGuid { get; }

        /// <summary>
        /// Gets a target maxmode id.
        /// </summary>
        public abstract int MaxModeId { get; }

        /// <summary>
        /// Gets a records video link.
        /// </summary>
        public abstract string VideoLink { get; }

        /// <summary>
        /// Gets a record 'progress'.
        /// </summary>
        /// <remarks>
        /// Will be 100 in 100% times.
        /// </remarks>
        public abstract int Progress { get; }

        /// <summary>
        /// Gets a record completion date.
        /// </summary>
        public abstract DateOnly CompletionDate { get; }

        /// <summary>
        /// Gets a time taken idk.
        /// </summary>
        /// <remarks>
        /// Idk
        /// </remarks>
        public abstract TimeSpan? TimeTaken { get; }

        /// <summary>
        /// Gets a value that indicates whether the record was on mobile.
        /// </summary>
        public abstract bool IsMobile { get; }

        /// <summary>
        /// Gets a value that indicates whether the record was checked.
        /// </summary>
        public abstract bool IsChecked { get; }

        /// <summary>
        /// Gets a record comment.
        /// </summary>
        public abstract string? Comment { get; }

        /// <summary>
        /// Gets a record video FPS.
        /// </summary>
        public abstract int? FPS { get; }

        /// <summary>
        /// Gets a value that indicates whether the notification was sent to a target player.
        /// </summary>
        public abstract bool IsNotificationSent { get; }

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
