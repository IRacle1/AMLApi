using AMLApi.Core.Data.MaxModes;
using AMLApi.Core.Data.Players;

namespace AMLApi.Core.Data
{
    public class SearchResult
    {
        public MaxModeData[] MaxModes { get; set; } = null!;

        public ShortPlayerData[] Players { get; set; } = null!;
    }
}
