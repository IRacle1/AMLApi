using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using AMLApi.Core.Base;
using AMLApi.Core.Cached.Instances;
using AMLApi.Core.Data;
using AMLApi.Core.Enums;

namespace AMLApi.Core.Cached
{
    public abstract class CachedClient : IClient
    {
        public static async Task<CachedClient> CreateClient()
        {
            RawAmlClient baseClient = RawAmlClient.CreateClient();
            CachedClient client = new CachedAmlClient(baseClient);
            await client.RefillCache();
            return client;
        }

        public abstract IReadOnlyCollection<CachedMaxMode> MaxModes { get; }
        public abstract IReadOnlyCollection<CachedPlayer> Players { get; }

        public abstract Task RefillCache();

        public abstract CachedPlayer? GetPlayer(Guid guid);
        public abstract bool TryGetPlayer(Guid guid, [NotNullWhen(true)] out CachedPlayer? player);
        public abstract IEnumerable<CachedPlayer> GetPlayerLeaderboard(StatType statType);

        public abstract CachedMaxMode? GetMaxMode(int id);
        public abstract bool TryGetMaxMode(int id, [NotNullWhen(true)] out CachedMaxMode? maxMode);
        public abstract IEnumerable<CachedMaxMode> GetMaxModeListByRatio(int skillPersent);

        public abstract Task<IReadOnlyCollection<CachedRecord>> GetOrFetchPlayerRecords(CachedPlayer player);
        public abstract Task<IReadOnlyCollection<CachedRecord>> GetOrFetchPlayerRecords(Guid guid);

        public abstract Task<IReadOnlyCollection<CachedRecord>> GetOrFetchMaxModeRecords(CachedMaxMode maxMode);
        public abstract Task<IReadOnlyCollection<CachedRecord>> GetOrFetchMaxModeRecords(int id);

        public abstract Task<(IReadOnlyCollection<CachedMaxMode>, IReadOnlyCollection<CachedPlayer>)> Search(string query);

        Task<Player> IClient.FetchPlayer(Guid guid)
        {
            return Task.FromResult<Player>(GetPlayer(guid)!);
        }

        Task<IReadOnlyCollection<Player>> IClient.FetchPlayers()
        {
            return Task.FromResult<IReadOnlyCollection<Player>>(Players);
        }

        Task<IEnumerable<Player>> IClient.FetchPlayerLeaderboard(StatType statType)
        {
            return Task.FromResult<IEnumerable<Player>>(GetPlayerLeaderboard(statType));
        }

        Task<MaxMode> IClient.FetchMaxMode(int id)
        {
            return Task.FromResult<MaxMode>(GetMaxMode(id)!);
        }

        Task<IReadOnlyCollection<MaxMode>> IClient.FetchMaxModes()
        {
            return Task.FromResult<IReadOnlyCollection<MaxMode>>(MaxModes);
        }

        Task<IEnumerable<MaxMode>> IClient.FetchMaxModeListByRatio(int skillPersent)
        {
            return Task.FromResult<IEnumerable<MaxMode>>(GetMaxModeListByRatio(skillPersent));
        }

        async Task<IReadOnlyCollection<Record>> IClient.FetchPlayerRecords(Player player)
        {
            return await GetOrFetchPlayerRecords(player.Guid);
        }

        async Task<IReadOnlyCollection<Record>> IClient.FetchPlayerRecords(Guid guid)
        {
            return await GetOrFetchPlayerRecords(guid);
        }

        async Task<IReadOnlyCollection<Record>> IClient.FetchMaxModeRecords(MaxMode maxMode)
        {
            return await GetOrFetchMaxModeRecords(maxMode.Id);
        }

        async Task<IReadOnlyCollection<Record>> IClient.FetchMaxModeRecords(int id)
        {
            return await GetOrFetchMaxModeRecords(id);
        }

        async Task<(IReadOnlyCollection<MaxMode>, IReadOnlyCollection<ShortPlayerData>)> IClient.Search(string query)
        {
            var result = await Search(query);
            var maxModes = result.Item1;
            var players = result.Item2.Select(p => new ShortPlayerData { Guid = p.Guid, Name = p.Name }).ToList();
            return (maxModes, players);
        }
    }
}
