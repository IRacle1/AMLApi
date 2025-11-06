

namespace AMLApi.Core.Objects.Cached.Instances
{
    internal class AmlCachedRecord : CachedRecord
    {
        private readonly CachedPlayer player;
        private readonly CachedMaxMode maxMode;

        internal AmlCachedRecord(CachedClient amlClient, Record record)
            : base(record)
        {
            maxMode = amlClient.GetMaxMode(record.MaxModeId)!;
            player = amlClient.GetPlayer(record.PlayerGuid)!;
        }

        public override CachedMaxMode MaxMode => maxMode;

        public override CachedPlayer Player => player;

        public override Task<Player> GetPlayer()
        {
            return Task.FromResult((Player)player);
        }

        public override Task<MaxMode> GetMaxMode()
        {
            return Task.FromResult((MaxMode)maxMode);
        }

        public override string ToString()
        {
            return MaxMode.ToString() + ": " + Player.ToString();
        }
    }
}
