using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Core.Base.Instances;
using AMLApi.Core.Data.Players;
using AMLApi.Core.Enums;

namespace AMLApi.Core.Base
{
    /// <summary>
    /// Base abstract object for player.
    /// </summary>
    public abstract class Player : ShortPlayer
    {
        protected Player()
        {
        }

        /// <summary>
        /// Gets a url format for custom avatar url's on aml.
        /// </summary>
        public const string AvatarUrlFormat = "https://zirlaiexwekjusbhjibc.supabase.co/storage/v1/object/public/avatars/{0}.jpg";

        /// <summary>
        /// Gets a player id.
        /// </summary>
        public abstract int Id { get; }

        /// <summary>
        /// Gets a player email, oh.
        /// </summary>
        /// <remarks>
        /// It will be null for 100% times, so idk dont use it.
        /// </remarks>
        public abstract string? Email { get; }

        /// <summary>
        /// Gets a player account creation date time.
        /// </summary>
        public abstract DateTime CreatedAt { get; }

        /// <summary>
        /// Gets a player avatar url.
        /// </summary>
        public abstract string AvatarUrl { get; }

        /// <summary>
        /// Gets a player youtube channel url(if specified).
        /// </summary>
        public abstract string? YoutubeUrl { get; }

        /// <summary>
        /// Gets a player discord nickname(if specified).
        /// </summary>
        public abstract string? DiscordNickname { get; }

        /// <summary>
        /// Gets a player discord id(can be 0).
        /// </summary>
        public abstract ulong DiscordId { get; }

        /// <summary>
        /// Gets a player modes beaten count.
        /// </summary>
        public abstract int ModesBeaten { get; }

        /// <summary>
        /// Gets a player continent(if specified).
        /// </summary>
        public abstract Continent Continent { get; }

        /// <summary>
        /// Gets a player country(if specified).
        /// </summary>
        public abstract string? Country { get; }

        /// <summary>
        /// Gets a value that indicates whether the player is an admin.
        /// </summary>
        public abstract bool IsAdmin { get; }

        /// <summary>
        /// Gets a value that indicates whether the player is a manager.
        /// </summary>
        public abstract bool IsManager { get; }

        /// <summary>
        /// Gets a value that indicates whether the player is a banned.
        /// </summary>
        public abstract bool IsBanned { get; }

        /// <summary>
        /// Gets a value that indicates whether the player is a hidden.
        /// </summary>
        public abstract bool IsHidden { get; }

        /// <summary>
        /// Gets a clan tag for player.
        /// </summary>
        public abstract string? ClanTag { get; }

        /// <summary>
        /// Gets a value that indicates whether the player is a placeholder account.
        /// </summary>
        public abstract bool IsPlaceholder { get; }

        /// <summary>
        /// Gets a player stst value by <see cref="StatType"/> argument.
        /// </summary>
        /// <param name="statType">Target <see cref="StatType"/>.</param>
        /// <returns>Total stat value.</returns>
        public abstract int GetStatValue(StatType statType);

        /// <summary>
        /// Gets a player max points value by <see cref="PointType"/> argument.
        /// </summary>
        /// <param name="pointType"><see cref="PointType"/> needed to calculate.</param>
        /// <returns>Total max points value.</returns>
        /// <remarks>
        /// Support <see cref="PointType"/> flags.
        /// 'Max points' means maxinum amount of points given form a single maxmode.
        /// </remarks>
        public abstract int GetMaxPointsBy(PointType pointType);

        /// <summary>
        /// Gets a player rank value by <see cref="StatType"/> argument.
        /// </summary>
        /// <param name="statType">Target <see cref="StatType"/>.</param>
        /// <returns>Player rank value.</returns>
        /// <remarks>
        /// in most of cases when player has 0 records, most of the stats will return <see cref="int.MaxValue"/>.        
        /// </remarks>
        public abstract int GetRankBy(StatType statType);
    }
}
