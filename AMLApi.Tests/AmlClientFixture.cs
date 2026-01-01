using AMLApi.Core.Base;
using AMLApi.Core.Rest;

namespace AMLApi.Tests
{
    public class AmlClientFixture
    {
        private RestClient? restClient;
        private RawAmlClient? baseClient;

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
