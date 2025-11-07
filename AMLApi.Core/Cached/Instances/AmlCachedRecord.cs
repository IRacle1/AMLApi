using AMLApi.Core.Data;

namespace AMLApi.Core.Cached.Instances
{
    internal class AmlCachedRecord : CachedRecord
    {
        internal AmlCachedRecord(CachedClient amlClient, RecordData data)
            : base(data)
        {
            MaxMode = amlClient.GetMaxMode(data.MaxModeId)!;
            Player = amlClient.GetPlayer(data.UId)!;
        }

        public override CachedMaxMode MaxMode { get; }

        public override CachedPlayer Player { get; }
    }
}
