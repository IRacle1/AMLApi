using System.Numerics;

using AMLApi.Core.Base;
using AMLApi.Core.Data;
using AMLApi.Core.Data.MaxModes;
using AMLApi.Core.Data.Players;

namespace AMLApi.Core.Rest.Instances
{
    internal class AmlRestRecord : RestRecord
    {
        protected RestClient client;

        internal AmlRestRecord(RestClient amlClient, RecordData data, RestPlayer? player, RestShortMaxMode? maxMode)
            : base(data)
        {
            client = amlClient;

            Player = player;
            MaxMode = maxMode;
        }

        public override async Task<RestMaxMode> FetchMaxMode()
        {
            return await client.FetchMaxMode(MaxModeId);
        }

        public override Task<RestPlayer> FetchPlayer()
        {
            return client.FetchPlayer(PlayerGuid);
        }

        public override RestShortMaxMode? MaxMode { get; }

        public override RestPlayer? Player { get; }
    }
}
