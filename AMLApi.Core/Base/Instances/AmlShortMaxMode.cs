using AMLApi.Core.Data.MaxModes;
using AMLApi.Core.Enums;

namespace AMLApi.Core.Base.Instances
{
    public abstract class AmlShortMaxMode : ShortMaxMode
    {
        protected readonly ShortMaxModeData maxModeData;
        protected readonly int id;

        protected AmlShortMaxMode(ShortMaxModeData data, int id)
        {
            maxModeData = data;
            VerificationVideoUrl = $"https://youtu.be/{data.VideoId}";
            this.id = id;
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
    }
}
