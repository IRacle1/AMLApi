using AMLApi.Core.Base;
using AMLApi.Core.Base.Instances;
using AMLApi.Core.Data.Players;

namespace AMLApi.Core.Rest
{
    public abstract class RestPlayer : AmlPlayer
    {
        protected RestPlayer(PlayerData data) :
            base(data)
        {
        }

        public abstract Task<IReadOnlyCollection<RestRecord>> FetchRecords();
    }
}
