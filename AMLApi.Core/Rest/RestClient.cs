using AMLApi.Core.Base;
using AMLApi.Core.Data;
using AMLApi.Core.Enums;
using AMLApi.Core.Rest.Instances;

namespace AMLApi.Core.Rest
{
    public abstract class RestClient
    {
        public static RestClient CreateClient()
        {
            BaseAmlClient client = BaseAmlClient.CreateClient();
            return new RestAmlClient(client);
        }

        public abstract Task<RestPlayer> FetchPlayer(Guid guid);
        public abstract Task<IReadOnlyCollection<RestPlayer>> FetchPlayers();
        public abstract Task<IReadOnlyList<RestPlayer>> FetchPlayerLeaderboard(StatType statType);

        public abstract Task<RestMaxMode> FetchMaxMode(int id);
        public abstract Task<IReadOnlyCollection<RestMaxMode>> FetchMaxModes();
        public abstract Task<IEnumerable<RestMaxMode>> FetchMaxModeListByRatio(int skillPersent);

        public abstract Task<IReadOnlyCollection<RestRecord>> FetchPlayerRecords(Player player);
        public abstract Task<IReadOnlyCollection<RestRecord>> FetchPlayerRecords(Guid guid);

        public abstract Task<IReadOnlyCollection<RestRecord>> FetchMaxModeRecords(MaxMode maxMode);
        public abstract Task<IReadOnlyCollection<RestRecord>> FetchMaxModeRecords(int id);

        public abstract Task<(IReadOnlyCollection<RestMaxMode>, IReadOnlyCollection<ShortPlayerData>)> Search(string query);
    }
}
