using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Core.Data;
using AMLApi.Core.Enums;
using AMLApi.Core.Objects.Rest;

namespace AMLApi.Core.Objects.Cached
{
    public abstract class CachedMaxMode : MaxMode
    {
        protected CachedMaxMode(MaxMode maxMode) :
            base(maxMode)
        {
        }

        public abstract IReadOnlyCollection<CachedRecord> RecordsCache { get; }
        public abstract bool RecordsFetched { get; }
    }
}
