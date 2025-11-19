using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using AMLApi.Core.Base;
using AMLApi.Core.Cached.Instances;
using AMLApi.Core.Data;
using AMLApi.Core.Enums;

namespace AMLApi.Core.Cached
{
    /// <summary>
    /// Abstract interface for cached 
    /// </summary>
    /// <remarks>
    /// <seealso cref="Base.IClient"/>
    /// <seealso cref="RawAmlClient"/>
    /// <seealso cref="Rest.RestClient"/>
    /// </remarks>
    public abstract class CachedClient : IClient
    {
        /// <summary>
        /// Creates new instance of <see cref="CachedClient"/>.
        /// </summary>
        /// <returns>New instance of <see cref="CachedClient"/>.</returns>
        public static async Task<CachedClient> CreateClient()
        {
            RawAmlClient baseClient = RawAmlClient.CreateClient();
            CachedClient client = new CachedAmlClient(baseClient);
            await client.RefillCache();
            return client;
        }

        /// <summary>
        /// Gets all avaible <see cref="CachedMaxMode"/>'s.
        /// </summary>
        public abstract IReadOnlyCollection<CachedMaxMode> MaxModes { get; }

        /// <summary>
        /// Gets all avaible <see cref="CachedPlayer"/>'s.
        /// </summary>
        public abstract IReadOnlyCollection<CachedPlayer> Players { get; }

        /// <summary>
        /// Refils <see cref="MaxModes"/> and <see cref="Players"/> cache.
        /// </summary>
        /// <returns></returns>
        public abstract Task RefillCache();

        /// <summary>
        /// Gets a player by target <see cref="Guid"/>.
        /// </summary>
        /// <param name="guid">Target <see cref="Guid"/>.</param>
        /// <returns>Target <see cref="CachedPlayer"/>.</returns>
        public abstract CachedPlayer? GetPlayer(Guid guid);

        /// <summary>
        /// Tries to get a player by target <see cref="Guid"/>.
        /// </summary>
        /// <param name="guid">Target <see cref="Guid"/>.</param>
        /// <param name="player">Found <see cref="CachedPlayer"/>.</param>
        /// <returns>A value that indicates whether the player is found.</returns>
        public abstract bool TryGetPlayer(Guid guid, [NotNullWhen(true)] out CachedPlayer? player);

        /// <summary>
        /// Gets players leaderboard by target <see cref="StatType"/>.
        /// </summary>
        /// <param name="statType">Target <see cref="StatType"/>.</param>
        /// <returns><see cref="IEnumerable{T}"/> of <see cref="CachedPlayer"/>'s.</returns>
        public abstract IEnumerable<CachedPlayer> GetPlayerLeaderboard(StatType statType);

        /// <summary>
        /// Gets a maxmode by target id.
        /// </summary>
        /// <param name="id">Target id.</param>
        /// <returns>Target <see cref="CachedMaxMode"/></returns>
        public abstract CachedMaxMode? GetMaxMode(int id);

        /// <summary>
        /// Tries to get a maxmode by target id.
        /// </summary>
        /// <param name="id">Target <see cref="Guid"/>.</param>
        /// <param name="maxMode">Found <see cref="CachedMaxMode"/>.</param>
        /// <returns>A value that indicates whether the maxmode is found.</returns>
        public abstract bool TryGetMaxMode(int id, [NotNullWhen(true)] out CachedMaxMode? maxMode);

        /// <summary>
        /// Gets maxmode list by target skill ratio.
        /// </summary>
        /// <param name="skillPersent">Target skill ratio.</param>
        /// <returns><see cref="IEnumerable{T}"/> of <see cref="CachedMaxMode"/>'s.</returns>
        public abstract IEnumerable<CachedMaxMode> GetMaxModeListByRatio(int skillPersent);

        /// <summary>
        /// Gets target <see cref="CachedPlayer"/> records.
        /// </summary>
        /// <param name="player">Target <see cref="CachedPlayer"/>.</param>
        /// <returns><see cref="IReadOnlyCollection{T}"/> of <see cref="CachedRecord"/>'s.</returns>
        public abstract Task<IReadOnlyCollection<CachedRecord>> GetOrFetchPlayerRecords(CachedPlayer player);

