using System;
using System.Configuration;
using System.Data;
using AirportIQ.Data;
using AirportIQ.Data.SqlServer.Initializers;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Linq;

namespace AirportIQ.Data.SqlServer.Repositories
{
    public class QueryRepository : IQueryRepository
    {
        public Model.Models.QueryBrowser.Query GetById(int id)
        {
            var storedProcedure = new StoredProcedure() { StoredProcedureName = "[App.Sbo].[QueryBrowser.GetQueryById]" };

            StoredProcedureParameter dbParam = new StoredProcedureParameter("@queryId", ParameterType.DBInteger, id);
            storedProcedure.Parameters.Add(dbParam);

            var result = storedProcedure.ExecuteMultipleDataSet();
            if (result == null || result.Tables.Count == 0 || result.Tables[0].Rows.Count == 0)
                return null;

            var json = result.Tables[0].Rows[0]["QueryJson"].ToString();
            var userQuery = (new JavaScriptSerializer()).Deserialize<Model.Models.QueryBrowser.Query>(json);
            userQuery.Id = id; //set the correct id

            return userQuery;
        }

        public int Insert(Model.Models.QueryBrowser.Query query, int UserID)
        {
            var storedProcedure = new StoredProcedure() { StoredProcedureName = "[App.Sbo].[QueryBrowser.InsertQuery]" };
            var json = (new JavaScriptSerializer()).Serialize(query);

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@fkOwnerId", ParameterType.DBInteger, query.OwnerId));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@QueryName", ParameterType.DBString, query.Name));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@QueryType", ParameterType.DBString, query.QueryType));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@QueryKey", ParameterType.DBString, query.QueryKey));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@QueryJson", ParameterType.DBString, json));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));

            var result = storedProcedure.ExecuteMultipleDataSet();
            if (result == null || result.Tables.Count == 0 || result.Tables[0].Rows.Count == 0)
                return -1;
            else
                return int.Parse(result.Tables[0].Rows[0][0].ToString());
        }

        public void Update(Model.Models.QueryBrowser.Query query, int UserID)
        {
            var storedProcedure = new StoredProcedure() { StoredProcedureName = "[App.Sbo].[QueryBrowser.UpdateQuery]" };
            var json = (new JavaScriptSerializer()).Serialize(query);

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@pkQueryId", ParameterType.DBInteger, query.Id));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@fkOwnerId", ParameterType.DBInteger, query.OwnerId));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@QueryName", ParameterType.DBString, query.Name));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@QueryType", ParameterType.DBString, query.QueryType));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@QueryKey", ParameterType.DBString, query.QueryKey));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@QueryJson", ParameterType.DBString, json));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));

            var result = storedProcedure.ExecuteMultipleDataSet();
        }

        public void Delete(int queryId, int UserID)
        {
            var storedProcedure = new StoredProcedure() { StoredProcedureName = "[App.Sbo].[QueryBrowser.DeleteQuery]" };
            StoredProcedureParameter dbParam = new StoredProcedureParameter("@pkQueryId", ParameterType.DBInteger, queryId);
            StoredProcedureParameter dbUserIDParam = new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID);
            storedProcedure.Parameters.Add(dbParam);
            storedProcedure.Parameters.Add(dbUserIDParam);

            storedProcedure.ExecuteMultipleDataSet();
        }

        public IEnumerable<Model.Models.QueryBrowser.Query> QueryList(List<Model.Models.QueryBrowser.Query> systemQueries, int ownerId, int startIndex, int maxRows)
        {
            var list = new List<Model.Models.QueryBrowser.Query>();
            var table = GetQueryList(ownerId);

            if (systemQueries != null)
                list.AddRange(systemQueries);

            if (table != null)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    var row = table.Rows[i];
                    Model.Models.QueryBrowser.Query q = new Model.Models.QueryBrowser.Query
                    {
                        Id = int.Parse(row["pkQueryId"].ToString()),
                        Name = row["QueryName"].ToString(),
                        QueryType = row["QueryType"].ToString(),
                        QueryKey = row["QueryKey"].ToString()
                    };

                    list.Add(q);
                }
            }

            return list
                    .OrderBy(i => i.QueryType)
                    .ThenBy(i => i.Name)
                    .Skip(startIndex)
                    .Take(maxRows)
                    .ToList();
        }

        public int QueryListCount(int ownerId)
        {
            var table = GetQueryList(ownerId);
            return (table != null) ? table.Rows.Count : 0;
        }

        private DataTable GetQueryList(int ownerId)
        {
            var storedProcedure = new StoredProcedure() { StoredProcedureName = "[App.Sbo].[QueryBrowser.GetQueryList]" };

            StoredProcedureParameter dbParam = new StoredProcedureParameter("@ownerId", ParameterType.DBInteger, ownerId);
            storedProcedure.Parameters.Add(dbParam);

            var result = storedProcedure.ExecuteMultipleDataSet();
            if (result == null || result.Tables.Count == 0 || result.Tables[0].Rows.Count == 0)
                return null;
            else
                return result.Tables[0];
        }
    }
}
