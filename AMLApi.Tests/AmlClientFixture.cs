using AMLApi.Core.Base;
using AMLApi.Core.Cached;
using AMLApi.Core.Rest;

namespace AMLApi.Tests
{
    public class AmlClientFixture
    {
        private CachedClient? cachedClient;
        private RestClient? restClient;
        private BaseAmlClient? baseClient;

        public async Task<CachedClient> GetCachedClient()
        {
            return cachedClient ??= await CachedClient.CreateClient();
        }

        public RestClient GetRestClient()
        {
            return restClient ??= RestClient.CreateClient();
        }

        public BaseAmlClient GetBaseClient()
        {
            return baseClient ??= BaseAmlClient.CreateClient();
        }
    }
}
