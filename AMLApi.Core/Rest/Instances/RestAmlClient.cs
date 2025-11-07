using AMLApi.Core.Data;
using AMLApi.Core.Enums;

namespace AMLApi.Core.Rest.Instances
{
    internal class RestAmlClient : RestClient
    {
        private readonly BaseAmlClient baseClient;

        internal RestAmlClient(BaseAmlClient baseClient)
        {
            this.baseClient = baseClient;
        }

        public override async Task<RestPlayer> FetchPlayer(Guid guid)
        {
            return CreatePlayer(await baseClient.FetchPlayer(guid));
        }

        public override async Task<RestMaxMode> FetchMaxMode(int id)
        {
            FullMaxModeData fullData = await baseClient.FetchMaxMode(id);
            return CreateFullMaxMode(fullData);
        }

        public override async Task<IReadOnlyCollection<RestPlayer>> FetchPlayers()
        {
            return await FetchPlayerLeaderboard(StatType.Skill);
        }

        public override async Task<IReadOnlyCollection<RestMaxMode>> FetchMaxModes()
        {
            MaxModeData[] result = await baseClient.FetchMaxModes();
            return Array.ConvertAll(result, CreateMaxMode);
        }

        public override async Task<IReadOnlyList<RestPlayer>> FetchPlayerLeaderboard(StatType statType)
        {
            PlayerData[] result = await baseClient.FetchPlayerLeaderboard(statType);
            return Array.ConvertAll(result, CreatePlayer);
        }

        public override async Task<IEnumerable<RestMaxMode>> FetchMaxModeListByRatio(int skillPersent)
        {
            return (await FetchMaxModes()).OrderByDescending(m => m.GetPointsByRatio(skillPersent));
        }

        public override async Task<IReadOnlyCollection<RestRecord>> FetchPlayerRecords(Guid guid)
        {
            RecordData[] result = await baseClient.FetchPlayerRecords(guid);
            return Array.ConvertAll(result, CreateRecord);
        }

        public override async Task<IReadOnlyCollection<RestRecord>> FetchPlayerRecords(Player player)
        {
            return await FetchPlayerRecords(player.Guid);
        }

        public override async Task<IReadOnlyCollection<RestRecord>> FetchMaxModeRecords(int id)
        {
            FullMaxModeData result = await baseClient.FetchMaxMode(id);
            return Array.ConvertAll(result.Records, CreateRecord);
        }

        public override async Task<IReadOnlyCollection<RestRecord>> FetchMaxModeRecords(MaxMode maxMode)
        {
            return await FetchMaxModeRecords(maxMode.Id);
        }

        public override async Task<(IReadOnlyCollection<RestMaxMode>, IReadOnlyCollection<ShortPlayerData>)> Search(string query)
        {
            SearchResult result = await baseClient.Search(query);

            IReadOnlyCollection<RestMaxMode> maxModes = Array.ConvertAll(result.MaxModes, CreateMaxMode);
            return (maxModes, result!.Players);
        }

        private RestPlayer CreatePlayer(PlayerData data)
        {
            return new AmlRestPlayer(this, data);
        }

        private RestMaxMode CreateMaxMode(MaxModeData data)
        {
            return new AmlRestMaxMode(this, data);
        }

        private RestMaxMode CreateFullMaxMode(FullMaxModeData data)
        {
            RestRecord[] records = Array.ConvertAll(data.Records, CreateRecord);
            return new AmlRestMaxMode(this, data.Data, records);
        }

        private RestRecord CreateRecord(RecordData data)
        {
            return new AmlRestRecord(this, data);
        }
    }
}
