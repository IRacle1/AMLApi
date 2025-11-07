using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Core.Cached;

namespace AMLApi.Core.Cached.Interfaces
{
    internal interface ICacheRecordsHolder
    {
        void AddRecord(CachedRecord record);
        void SetFetched();
    }
}
