using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AMLApi.Api.Enums;

namespace AMLApi.Api.Objects
{
    public abstract class MaxMode : IEquatable<MaxMode>
    {
        public abstract int Id { get; }
        public abstract string Name { get; }
        public abstract string Creator { get; }
        public abstract string Length { get; }
        public abstract string VerificationVideoUrl { get; }

        public abstract string GameName { get; }
        public abstract string GameUrl { get; }

        public abstract int Top { get; }

        public abstract bool IsSelfImposed { get; }
        public abstract bool IsPrePatch { get; }
        public abstract bool IsExtra { get; }
        public abstract bool IsMaxModeOfTheMonth { get; }

        public abstract IReadOnlyCollection<Record> RecordsCache { get; }
        public abstract bool RecordsFetched { get; }

        public abstract int GetPoints(PointType pointsType);

        public abstract Task<IReadOnlyCollection<Record>> GetOrFetchRecords();

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
