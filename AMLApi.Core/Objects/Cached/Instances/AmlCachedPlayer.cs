using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Core.Data;
using AMLApi.Core.Enums;
using AMLApi.Core.Objects.Cached;
using AMLApi.Core.Objects.Cached.Interfaces;

namespace AMLApi.Core.Objects.Cached.Instances
{
    internal class AmlCachedPlayer : CachedPlayer, IRecordsCacheHolder
    {
        private CachedClient client;

        internal bool recordsFetched;
        internal HashSet<CachedRecord> recordsCache = new();

        internal AmlCachedPlayer(CachedClient amlClient, Player player) 
            : base(player)
        {
            client = amlClient;
        }

        public override IReadOnlyCollection<CachedRecord> RecordsCache => recordsCache;

        public override bool RecordsFetched => recordsFetched;

        public override async Task<IReadOnlyCollection<Record>> GetRecords()
        {
            return await client.GetOrFetchPlayerRecords(this);
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
