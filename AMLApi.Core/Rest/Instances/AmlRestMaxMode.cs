using System.Diagnostics.CodeAnalysis;

using AMLApi.Core.Data.MaxModes;

namespace AMLApi.Core.Rest.Instances
{
    internal class AmlRestMaxMode : RestMaxMode
    {
        private IReadOnlyCollection<RestRecord>? records;

        protected RestClient client;

        internal AmlRestMaxMode(RestClient amlClient, MaxModeData data)
            : base(data)
        {
            client = amlClient;
        }

        internal AmlRestMaxMode(RestClient amlClient, MaxModeData data, IReadOnlyCollection<RestRecord> records)
            : this(amlClient, data)
        {
            this.records = records;
            HaveRecords = true;
        }

        public override bool HaveRecords { get; }

        public override bool TryGetRecordsNoFetch([NotNullWhen(true)] out IReadOnlyCollection<RestRecord>? records)
        {
            records = this.records;
            return HaveRecords;
        }

        public override Task<IReadOnlyCollection<RestRecord>> FetchRecords()
        {
            return client.FetchMaxModeRecords(this);
        }
    }
}
