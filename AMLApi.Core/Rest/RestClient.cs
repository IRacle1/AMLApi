using System;
using System.Numerics;

using AMLApi.Core.Base;
using AMLApi.Core.Data;
using AMLApi.Core.Enums;
using AMLApi.Core.Rest.Instances;

namespace AMLApi.Core.Rest
{
    public abstract class RestClient : IClient
    {
        public static RestClient CreateClient()
        {
            RawAmlClient client = RawAmlClient.CreateClient();
            return new RestAmlClient(client);
        }

        public abstract Task<RestPlayer> FetchPlayer(Guid guid);
        public abstract Task<IReadOnlyList<RestPlayer>> FetchPlayerLeaderboard(StatType statType, int page = 1);

        public abstract Task<RestMaxMode> FetchMaxMode(int id);
        public abstract Task<IReadOnlyCollection<RestMaxMode>> FetchMaxModes();
        public abstract Task<IEnumerable<RestMaxMode>> FetchMaxModeListByRatio(int skillPersent);

        public abstract Task<IReadOnlyCollection<RestRecord>> FetchPlayerRecords(Player player);
        public abstract Task<IReadOnlyCollection<RestRecord>> FetchPlayerRecords(Guid guid);

        public abstract Task<IReadOnlyCollection<RestRecord>> FetchMaxModeRecords(MaxMode maxMode);
        public abstract Task<IReadOnlyCollection<RestRecord>> FetchMaxModeRecords(int id);

        public abstract Task<(IReadOnlyCollection<RestMaxMode>, IReadOnlyCollection<ShortPlayerData>)> Search(string query);

        async Task<Player> IClient.FetchPlayer(Guid guid)
        {
            return await FetchPlayer(guid);
        }

        async Task<IEnumerable<Player>> IClient.FetchPlayerLeaderboard(StatType statType, int page)
        {
            return await FetchPlayerLeaderboard(statType, page);
        }

        async Task<MaxMode> IClient.FetchMaxMode(int id)
        {
            return await FetchMaxMode(id);
        }

        async Task<IReadOnlyCollection<MaxMode>> IClient.FetchMaxModes()
        {
            return await FetchMaxModes();
        }

        async Task<IEnumerable<MaxMode>> IClient.FetchMaxModeListByRatio(int skillPersent)
        {
            return await FetchMaxModeListByRatio(skillPersent);
        }

        async Task<IReadOnlyCollection<Record>> IClient.FetchPlayerRecords(Player player)
        {
            return await FetchPlayerRecords(player);
        }

        async Task<IReadOnlyCollection<Record>> IClient.FetchPlayerRecords(Guid guid)
        {
            return await FetchPlayerRecords(guid);
        }

        async Task<IReadOnlyCollection<Record>> IClient.FetchMaxModeRecords(MaxMode maxMode)
        {
            return await FetchMaxModeRecords(maxMode);
        }

        async Task<IReadOnlyCollection<Record>> IClient.FetchMaxModeRecords(int id)
        {
            return await FetchMaxModeRecords(id);
        }

        async Task<(IReadOnlyCollection<MaxMode>, IReadOnlyCollection<ShortPlayerData>)> IClient.Search(string query)
        {
            return await Search(query);
        }
    }
}
