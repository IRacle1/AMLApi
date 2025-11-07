using AMLApi.Core.Data;

namespace AMLApi.Core.Cached
{
    public abstract class CachedRecord : Record
    {
        protected CachedRecord(RecordData data) :
            base(data)
        {
        }

        public abstract CachedMaxMode MaxMode { get; }
        public abstract CachedPlayer Player { get; }

        public override string ToString()
        {
            return MaxMode.ToString() + ": " + Player.ToString();
        }
    }
}
