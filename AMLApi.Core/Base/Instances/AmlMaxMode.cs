using AMLApi.Core.Data.MaxModes;
using AMLApi.Core.Enums;

namespace AMLApi.Core.Base.Instances
{
    /// <summary>
    /// Base abstract object for max mode.
    /// </summary>
    public abstract class AmlMaxMode : MaxMode
    {
        protected readonly MaxModeData maxModeData;

        protected AmlMaxMode(MaxModeData data)
        {
            maxModeData = data;
            VerificationVideoUrl = $"https://youtu.be/{data.VideoId}";
        }

        /// <inheritdoc/>
        public override int Id => maxModeData.Id;

        /// <inheritdoc/>
        public override string Name => maxModeData.Name;

        /// <inheritdoc/>
        public override string Creator => maxModeData.Creator;

        /// <inheritdoc/>
        public override string Length => maxModeData.Length;

        /// <inheritdoc/>
        public override string VerificationVideoUrl { get; }

        /// <inheritdoc/>
        public override string VerificationVideoId => maxModeData.VideoId;

        /// <inheritdoc/>
        public override string GameName => maxModeData.GameName;

        /// <inheritdoc/>
        public override string GameUrl => maxModeData.GameUrl;

        /// <inheritdoc/>
        public override int Top => maxModeData.Top;

        /// <inheritdoc/>
        public override bool IsSelfImposed => maxModeData.IsSelfImposed;

        /// <inheritdoc/>
        public override bool IsPrePatch => maxModeData.IsPrePatch;

        /// <inheritdoc/>
        public override bool IsExtra => maxModeData.IsExtra;

        /// <inheritdoc/>
        public override bool IsMaxModeOfTheMonth => maxModeData.IsMaxModeOfTheMonth;

        /// <inheritdoc/>
        public override string? Description => maxModeData.Description;

        /// <inheritdoc/>
        public override int GetPoints(PointType pointType)
        {
            int res = 0;
            if (pointType.HasFlag(PointType.Rng))
                res += maxModeData.RngPoints;
            if (pointType.HasFlag(PointType.Skill))
                res += maxModeData.SkillPoints;

            return res;
        }

        /// <inheritdoc/>
        public override double GetPointsByRatio(int skillRatio)
        {
            if (skillRatio < 0 || skillRatio > 100)
                throw new ArgumentOutOfRangeException(nameof(skillRatio));

            double skillScale = skillRatio / 100.0;
            double rngScale = 1 - skillScale;

            return skillScale * maxModeData.SkillPoints + rngScale * maxModeData.RngPoints;
        }

        /// <inheritdoc/>
        public override int GetSkillSetPercent(SkillSetType skillSetType)
        {
            int ret = 0;

            foreach (SkillSetType check in Enum.GetValues<SkillSetType>())
            {
                if (check is SkillSetType.None or SkillSetType.All)
                    continue;

                if (skillSetType.HasFlag(check))
                    ret += maxModeData.GetSkillSetValue(check);
            }

            return ret;
        }
    }
}
