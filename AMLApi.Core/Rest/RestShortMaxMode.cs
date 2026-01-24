using AMLApi.Core.Base;
using AMLApi.Core.Base.Instances;
using AMLApi.Core.Data.MaxModes;
using AMLApi.Core.Data.Players;

namespace AMLApi.Core.Rest
{
    public abstract class RestShortMaxMode : AmlShortMaxMode
    {
        protected RestShortMaxMode(ShortMaxModeData data, int id, int skillPoints, int rngPoints) 
            : base(data, id, skillPoints, rngPoints)
        {
        }

        public abstract Task<RestMaxMode> Fetch();
    }
}
