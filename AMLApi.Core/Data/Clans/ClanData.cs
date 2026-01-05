using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AMLApi.Core.Data.Clans
{
    public class ClanData
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("tag")]
        public string Tag { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("owner_id")]
        public Guid OwnerId { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAtUtc { get; set; }

        [JsonPropertyName("total_members")]
        public int TotalMembers { get; set; }

        [JsonPropertyName("total_skill")]
        public int TotalSkill { get; set; }

        [JsonPropertyName("total_rng")]
        public int TotalRng { get; set; }

        [JsonPropertyName("total_modes_beaten")]
        public int TotalModesBeaten { get; set; }

        [JsonPropertyName("total_rating")]
        public int TotalRating { get; set; }

        [JsonPropertyName("avg_skill")]
        public double AvgSkill { get; set; }

        [JsonPropertyName("avg_rng")]
        public double AvgRng { get; set; }

        [JsonPropertyName("avg_rating")]
        public double AvgRating { get; set; }

        [JsonPropertyName("skill_rank")]
        public int SkillRank { get; set; }

        [JsonPropertyName("rng_rank")]
        public int RngRank { get; set; }

        [JsonPropertyName("modes_rank")]
        public int ModesRank { get; set; }

        [JsonPropertyName("overall_rank")]
        public int OverallRank { get; set; }

        [JsonPropertyName("players")]
        public ClanShortPlayerData OwnerData { get; set; } = null!;
    }
}
