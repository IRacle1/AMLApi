using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AMLApi.Api.Objects.Data
{
    public class SearchResult
    {
        public MaxModeData[] MaxModes { get; set; } = null!;

        public ShortPlayerData[] Players { get; set; } = null!;
    }
}
