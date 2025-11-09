using AMLApi.Core.Base;
using AMLApi.Core.Cached;
using AMLApi.Core.Enums;
using AMLApi.Core.Rest;

using Xunit.Abstractions;

using Record = AMLApi.Core.Base.Record;

namespace AMLApi.Tests
{
    public class ClientTests : IClassFixture<AmlClientFixture>
    {
        private readonly AmlClientFixture clientFixture;
        private readonly ITestOutputHelper output;

        public ClientTests(AmlClientFixture clientFixture, ITestOutputHelper output)
        {
            this.clientFixture = clientFixture;
            this.output = output;
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task MaxMode_ValidProperties(bool cachedClient)
        {
            IClient client = cachedClient ? await clientFixture.GetCachedClient() : clientFixture.GetRestClient();

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

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task MaxMode_ValidMaxModeFlags(bool cachedClient)
        {
            IClient client = cachedClient ? await clientFixture.GetCachedClient() : clientFixture.GetRestClient();

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
        [InlineData(true, 100)]
        [InlineData(true, 90)]
        [InlineData(true, 50)]
        [InlineData(true, 0)]
        [InlineData(false, 100)]
        [InlineData(false, 90)]
        [InlineData(false, 50)]
        [InlineData(false, 0)]
        public async Task MaxModes_ValidOrder(bool cachedClient, int skillPersent)
        {
            IClient client = cachedClient ? await clientFixture.GetCachedClient() : clientFixture.GetRestClient();

            List<MaxMode> list = (await client.FetchMaxModeListByRatio(skillPersent)).ToList();

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
        [InlineData(true, StatType.Skill)]
        [InlineData(true, StatType.Rng)]
        [InlineData(true, StatType.MaxModeBeaten)]
        [InlineData(true, StatType.Overall)]
        [InlineData(false, StatType.Skill)]
        [InlineData(false, StatType.Rng)]
        [InlineData(false, StatType.MaxModeBeaten)]
        [InlineData(false, StatType.Overall)]
        public async Task Players_ValidLeaderboardOrder(bool cachedClient, StatType statType)
        {
            IClient client = cachedClient ? await clientFixture.GetCachedClient() : clientFixture.GetRestClient();

            IReadOnlyList<Player> list = (await client.FetchPlayerLeaderboard(statType)).ToList();

            Player lastPlayer = list[0];

            output.WriteLine("Stat {0}", statType);
            output.WriteLine("{0}: {1}", lastPlayer, lastPlayer.GetStatValue(statType));

            for (int i = 1; i < list.Count; i++)
            {
                double lastValue = lastPlayer.GetStatValue(statType);
                double curValue = list[i].GetStatValue(statType);
                output.WriteLine("{0}: {1}", list[i], curValue);
                output.WriteLine("stats: {0} >= {1}", lastValue, curValue);
                Assert.False(lastValue < curValue);

                int lastRank = lastPlayer.GetRankBy(statType);
                int curRank = list[i].GetRankBy(statType);
                output.WriteLine("rank: {0} <= {1}", lastRank, curRank);
                Assert.False(lastRank > curRank);

                lastPlayer = list[i];
            }
        }

        // hell nah
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Players_ValidPlayerList(bool cachedClient)
        {
            IClient client = cachedClient ? await clientFixture.GetCachedClient() : clientFixture.GetRestClient();

            IReadOnlyCollection<Player> list = await client.FetchPlayers();

            foreach (Player player in list)
            {
                output.WriteLine("Player: {0}", player);
                Assert.NotNull(player.Name);
            }
        }

        [Theory]
        [InlineData(true, "1f30ece4-bfd2-40e7-8b52-855ab7cffde3")] // serd
        [InlineData(true, "0bfc57bc-7c6f-4bb9-9a1e-de189dde38b5")] // ultament
        [InlineData(true, "1d5fcf64-5686-45e8-b0b8-1402d2cdef44")] // meee :3
        [InlineData(false, "1f30ece4-bfd2-40e7-8b52-855ab7cffde3")] // serd
        [InlineData(false, "0bfc57bc-7c6f-4bb9-9a1e-de189dde38b5")] // ultament
        [InlineData(false, "1d5fcf64-5686-45e8-b0b8-1402d2cdef44")] // meee :3
        public async Task Records_ValidPlayerRecords(bool cachedClient, string rawGuid)
        {
            IClient client = cachedClient ? await clientFixture.GetCachedClient() : clientFixture.GetRestClient();

            Guid guid = Guid.Parse(rawGuid);
            Player player = await client.FetchPlayer(guid);

            output.WriteLine("Player: {0}", player);

            IReadOnlyCollection<Record> records = await client.FetchPlayerRecords(player);

            foreach (Record record in records)
            {
                output.WriteLine("Record: {0}", record);

                Assert.NotNull(record.VideoLink);

                Assert.Equal(guid, record.PlayerGuid);
            }
        }

        [Theory]
        [InlineData(true, 89)] // ems
        [InlineData(true, 171)] // 50/20 ndc vanilla
        [InlineData(false, 89)] // ems
        [InlineData(false, 171)] // 50/20 ndc vanilla
        public async Task Records_ValidMaxModeRecords(bool cachedClient, int id)
        {
            IClient client = cachedClient ? await clientFixture.GetCachedClient() : clientFixture.GetRestClient();

            MaxMode maxMode = await client.FetchMaxMode(id);

            output.WriteLine("MaxMode: {0}", maxMode);

            IReadOnlyCollection<Record> records = await client.FetchMaxModeRecords(maxMode);

            Assert.NotNull(records);

            foreach (Record record in records!)
            {
                output.WriteLine("Record: {0}", record);

                Assert.NotNull(record.VideoLink);

                Assert.Equal(id, record.MaxModeId);
            }
        }
    }
}