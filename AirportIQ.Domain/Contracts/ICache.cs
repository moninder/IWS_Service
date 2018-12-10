using System;
using System.Data;


namespace AirportIQ.Domain.Contracts
{
    public interface ICache
    {
        void addtoCache(string Cachekey, DataTable dataToStore);
        void addtoCacheDS(string Cachekey, DataSet dataToStore);
        DataTable retrievefromCache(string Cachekey);
        DataSet retrievefromCacheDS(string Cachekey);
        void removeCache(string Cachekey);
    }
}
