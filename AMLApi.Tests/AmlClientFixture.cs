using AMLApi.Core.Base;
using AMLApi.Core.Cached;
using AMLApi.Core.Rest;

namespace AMLApi.Tests
{
    public class AmlClientFixture
    {
        private CachedClient? cachedClient;
        private RestClient? restClient;
        private RawAmlClient? baseClient;

        public async Task<CachedClient> GetCachedClient()
        {
            return cachedClient ??= await CachedClient.CreateClient();
        }

        public RestClient GetRestClient()
        {
            return restClient ??= RestClient.CreateClient();
        }

        public RawAmlClient GetBaseClient()
        {
            return baseClient ??= RawAmlClient.CreateClient();
        }
    }
}
