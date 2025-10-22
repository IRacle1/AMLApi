using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Api.Objects.Data;

namespace AMLApi.Api.Objects.Instances
{
    internal class AmlRecord : Record
    {
        private RecordData recordData;
        private AmlClient client;

        private Player player;
        private MaxMode maxMode;

        internal AmlRecord(AmlClient amlClient, RecordData data, Player player)
        {
            client = amlClient;
            recordData = data;

            this.player = player;
            this.maxMode = amlClient.CachedMaxModes.First(m => m.Id == data.MaxModeId);
        }

        internal AmlRecord(AmlClient amlClient, RecordData data, MaxMode maxMode)
        {
            client = amlClient;
            recordData = data;

            this.maxMode = maxMode;
            this.player = amlClient.CachedPlayers.First(ply => ply.Guid == data.UId);
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
