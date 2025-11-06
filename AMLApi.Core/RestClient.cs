
using AMLApi.Core.Data;
using AMLApi.Core.Enums;
using AMLApi.Core.Objects;
using AMLApi.Core.Objects.Rest;

namespace AMLApi.Core
{
    public abstract class RestClient
    {
        public static RestClient CreateClient()
        {
            return new RestAmlClient();
        }

        public abstract Task<Player?> FetchPlayer(Guid guid);
        public abstract Task<IReadOnlyCollection<Player>> FetchPlayers();
        public abstract Task<IReadOnlyCollection<Player>> FetchPlayerLeaderboard(StatType statType);

        public abstract Task<MaxMode?> FetchMaxMode(int id);
        public abstract Task<IReadOnlyCollection<MaxMode>> FetchMaxModes();
        public abstract Task<IEnumerable<MaxMode>> FetchMaxModeListByRatio(int skillPersent);

        public abstract Task<IReadOnlyCollection<Record>> FetchPlayerRecords(Player player);
        public abstract Task<IReadOnlyCollection<Record>> FetchPlayerRecords(Guid guid);

        public abstract Task<IReadOnlyCollection<Record>> FetchMaxModeRecords(MaxMode maxMode);
        public abstract Task<IReadOnlyCollection<Record>> FetchMaxModeRecords(int id);

        public abstract Task<(IReadOnlyCollection<MaxMode>, IReadOnlyCollection<ShortPlayerData>)> Search(string query);
    }
}
