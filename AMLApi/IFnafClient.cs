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
        IReadOnlyCollection<MaxMode> MaxModes { get; }
        IReadOnlyCollection<Player> Players { get; }

        Task RefillCache();

        Player? GetPlayer(Guid guid);
        IEnumerable<Player> GetPlayerLeaderboard(StatType statType);

        MaxMode? GetMaxMode(int id);
        IEnumerable<MaxMode> GetMaxModeListByRatio(int skillPersent);

        Task<IReadOnlyCollection<Record>> GetOrFetchPlayerRecords(Player player);
        Task<IReadOnlyCollection<Record>> GetOrFetchMaxModeRecords(MaxMode maxMode);

        Task<(IReadOnlyCollection<MaxMode>, IReadOnlyCollection<Player>)?> Search(string query);
    }
}
