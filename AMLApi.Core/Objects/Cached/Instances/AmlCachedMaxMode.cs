using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMLApi.Core.Enums;
using AMLApi.Core.Objects.Cached;
using AMLApi.Core.Data;
using AMLApi.Core.Objects.Cached.Interfaces;

namespace AMLApi.Core.Objects.Cached.Instances
{
    internal class AmlCachedMaxMode : CachedMaxMode, IRecordsCacheHolder
    {
        private CachedClient client;
        
        internal bool recordsFetched;
        internal HashSet<CachedRecord> recordsCache = new();

        internal AmlCachedMaxMode(CachedClient amlClient, MaxMode maxMode) 
            : base(maxMode)
        {
            client = amlClient;
        }

        public override IReadOnlyCollection<CachedRecord> RecordsCache => recordsCache;

        public override bool RecordsFetched => recordsFetched;

        public override async Task<IReadOnlyCollection<Record>> GetRecords()
        {
            return await client.GetOrFetchMaxModeRecords(this);
        }

        public void AddRecord(CachedRecord record)
        {
            recordsCache.Add(record);
        }

        public void SetFetched()
        {
            recordsFetched = true;
        }
    }
}
