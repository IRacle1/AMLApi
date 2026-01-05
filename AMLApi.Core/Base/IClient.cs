using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Core.Data.Clans;
using AMLApi.Core.Data.Players;
using AMLApi.Core.Enums;
using AMLApi.Core.Rest;

namespace AMLApi.Core.Base
{
    /// <summary>
    /// Abstract interface for clients.
    /// </summary>
    /// <remarks>
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
        /// Fetches players leaderboard by target <see cref="StatType"/>.
        /// </summary>
        /// <param name="statType">Target <see cref="StatType"/>.</param>
        /// <param name="page">Page number.</param>
        /// <returns><see cref="IEnumerable{T}"/> of <see cref="Player"/>'s.</returns>
        Task<IEnumerable<Player>> FetchPlayerLeaderboard(StatType statType, int page = 1);

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

        Task<IReadOnlyCollection<Clan>> FetchClans();

        Task<Clan> FetchClan(Guid clanGuid);

        Task<IReadOnlyCollection<ShortPlayer>> FetchClanMembers(Clan clan);

        Task<IReadOnlyCollection<ShortPlayer>> FetchClanMembers(Guid clanGuid);

        /// <summary>
        /// Searches by specific query. 
        /// </summary>
        /// <param name="query">Target query.</param>
        /// <returns><see cref="Tuple{T1, T2}"/> of <see cref="IReadOnlyCollection{T}"/>'s of <see cref="MaxMode"/> and <see cref="ShortPlayer"/> respectively.</returns>
        Task<(IReadOnlyCollection<MaxMode>, IReadOnlyCollection<ShortPlayer>)> Search(string query);
    }
}
