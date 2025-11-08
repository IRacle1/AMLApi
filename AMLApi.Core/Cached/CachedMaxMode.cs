using AMLApi.Core.Base;
using AMLApi.Core.Data;

namespace AMLApi.Core.Cached
{
    public abstract class CachedMaxMode : MaxMode
    {
        protected CachedMaxMode(MaxModeData data) :
            base(data)
        {
        }

        public abstract IReadOnlyCollection<CachedRecord> RecordsCache { get; }
        public abstract bool RecordsFetched { get; }

        public abstract Task<IReadOnlyCollection<CachedRecord>> GetOrFetchRecords();
    }
}
