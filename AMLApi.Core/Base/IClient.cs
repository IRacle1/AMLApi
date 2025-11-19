using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Core.Data;
using AMLApi.Core.Enums;

namespace AMLApi.Core.Base
{
    /// <summary>
    /// Abstract interface for clients.
    /// </summary>
    /// <remarks>
    /// <seealso cref="Cached.CachedClient"/>
    /// <seealso cref="RawAmlClient"/>
    /// <seealso cref="Rest.RestClient"/>
    /// </remarks>
    public interface IClient
    {
        /// <summary>
        /// Fetches a player by target <see cref="Guid"/>.
        /// </summary>
        /// <param name="guid">Target <see cref="Guid"/>.</param>
        /// <returns>Target <see cref="Player"/></returns>
        Task<Player> FetchPlayer(Guid guid);

        /// <summary>
        /// Fetches all players.
        /// </summary>
        /// <returns><see cref="IReadOnlyCollection{T}"/> of <see cref="Player"/>'s.</returns>
        Task<IReadOnlyCollection<Player>> FetchPlayers();

        /// <summary>
        /// Fetches players leaderboard by target <see cref="StatType"/>.
        /// </summary>
        /// <param name="statType">Target <see cref="StatType"/>.</param>
        /// <returns><see cref="IEnumerable{T}"/> of <see cref="Player"/>'s.</returns>
        Task<IEnumerable<Player>> FetchPlayerLeaderboard(StatType statType);

        /// <summary>
        /// Fetches a maxmode by target id.
        /// </summary>
        /// <param name="id">Target id.</param>
        /// <returns>Target <see cref="MaxMode"/></returns>
        Task<MaxMode> FetchMaxMode(int id);

        /// <summary>
        /// Fetches all maxmodes.
        /// </summary>
        /// <returns><see cref="IReadOnlyCollection{T}"/> of <see cref="MaxMode"/>'s.</returns>
        Task<IReadOnlyCollection<MaxMode>> FetchMaxModes();

        /// <summary>
        /// Fetches maxmode list by target skill ratio.
        /// </summary>
        /// <param name="skillPersent">Target skill ratio.</param>
        /// <returns><see cref="IEnumerable{T}"/> of <see cref="MaxMode"/>'s.</returns>
        Task<IEnumerable<MaxMode>> FetchMaxModeListByRatio(int skillPersent);

        /// <summary>
        /// Fetches target <see cref="Player"/> records.
        /// </summary>
        /// <param name="player">Target <see cref="Player"/>.</param>
        /// <returns><see cref="IReadOnlyCollection{T}"/> of <see cref="Record"/>'s.</returns>
        Task<IReadOnlyCollection<Record>> FetchPlayerRecords(Player player);

        /// <summary>
        /// Fetches target <see cref="Player"/> records.
        /// </summary>
        /// <param name="guid">Target <see cref="Player"/> guid.</param>
        /// <returns><see cref="IReadOnlyCollection{T}"/> of <see cref="Record"/>'s.</returns>
        Task<IReadOnlyCollection<Record>> FetchPlayerRecords(Guid guid);

        /// <summary>
        /// Fetches target <see cref="MaxMode"/> records.
        /// </summary>
        /// <param name="maxMode">Target <see cref="MaxMode"/>.</param>
        /// <returns><see cref="IReadOnlyCollection{T}"/> of <see cref="Record"/>'s.</returns>
        Task<IReadOnlyCollection<Record>> FetchMaxModeRecords(MaxMode maxMode);

        /// <summary>
        /// Fetches target <see cref="MaxMode"/> records.
        /// </summary>
        /// <param name="id">Target <see cref="MaxMode"/> id.</param>
        /// <returns><see cref="IReadOnlyCollection{T}"/> of <see cref="Record"/>'s.</returns>
        Task<IReadOnlyCollection<Record>> FetchMaxModeRecords(int id);

        /// <summary>
        /// Searches by specific query. 
        /// </summary>
        /// <param name="query">Target query.</param>
        /// <returns><see cref="Tuple{T1, T2}"/> of <see cref="IReadOnlyCollection{T}"/>'s of <see cref="MaxMode"/> and <see cref="ShortPlayerData"/> respectively.</returns>
        Task<(IReadOnlyCollection<MaxMode>, IReadOnlyCollection<ShortPlayerData>)> Search(string query);
    }
}
