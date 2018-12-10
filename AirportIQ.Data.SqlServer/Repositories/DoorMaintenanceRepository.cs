using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AirportIQ.Data;
using AirportIQ.Data.SqlServer.Initializers;
using System.Configuration;

namespace AirportIQ.Data.SqlServer.Repositories
{
    /// <summary>
    /// The Door Maintenance Repository
    /// </summary>
    public class DoorMaintenanceRepository : IDoorMaintenanceRepository
    {
        private readonly string Schema = ConfigurationManager.AppSettings["SBO.ApplicationSchema"];

        #region Load
        public DataSet Load(int userId, int? locationId, int? departmentId)
        {
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = Schema + ".[DoorMaintenance.Load]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBInteger, userId));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@locationId", ParameterType.DBInteger, (object)locationId ?? DBNull.Value));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@deptId", ParameterType.DBInteger, (object)departmentId ?? DBNull.Value));
            return storedProcedure.ExecuteMultipleDataSet();
        }
        #endregion

        #region Save
        public void Save(int doorId, int departmentId, int tsaMaximumBadgeCount)
        {
            var storedProcedure = new StoredProcedure();

            storedProcedure.StoredProcedureName = Schema + ".[DoorMaintenance.Save]";

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@doorId", ParameterType.DBInteger, doorId));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@TSAMaximumBadgeCount", ParameterType.DBInteger, tsaMaximumBadgeCount));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@deptId", ParameterType.DBInteger, departmentId));

            storedProcedure.ExecuteNonQuery();
        }
        #endregion

        #region GetMaximumBadgeCount
        public int GetMaximumBadgeCount(int doorID)
        {
            var storedProcedure = new StoredProcedure();

            storedProcedure.StoredProcedureName = Schema + ".[DoorMaintenance.GetMaximumBadgeCount]";

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@doorId", ParameterType.DBInteger, doorID));

            int badgeCount = int.Parse(storedProcedure.ExecuteDataSet().Rows[0][0].ToString());

            return badgeCount;
        }
        #endregion
    }
}
