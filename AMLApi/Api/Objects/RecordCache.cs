using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMLApi.Api.Objects
{
    public abstract class RecordCache
    {
        public abstract IReadOnlyCollection<Record> Cache { get; }

        public abstract Task Fetch();
    }
}
