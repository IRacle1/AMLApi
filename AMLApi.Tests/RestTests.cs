using AMLApi.Core.Enums;
using AMLApi.Core.Rest;

using Xunit.Abstractions;

namespace AMLApi.Tests
{
    public class RestTests : IClassFixture<AmlClientFixture>
    {
        private readonly AmlClientFixture clientFixture;
        private readonly ITestOutputHelper output;

        public RestTests(AmlClientFixture clientFixture, ITestOutputHelper output)
        {
            this.clientFixture = clientFixture;
            this.output = output;
        }

        [Fact]
        public async Task GetMaxMode_ValidProperties()
        {
            RestClient client = clientFixture.GetRestClient();
            // ultimatum
            MaxMode maxMode = await client.FetchMaxMode(9);

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
        public async Task ValidMaxModeFlags()
        {
            RestClient client = clientFixture.GetRestClient();

            // hp npg rv
            MaxMode maxMode = await client.FetchMaxMode(425);

            Assert.NotNull(maxMode);

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
        [InlineData(StatType.Skill)]
        [InlineData(StatType.Rng)]
        [InlineData(StatType.MaxModeBeaten)]
        [InlineData(StatType.Overall)]
        public async Task Players_ValidLeaderboardOrder(StatType statType)
        {
            RestClient client = clientFixture.GetRestClient();

            IReadOnlyList<RestPlayer> list = await client.FetchPlayerLeaderboard(statType);

            RestPlayer lastPlayer = list[0];

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

        // hell nah
        [Fact]
        public async Task Players_ValidPlayerList()
        {
            RestClient client = clientFixture.GetRestClient();

            IReadOnlyCollection<RestPlayer> list = await client.FetchPlayers();

            foreach (RestPlayer player in list)
            {
                output.WriteLine("Player: {0}", player);
                Assert.NotNull(player.Name);
            }
        }

        [Theory]
        [InlineData("1f30ece4-bfd2-40e7-8b52-855ab7cffde3")] // serd
        [InlineData("0bfc57bc-7c6f-4bb9-9a1e-de189dde38b5")] // ultament
        [InlineData("1d5fcf64-5686-45e8-b0b8-1402d2cdef44")] // meee :3
        public async Task Records_ValidPlayerRecord(string rawGuid)
        {
            RestClient client = clientFixture.GetRestClient();

            Guid guid = Guid.Parse(rawGuid);
            RestPlayer player = await client.FetchPlayer(guid);

            output.WriteLine("Player: {0}", player);

            IReadOnlyCollection<RestRecord> records = await player.FetchRecords();

            foreach (RestRecord record in records)
            {
                output.WriteLine("Record: {0}", record);

                Assert.NotNull(record.VideoLink);

                Assert.Equal(guid, record.PlayerGuid);
            }
        }
    }
}