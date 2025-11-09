using System.Net.Http.Json;
using System.Text.Json;

using AMLApi.Core.Data;
using AMLApi.Core.Enums;
using AMLApi.Core.Json;

namespace AMLApi.Core.Base
{
    /// <summary>
    /// Base AML client that does 0% logic, and just fetches data
    /// </summary>
    /// <remarks></remarks>
    /// <seealso cref="Cached.CachedClient"/>
    /// <seealso cref="Rest.RestClient"/>
    /// </remarks>
    public class RawAmlClient
    {
        private HttpClient httpClient;
        private static JsonSerializerOptions options = new();

        static RawAmlClient()
        {
            options.Converters.Add(new SearchResultConverter());
        }

        private RawAmlClient()
        {
            httpClient = new();
            httpClient.BaseAddress = new Uri(BaseUrl);
        }

        /// <summary>
        /// Main AML public api url.
        /// </summary>
        public const string BaseUrl = "https://aml-api-eta.vercel.app";

        /// <summary>
        /// Creates new object of <see cref="RawAmlClient"/>.
        /// </summary>
        /// <returns><see cref="RawAmlClient"/> object.</returns>
        public static RawAmlClient CreateClient()
        {
            return new RawAmlClient();
        }

        /// <summary>
        /// Fetches player data by specific <see cref="Guid"/>.
        /// </summary>
        /// <param name="guid">Target player <see cref="Guid"/>.</param>
        /// <returns><see cref="PlayerData"/> object.</returns>
        public async Task<PlayerData> FetchPlayer(Guid guid)
        {
            return await GetResponse<PlayerData>($"/player/{guid}");
        }

        /// <summary>
        /// Fetches maxmode data by specific id.
        /// </summary>
        /// <param name="id">Target maxmode id.</param>
        /// <returns><see cref="FullMaxModeData"/> object.</returns>
        /// <remarks>
        /// Maxmode records also included in <see cref="FullMaxModeData"/> object. 
        /// </remarks>
        public async Task<FullMaxModeData> FetchMaxMode(int id)
        {
            return await GetResponse<FullMaxModeData>($"/level/{id}");
        }

        /// <summary>
        /// Fetches full maxmode list.
        /// </summary>
        /// <returns><see cref="Array"/> of <see cref="MaxModeData"/> objects.</returns>
        public async Task<MaxModeData[]> FetchMaxModes()
        {
            return await GetResponse<MaxModeData[]>("/levels/ml/page/1");
        }

        /// <summary>
        /// Fetches a player leaderboard by specific <see cref="StatType"/>.
        /// </summary>
        /// <returns><see cref="Array"/> of <see cref="PlayerData"/> objects.</returns>
        public async Task<PlayerData[]> FetchPlayerLeaderboard(StatType statType)
        {
            return await GetResponse<PlayerData[]>($"/players/{statType.ToRoute()}/page/1");
        }

        /// <summary>
        /// Fetches a player records by specific <see cref="Guid"/>.
        /// </summary>
        /// <param name="guid">Target player <see cref="Guid"/>.</param>
        /// <returns><see cref="Array"/> of <see cref="RecordData"/> objects.</returns>
        public async Task<RecordData[]> FetchPlayerRecords(Guid guid)
        {
            return await GetResponse<RecordData[]>($"player/{guid}/records/skillValue");
        }

        /// <summary>
        /// Fetches a search request with specific query.
        /// </summary>
        /// <param name="query">Target query.</param>
        /// <returns><see cref="SearchResult"/> object.</returns>
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
