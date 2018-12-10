using System;
using System.Configuration;
using System.Data;
using AirportIQ.Data;
using AirportIQ.Data.SqlServer.Initializers;

namespace AirportIQ.Data.SqlServer.Repositories
{
	public class WorkQueueRepository : IWorkQueueRepository
	{
		#region Private Variables

		private readonly string _Schema = ConfigurationManager.AppSettings["SBO.ApplicationSchema"];

		#endregion Private Variables

		public DataSet WorkQueueLoad(int userID)
		{
			DataSet result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[WorkQueue.Load]" };
			StoredProcedureParameter paramUserID = new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID);			

			storedProcedure.Parameters.Add(paramUserID);
			
			result = storedProcedure.ExecuteMultipleDataSet();
			return result;
		}
	}
}
