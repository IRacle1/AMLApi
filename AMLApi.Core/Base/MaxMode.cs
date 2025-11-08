using AMLApi.Core.Data;
using AMLApi.Core.Enums;

namespace AMLApi.Core.Base
{
    public abstract class MaxMode : IEquatable<MaxMode>
    {
        protected readonly MaxModeData maxModeData;

        protected MaxMode(MaxModeData data)
        {
            maxModeData = data;
            VerificationVideoUrl = $"https://youtu.be/{data.VideoId}";
        }

        public int Id => maxModeData.Id;
        public string Name => maxModeData.Name;
        public string Creator => maxModeData.Creator;
        public string Length => maxModeData.Length;

        public string VerificationVideoUrl { get; }

        public string GameName => maxModeData.GameName;
        public string GameUrl => maxModeData.GameUrl;

        public int Top => maxModeData.Top;

        public bool IsSelfImposed => maxModeData.IsSelfImposed;
        public bool IsPrePatch => maxModeData.IsPrePatch;
        public bool IsExtra => maxModeData.IsExtra;
        public bool IsMaxModeOfTheMonth => maxModeData.IsMaxModeOfTheMonth;

        public int GetPoints(PointType pointType)
        {
            int res = 0;
            if (pointType.HasFlag(PointType.Rng))
                res += maxModeData.RngPoints;
            if (pointType.HasFlag(PointType.Skill))
                res += maxModeData.SkillPoints;

            return res;
        }

        public double GetPointsByRatio(int skillRatio)
        {
            if (skillRatio < 0 || skillRatio > 100)
                throw new ArgumentOutOfRangeException(nameof(skillRatio));

            double skillScale = skillRatio / 100.0;
            double rngScale = 1 - skillScale;

            return skillScale * maxModeData.SkillPoints + rngScale * maxModeData.RngPoints;
        }

        public int GetSkillSetPercent(SkillSetType skillSetType)
        {
            int ret = 0;

            foreach (SkillSetType check in Enum.GetValues<SkillSetType>())
            {
                if (check is SkillSetType.None or SkillSetType.All)
                    continue;

                if (skillSetType.HasFlag(check))
                    ret += maxModeData.GetSkillSetValue(check);
            }

            return ret;
        }

        public bool Equals(MaxMode? other)
        {
            if (other is null)
                return false;

            return Id == other.Id;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as MaxMode);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
