using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AMLApi.Core.Data.Clans
{
    public class ClanShortPlayerData
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;
    }
}
