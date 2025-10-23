using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using AMLApi.Core.Json;
using AMLApi.Core.Objects.Data;
using AMLApi.Core.Objects;
using AMLApi.Core.Objects.Instances;
using AMLApi.Core.Enums;

namespace AMLApi.Core
{
    public class AmlClient : IFnafClient
    {
        private HttpClient httpClient;
        private static JsonSerializerOptions options = new();

        public bool IsInit { get; private set; }

        private Dictionary<int, AmlMaxMode> cachedMaxModes = new();
        private Dictionary<Guid, AmlPlayer> cachedPlayers = new();

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
        public const string AvatarUrlFormat = "https://zirlaiexwekjusbhjibc.supabase.co/storage/v1/object/public/avatars/{0}.jpg";

        public IReadOnlyCollection<MaxMode> MaxModes => cachedMaxModes.Values;

        public IReadOnlyCollection<Player> Players => cachedPlayers.Values;

        public static async Task<IFnafClient> CreateClient()
        {
            AmlClient client = new();
            await client.RefillCache();
            return client;
        }

        public async Task RefillCache()
        {
            PurgeCache();
            await UpdatePlayersCache();
            await UpdateMaxModesCache();
        }

        public Player? GetPlayer(Guid guid)
        {
            if (cachedPlayers.TryGetValue(guid, out AmlPlayer? ply))
                return ply;

            return null;
        }

        public IEnumerable<Player> GetPlayerLeaderboard(StatType statType)
        {
            return cachedPlayers.Values.OrderBy(ply => ply.GetRankBy(statType));
        }

        public MaxMode? GetMaxMode(int id)
        {
            if (cachedMaxModes.TryGetValue(id, out AmlMaxMode? maxMode))
                return maxMode;

            return null;
        }

        public IReadOnlyCollection<MaxMode> GetOrFetchMaxModes()
        {
            return cachedMaxModes.Values;
        }

        public IEnumerable<MaxMode> GetMaxModeListByRatio(int skillPersent)
        {
            int rngPersent = 100 - skillPersent;

            return cachedMaxModes.Values.OrderByDescending(m => m.GetPoints(PointType.Skill) * rngPersent + m.GetPoints(PointType.Rng) * rngPersent);
        }

        public async Task<(IReadOnlyCollection<MaxMode>, IReadOnlyCollection<Player>)?> Search(string query)
        {
            if (cachedPlayers.Count == 0)
                await UpdatePlayersCache();

            var result = await GetResponse<SearchResult>($"/search/{Uri.EscapeDataString(query)}");

            if (result is null)
                return null;

            IReadOnlyCollection<MaxMode> maxModes = result!.MaxModes.Select(a => (MaxMode)new AmlMaxMode(this, a)).ToList();
            List<Player> players = new(result!.Players.Length);
            foreach (var item in result!.Players)
            {
                if (cachedPlayers.TryGetValue(item.Guid, out AmlPlayer? player))
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

        private async Task UpdateMaxModesCache()
        {
            foreach (var item in await FetchMaxModes())
            {
                cachedMaxModes.Add(item.Id, item);
            }
        }

        private async Task UpdatePlayersCache()
        {
            foreach (var item in await FetchPlayers())
            {
                cachedPlayers.Add(item.Guid, item);
            }
        }

        private void PurgeCache()
        {
            cachedPlayers.Clear();
            cachedMaxModes.Clear();
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
