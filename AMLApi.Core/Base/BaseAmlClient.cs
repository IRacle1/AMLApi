using System.Net.Http.Json;
using System.Text.Json;

using AMLApi.Core.Data;
using AMLApi.Core.Enums;
using AMLApi.Core.Json;

namespace AMLApi.Core.Base
{
    public class BaseAmlClient
    {
        private HttpClient httpClient;
        private static JsonSerializerOptions options = new();

        static BaseAmlClient()
        {
            options.Converters.Add(new SearchResultConverter());
        }

        private BaseAmlClient()
        {
            httpClient = new();
            httpClient.BaseAddress = new Uri(BaseUrl);
        }

        public const string BaseUrl = "https://aml-api-eta.vercel.app";

        public static BaseAmlClient CreateClient()
        {
            return new BaseAmlClient();
        }

        public async Task<PlayerData> FetchPlayer(Guid guid)
        {
            return await GetResponse<PlayerData>($"/player/{guid}");
        }

        public async Task<FullMaxModeData> FetchMaxMode(int id)
        {
            return await GetResponse<FullMaxModeData>($"/level/{id}");
        }

        public async Task<MaxModeData[]> FetchMaxModes()
        {
            return await GetResponse<MaxModeData[]>("/levels/ml/page/1");
        }

        public async Task<PlayerData[]> FetchPlayerLeaderboard(StatType statType)
        {
            return await GetResponse<PlayerData[]>($"/players/{statType.ToRoute()}/page/1");
        }

        public async Task<RecordData[]> FetchPlayerRecords(Guid guid)
        {
            return await GetResponse<RecordData[]>($"player/{guid}/records/skillValue");
        }

        public async Task<SearchResult> Search(string query)
        {
            return await GetResponse<SearchResult>($"/search/{Uri.EscapeDataString(query)}");
        }

        private async Task<T> GetResponse<T>(string url)
        {
            T? result = await httpClient.GetFromJsonAsync<T>(url, options);

            return result ?? throw new InvalidOperationException($"Null response on '{url}' route");
        }
    }
}
