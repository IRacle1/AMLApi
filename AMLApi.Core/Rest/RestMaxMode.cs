using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Core.Cached;
using AMLApi.Core.Data;
using AMLApi.Core.Objects;

namespace AMLApi.Core.Rest
{
    public abstract class RestMaxMode : MaxMode
    {
        protected RestMaxMode(MaxModeData data) : 
            base(data)
        {
        }

        public abstract bool HaveRecords { get; }

        public abstract bool TryGetRecordsNoFetch(out IReadOnlyCollection<RestRecord>? records);

        public abstract Task<IReadOnlyCollection<RestRecord>> FetchRecords();
    }
}
