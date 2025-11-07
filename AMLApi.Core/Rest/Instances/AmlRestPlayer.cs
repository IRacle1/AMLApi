using AMLApi.Core.Data;

namespace AMLApi.Core.Rest.Instances
{
    internal class AmlRestPlayer : RestPlayer
    {
        protected RestClient client;

        internal AmlRestPlayer(RestClient amlClient, PlayerData data)
            : base(data)
        {
            client = amlClient;
        }

        public override Task<IReadOnlyCollection<RestRecord>> FetchRecords()
        {
            return client.FetchPlayerRecords(this);
        }
    }
}
