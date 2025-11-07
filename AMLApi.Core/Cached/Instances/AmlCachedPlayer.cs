using AMLApi.Core.Cached.Interfaces;
using AMLApi.Core.Data;

namespace AMLApi.Core.Cached.Instances
{
    internal class AmlCachedPlayer : CachedPlayer, ICacheRecordsHolder
    {
        protected CachedClient client;
        private bool recordsFetched;
        private readonly HashSet<CachedRecord> recordsCache = new();

        internal AmlCachedPlayer(CachedClient amlClient, PlayerData data)
            : base(data)
        {
            client = amlClient;
        }

        public override IReadOnlyCollection<CachedRecord> RecordsCache => recordsCache;

        public override bool RecordsFetched => recordsFetched;

        public void AddRecord(CachedRecord record)
        {
            recordsCache.Add(record);
        }

        public void SetFetched()
        {
            recordsFetched = true;
        }

        public override Task<IReadOnlyCollection<CachedRecord>> GetRecords()
        {
            return client.GetOrFetchPlayerRecords(this);
        }
    }
}
