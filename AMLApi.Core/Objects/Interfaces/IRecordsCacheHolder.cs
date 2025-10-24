using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMLApi.Core.Objects.Interfaces
{
    public interface IRecordsCacheHolder
    {
        public void AddRecord(Record record);
        public void SetFetched();
    }
}
