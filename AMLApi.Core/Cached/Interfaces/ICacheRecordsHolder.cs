namespace AMLApi.Core.Cached.Interfaces
{
    internal interface ICacheRecordsHolder
    {
        void AddRecord(CachedRecord record);
        void SetFetched();
    }
}
