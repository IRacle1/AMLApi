using AMLApi.Core.Data.MaxModes;
using AMLApi.Core.Enums;

namespace AMLApi.Core.Base.Instances
{
    public abstract class AmlShortMaxMode : ShortMaxMode
    {
        protected readonly ShortMaxModeData maxModeData;
        protected readonly int id;

        private readonly int skillPoints;
        private readonly int rngPoints;

        protected AmlShortMaxMode(ShortMaxModeData data, int id, int skillPoints, int rngPoints)
        {
            maxModeData = data;
            VerificationVideoUrl = $"https://youtu.be/{data.VideoId}";
            this.id = id;
            this.skillPoints = skillPoints;
            this.rngPoints = rngPoints;
        }

        /// <inheritdoc/>
        public override int Id => id;

        /// <inheritdoc/>
        public override string Name => maxModeData.Name;

        /// <inheritdoc/>
        public override string VerificationVideoUrl { get; }

        /// <inheritdoc/>
        public override string VerificationVideoId => maxModeData.VideoId;

        /// <inheritdoc/>
        public override string GameName => maxModeData.GameName;

        /// <inheritdoc/>
        public override int Top => maxModeData.Top;

        /// <inheritdoc/>
        public override int GetPoints(PointType pointType)
        {
            int res = 0;
            if (pointType.HasFlag(PointType.Rng))
                res += rngPoints;
            if (pointType.HasFlag(PointType.Skill))
                res += skillPoints;

            return res;
        }

        /// <inheritdoc/>
        public override double GetPointsByRatio(int skillRatio)
        {
            if (skillRatio < 0 || skillRatio > 100)
                throw new ArgumentOutOfRangeException(nameof(skillRatio));

            double skillScale = skillRatio / 100.0;
            double rngScale = 1 - skillScale;

            return skillScale * skillPoints + rngScale * rngPoints;
        }
    }
}
