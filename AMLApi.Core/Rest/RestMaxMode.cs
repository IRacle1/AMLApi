using AMLApi.Core.Data;

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
