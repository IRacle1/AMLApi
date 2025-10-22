using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using AMLApi.Api.Enums;
using AMLApi.Api.Json;
using AMLApi.Api.Objects;
using AMLApi.Api.Objects.Data;
using AMLApi.Api.Objects.Instances;

namespace AMLApi.Api
{
    public class AmlClient : IFnafClient
    {
        private HttpClient httpClient;
        private static JsonSerializerOptions options = new JsonSerializerOptions();

        public bool IsInit { get; private set; }

        private List<AmlMaxMode> cachedMaxModes = new();
        private List<AmlPlayer> cachedPlayers = new();

        static AmlClient()
        {
            options.Converters.Add(new SearchResultConverter());
        }

        private AmlClient()
        {
            httpClient = new();
            httpClient.BaseAddress = new Uri(BaseUrl);
        }

        public const string BaseUrl = "https://aml-api-eta.vercel.app";

        public IReadOnlyList<MaxMode> CachedMaxModes => cachedMaxModes;

        public IReadOnlyList<Player> CachedPlayers => cachedPlayers;

        public static async Task<IFnafClient> CreateClient()
        {
            AmlClient client = new();
            await client.RefillCache();
            return client;
        }

        public async Task RefillCache()
        {
            PurgeCache();
            await GetOrFetchPlayerLeaderboard(StatType.Skill);
            await GetOrFetchMaxModes();
        }

        public void PurgeCache()
        {
            cachedPlayers.Clear();
            cachedMaxModes.Clear();
        }

        public async Task<Player?> GetOrFetchPlayer(Guid guid)
        {
            if (cachedPlayers.Find(ply => ply.Guid == guid) is AmlPlayer ply)
                return ply;

            AmlPlayer? newPly = await FetchPlayer(guid);
            if (newPly is not null)
                cachedPlayers.Add(newPly);
            return newPly;
        }

        public async Task<IReadOnlyList<Player>> GetOrFetchPlayers()
        {
            if (cachedPlayers.Count == 0)
                cachedPlayers.AddRange(await FetchPlayers());
            return cachedPlayers;
        }

        public async Task<IEnumerable<Player>> GetOrFetchPlayerLeaderboard(StatType statType)
        {
            var list = await GetOrFetchPlayers();
            return list.OrderBy(ply => ply.GetRankBy(statType));
        }

        public async Task<MaxMode?> GetOrFetchMaxMode(int id)
        {
            if (cachedMaxModes.Find(mode => mode.Id == id) is AmlMaxMode maxMode)
                return maxMode;

            AmlMaxMode? newMode = await FetchMaxMode(id);
            if (newMode is not null)
                cachedMaxModes.Add(newMode);
            return newMode;
        }

        public async Task<IReadOnlyList<MaxMode>> GetOrFetchMaxModes()
        {
            if (cachedMaxModes.Count == 0)
                cachedMaxModes.AddRange(await FetchMaxModes());

            return cachedMaxModes;
        }

        public async Task<IEnumerable<MaxMode>> GetOrFetchMaxModeListByRatio(int skillPersent)
        {
            if (cachedMaxModes.Count == 0)
                cachedMaxModes.AddRange(await FetchMaxModes());

            double skillRatio = skillPersent / 100.0;
            double rngRatio = 1 - skillRatio;

            return cachedMaxModes.OrderByDescending(m => m.GetPoints(PointType.Skill) * skillRatio + m.GetPoints(PointType.Rng) * rngRatio);
        }

        public async Task<(IReadOnlyCollection<MaxMode>, IReadOnlyCollection<Player>)> Search(string query)
        {
            if (cachedPlayers.Count == 0)
                cachedPlayers.AddRange(await FetchPlayers());

            var result = await GetResponse<SearchResult>($"/search/{Uri.EscapeDataString(query)}");

            IReadOnlyCollection<MaxMode> maxModes = result!.MaxModes.Select(a => (MaxMode)new AmlMaxMode(this, a)).ToList();
            List<Player> players = new List<Player>();
            foreach (var item in result!.Players)
            {
                if (cachedPlayers.Find(ply => ply.Guid == item.Guid) is Player player)
                    players.Add(player);
            }

            return (maxModes, players);
        }

        public async Task<IReadOnlyCollection<Record>> GetOrFetchPlayerRecords(Player player)
        {
            if (player.RecordsFetched)
                return player.RecordsCache;

            var records = await FetchPlayerRecords(player);
            foreach (var item in records)
            {
                if (item.MaxMode is AmlMaxMode amlMaxMode)
                    amlMaxMode.recordsCache.Add(item);
            }

            if (player is AmlPlayer amlPlayer)
            {
                foreach (var item in records)
                {
                    amlPlayer.recordsCache.Add(item);
                }
                amlPlayer.recordsFetched = true;
            }

            return records;
        }

        public async Task<IReadOnlyCollection<Record>> GetOrFetchMaxModeRecords(MaxMode maxMode)
        {
            if (maxMode.RecordsFetched)
                return maxMode.RecordsCache;

            var records = await FetchMaxModeRecords(maxMode);
            foreach (var item in records)
            {
                if (item.Player is AmlPlayer amlPlayer)
                    amlPlayer.recordsCache.Add(item);
            }

            if (maxMode is AmlMaxMode amlMaxMode)
            {
                foreach (var item in records)
                {
                    amlMaxMode.recordsCache.Add(item);
                }
                amlMaxMode.recordsFetched = true;
            }

            return records;
        }

        private async Task<AmlMaxMode?> FetchMaxMode(int id)
        {
            var result = await GetResponse<FullMaxModeData>($"/level/{id}");
            if (result is null)
                return null;
            return new AmlMaxMode(this, result.Data);
        }

        private async Task<AmlPlayer?> FetchPlayer(Guid guid)
        {
            var result = await GetResponse<PlayerData>($"/player/{guid}");
            if (result is null)
                return null;
            return new AmlPlayer(this, result);
        }

        private async Task<List<AmlPlayer>> FetchPlayers()
        {
            var result = await GetResponse<PlayerData[]>($"/players/skill/page/1");
            return result!.Select(ply => new AmlPlayer(this, ply)).ToList();
        }

        private async Task<List<AmlMaxMode>> FetchMaxModes()
        {
            var result = await GetResponse<MaxModeData[]>("/levels/ml/page/1");
            return result!.Select(a => new AmlMaxMode(this, a)).ToList();
        }

        private async Task<List<AmlRecord>> FetchPlayerRecords(Player player)
        {
            var result = await GetResponse<RecordData[]>($"player/{player.Guid}/records/skillValue");
            return result!.Select(res => new AmlRecord(this, res, player)).ToList();
        }

        private async Task<List<AmlRecord>> FetchMaxModeRecords(MaxMode maxMode)
        {
            var result = await GetResponse<FullMaxModeData>($"level/{maxMode.Id}");
            return result!.Records.Select(res => new AmlRecord(this, res, maxMode)).ToList();
        }

        private async Task<T?> GetResponse<T>(string url)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<T>(url, options);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return default;
            }
        }
    }
}
