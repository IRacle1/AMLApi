using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Core.Objects.Data;

namespace AMLApi.Core.Objects.Instances
{
    internal class AmlRecord : Record
    {
        private RecordData recordData;

        private readonly Player player;
        private readonly MaxMode maxMode;

        internal AmlRecord(IAmlClient amlClient, RecordData data)
        {
            recordData = data;
            DateUtc = DateTimeOffset.FromUnixTimeMilliseconds(data.DateUtc).UtcDateTime;
            if (data.TimeTaken is not null)
                TimeTaken = TimeSpan.FromMilliseconds(data.TimeTaken.Value);

            maxMode = amlClient.GetMaxMode(data.MaxModeId)!;
            player = amlClient.GetPlayer(data.UId)!;
        }

        public override string VideoLink => recordData.VideoLink;

        public override int Progress => recordData.Progress ?? 100;

        public override DateTime DateUtc { get; }

        public override TimeSpan? TimeTaken { get; }

        public override bool IsMobile => recordData.IsMobile != 0;

        public override bool IsChecked => recordData.IsChecked;

        public override string? Comment => recordData.Comment;

        public override int? FPS => recordData.FPS;

        public override bool IsNotificationSent => recordData.IsNotificationSent;

        public override MaxMode MaxMode => maxMode;

        public override Player Player => player;
    }
}
