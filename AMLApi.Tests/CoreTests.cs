using AMLApi.Core;
using AMLApi.Core.Enums;
using AMLApi.Core.Objects;
using AMLApi.Core.Objects.Cached;

using Xunit.Abstractions;

namespace AMLApi.Tests
{
    public class CoreTests : IClassFixture<AmlClientFixture>
    {
        private readonly AmlClientFixture clientFixture;
        private readonly ITestOutputHelper output;

        public CoreTests(AmlClientFixture clientFixture, ITestOutputHelper output)
        {
            this.clientFixture = clientFixture;
            this.output = output;
        }

        [Fact]
        public async Task CachedClient_GetMaxMode_ValidProperties()
        {
            CachedClient client = await clientFixture.GetCachedClient();

            // ultimatum
            Assert.True(client.TryGetMaxMode(9, out CachedMaxMode? maxMode));

            output.WriteLine("Max mode: {0}", maxMode);

            Assert.Equal("Ultimatum", maxMode.Name);
            Assert.Equal("Shooter25", maxMode.Creator);
            Assert.Equal("6:00", maxMode.Length);
            Assert.Contains("TLQM1PsjPiw", maxMode.VerificationVideoUrl);
            Assert.Equal("UCN: Recode", maxMode.GameName);

            Assert.Equal(250, maxMode.GetPoints(PointType.Rng));

            Assert.False(maxMode.IsSelfImposed);
            Assert.False(maxMode.IsPrePatch);
            Assert.False(maxMode.IsExtra);
            Assert.False(maxMode.IsMaxModeOfTheMonth);
        }

        [Fact]
        public async Task CachedClient_GetMaxMode_ValidMaxModeFlags()
        {
            CachedClient client = await clientFixture.GetCachedClient();

            // hp npg rv
            Assert.True(client.TryGetMaxMode(425, out CachedMaxMode? maxMode));

            output.WriteLine("Max mode: {0}", maxMode);

            Assert.True(maxMode.IsSelfImposed);
            Assert.True(maxMode.IsPrePatch);
            Assert.False(maxMode.IsExtra);
            Assert.False(maxMode.IsMaxModeOfTheMonth);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(90)]
        [InlineData(50)]
        [InlineData(0)]
        public async Task CachedClient_MaxModes_ValidFilter(int skillPersent)
        {
            CachedClient client = await clientFixture.GetCachedClient();

            var list = client.GetMaxModeListByRatio(skillPersent).ToList();

            double lastValue = list[0].GetPointsByRatio(skillPersent);

            output.WriteLine("Max mode {0}, value: {1}", list[0], lastValue);

            for (int i = 1; i < list.Count; i++)
            {
                double newValue = list[i].GetPointsByRatio(skillPersent);
                output.WriteLine("Max mode {0}, value: {1}", list[i], newValue);

                Assert.False(lastValue < newValue);

                lastValue = newValue;
            }
        }

        [Theory]
        [InlineData(StatType.Skill)]
        [InlineData(StatType.Rng)]
        [InlineData(StatType.MaxModeBeaten)]
        [InlineData(StatType.Overall)]
        public async Task RestClient_Players_ValidLeaderboard(StatType statType)
        {
            RestClient client = clientFixture.GetRestClient();

            var list = (await client.FetchPlayerLeaderboard(statType)).ToList();

            double lastValue = list[0].GetStatValue(statType);

            output.WriteLine("stat value {0}, {1}: {2}", statType, list[0], lastValue);

            for (int i = 1; i < list.Count; i++)
            {
                double newValue = list[i].GetStatValue(statType);
                output.WriteLine("{0}: {1}", list[i], newValue);

                Assert.False(lastValue < newValue);

                lastValue = newValue;
            }
        }
    }
}