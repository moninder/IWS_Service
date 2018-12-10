using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AirportIQ.Model.Models.QueryBrowser;

namespace AirportIQ.Data
{
    public interface IQueryRepository
    {
        Query GetById(int id);
        int Insert(Query query, int UserID);
        void Update(Query query, int UserID);
        void Delete(int queryId, int UserID);
        IEnumerable<Query> QueryList(List<Query> systemQueries, int ownerId, int startIndex, int maxRows);
        int QueryListCount(int ownerId);
    }

}
