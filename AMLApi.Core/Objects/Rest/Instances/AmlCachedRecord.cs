using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Core.Data;
using AMLApi.Core.Objects.Cached;

namespace AMLApi.Core.Objects.Rest.Instances
{
    internal class AmlRestRecord : Record
    {
        private readonly RestClient client;

        internal AmlRestRecord(RestClient amlClient, RecordData data)
            : base(data)
        {
            client = amlClient;
        }

        public override async Task<Player> GetPlayer()
        {
            return (await client.FetchPlayer(PlayerGuid))!;
        }

        public override async Task<MaxMode> GetMaxMode()
        {
            return (await client.FetchMaxMode(MaxModeId))!;
        }
    }
}
