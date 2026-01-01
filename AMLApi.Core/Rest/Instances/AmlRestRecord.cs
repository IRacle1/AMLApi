using System.Numerics;

using AMLApi.Core.Base;
using AMLApi.Core.Data;

namespace AMLApi.Core.Rest.Instances
{
    internal class AmlRestRecord : RestRecord
    {
        protected RestClient client;

        internal AmlRestRecord(RestClient amlClient, RecordData data)
            : base(data)
        {
            client = amlClient;

            PlayerData = data.Player!;
            MaxModeData = data.MaxMode!;
        }

        public override async Task<RestMaxMode> FetchMaxMode()
        {
            return await client.FetchMaxMode(MaxModeId);
        }

        public override Task<RestPlayer> FetchPlayer()
        {
            return client.FetchPlayer(PlayerGuid);
        }

        public override ShortMaxModeData? MaxModeData { get; }

        public override PlayerData? PlayerData { get; }
    }
}
