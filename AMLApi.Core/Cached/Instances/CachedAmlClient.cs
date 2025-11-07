using System.Diagnostics.CodeAnalysis;

using AMLApi.Core.Cached.Interfaces;
using AMLApi.Core.Data;
using AMLApi.Core.Enums;

namespace AMLApi.Core.Cached.Instances
{
    internal class CachedAmlClient : CachedClient
    {
        private BaseAmlClient baseClient;

        private Dictionary<int, CachedMaxMode> cachedMaxModes = new();
        private Dictionary<Guid, CachedPlayer> cachedPlayers = new();

        internal CachedAmlClient(BaseAmlClient baseClient)
        {
            this.baseClient = baseClient;
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
            SearchResult result = await baseClient.Search(query);

            List<CachedMaxMode> maxModes = new(result.MaxModes.Length);
            foreach (MaxModeData item in result.MaxModes)
            {
                if (cachedMaxModes.TryGetValue(item.Id, out CachedMaxMode? maxMode))
                    maxModes.Add(maxMode);
            }

            List<CachedPlayer> players = new(result.Players.Length);
            foreach (ShortPlayerData item in result.Players)
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

            RecordData[] restRecords = await baseClient.FetchPlayerRecords(player.Guid);

            List<CachedRecord> cachedRecords = new(restRecords.Length);

            if (player is ICacheRecordsHolder playerCacheHolder)
            {
                foreach (RecordData restRecord in restRecords)
                {
                    CachedRecord record = CreateRecord(restRecord);
                    cachedRecords.Add(record);

                    playerCacheHolder.AddRecord(record);

                    if (record.MaxMode is ICacheRecordsHolder maxModeCacheHolder)
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

            RecordData[] restRecords = (await baseClient.FetchMaxMode(maxMode.Id)).Records;

            List<CachedRecord> cachedRecords = new(restRecords.Length);

            if (maxMode is ICacheRecordsHolder maxModeCacheHolder)
            {
                foreach (RecordData restRecord in restRecords)
                {
                    CachedRecord record = CreateRecord(restRecord);
                    cachedRecords.Add(record);

                    maxModeCacheHolder.AddRecord(record);

                    if (record.Player is ICacheRecordsHolder playerCacheHolder)
                        playerCacheHolder.AddRecord(record);
                }

                maxModeCacheHolder.SetFetched();
            }

            return cachedRecords;
        }

        private CachedPlayer CreatePlayer(PlayerData player)
        {
            return new AmlCachedPlayer(this, player);
        }

        private CachedMaxMode CreateMaxMode(MaxModeData maxMode)
        {
            return new AmlCachedMaxMode(this, maxMode);
        }

        private CachedRecord CreateRecord(RecordData record)
        {
            return new AmlCachedRecord(this, record);
        }

        private async Task UpdateMaxModesCache()
        {
            foreach (MaxModeData item in await baseClient.FetchMaxModes())
            {
                cachedMaxModes.Add(item.Id, CreateMaxMode(item));
            }
        }

        private async Task UpdatePlayersCache()
        {
            foreach (PlayerData item in await baseClient.FetchPlayerLeaderboard(StatType.Skill))
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
