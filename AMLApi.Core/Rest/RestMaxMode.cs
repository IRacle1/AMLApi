using AMLApi.Core.Base;
using AMLApi.Core.Base.Instances;
using AMLApi.Core.Data.MaxModes;

namespace AMLApi.Core.Rest
{
    public abstract class RestMaxMode : AmlMaxMode
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
