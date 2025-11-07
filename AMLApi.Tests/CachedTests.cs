using AMLApi.Core.Cached;
using AMLApi.Core.Enums;
using AMLApi.Core.Objects;

using Xunit.Abstractions;

namespace AMLApi.Tests
{
    public class CachedTests : IClassFixture<AmlClientFixture>
    {
        private readonly AmlClientFixture clientFixture;
        private readonly ITestOutputHelper output;

        public CachedTests(AmlClientFixture clientFixture, ITestOutputHelper output)
        {
            this.clientFixture = clientFixture;
            this.output = output;
        }

        [Fact]
        public async Task MaxMode_ValidProperties()
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
        public async Task MaxMode_ValidMaxModeFlags()
        {
            CachedClient client = await clientFixture.GetCachedClient();

            // hp npg rv
            Assert.True(client.TryGetMaxMode(425, out CachedMaxMode? maxMode));

            output.WriteLine("Max mode: {0}", maxMode);

            output.WriteLine("SelfImposed(true): {0}", maxMode.IsSelfImposed);
            Assert.True(maxMode.IsSelfImposed);

            output.WriteLine("Prepatch(true): {0}", maxMode.IsPrePatch);
            Assert.True(maxMode.IsPrePatch);

            output.WriteLine("Extra(false): {0}", maxMode.IsExtra);
            Assert.False(maxMode.IsExtra);

            output.WriteLine("IsMmotm(false): {0}", maxMode.IsMaxModeOfTheMonth);
            Assert.False(maxMode.IsMaxModeOfTheMonth);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(90)]
        [InlineData(50)]
        [InlineData(0)]
        public async Task MaxModes_ValidOrder(int skillPersent)
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
        public async Task Players_ValidLeaderboardOrder(StatType statType)
        {
            CachedClient client = await clientFixture.GetCachedClient();

            var list = client.GetPlayerLeaderboard(statType).ToList();

            CachedPlayer lastPlayer = list[0];

            output.WriteLine("Stat {0}", statType);
            output.WriteLine("{0}: {1}", lastPlayer, lastPlayer.GetStatValue(statType));

            for (int i = 1; i < list.Count; i++)
            {
                double lastValue = lastPlayer.GetStatValue(statType);
                double newValue = list[i].GetStatValue(statType);
                output.WriteLine("{0}: {1}", list[i], newValue);

                Assert.False(lastValue < newValue);
                Assert.False(lastPlayer.GetRankBy(statType) > list[i].GetRankBy(statType));

                lastPlayer = list[i];
            }
        }
    }
}