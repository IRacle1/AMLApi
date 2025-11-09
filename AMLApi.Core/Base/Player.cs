using AMLApi.Core.Data;
using AMLApi.Core.Enums;

namespace AMLApi.Core.Base
{
    /// <summary>
    /// Base abstract object for player.
    /// </summary>
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

        /// <summary>
        /// Gets a url format for custom avatar url's on aml.
        /// </summary>
        public const string AvatarUrlFormat = "https://zirlaiexwekjusbhjibc.supabase.co/storage/v1/object/public/avatars/{0}.jpg";

        /// <summary>
        /// Gets a player <see cref="System.Guid">guid</see>.
        /// </summary>
        public Guid Guid => playerData.Guid;

        /// <summary>
        /// Gets a player id.
        /// </summary>
        public int Id => playerData.Id;

        /// <summary>
        /// Gets a player nickname.
        /// </summary>
        public string Nickname => playerData.Name;

        /// <summary>
        /// Gets a player email, oh.
        /// </summary>
        /// <remarks>
        /// It will be null for 100% times, so idk dont use it.
        /// </remarks>
        public string? Email => playerData.Email;

        /// <summary>
        /// Gets a player account creation date time.
        /// </summary>
        public DateTime CreatedAt => playerData.CreatedAt;

        /// <summary>
        /// Gets a player avatar url.
        /// </summary>
        public string AvatarUrl { get; }

        /// <summary>
        /// Gets a player youtube channel url(if specified).
        /// </summary>
        public string? YoutubeUrl => playerData.YoutubeUrl;

        /// <summary>
        /// Gets a player discord nickname(if specified).
        /// </summary>
        public string? DiscordNickname => playerData.DiscordName;

        /// <summary>
        /// Gets a player discord id(can be 0).
        /// </summary>
        public ulong DiscordId { get; }

        /// <summary>
        /// Gets a player modes beaten count.
        /// </summary>
        public int ModesBeaten => playerData.ModesBeaten;

        /// <summary>
        /// Gets a player continent(if specified).
        /// </summary>
        public Continent Continent { get; }

        /// <summary>
        /// Gets a player country(if specified).
        /// </summary>
        public string? Country => playerData.Country;

        /// <summary>
        /// Gets a value that indicates whether the player is an admin.
        /// </summary>
        public bool IsAdmin => playerData.IsAdmin;

        /// <summary>
        /// Gets a value that indicates whether the player is a manager.
        /// </summary>
        public bool IsManager => playerData.IsManager;

        /// <summary>
        /// Gets a value that indicates whether the player is a banned.
        /// </summary>
        public bool IsBanned => playerData.IsBanned;

        /// <summary>
        /// Gets a value that indicates whether the player is a hidden.
        /// </summary>
        public bool IsHidden => playerData.IsHidden;

        /// <summary>
        /// Gets a cla tag for player.
        /// </summary>
        public string? Clan => playerData.Clan;

        /// <summary>
        /// Gets a value that indicates whether the player is a placeholder account.
        /// </summary>
        public bool IsPlaceholder { get; }

        /// <summary>
        /// Gets a player total points value by <see cref="PointType"/> argument.
        /// </summary>
        /// <param name="pointType"><see cref="PointType"/> needed to calculate.</param>
        /// <returns>Total points value.</returns>
        /// <remarks>
        /// Support <see cref="PointType"/> flags.
        /// </remarks>
        public int GetPointsBy(PointType pointType)
        {
            int res = 0;
            if (pointType.HasFlag(PointType.Rng))
                res += playerData.RngPoints ?? 0;
            if (pointType.HasFlag(PointType.Skill))
                res += playerData.SkillPoints ?? 0;

            return res;
        }

        /// <summary>
        /// Gets a player stst value by <see cref="StatType"/> argument.
        /// </summary>
        /// <param name="statType">Target <see cref="StatType"/>.</param>
        /// <returns>Total stat value.</returns>
        public int GetStatValue(StatType statType)
        {
            if (statType == StatType.MaxModeBeaten)
                return ModesBeaten;

            return GetPointsBy((PointType)(int)statType);
        }

        /// <summary>
        /// Gets a player max points value by <see cref="PointType"/> argument.
        /// </summary>
        /// <param name="pointType"><see cref="PointType"/> needed to calculate.</param>
        /// <returns>Total max points value.</returns>
        /// <remarks>
        /// Support <see cref="PointType"/> flags.
        /// 'Max points' means maxinum amount of points given form a single maxmode.
        /// </remarks>
        public int GetMaxPointsBy(PointType pointType)
        {
            int res = 0;
            if (pointType.HasFlag(PointType.Rng))
                res += playerData.RngMaxPoints ?? 0;
            if (pointType.HasFlag(PointType.Skill))
                res += playerData.SkillMaxPoints ?? 0;

            return res;
        }

        /// <summary>
        /// Gets a player rank value by <see cref="StatType"/> argument.
        /// </summary>
        /// <param name="statType">Target <see cref="StatType"/>.</param>
        /// <returns>Player rank value.</returns>
        /// <remarks>
        /// in most of cases when player has 0 records, most of the stats will return <see cref="int.MaxValue"/>.        
        /// </remarks>
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

        /// <inheritdoc/>
        public bool Equals(Player? other)
        {
            if (other is null)
                return false;

            return Id == other.Id;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return Equals(obj as Player);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return Nickname;
        }
    }
}
