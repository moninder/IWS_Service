using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AirportIQ.Model.Models.QueryBrowser;

namespace AirportIQ.Domain.Contracts
{
    public interface IQueryService
    {
        void Delete(int queryId, int UserID);
        Query GetById(int queryId, int userId);
        Query Create(string queryKey, int userId);
        int Insert(Query query, int UserID);
        void Update(Query query, int UserID);
        System.Collections.Generic.IEnumerable<Query> QueryList(int userId, int startIndex, int maxRows);
        int QueryListCount(int userId);
        string CreateSql(Query query, string facilityCode, int? startIndex, int? maxRows);
        string CreateSqlTotalRowCount(Query query, string facilityCode);
    }
}
