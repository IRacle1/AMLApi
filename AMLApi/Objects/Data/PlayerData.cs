using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AMLApi.Core.Objects.Data
{
    public class PlayerData
    {
        [JsonPropertyName("uid")]
        public Guid Guid { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("avatar")]
        public string? AvatarUrl { get; set; }

        [JsonPropertyName("youtube")]
        public string? YoutubeUrl { get; set; }

        [JsonPropertyName("discord")]
        public string? DiscordName { get; set; }

        [JsonPropertyName("discordId")]
        public string? DiscordId { get; set; }

        [JsonPropertyName("totalSkillpt")]
        public int SkillPoints { get; set; }

        [JsonPropertyName("skillrank")]
        public int SkillRank { get; set; }

        [JsonPropertyName("totalRNGpt")]
        public int RngPoints { get; set; }

        [JsonPropertyName("rngrank")]
        public int RngRank { get; set; }

        [JsonPropertyName("rating")]
        public int TotalPoints { get; set; }

        [JsonPropertyName("overallRank")]
        public int TotalRank { get; set; }

        [JsonPropertyName("skillMaxPt")]
        public int SkillMaxPoints { get; set; }

        [JsonPropertyName("rngMaxPt")]
        public int RngMaxPoints { get; set; }

        [JsonPropertyName("modesBeaten")]
        public int ModesBeaten { get; set; }

        [JsonPropertyName("modesbeatenrank")]
        public int ModesBeatenRank { get; set; }

        [JsonPropertyName("continent")]
        public string? Continent { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("isAdmin")]
        public bool IsAdmin { get; set; }

        [JsonPropertyName("isManager")]
        public bool IsManager { get; set; }

        [JsonPropertyName("isBanned")]
        public bool IsBanned { get; set; }

        [JsonPropertyName("isHidden")]
        public bool IsHidden { get; set; }

        [JsonPropertyName("clan")]
        public string? Clan { get; set; }
    }
}
