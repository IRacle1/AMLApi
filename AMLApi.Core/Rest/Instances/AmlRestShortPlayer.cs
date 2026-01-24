using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Core.Data.Clans;
using AMLApi.Core.Data.MaxModes;
using AMLApi.Core.Data.Players;

namespace AMLApi.Core.Rest.Instances
{
    public class AmlRestShortMaxMode : RestShortMaxMode
    {
        protected RestClient client;

        internal AmlRestShortMaxMode(RestClient amlClient, ShortMaxModeData data, int id, int skillPoints, int rngPoints)
            : base(data, id, skillPoints, rngPoints)
        {
            client = amlClient;
        }

        public override Task<RestMaxMode> Fetch()
        {
            return client.FetchMaxMode(id);
        }
    }
}