        /// <summary>
        /// Gets target <see cref="CachedPlayer"/> records.
        /// </summary>
        /// <param name="player">Target <see cref="CachedPlayer"/> guid.</param>
        /// <returns><see cref="IReadOnlyCollection{T}"/> of <see cref="CachedRecord"/>'s.</returns>
        public abstract Task<IReadOnlyCollection<CachedRecord>> GetOrFetchPlayerRecords(Guid guid);

        /// <summary>
        /// Gets target <see cref="CachedMaxMode"/> records.
        /// </summary>
        /// <param name="maxMode">Target <see cref="CachedMaxMode"/>.</param>
        /// <returns><see cref="IReadOnlyCollection{T}"/> of <see cref="CachedRecord"/>'s.</returns>
        public abstract Task<IReadOnlyCollection<CachedRecord>> GetOrFetchMaxModeRecords(CachedMaxMode maxMode);

        /// <summary>
        /// Gets target <see cref="CachedMaxMode"/> records.
        /// </summary>
        /// <param name="maxMode">Target <see cref="CachedMaxMode"/> id.</param>
        /// <returns><see cref="IReadOnlyCollection{T}"/> of <see cref="CachedRecord"/>'s.</returns>
        public abstract Task<IReadOnlyCollection<CachedRecord>> GetOrFetchMaxModeRecords(int id);

        /// <summary>
        /// Searches by specific query. 
        /// </summary>
        /// <param name="query">Target query.</param>
        /// <returns><see cref="Tuple{T1, T2}"/> of <see cref="IReadOnlyCollection{T}"/>'s of <see cref="CachedMaxMode"/> and <see cref="CachedPlayer"/> respectively.</returns>
        public abstract Task<(IReadOnlyCollection<CachedMaxMode>, IReadOnlyCollection<CachedPlayer>)> Search(string query);

        /// <inheritdoc/>
        Task<Player> IClient.FetchPlayer(Guid guid)
        {
            return Task.FromResult<Player>(GetPlayer(guid)!);
        }

        /// <inheritdoc/>
        Task<IReadOnlyCollection<Player>> IClient.FetchPlayers()
        {
            return Task.FromResult<IReadOnlyCollection<Player>>(Players);
        }

        /// <inheritdoc/>
        Task<IEnumerable<Player>> IClient.FetchPlayerLeaderboard(StatType statType)
        {
            return Task.FromResult<IEnumerable<Player>>(GetPlayerLeaderboard(statType));
        }

        /// <inheritdoc/>
        Task<MaxMode> IClient.FetchMaxMode(int id)
        {
            return Task.FromResult<MaxMode>(GetMaxMode(id)!);
        }

        /// <inheritdoc/>
        Task<IReadOnlyCollection<MaxMode>> IClient.FetchMaxModes()
        {
            return Task.FromResult<IReadOnlyCollection<MaxMode>>(MaxModes);
        }

        /// <inheritdoc/>
        Task<IEnumerable<MaxMode>> IClient.FetchMaxModeListByRatio(int skillPersent)
        {
            return Task.FromResult<IEnumerable<MaxMode>>(GetMaxModeListByRatio(skillPersent));
        }

        /// <inheritdoc/>
        async Task<IReadOnlyCollection<Record>> IClient.FetchPlayerRecords(Player player)
        {
            return await GetOrFetchPlayerRecords(player.Guid);
        }

        /// <inheritdoc/>
        async Task<IReadOnlyCollection<Record>> IClient.FetchPlayerRecords(Guid guid)
        {
            return await GetOrFetchPlayerRecords(guid);
        }

        /// <inheritdoc/>
        async Task<IReadOnlyCollection<Record>> IClient.FetchMaxModeRecords(MaxMode maxMode)
        {
            return await GetOrFetchMaxModeRecords(maxMode.Id);
        }

        /// <inheritdoc/>
        async Task<IReadOnlyCollection<Record>> IClient.FetchMaxModeRecords(int id)
        {
            return await GetOrFetchMaxModeRecords(id);
        }

        /// <inheritdoc/>
        async Task<(IReadOnlyCollection<MaxMode>, IReadOnlyCollection<ShortPlayerData>)> IClient.Search(string query)
        {
            var result = await Search(query);
            var maxModes = result.Item1;
            var players = result.Item2.Select(p => new ShortPlayerData { Guid = p.Guid, Name = p.Nickname }).ToList();
            return (maxModes, players);
        }
    }
}
