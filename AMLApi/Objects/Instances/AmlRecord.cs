using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Core.Objects.Data;

namespace AMLApi.Core.Objects.Instances
{
    internal class AmlRecord : Record
    {
        private RecordData recordData;

        private Player player;
        private MaxMode maxMode;

        internal AmlRecord(AmlClient amlClient, RecordData data, Player player)
        {
            recordData = data;

            this.player = player;
            maxMode = amlClient.GetMaxMode(data.MaxModeId)!;
        }

        internal AmlRecord(AmlClient amlClient, RecordData data, MaxMode maxMode)
        {
            recordData = data;

            this.maxMode = maxMode;
            player = amlClient.GetPlayer(data.UId)!;
        }

        public override string VideoLink => recordData.VideoLink;

        public override int Progress => recordData.Progress ?? 100;

        public override DateTime DateUtc => recordData.DateUtc;

        public override TimeSpan? TimeTaken => recordData.TimeTaken;

        public override bool IsMobile => recordData.IsMobile != 0;

        public override bool IsChecked => recordData.IsChecked;

        public override string? Comment => recordData.Comment;

        public override int FPS => recordData.FPS ?? 60;

        public override bool IsNotificationSent => recordData.IsNotificationSent;

        public override MaxMode MaxMode => maxMode;

        public override Player Player => player;
    }
}
