using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Core.Base;
using AMLApi.Core.Base.Instances;
using AMLApi.Core.Data.Clans;
using AMLApi.Core.Data.MaxModes;

namespace AMLApi.Core.Rest
{
    public abstract class RestClan : AmlClan
    {
        protected RestClan(ClanData data) :
            base(data)
        {
        }

        public abstract Task<IReadOnlyCollection<RestShortPlayer>> FetchMembers();
    }
}
