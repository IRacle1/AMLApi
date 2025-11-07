using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Core.Data;
using AMLApi.Core.Objects;

namespace AMLApi.Core.Rest
{
    public abstract class RestPlayer : Player
    {
        protected RestPlayer(PlayerData data) :
            base(data)
        {
        }

        public abstract Task<IReadOnlyCollection<RestRecord>> FetchRecords();
    }
}
