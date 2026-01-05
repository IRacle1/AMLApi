using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Core.Base.Instances;
using AMLApi.Core.Data.Clans;
using AMLApi.Core.Enums;

namespace AMLApi.Core.Base
{
    public abstract class Clan : IEquatable<Clan>
    {
        public static string LogoUrlFormat = "https://zirlaiexwekjusbhjibc.supabase.co/storage/v1/object/public/clan-logos/{0}.jpg";

        protected Clan()
        {
        }

        public abstract Guid Guid { get; }

        public abstract string Name { get; }

        public abstract string Tag { get; }

        public abstract string Description { get; }

        public abstract string LogoUrl { get; }

        public abstract Guid OwnerId { get; }

        public abstract DateTime CreatedAtUtc { get; }

        public abstract int MembersCount { get; }

        public abstract int SkillRank { get; }

        public abstract int RngRank { get; }

        public abstract int ModesRank { get; }

        public abstract int OverallRank { get; }

        public abstract int GetTotalStatBy(StatType statType);

        public abstract double GetAveragePointBy(PointType pointType);

        public abstract int GetRankBy(StatType statType);

        /// <inheritdoc/>
        public bool Equals(Clan? other)
        {
            if (other is null)
                return false;

            return Guid == other.Guid;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return Equals(obj as Clan);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Guid.GetHashCode();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[{Tag}] | {Name}";
        }
    }
}
