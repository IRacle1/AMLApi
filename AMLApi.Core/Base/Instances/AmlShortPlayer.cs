using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Core.Data.Players;
using AMLApi.Core.Enums;

namespace AMLApi.Core.Base.Instances
{
    public abstract class AmlShortPlayer : ShortPlayer
    {
        protected readonly ShortPlayerData playerData;

        protected AmlShortPlayer(ShortPlayerData data)
        {
            playerData = data;
        }

        /// <inheritdoc/>
        public override Guid Guid => playerData.Guid;

        /// <inheritdoc/>
        public override string Nickname => playerData.Name;
    }
}
