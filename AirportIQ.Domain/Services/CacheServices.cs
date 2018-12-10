using System;
using System.Data;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;

using AirportIQ.Domain.Contracts;

namespace AirportIQ.Domain.Services 
{
    [Obsolete("DO NOT USE THIS ON PAIN OF DEATH - MAV", true)]
    public class CacheServices : ICache
    {
        ICacheManager cacheManager = CacheFactory.GetCacheManager();
        SlidingTime slidingTime = new SlidingTime(TimeSpan.FromSeconds(Convert.ToInt16(ConfigurationManager.AppSettings["CacheSlidingTime"])));

             [Obsolete("DO NOT USE THIS ON PAIN OF DEATH - MAV", true)]
            public void addtoCache(string Cachekey, DataTable dataToStore)
            {
                cacheManager.Add(Cachekey, dataToStore, CacheItemPriority.Normal, null, slidingTime);
            }

             [Obsolete("DO NOT USE THIS ON PAIN OF DEATH - MAV", true)]
            public void addtoCacheDS(string Cachekey, DataSet dataToStore)
            {
                cacheManager.Add(Cachekey, dataToStore, CacheItemPriority.Normal, null, slidingTime);
            }

             [Obsolete("DO NOT USE THIS ON PAIN OF DEATH - MAV", true)]
            public DataTable retrievefromCache(string Cachekey)
            {    
                DataTable cacheObject=null;
                if(cacheManager.Contains(Cachekey))
                {           
                  cacheObject = (DataTable)cacheManager.GetData(Cachekey);
                }
                return cacheObject;  
            }

            [Obsolete("DO NOT USE THIS ON PAIN OF DEATH - MAV", true)]
            public DataSet retrievefromCacheDS(string Cachekey)
            {
                DataSet cacheObject = null;
                if (cacheManager.Contains(Cachekey))
                {
                    cacheObject = (DataSet)cacheManager.GetData(Cachekey);
                }
                return cacheObject;
            }
            [Obsolete("DO NOT USE THIS ON PAIN OF DEATH - MAV", true)]
            public void removeCache(string Cachekey)
            {
                 cacheManager.Remove(Cachekey);   
            }
    }   
}
