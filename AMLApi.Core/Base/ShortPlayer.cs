using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Core.Base.Instances;
using AMLApi.Core.Data.Players;

namespace AMLApi.Core.Base
{
    public abstract class ShortPlayer : IEquatable<ShortPlayer>
    {
        protected ShortPlayer()
        {
        }

        /// <summary>
        /// Gets a player <see cref="System.Guid">guid</see>.
        /// </summary>
        public abstract Guid Guid { get; }

        /// <summary>
        /// Gets a player nickname.
        /// </summary>
        public abstract string Nickname { get; }

        /// <inheritdoc/>
        public bool Equals(ShortPlayer? other)
        {
            if (other is null)
                return false;

            return Guid == other.Guid;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return Equals(obj as ShortPlayer);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Guid.GetHashCode();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return Nickname;
        }
    }
}
