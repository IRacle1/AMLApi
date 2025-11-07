using AMLApi.Core.Base;
using AMLApi.Core.Cached;
using AMLApi.Core.Enums;
using AMLApi.Core.Rest;

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

            List<CachedMaxMode> list = client.GetMaxModeListByRatio(skillPersent).ToList();

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

            List<CachedPlayer> list = client.GetPlayerLeaderboard(statType).ToList();

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

        [Theory]
        [InlineData("1f30ece4-bfd2-40e7-8b52-855ab7cffde3")] // serd
        [InlineData("0bfc57bc-7c6f-4bb9-9a1e-de189dde38b5")] // ultament
        [InlineData("1d5fcf64-5686-45e8-b0b8-1402d2cdef44")] // meee :3
        public async Task Records_ValidPlayerRecords(string rawGuid)
        {
            CachedClient client = await clientFixture.GetCachedClient();

            Guid guid = Guid.Parse(rawGuid);
            CachedPlayer? player = client.GetPlayer(guid);

            output.WriteLine("Player: {0}", player);

            Assert.NotNull(player);

            IReadOnlyCollection<CachedRecord> records = await player.GetRecords();

            foreach (CachedRecord record in records)
            {
                output.WriteLine("Record: {0}", record);

                Assert.NotNull(record.VideoLink);

                Assert.Equal(guid, record.PlayerGuid);

                Assert.NotNull(record.MaxMode);
                Assert.Equal(record.MaxModeId, record.MaxMode.Id);

                Assert.Equal(player, record.Player);
            }
        }

        [Theory]
        [InlineData(89)] // esp
        [InlineData(171)] // 50/20 ndc vanilla
        public async Task Records_ValidMaxModeRecords(int id)
        {
            CachedClient client = await clientFixture.GetCachedClient();

            CachedMaxMode? maxMode = client.GetMaxMode(id);

            output.WriteLine("MaxMode: {0}", maxMode);

            Assert.NotNull(maxMode);

            var records = await maxMode.GetRecords();

            Assert.NotNull(records);

            foreach (CachedRecord record in records!)
            {
                output.WriteLine("Record: {0}", record);

                Assert.NotNull(record.VideoLink);

                Assert.Equal(id, record.MaxModeId);

                Assert.NotNull(record.Player);
                Assert.Equal(record.PlayerGuid, record.Player.Guid);

                Assert.Equal(maxMode, record.MaxMode);
            }
        }
    }
}