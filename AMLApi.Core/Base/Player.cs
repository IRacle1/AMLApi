using AMLApi.Core.Data;
using AMLApi.Core.Enums;

namespace AMLApi.Core.Base
{
    public abstract class Player : IEquatable<Player>
    {
        protected readonly PlayerData playerData;

        protected Player(PlayerData data)
        {
            playerData = data;
            DiscordId = ulong.TryParse(data.DiscordId, out ulong res) ? res : 0;
            IsPlaceholder = DiscordId == 0;
            AvatarUrl = string.Format(AvatarUrlFormat, Guid.ToString());
            Continent = Extensions.ContinentFromString(data.Continent);
        }

        public const string AvatarUrlFormat = "https://zirlaiexwekjusbhjibc.supabase.co/storage/v1/object/public/avatars/{0}.jpg";

        public Guid Guid => playerData.Guid;
        public int Id => playerData.Id;
        public string Name => playerData.Name;
        public string? Email => playerData.Email;
        public DateTime CreatedAt => playerData.CreatedAt;

        public string AvatarUrl { get; }
        public string? YoutubeUrl => playerData.YoutubeUrl;
        public string? DiscordName => playerData.DiscordName;
        public ulong DiscordId { get; }

        public int ModesBeaten => playerData.ModesBeaten;

        public Continent Continent { get; }
        public string? Country => playerData.Country;

        public bool IsAdmin => playerData.IsAdmin;
        public bool IsManager => playerData.IsManager;
        public bool IsBanned => playerData.IsBanned;
        public bool IsHidden => playerData.IsHidden;

        public string? Clan => playerData.Clan;

        public bool IsPlaceholder { get; }

        public int GetPointsBy(PointType pointType)
        {
            int res = 0;
            if (pointType.HasFlag(PointType.Rng))
                res += playerData.RngPoints ?? 0;
            if (pointType.HasFlag(PointType.Skill))
                res += playerData.SkillPoints ?? 0;

            return res;
        }

        public int GetStatValue(StatType statType)
        {
            if (statType == StatType.MaxModeBeaten)
                return ModesBeaten;

            return GetPointsBy((PointType)(int)statType);
        }

        public int GetMaxPointsBy(PointType pointType)
        {
            int res = 0;
            if (pointType.HasFlag(PointType.Rng))
                res += playerData.RngMaxPoints ?? 0;
            if (pointType.HasFlag(PointType.Skill))
                res += playerData.SkillMaxPoints ?? 0;

            return res;
        }

        public int GetRankBy(StatType statType)
        {
            int? targetVal = statType switch
            {
                StatType.Skill => playerData.SkillRank,
                StatType.Rng => playerData.RngRank,
                StatType.Overall => playerData.TotalRank,
                StatType.MaxModeBeaten => playerData.ModesBeatenRank,
                _ => int.MaxValue,
            };

            return targetVal.GetValueOrDefault(int.MaxValue);
        }

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
