using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Core.Enums;
using AMLApi.Core.Objects;
using AMLApi.Core.Objects.Cached;
using AMLApi.Core.Objects.Rest;

namespace AMLApi.Core
{
    public abstract class CachedClient
    {
        public static async Task<CachedClient> CreateClient()
        {
            RestClient restClient = RestClient.CreateClient();
            CachedClient client = new CachedAmlClient(restClient);
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
        public abstract Task<IReadOnlyCollection<CachedRecord>> GetOrFetchMaxModeRecords(CachedMaxMode maxMode);

        public abstract Task<(IReadOnlyCollection<CachedMaxMode>, IReadOnlyCollection<CachedPlayer>)> Search(string query);
    }
}
