using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using AMLApi.Core.Data;
using AMLApi.Core.Objects.Rest;

namespace AMLApi.Core.Objects.Cached
{
    public abstract class CachedRecord : Record
    {
        protected CachedRecord(Record record) :
            base(record)
        {
        }

        public abstract CachedMaxMode MaxMode { get; }
        public abstract CachedPlayer Player { get; }
    }
}
