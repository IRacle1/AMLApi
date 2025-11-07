using AMLApi.Core.Base;
using AMLApi.Core.Data;

namespace AMLApi.Core.Rest
{
    public abstract class RestPlayer : Player
    {
        protected RestPlayer(PlayerData data) :
            base(data)
        {
        }

        public abstract Task<IReadOnlyCollection<RestRecord>> FetchRecords();
    }
}
