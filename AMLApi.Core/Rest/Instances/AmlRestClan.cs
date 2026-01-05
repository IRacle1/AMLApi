using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Core.Base;
using AMLApi.Core.Data.Clans;
using AMLApi.Core.Data.MaxModes;

namespace AMLApi.Core.Rest.Instances
{
    internal class AmlRestClan : RestClan
    {
        protected RestClient client;

        internal AmlRestClan(RestClient amlClient, ClanData data)
            : base(data)
        {
            client = amlClient;
        }

        public override Task<IReadOnlyCollection<RestShortPlayer>> FetchMembers()
        {
            return client.FetchClanMembers(this);
        }
    }
}
