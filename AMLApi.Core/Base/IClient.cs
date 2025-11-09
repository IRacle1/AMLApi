using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Core.Data;
using AMLApi.Core.Enums;
using AMLApi.Core.Rest;

namespace AMLApi.Core.Base
{
    public interface IClient
    {
        Task<Player> FetchPlayer(Guid guid);
        Task<IReadOnlyCollection<Player>> FetchPlayers();
        Task<IEnumerable<Player>> FetchPlayerLeaderboard(StatType statType);

        Task<MaxMode> FetchMaxMode(int id);
        Task<IReadOnlyCollection<MaxMode>> FetchMaxModes();
        Task<IEnumerable<MaxMode>> FetchMaxModeListByRatio(int skillPersent);

        Task<IReadOnlyCollection<Record>> FetchPlayerRecords(Player player);
        Task<IReadOnlyCollection<Record>> FetchPlayerRecords(Guid guid);

        Task<IReadOnlyCollection<Record>> FetchMaxModeRecords(MaxMode maxMode);
        Task<IReadOnlyCollection<Record>> FetchMaxModeRecords(int id);

        Task<(IReadOnlyCollection<MaxMode>, IReadOnlyCollection<ShortPlayerData>)> Search(string query);
    }
}
