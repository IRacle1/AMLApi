using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AMLApi.Api.Objects
{
    public abstract class Record : IEquatable<Record>
    {
        public abstract string VideoLink { get; }
        public abstract int Progress { get; }
        public abstract DateTime DateUtc { get; }
        public abstract TimeSpan? TimeTaken { get; } 
        public abstract bool IsMobile { get; }
        public abstract bool IsChecked { get; }
        public abstract string? Comment { get; }
        public abstract int FPS { get; }
        public abstract bool IsNotificationSent { get; }

        public abstract MaxMode MaxMode { get; }
        public abstract Player Player { get; }

        public bool Equals(Record? other)
        {
            if (other == null)
                return false;

            return MaxMode.Equals(other.MaxMode) && 
                Player.Equals(other.Player);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Record);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(MaxMode.GetHashCode(), Player.GetHashCode());
        }

        public override string ToString()
        {
            return MaxMode.ToString() + " + " + Player.ToString();
        }
    }
}
