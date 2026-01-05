using AMLApi.Core.Base;
using AMLApi.Core.Base.Instances;
using AMLApi.Core.Data;
using AMLApi.Core.Data.MaxModes;
using AMLApi.Core.Data.Players;

namespace AMLApi.Core.Rest
{
    public abstract class RestRecord : AmlRecord
    {
        protected RestRecord(RecordData data) :
            base(data)
        {
        }

        public abstract Task<RestMaxMode> FetchMaxMode();
        public abstract Task<RestPlayer> FetchPlayer();

        public abstract RestPlayer? Player { get; }
        public abstract RestShortMaxMode? MaxMode { get; }
    }
}
