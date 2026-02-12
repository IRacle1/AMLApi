using System;
using System.Numerics;

using AMLApi.Core.Base;
using AMLApi.Core.Data.Clans;
using AMLApi.Core.Data.Players;
using AMLApi.Core.Enums;
using AMLApi.Core.Rest.Instances;

namespace AMLApi.Core.Rest
{
    public abstract class RestClient : IClient
    {
        public static RestClient CreateClient()
        {
            RawAmlClient client = RawAmlClient.CreateClient();
            return new AmlRestClient(client);
        }

        public abstract Task<RestPlayer> FetchPlayer(Guid guid);
        public abstract Task<IReadOnlyList<RestPlayer>> FetchPlayerLeaderboard(StatType statType, int page = 1);

        public abstract Task<RestMaxMode> FetchMaxMode(int id);
        public abstract Task<IReadOnlyCollection<RestMaxMode>> FetchMaxModes();
        public abstract Task<IReadOnlyList<RestMaxMode>> FetchMaxModeListByRatio(int skillPersent);

        public abstract Task<IReadOnlyCollection<RestRecord>> FetchPlayerRecords(Player player);
        public abstract Task<IReadOnlyCollection<RestRecord>> FetchPlayerRecords(Guid guid);

        public abstract Task<IReadOnlyCollection<RestRecord>> FetchMaxModeRecords(MaxMode maxMode);
        public abstract Task<IReadOnlyCollection<RestRecord>> FetchMaxModeRecords(int id);

        public abstract Task<IReadOnlyCollection<RestClan>> FetchClans();
        public abstract Task<RestClan> FetchClan(Guid clanGuid);
        public abstract Task<IReadOnlyCollection<RestShortPlayer>> FetchClanMembers(Clan clan);
        public abstract Task<IReadOnlyCollection<RestShortPlayer>> FetchClanMembers(Guid clanGuid);

        public abstract Task<(IReadOnlyCollection<RestMaxMode>, IReadOnlyCollection<RestShortPlayer>)> Search(string query);

        async Task<Player> IClient.FetchPlayer(Guid guid)
        {
            return await FetchPlayer(guid);
        }

        async Task<IReadOnlyList<Player>> IClient.FetchPlayerLeaderboard(StatType statType, int page)
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

        async Task<IReadOnlyList<MaxMode>> IClient.FetchMaxModeListByRatio(int skillPersent)
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

        async Task<IReadOnlyCollection<Clan>> IClient.FetchClans()
        {
            return await FetchClans();
        }

        async Task<Clan> IClient.FetchClan(Guid clanGuid)
        {
            return await FetchClan(clanGuid);
        }

        async Task<IReadOnlyCollection<ShortPlayer>> IClient.FetchClanMembers(Clan clan)
        {
            return await FetchClanMembers(clan);
        }

        async Task<IReadOnlyCollection<ShortPlayer>> IClient.FetchClanMembers(Guid clanGuid)
        {
            return await FetchClanMembers(clanGuid);
        }

        async Task<(IReadOnlyCollection<MaxMode>, IReadOnlyCollection<ShortPlayer>)> IClient.Search(string query)
        {
            return await Search(query);
        }
    }
}
