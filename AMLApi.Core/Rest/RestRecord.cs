using AMLApi.Core.Base;
using AMLApi.Core.Data;

namespace AMLApi.Core.Rest
{
    public abstract class RestRecord : Record
    {
        protected RestRecord(RecordData data) :
            base(data)
        {
        }

        public abstract Task<RestMaxMode> FetchMaxMode();
        public abstract Task<RestPlayer> FetchPlayer();

        public abstract PlayerData? PlayerData { get; }
        public abstract ShortMaxModeData? MaxModeData { get; }
    }
}
