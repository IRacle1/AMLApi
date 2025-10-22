using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Core;
using AMLApi.Core.Enums;
using AMLApi.Core.Objects.Data;

namespace AMLApi.Core.Objects.Instances
{
    internal class AmlMaxMode : MaxMode
    {
        private MaxModeData maxModeData;
        private AmlClient client;
        
        internal bool recordsFetched;
        internal HashSet<Record> recordsCache = new();

        internal AmlMaxMode(AmlClient amlClient, MaxModeData data)
        {
            client = amlClient;
            maxModeData = data;
        }

        public override int Id => maxModeData.Id;

        public override string Name => maxModeData.Name;

        public override string Creator => maxModeData.Creator;

        public override string Length => maxModeData.Length;

        public override string VerificationVideoUrl => maxModeData.VerificationVideoUrl;

        public override string GameName => maxModeData.GameName;

        public override string GameUrl => maxModeData.GameUrl;

        public override int Top => maxModeData.Top;

        public override bool IsSelfImposed => maxModeData.IsSelfImposed;

        public override bool IsPrePatch => maxModeData.IsPrePatch;

        public override bool IsExtra => maxModeData.IsExtra;

        public override bool IsMaxModeOfTheMonth => maxModeData.IsMaxModeOfTheMonth;

        public override IReadOnlyCollection<Record> RecordsCache => recordsCache;
        
        public override bool RecordsFetched => recordsFetched;

        public override int GetPoints(PointType pointType)
        {
            int res = 0;
            if (pointType.HasFlag(PointType.Rng))
                res += maxModeData.RngPoints;
            if (pointType.HasFlag(PointType.Skill))
                res += maxModeData.SkillPoints;

            return res;
        }

        public override async Task<IReadOnlyCollection<Record>> GetOrFetchRecords()
        {
            return await client.GetOrFetchMaxModeRecords(this);
        }
    }
}
