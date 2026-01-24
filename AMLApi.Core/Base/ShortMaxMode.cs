using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Core.Base.Instances;
using AMLApi.Core.Data.Players;
using AMLApi.Core.Enums;

namespace AMLApi.Core.Base
{
    public abstract class ShortMaxMode : IEquatable<ShortMaxMode>
    {
        protected ShortMaxMode()
        {
        }

        /// <summary>
        /// Gets a maxmode id.
        /// </summary>
        public abstract int Id { get; }

        /// <summary>
        /// Gets a maxmode name.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Gets a full maxmode verification video url.
        /// </summary>
        public abstract string VerificationVideoUrl { get; }

        /// <summary>
        /// Gets a youtube verification video id for maxmode.
        /// </summary>
        public abstract string VerificationVideoId { get; }

        /// <summary>
        /// Gets a maxmode game name.
        /// </summary>
        public abstract string GameName { get; }

        /// <summary>
        /// Gets a max mode top ranked by 100/0
        /// </summary>
        /// <remarks>
        /// Maxmode has 2 points type - <see cref="PointType.Skill">skill</see> and <see cref="PointType.Rng">rng</see>.
        /// Skill points are calculated based on abstract formula `p=f(t)`, where t - actual max mode top based on 100% skill.
        /// And rng is an absolute value between 0-3000 for specific maxmode.
        /// In other words, for ranking maxmode staff uses only `Top` and `rng value`, so they ranks them by 100/0 and 0/100, other ratios are automatic.
        /// </remarks>
        public abstract int Top { get; }

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

        /// <inheritdoc/>
        public bool Equals(ShortMaxMode? other)
        {
            if (other is null)
                return false;

            return Id == other.Id;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return Equals(obj as ShortMaxMode);
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
}
