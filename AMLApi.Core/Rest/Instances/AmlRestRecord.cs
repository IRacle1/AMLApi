using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Core.Data;
using AMLApi.Core.Objects;

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
