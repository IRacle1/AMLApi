using AMLApi.Core.Base;
using AMLApi.Core.Base.Instances;
using AMLApi.Core.Data;
using AMLApi.Core.Data.Clans;
using AMLApi.Core.Data.MaxModes;
using AMLApi.Core.Data.Players;
using AMLApi.Core.Enums;

namespace AMLApi.Core.Rest.Instances
{
    internal class AmlRestClient : RestClient
    {
        private readonly RawAmlClient baseClient;

        internal AmlRestClient(RawAmlClient baseClient)
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

        public override async Task<IReadOnlyCollection<RestMaxMode>> FetchMaxModes()
        {
            MaxModeData[] result = await baseClient.FetchMaxModes();
            return Array.ConvertAll(result, CreateMaxMode);
        }

        public override async Task<IReadOnlyList<RestPlayer>> FetchPlayerLeaderboard(StatType statType, int page)
        {
            PlayerData[] result = await baseClient.FetchPlayerLeaderboard(statType, page);
            return Array.ConvertAll(result, CreatePlayer);
        }

        public override async Task<IReadOnlyList<RestMaxMode>> FetchMaxModeListByRatio(int skillPersent)
        {
            return (await FetchMaxModes()).OrderDescending(MaxModeRatioComparer<RestMaxMode>.CreateNew(skillPersent)).ToArray();
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

        public override async Task<RestClan> FetchClan(Guid clanGuid)
        {
            var raw = await baseClient.FetchClan(clanGuid);

            return CreateClan(raw);
        }

        public override async Task<IReadOnlyCollection<RestClan>> FetchClans()
        {
            var raw = await baseClient.FetchClans();
            return Array.ConvertAll(raw, CreateClan);
        }

        public override Task<IReadOnlyCollection<RestShortPlayer>> FetchClanMembers(Clan clan)
        {
            return FetchClanMembers(clan.Guid);
        }

        public override async Task<IReadOnlyCollection<RestShortPlayer>> FetchClanMembers(Guid clanGuid)
        {
            var raw = await baseClient.FetchClanMembers(clanGuid);
            return Array.ConvertAll(raw, CreateShortPlayerClan);
        }

        public override async Task<(IReadOnlyCollection<RestMaxMode>, IReadOnlyCollection<RestShortPlayer>)> Search(string query)
        {
            SearchResult result = await baseClient.Search(query);

            IReadOnlyCollection<RestMaxMode> maxModes = Array.ConvertAll(result.MaxModes, CreateMaxMode);
            IReadOnlyCollection<RestShortPlayer> players = Array.ConvertAll(result.Players, CreateShortPlayer);
            return (maxModes, players);
        }

        private RestShortPlayer CreateShortPlayer(ShortPlayerData data)
        {
            return new AmlRestShortPlayer(this, data);
        }

        private RestShortPlayer CreateShortPlayerClan(ClanPlayerData data)
        {
            return CreateShortPlayer(new ShortPlayerData
                {
                    Guid = data.UserId,
                    Name = data.PlayerData.Name,
                });
        }

        private RestShortMaxMode CreateShortMaxMode(ShortMaxModeData data, int id, int skillPoints, int rngPoints)
        {
            return new AmlRestShortMaxMode(this, data, id, skillPoints, rngPoints);
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
            RestPlayer? player = data.Player is null ? null : CreatePlayer(data.Player);
            RestShortMaxMode? maxMode = data.MaxMode is null ? null : CreateShortMaxMode(data.MaxMode, data.MaxModeId, data.SkillValue, data.RngValue);
            return new AmlRestRecord(this, data, player, maxMode);
        }

        private RestClan CreateClan(ClanData data)
        {
            return new AmlRestClan(this, data);
        }
    }
}
