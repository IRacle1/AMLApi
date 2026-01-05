using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Core.Base.Instances;
using AMLApi.Core.Data.MaxModes;
using AMLApi.Core.Enums;

namespace AMLApi.Core.Base
{
    public abstract class MaxMode : ShortMaxMode
    {
        protected MaxMode()
        {
        }

        /// <summary>
        /// Gets a maxmode creator nickname.
        /// </summary>
        public abstract string Creator { get; }

        /// <summary>
        /// Gets a maxmode creator nickname.
        /// </summary>
        public abstract string Length { get; }

        /// <summary>
        /// Gets a maxmode game download url.
        /// </summary>
        public abstract string GameUrl { get; }

        /// <summary>
        /// Gets a value that indicates whether the maxmode is self imposed.
        /// </summary>
        public abstract bool IsSelfImposed { get; }

        /// <summary>
        /// Gets a value that indicates whether the maxmode is on pre patch verion of a game.
        /// </summary>
        public abstract bool IsPrePatch { get; }

        /// <summary>
        /// Gets a value that indicates whether the maxmode is extra.
        /// </summary>
        /// <remarks>
        /// Idk what extra means bro😭.
        /// </remarks>
        public abstract bool IsExtra { get; }

        /// <summary>
        /// Xd.
        /// </summary>
        public abstract bool IsMaxModeOfTheMonth { get; }

        /// <summary>
        /// Gets a maxmode description.
        /// </summary>
        public abstract string? Description { get; }

        /// <summary>
        /// Gets a maxmode points value by <see cref="PointType"/> argument.
        /// </summary>
        /// <param name="pointType"><see cref="PointType"/> needed to calculate.</param>
        /// <returns>Total points value.</returns>
        /// <remarks>
        /// Support <see cref="PointType"/> flags.
        /// </remarks>
        public abstract int GetPoints(PointType pointType);

        /// <summary>
        /// Calculates total maxmode points by skill ratio.
        /// </summary>
        /// <param name="skillRatio">skill ratio in percentage, [0,100].</param>
        /// <returns>Total points by ratio.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="skillRatio"/> is out of range [0,100].</exception>
        public abstract double GetPointsByRatio(int skillRatio);

        /// <summary>
        /// Return skillset value in percentage, for specific <see cref="SkillSetType"/>.
        /// </summary>
        /// <param name="skillSetType">Target <see cref="SkillSetType"/>.</param>
        /// <returns>Skillset value in percentage.</returns>
        /// <remarks>
        /// Supports <see cref="SkillSetType"/> flags.
        /// </remarks>
        public abstract int GetSkillSetPercent(SkillSetType skillSetType);
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
