using System;
using System.Configuration;
using System.Data;
using AirportIQ.Data;
using AirportIQ.Data.SqlServer.Initializers;

namespace AirportIQ.Data.SqlServer.Repositories
{
	public class SafeRepository: ISafeRepository
	{
		#region Private Variables

		private string schema = ConfigurationManager.AppSettings["SBO.ApplicationSchema"].ToString();

		#endregion Private Variables


		#region Public Methods
		public DataSet GetCitationsForPreviousNMonths(int personID, int userID, int numberOfMonths)
		{
			DataSet result = null;
			var storedProcedure = new StoredProcedure { StoredProcedureName = schema + ".[Review.Badge.CitationTotals]" };

			storedProcedure.Parameters.Add(new StoredProcedureParameter("@PersonID", ParameterType.DBInteger, personID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@NumberOfMonths", ParameterType.DBInteger, numberOfMonths));

			result = storedProcedure.ExecuteMultipleDataSet();
			return result;
		}

		public DataSet GetCitationTotals(int personID, int userID)
		{
			DataSet result = null;
			var storedProcedure = new StoredProcedure { StoredProcedureName = schema + ".[Review.Badge.Citations]" };

			storedProcedure.Parameters.Add(new StoredProcedureParameter("@PersonID", ParameterType.DBInteger, personID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID));

			result = storedProcedure.ExecuteMultipleDataSet();
			return result;

		}

		public DataSet GetCitationDetails(int citationID, int userID)
		{
			DataSet result = null;
			var storedProcedure = new StoredProcedure { StoredProcedureName = schema + ".[Review.Bages.CitationDetails]" };

			storedProcedure.Parameters.Add(new StoredProcedureParameter("@CitationID", ParameterType.DBInteger, citationID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID));

			result = storedProcedure.ExecuteMultipleDataSet();
			return result;
		}



		public DataSet LoadSAFE(Int32 userID, Int32 personID, Int32? citationID)
		{
			DataSet result = null;
			var storedProcedure = new StoredProcedure { StoredProcedureName = schema + ".[Data.SAFEInformation.Load]" };

			storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBInteger, userID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@personID", ParameterType.DBInteger, personID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@citationID", ParameterType.DBInteger, citationID));

			result = storedProcedure.ExecuteMultipleDataSet();
			return result;
		}


		#endregion
	}
}
