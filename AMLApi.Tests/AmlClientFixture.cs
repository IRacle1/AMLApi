using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Core.Cached;
using AMLApi.Core.Rest;

namespace AMLApi.Tests
{
    public class AmlClientFixture
    {
        private CachedClient? cachedClient;
        private RestClient? restClient;

        public async Task<CachedClient> GetCachedClient()
        {
            return cachedClient ??= await CachedClient.CreateClient();
        }

        public RestClient GetRestClient()
        {
            return restClient ??= RestClient.CreateClient();
        }
    }
}
