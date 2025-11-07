using AMLApi.Core.Data;

namespace AMLApi.Core.Cached
{
    public abstract class CachedPlayer : Player
    {
        protected CachedPlayer(PlayerData data) :
            base(data)
        {
        }

        public abstract IReadOnlyCollection<CachedRecord> RecordsCache { get; }
        public abstract bool RecordsFetched { get; }

        public abstract Task<IReadOnlyCollection<CachedRecord>> GetRecords();
    }
}
