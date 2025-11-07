using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Core.Data;
using AMLApi.Core.Enums;
using AMLApi.Core.Objects;

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
