using AMLApi.Core.Base;
using AMLApi.Core.Base.Instances;
using AMLApi.Core.Data.Players;

namespace AMLApi.Core.Rest
{
    public abstract class RestShortPlayer : AmlShortPlayer
    {
        protected RestShortPlayer(ShortPlayerData data) 
            : base(data)
        {
        }

        public abstract Task<RestPlayer> Fetch();
    }
}
