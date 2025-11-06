using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Core.Data;
using AMLApi.Core.Enums;
using AMLApi.Core.Objects.Rest;

namespace AMLApi.Core.Objects.Rest.Instances
{
    internal class AmlRestPlayer : Player
    {
        private RestClient client;

        internal AmlRestPlayer(RestClient amlClient, PlayerData data)
            : base(data)
        {
            client = amlClient;
        }

        public override async Task<IReadOnlyCollection<Record>> GetRecords()
        {
            return await client.FetchPlayerRecords(this);
        }
    }
}
