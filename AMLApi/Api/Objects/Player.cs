using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Api.Enums;

namespace AMLApi.Api.Objects
{
    public abstract class Player : IEquatable<Player>
    {
        public abstract Guid Guid { get; }
        public abstract int Id { get; }
        public abstract string Name { get; }
        public abstract string Email { get; }
        public abstract DateTime CreatedAt { get; }

        public abstract string? AvatarUrl { get; }
        public abstract string? YoutubeUrl { get; }
        public abstract string? DiscordName { get; }
        public abstract ulong DiscordId { get; }

        public abstract int ModesBeaten { get; }

        public abstract string Continent { get; }
        public abstract string Country { get; }

        public abstract bool IsAdmin { get; }
        public abstract bool IsManager { get; }
        public abstract bool IsBanned { get; }
        public abstract bool IsHidden { get; }

        public abstract string? Clan { get; }

        public abstract IReadOnlyCollection<Record> RecordsCache { get; }
        public abstract bool RecordsFetched { get; }

        public abstract int GetRankBy(StatType statType);
        public abstract int GetPointsBy(PointType pointType);
        public abstract int GetMaxPointsBy(PointType pointType);

        public abstract Task<IReadOnlyCollection<Record>> GetOrFetchRecords();

        public bool Equals(Player? other)
        {
            if (other is null)
                return false;

            return Id == other.Id;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Player);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
