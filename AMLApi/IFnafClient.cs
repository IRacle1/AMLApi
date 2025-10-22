using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Core.Enums;
using AMLApi.Core.Objects;

namespace AMLApi.Core
{
    public interface IFnafClient
    {
        IReadOnlyList<MaxMode> CachedMaxModes { get; }
        IReadOnlyList<Player> CachedPlayers { get; }

        Task RefillCache();
        void PurgeCache();

        Task<Player?> GetOrFetchPlayer(Guid guid);
        Task<IReadOnlyList<Player>> GetOrFetchPlayers();
        Task<IEnumerable<Player>> GetOrFetchPlayerLeaderboard(StatType statType);

        Task<MaxMode?> GetOrFetchMaxMode(int id);
        Task<IReadOnlyList<MaxMode>> GetOrFetchMaxModes();
        Task<IEnumerable<MaxMode>> GetOrFetchMaxModeListByRatio(int skillPersent);

        Task<IReadOnlyCollection<Record>> GetOrFetchPlayerRecords(Player player);
        Task<IReadOnlyCollection<Record>> GetOrFetchMaxModeRecords(MaxMode maxMode);

        Task<(IReadOnlyCollection<MaxMode>, IReadOnlyCollection<Player>)> Search(string query);
    }
}
