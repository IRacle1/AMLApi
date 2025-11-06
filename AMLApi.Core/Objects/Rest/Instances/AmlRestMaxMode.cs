using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMLApi.Core.Enums;
using AMLApi.Core.Objects.Cached;
using AMLApi.Core.Data;
using AMLApi.Core.Objects.Cached.Interfaces;

namespace AMLApi.Core.Objects.Rest.Instances
{
    internal class AmlRestMaxMode : MaxMode
    {
        private RestClient client;

        internal AmlRestMaxMode(RestClient amlClient, MaxModeData data) 
            : base(data)
        {
            client = amlClient;
        }

        public override async Task<IReadOnlyCollection<Record>> GetRecords()
        {
            return await client.FetchMaxModeRecords(this);
        }
    }
}
