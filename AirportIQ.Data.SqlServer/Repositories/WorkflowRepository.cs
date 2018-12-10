using System;
using System.Configuration;
using System.Data;
using AirportIQ.Data;
using AirportIQ.Data.SqlServer.Initializers;

namespace AirportIQ.Data.SqlServer.Repositories
{
	public class WorkflowRepository : IWorkflowRepository
	{
		#region Private Variables

		private readonly string _Schema = ConfigurationManager.AppSettings["SBO.ApplicationSchema"];		

		#endregion Private Variables


		#region Public Methods

		public int GetWorkID(int iD, int facilityWorkflowID)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[Workflow.GetWorkID]" };

			StoredProcedureParameter paramID = new StoredProcedureParameter("@ID", ParameterType.DBInteger, iD);
			StoredProcedureParameter paramFacilityWorkflowID = new StoredProcedureParameter("@FacilityWorkflowID", ParameterType.DBInteger, facilityWorkflowID);

			storedProcedure.Parameters.Add(paramID);
			storedProcedure.Parameters.Add(paramFacilityWorkflowID);

			result = storedProcedure.ExecuteDataSet();

			return (int)result.Rows[0][0];
		}


		#endregion
		
	}
}
