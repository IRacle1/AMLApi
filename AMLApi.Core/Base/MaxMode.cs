using AMLApi.Core.Data;
using AMLApi.Core.Enums;

namespace AMLApi.Core.Base
{
    /// <summary>
    /// Base abstract object for max mode.
    /// </summary>
    public abstract class MaxMode : IEquatable<MaxMode>
    {
        protected readonly MaxModeData maxModeData;

        protected MaxMode(MaxModeData data)
        {
            maxModeData = data;
            VerificationVideoUrl = $"https://youtu.be/{data.VideoId}";
        }

        /// <summary>
        /// Gets a maxmode id.
        /// </summary>
        public int Id => maxModeData.Id;

        /// <summary>
        /// Gets a maxmode name.
        /// </summary>
        public string Name => maxModeData.Name;

        /// <summary>
        /// Gets a maxmode creator nickname.
        /// </summary>
        public string Creator => maxModeData.Creator;

        /// <summary>
        /// Gets a maxmode creator nickname.
        /// </summary>
        public string Length => maxModeData.Length;

        /// <summary>
        /// Gets a full maxmode verification video url.
        /// </summary>
        public string VerificationVideoUrl { get; }

        /// <summary>
        /// Gets a youtube verification video id for maxmode.
        /// </summary>
        public string VerificationVideoId => maxModeData.VideoId;

        /// <summary>
        /// Gets a maxmode game name.
        /// </summary>
        public string GameName => maxModeData.GameName;

        /// <summary>
        /// Gets a maxmode game download url.
        /// </summary>
        public string GameUrl => maxModeData.GameUrl;

        /// <summary>
        /// Gets a max mode top ranked by 100/0
        /// </summary>
        /// <remarks>
        /// Maxmode has 2 points type - <see cref="PointType.Skill">skill</see> and <see cref="PointType.Rng">rng</see>.
        /// Skill points are calculated based on abstract formula `p=f(t)`, where t - actual max mode top based on 100% skill.
        /// And rng is an absolute value between 0-3000 for specific maxmode.
        /// In other words, for ranking maxmode staff uses only `Top` and `rng value`, so they ranks them by 100/0 and 0/100, other ratios are automatic.
        /// </remarks>
        public int Top => maxModeData.Top;

        /// <summary>
        /// Gets a value that indicates whether the maxmode is self imposed.
        /// </summary>
        public bool IsSelfImposed => maxModeData.IsSelfImposed;

        /// <summary>
        /// Gets a value that indicates whether the maxmode is on pre patch verion of a game.
        /// </summary>
        public bool IsPrePatch => maxModeData.IsPrePatch;

        /// <summary>
        /// Gets a value that indicates whether the maxmode is extra.
        /// </summary>
        /// <remarks>
        /// Idk what extra means bro😭.
        /// </remarks>
        public bool IsExtra => maxModeData.IsExtra;

        /// <summary>
        /// Xd.
        /// </summary>
        public bool IsMaxModeOfTheMonth => maxModeData.IsMaxModeOfTheMonth;

        /// <summary>
        /// Gets a maxmode description.
        /// </summary>
        public string? Description => maxModeData.Description;

        /// <summary>
        /// Gets a maxmode points value by <see cref="PointType"/> argument.
        /// </summary>
        /// <param name="pointType"><see cref="PointType"/> needed to calculate.</param>
        /// <returns>Total points value.</returns>
        /// <remarks>
        /// Support <see cref="PointType"/> flags.
        /// </remarks>
        public int GetPoints(PointType pointType)
        {
            int res = 0;
            if (pointType.HasFlag(PointType.Rng))
                res += maxModeData.RngPoints;
            if (pointType.HasFlag(PointType.Skill))
                res += maxModeData.SkillPoints;

            return res;
        }

        /// <summary>
        /// Calculates total maxmode points by skill ratio.
        /// </summary>
        /// <param name="skillRatio">skill ratio in percentage, [0,100].</param>
        /// <returns>Total points by ratio.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="skillRatio"/> is out of range [0,100].</exception>
        public double GetPointsByRatio(int skillRatio)
        {
            if (skillRatio < 0 || skillRatio > 100)
                throw new ArgumentOutOfRangeException(nameof(skillRatio));

            double skillScale = skillRatio / 100.0;
            double rngScale = 1 - skillScale;

            return skillScale * maxModeData.SkillPoints + rngScale * maxModeData.RngPoints;
        }

        /// <summary>
        /// Return skillset value in percentage, for specific <see cref="SkillSetType"/>.
        /// </summary>
        /// <param name="skillSetType">Target <see cref="SkillSetType"/>.</param>
        /// <returns>Skillset value in percentage.</returns>
        /// <remarks>
        /// Supports <see cref="SkillSetType"/> flags.
        /// </remarks>
        public int GetSkillSetPercent(SkillSetType skillSetType)
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

        public MaxModeData ToData()
        {
            return maxModeData;
        }

        /// <inheritdoc/>
        public bool Equals(MaxMode? other)
        {
            if (other is null)
                return false;

            return Id == other.Id;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return Equals(obj as MaxMode);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return Name;
        }
    }

    public class MaxModeRatioComparer<T> : IComparer<T>
        where T : MaxMode
    {
        private readonly int skillRatio;

        private MaxModeRatioComparer(int skillRatio)
        {
            this.skillRatio = skillRatio;
        }

        public static MaxModeRatioComparer<T> CreateNew(int skillRatio)
        {
            return new MaxModeRatioComparer<T>(skillRatio);
        }

        public int Compare(T? x, T? y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    return 0;
                }
                return -1;
            }

            if (y == null)
            {
                return 1;
            }

            double xPoints = x.GetPointsByRatio(skillRatio);
            double yPoints = y.GetPointsByRatio(skillRatio);

            if (xPoints == yPoints)
            {
                // x > y
                // x harder y
                // top x less than top y
                // yTop - xTop
                return y.Top - x.Top;
            }

            // x > y
            // x harder y
            // x have more points than y
            // xPnts - yPnts > 0
            return double.Sign(xPoints - yPoints);
        }
    }
}
