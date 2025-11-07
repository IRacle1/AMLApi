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
        }

        public override async Task<RestMaxMode> FetchMaxMode()
        {
            return await client.FetchMaxMode(MaxModeId);
        }

        public override Task<RestPlayer> FetchPlayer()
        {
            return client.FetchPlayer(PlayerGuid);
        }
    }
}
