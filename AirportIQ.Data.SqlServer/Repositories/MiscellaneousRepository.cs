using System;
using System.Configuration;
using System.Data;
using AirportIQ.Data;
using AirportIQ.Data.SqlServer.Initializers;

namespace AirportIQ.Data.SqlServer.Repositories
{
	public class MiscellaneousRepository : IMiscellaneousRepository
	{
		#region Private Variables

		private readonly string _Schema = ConfigurationManager.AppSettings["SBO.ApplicationSchema"];

		#endregion Private Variables

		#region Public Methods

		public DataTable LoadCountries()
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[Miscellaneous.Countries.Load]" };
			
			result = storedProcedure.ExecuteDataSet();

			return result;
		}

		public DataTable LoadStates(string countryCode)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[Miscellaneous.States.Load]" };
			var paramCountryCode = new StoredProcedureParameter("@CountryCode", ParameterType.DBString, countryCode);
			storedProcedure.Parameters.Add(paramCountryCode);

			result = storedProcedure.ExecuteDataSet();

			return result;
		}


		public DataTable LoadContactTypes()
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[Miscellaneous.ContactTypes.Load]" };

			result = storedProcedure.ExecuteDataSet();

			return result;
		}
		#endregion

	}
}
