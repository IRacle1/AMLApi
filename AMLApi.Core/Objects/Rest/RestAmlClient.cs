using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using AMLApi.Core.Data;
using AMLApi.Core.Enums;
using AMLApi.Core.Json;
using AMLApi.Core.Objects;
using AMLApi.Core.Objects.Rest.Instances;

namespace AMLApi.Core.Objects.Rest
{
    internal class RestAmlClient : RestClient
    {
        private HttpClient httpClient;
        private static JsonSerializerOptions options = new();

        static RestAmlClient()
        {
            options.Converters.Add(new SearchResultConverter());
        }

        internal RestAmlClient()
        {
            httpClient = new();
            httpClient.BaseAddress = new Uri(BaseUrl);
        }

        public const string BaseUrl = "https://aml-api-eta.vercel.app";

        public override async Task<Player?> FetchPlayer(Guid guid)
        {
            var result = await GetResponse<PlayerData>($"/player/{guid}");
            if (result is null)
                return null;
            return CreatePlayer(result);
        }

        public override async Task<MaxMode?> FetchMaxMode(int id)
        {
            var result = await GetResponse<FullMaxModeData>($"/level/{id}");
            if (result is null)
                return null;
            return CreateMaxMode(result.Data);
        }

        public override async Task<IReadOnlyCollection<Player>> FetchPlayers()
        {
            return await FetchPlayerLeaderboard(StatType.Skill);
        }

        public override async Task<IReadOnlyCollection<MaxMode>> FetchMaxModes()
        {
            var result = await GetResponse<MaxModeData[]>("/levels/ml/page/1");
            return result!.Select(CreateMaxMode).ToArray();
        }

        public override async Task<IReadOnlyCollection<Player>> FetchPlayerLeaderboard(StatType statType)
        {
            var result = await GetResponse<PlayerData[]>($"/players/{statType.ToRoute()}/page/1");
            return result!.Select(CreatePlayer).ToArray();
        }

        public override async Task<IEnumerable<MaxMode>> FetchMaxModeListByRatio(int skillPersent)
        {
            return (await FetchMaxModes()).OrderByDescending(m => m.GetPointsByRatio(skillPersent));
        }

        public override async Task<IReadOnlyCollection<Record>> FetchPlayerRecords(Guid guid)
        {
            var result = await GetResponse<RecordData[]>($"player/{guid}/records/skillValue");
            return result!.Select(CreateRecord).ToArray();
        }

        public override async Task<IReadOnlyCollection<Record>> FetchPlayerRecords(Player player)
        {
            return await FetchPlayerRecords(player.Guid);
        }

        public override async Task<IReadOnlyCollection<Record>> FetchMaxModeRecords(int id)
        {
            var result = await GetResponse<FullMaxModeData>($"level/{id}");
            return result!.Records.Select(CreateRecord).ToArray();
        }

        public override async Task<IReadOnlyCollection<Record>> FetchMaxModeRecords(MaxMode maxMode)
        {
            return await FetchMaxModeRecords(maxMode.Id);
        }

        public override async Task<(IReadOnlyCollection<MaxMode>, IReadOnlyCollection<ShortPlayerData>)> Search(string query)
        {
            var result = await GetResponse<SearchResult>($"/search/{Uri.EscapeDataString(query)}");

            IReadOnlyCollection<MaxMode> maxModes = result!.MaxModes.Select(CreateMaxMode).ToArray();
            return (maxModes, result!.Players);
        }

        private Player CreatePlayer(PlayerData data)
        {
            return new AmlRestPlayer(this, data);
        }

        private MaxMode CreateMaxMode(MaxModeData data)
        {
            return new AmlRestMaxMode(this, data);
        }

        private Record CreateRecord(RecordData data)
        {
            return new AmlRestRecord(this, data);
        }

        private async Task<T?> GetResponse<T>(string url)
        {
            return await httpClient.GetFromJsonAsync<T>(url, options);
        }
    }
}
