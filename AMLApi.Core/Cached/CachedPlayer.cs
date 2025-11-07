using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Core.Data;
using AMLApi.Core.Objects;

namespace AMLApi.Core.Cached
{
    public abstract class CachedPlayer : Player
    {
        protected CachedPlayer(PlayerData data) : 
            base(data)
        {
        }

        public abstract IReadOnlyCollection<CachedRecord> RecordsCache { get; }
        public abstract bool RecordsFetched { get; }

        public abstract Task<IReadOnlyCollection<CachedRecord>> GetRecords();
    }
}
