using AMLApi.Core.Data.Clans;
using AMLApi.Core.Data.Players;
using AMLApi.Core.Enums;

namespace AMLApi.Core.Base.Instances
{
    /// <summary>
    /// Base abstract object for player.
    /// </summary>
    public abstract class AmlPlayer : Player
    {
        protected readonly PlayerData playerData;

        protected AmlPlayer(PlayerData data)
        {
            playerData = data;
            DiscordId = ulong.TryParse(data.DiscordId, out ulong res) ? res : 0;
            IsPlaceholder = DiscordId == 0;
            AvatarUrl = string.Format(AvatarUrlFormat, Guid.ToString());
            Continent = data.Continent.ContinentFromString();
        }

       /// <inheritdoc/>
        public override Guid Guid => playerData.Guid;

        /// <inheritdoc/>
        public override int Id => playerData.Id;

        /// <inheritdoc/>
        public override string Nickname => playerData.Name;

        /// <inheritdoc/>
        public override string? Email => playerData.Email;

        /// <inheritdoc/>
        public override DateTime CreatedAt => playerData.CreatedAt;

        /// <inheritdoc/>
        public override string AvatarUrl { get; }

        /// <inheritdoc/>
        public override string? YoutubeUrl => playerData.YoutubeUrl;

        /// <inheritdoc/>
        public override string? DiscordNickname => playerData.DiscordName;

        /// <inheritdoc/>
        public override ulong DiscordId { get; }

        /// <inheritdoc/>
        public override int ModesBeaten => playerData.ModesBeaten;

        /// <inheritdoc/>
        public override Continent Continent { get; }

        /// <inheritdoc/>
        public override string? Country => playerData.Country;

        /// <inheritdoc/>
        public override bool IsAdmin => playerData.IsAdmin;

        /// <inheritdoc/>
        public override bool IsManager => playerData.IsManager;

        /// <inheritdoc/>
        public override bool IsBanned => playerData.IsBanned;

        /// <inheritdoc/>
        public override bool IsHidden => playerData.IsHidden;

        /// <inheritdoc/>
        public override string? ClanTag => playerData.ClanTag;

        /// <inheritdoc/>
        public override bool IsPlaceholder { get; }

        /// <inheritdoc/>
        public override int GetStatValue(StatType statType)
            => statType switch
            {
                StatType.Skill => playerData.SkillPoints ?? 0,
                StatType.Rng => playerData.RngPoints ?? 0,
                StatType.Overall => playerData.TotalPoints,
                StatType.MaxModeBeaten => ModesBeaten,
                _ => 0,
            };

        /// <inheritdoc/>
        public override int GetMaxPointsBy(PointType pointType)
        {
            int res = 0;
            if (pointType.HasFlag(PointType.Rng))
                res += playerData.RngMaxPoints ?? 0;
            if (pointType.HasFlag(PointType.Skill))
                res += playerData.SkillMaxPoints ?? 0;

            return res;
        }

        /// <inheritdoc/>
        public override int GetRankBy(StatType statType)
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
    }
}
