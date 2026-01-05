using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AMLApi.Core.Data.Clans
{
    public class ClanPlayerData
    {
        [JsonPropertyName("id")]
        public Guid InClanId { get; set; }

        [JsonPropertyName("user_id")]
        public Guid UserId { get; set; }

        [JsonPropertyName("clan_id")]
        public Guid ClanId { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; } = string.Empty;

        [JsonPropertyName("players")]
        public ClanShortPlayerData PlayerData { get; set; } = null!;
    }
}
