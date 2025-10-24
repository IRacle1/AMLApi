using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Core.Enums;
using AMLApi.Core.Objects;

namespace AMLApi.Core
{
    public interface IAmlClient
    {
        IReadOnlyCollection<MaxMode> MaxModes { get; }
        IReadOnlyCollection<Player> Players { get; }

        Task RefillCache();

        Player? GetPlayer(Guid guid);
        bool TryGetPlayer(Guid guid, [NotNullWhen(true)] out Player? player);
        IEnumerable<Player> GetPlayerLeaderboard(StatType statType);

        MaxMode? GetMaxMode(int id);
        bool TryGetMaxMode(int id, [NotNullWhen(true)] out MaxMode? maxMode);
        IEnumerable<MaxMode> GetMaxModeListByRatio(int skillPersent);

        Task<IReadOnlyCollection<Record>> GetOrFetchPlayerRecords(Player player);
        Task<IReadOnlyCollection<Record>> GetOrFetchMaxModeRecords(MaxMode maxMode);

        Task<(IReadOnlyCollection<MaxMode>, IReadOnlyCollection<Player>)?> Search(string query);
    }
}
