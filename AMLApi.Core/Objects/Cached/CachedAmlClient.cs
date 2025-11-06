using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using AMLApi.Core.Json;
using AMLApi.Core.Objects;
using AMLApi.Core.Enums;
using System.Diagnostics.CodeAnalysis;
using AMLApi.Core.Objects.Cached.Instances;
using AMLApi.Core.Data;
using AMLApi.Core.Objects.Cached.Interfaces;
using AMLApi.Core.Objects.Rest;

namespace AMLApi.Core.Objects.Cached
{
    internal class CachedAmlClient : CachedClient
    {
        private RestClient restClient;

        private Dictionary<int, CachedMaxMode> cachedMaxModes = new();
        private Dictionary<Guid, CachedPlayer> cachedPlayers = new();

        internal CachedAmlClient(RestClient restClient)
        {
            this.restClient = restClient;
        }

        public override IReadOnlyCollection<CachedMaxMode> MaxModes => cachedMaxModes.Values;

        public override IReadOnlyCollection<CachedPlayer> Players => cachedPlayers.Values;

        public override async Task RefillCache()
        {
            PurgeCache();
            await UpdatePlayersCache();
            await UpdateMaxModesCache();
        }

        public override CachedPlayer? GetPlayer(Guid guid)
        {
            cachedPlayers.TryGetValue(guid, out CachedPlayer? ply);
            return ply;
        }

        public override bool TryGetPlayer(Guid guid, [NotNullWhen(true)] out CachedPlayer? player)
        {
            return cachedPlayers.TryGetValue(guid, out player);
        }

        public override IEnumerable<CachedPlayer> GetPlayerLeaderboard(StatType statType)
        {
            return cachedPlayers.Values.OrderBy(ply => ply.GetRankBy(statType));
        }

        public override CachedMaxMode? GetMaxMode(int id)
        {
            cachedMaxModes.TryGetValue(id, out CachedMaxMode? maxMode);
            return maxMode;
        }

        public override bool TryGetMaxMode(int id, [NotNullWhen(true)] out CachedMaxMode? maxMode)
        {
            return cachedMaxModes.TryGetValue(id, out maxMode);
        }

        public override IEnumerable<CachedMaxMode> GetMaxModeListByRatio(int skillPersent)
        {
            return cachedMaxModes.Values.OrderByDescending(m => m.GetPointsByRatio(skillPersent));
        }

        public override async Task<(IReadOnlyCollection<CachedMaxMode>, IReadOnlyCollection<CachedPlayer>)> Search(string query)
        {
            if (cachedPlayers.Count == 0)
                await UpdatePlayersCache();

            (IReadOnlyCollection<MaxMode> restMaxModes, IReadOnlyCollection<ShortPlayerData> restPlayers) = await restClient.Search(query);

            List<CachedMaxMode> maxModes = new(restMaxModes.Count);
            foreach (var item in restMaxModes)
            {
                if (cachedMaxModes.TryGetValue(item.Id, out CachedMaxMode? maxMode))
                    maxModes.Add(maxMode);
            }

            List<CachedPlayer> players = new(restPlayers.Count);
            foreach (var item in restPlayers)
            {
                if (cachedPlayers.TryGetValue(item.Guid, out CachedPlayer? player))
                    players.Add(player);
            }

            return (maxModes, players);
        }

        public override async Task<IReadOnlyCollection<CachedRecord>> GetOrFetchPlayerRecords(CachedPlayer player)
        {
            if (player.RecordsFetched)
                return player.RecordsCache;

            var restRecords = await restClient.FetchPlayerRecords(player);
            List<CachedRecord> cachedRecords = new(restRecords.Count);

            if (player is IRecordsCacheHolder playerCacheHolder)
            {
                foreach (var restRecord in restRecords)
                {
                    var record = CreateRecord(restRecord);
                    cachedRecords.Add(record);

                    playerCacheHolder.AddRecord(record);

                    if (record.MaxMode is IRecordsCacheHolder maxModeCacheHolder)
                        maxModeCacheHolder.AddRecord(record);
                }

                playerCacheHolder.SetFetched();
            }

            return cachedRecords;
        }

        public override async Task<IReadOnlyCollection<CachedRecord>> GetOrFetchMaxModeRecords(CachedMaxMode maxMode)
        {
            if (maxMode.RecordsFetched)
                return maxMode.RecordsCache;

            var restRecords = await restClient.FetchMaxModeRecords(maxMode);

            List<CachedRecord> cachedRecords = new(restRecords.Count);

            if (maxMode is IRecordsCacheHolder maxModeCacheHolder)
            {
                foreach (var restRecord in restRecords)
                {
                    var record = CreateRecord(restRecord);
                    cachedRecords.Add(record);

                    maxModeCacheHolder.AddRecord(record);

                    if (record.Player is IRecordsCacheHolder playerCacheHolder)
                        playerCacheHolder.AddRecord(record);
                }

                maxModeCacheHolder.SetFetched();
            }

            return cachedRecords;
        }

        private CachedPlayer CreatePlayer(Player player)
        {
            return new AmlCachedPlayer(this, player);
        }

        private CachedMaxMode CreateMaxMode(MaxMode maxMode)
        {
            return new AmlCachedMaxMode(this, maxMode);
        }

        private CachedRecord CreateRecord(Record record)
        {
            return new AmlCachedRecord(this, record);
        }

        private async Task UpdateMaxModesCache()
        {
            foreach (var item in await restClient.FetchMaxModes())
            {
                cachedMaxModes.Add(item.Id, CreateMaxMode(item));
            }
        }

        private async Task UpdatePlayersCache()
        {
            foreach (var item in await restClient.FetchPlayers())
            {
                cachedPlayers.Add(item.Guid, CreatePlayer(item));
            }
        }

        private void PurgeCache()
        {
            cachedPlayers.Clear();
            cachedMaxModes.Clear();
        }
    }
}
