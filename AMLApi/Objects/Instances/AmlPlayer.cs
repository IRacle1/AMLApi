using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Core;
using AMLApi.Core.Enums;
using AMLApi.Core.Objects;
using AMLApi.Core.Objects.Data;

namespace AMLApi.Core.Objects.Instances
{
    internal class AmlPlayer : Player
    {
        private PlayerData playerData;
        private AmlClient client;

        internal bool recordsFetched;
        internal HashSet<Record> recordsCache = new();

        internal AmlPlayer(AmlClient amlClient, PlayerData data) 
        {
            client = amlClient;
            playerData = data;
            DiscordId = ulong.TryParse(data.DiscordId, out ulong res) ? res : 0;
        }

        public override Guid Guid => playerData.Guid;

        public override int Id => playerData.Id;

        public override string Name => playerData.Name;

        public override string Email => playerData.Email;

        public override DateTime CreatedAt => playerData.CreatedAt;

        public override string? AvatarUrl => playerData.AvatarUrl;

        public override string? YoutubeUrl => playerData.YoutubeUrl;

        public override string? DiscordName => playerData.DiscordName;

        public override ulong DiscordId { get; }

        public override int ModesBeaten => playerData.ModesBeaten;

        public override string Continent => playerData.Continent;

        public override string Country => playerData.Country;

        public override bool IsAdmin => playerData.IsAdmin;

        public override bool IsManager => playerData.IsManager;

        public override bool IsBanned => playerData.IsBanned;

        public override bool IsHidden => playerData.IsHidden;

        public override string? Clan => playerData.Clan;

        public override IReadOnlyCollection<Record> RecordsCache => recordsCache;

        public override bool RecordsFetched => recordsFetched;

        public override int GetPointsBy(PointType pointType)
        {
            int res = 0;
            if (pointType.HasFlag(PointType.Rng))
                res += playerData.RngPoints;
            if (pointType.HasFlag(PointType.Skill))
                res += playerData.SkillPoints;

            return res;
        }

        public override int GetMaxPointsBy(PointType pointType)
        {
            int res = 0;
            if (pointType.HasFlag(PointType.Rng))
                res += playerData.RngMaxPoints;
            if (pointType.HasFlag(PointType.Skill))
                res += playerData.SkillMaxPoints;

            return res;
        }

        public override int GetRankBy(StatType statType)
        {
            return statType switch
            {
                StatType.Skill => playerData.SkillRank,
                StatType.Rng => playerData.RngRank,
                StatType.Overall => playerData.TotalRank,
                StatType.MaxModeBeaten => playerData.ModesBeatenRank,
                _ => -1,
            };
        }

        public override async Task<IReadOnlyCollection<Record>> GetOrFetchRecords()
        {
            return await client.GetOrFetchPlayerRecords(this);
        }
    }
}
