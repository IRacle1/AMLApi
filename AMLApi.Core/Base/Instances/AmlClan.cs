using AMLApi.Core.Data.Clans;
using AMLApi.Core.Enums;

namespace AMLApi.Core.Base.Instances
{
    public abstract class AmlClan : Clan
    {
        protected readonly ClanData clanData;

        protected AmlClan(ClanData clanData)
        {
            this.clanData = clanData;
            LogoUrl = string.Format(LogoUrlFormat, clanData.Id);
        }

        /// <inheritdoc/>
        public override Guid Guid => clanData.Id;

        /// <inheritdoc/>
        public override string Name => clanData.Name;

        /// <inheritdoc/>
        public override string Tag => clanData.Tag;

        /// <inheritdoc/>
        public override string Description => clanData.Description;

        /// <inheritdoc/>
        public override string LogoUrl { get; }

        /// <inheritdoc/>
        public override Guid OwnerId => clanData.OwnerId;

        /// <inheritdoc/>
        public override DateTime CreatedAtUtc => clanData.CreatedAtUtc;

        /// <inheritdoc/>
        public override int MembersCount => clanData.TotalMembers;

        /// <inheritdoc/>
        public override int SkillRank => clanData.SkillRank;

        /// <inheritdoc/>
        public override int RngRank => clanData.RngRank;

        /// <inheritdoc/>
        public override int ModesRank => clanData.ModesRank;

        /// <inheritdoc/>
        public override int OverallRank => clanData.OverallRank;

        /// <inheritdoc/>
        public override int GetTotalStatBy(StatType statType) => statType switch
        {
            StatType.Skill => clanData.TotalSkill,
            StatType.Rng => clanData.TotalRng,
            StatType.Overall => clanData.TotalRating,
            StatType.MaxModeBeaten => clanData.TotalModesBeaten,
            _ => 0,
        };

        /// <inheritdoc/>
        public override double GetAveragePointBy(PointType pointType)
        {
            return pointType switch
            {
                PointType.Skill => clanData.AvgSkill,
                PointType.Rng => clanData.AvgRng,
                PointType.All => clanData.AvgRating,
                _ => 0.0,
            };
        }

        public override int GetRankBy(StatType statType) => statType switch
        {
            StatType.Skill => clanData.TotalSkill,
            StatType.Rng => clanData.TotalRng,
            StatType.Overall => clanData.TotalRating,
            StatType.MaxModeBeaten => clanData.TotalModesBeaten,
            _ => 0,
        };
    }
}
