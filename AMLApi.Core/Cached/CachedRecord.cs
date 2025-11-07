using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using AMLApi.Core.Data;
using AMLApi.Core.Objects;

namespace AMLApi.Core.Cached
{
    public abstract class CachedRecord : Record
    {
        protected CachedRecord(RecordData data) :
            base(data)
        {
        }

        public abstract CachedMaxMode MaxMode { get; }
        public abstract CachedPlayer Player { get; }

        public override string ToString()
        {
            return MaxMode.ToString() + ": " + Player.ToString();
        }
    }
}
