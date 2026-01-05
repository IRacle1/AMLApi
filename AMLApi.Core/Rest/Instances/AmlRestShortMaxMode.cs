using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Core.Data.Clans;
using AMLApi.Core.Data.Players;

namespace AMLApi.Core.Rest.Instances
{
    public class AmlRestShortPlayer : RestShortPlayer
    {
        protected RestClient client;

        internal AmlRestShortPlayer(RestClient amlClient, ShortPlayerData data)
            : base(data)
        {
            client = amlClient;
        }

        public override Task<RestPlayer> Fetch()
        {
            return client.FetchPlayer(playerData.Guid);
        }
    }
}
