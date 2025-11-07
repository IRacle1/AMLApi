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
    public abstract class RestRecord : Record
    {
        protected RestRecord(RecordData data) :
            base(data)
        {
        }

        public abstract Task<RestMaxMode> FetchMaxMode();
        public abstract Task<RestPlayer> FetchPlayer();
    }
}
