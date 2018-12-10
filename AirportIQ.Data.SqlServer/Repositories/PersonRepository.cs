using System.Configuration;
using System.Data;
using AirportIQ.Data.SqlServer.Initializers;

namespace AirportIQ.Data.SqlServer.Repositories
{
	public class PersonRepository : IPersonRepository
	{
		#region Private Variables

		private string schema = ConfigurationManager.AppSettings["ReferenceTableSchema"].ToString();

		#endregion Private Variables

		public DataSet loadPersonForm(short PersonID)
		{
			DataSet ret = null;
			var storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = schema + ".[Data.PersonForm.Load]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@personID", ParameterType.DBString, PersonID));
			ret = storedProcedure.ExecuteMultipleDataSet();
			return ret;
		}

		public DataTable GetPersonLookupList(int userID)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure { StoredProcedureName = schema + ".[Data.PersonForm.Load]" };
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID));
			result = storedProcedure.ExecuteDataSet();
			return result;
		}

        public DataTable LoadNotes(int personId, int divisionId)
        {
            var storedProcedure = new StoredProcedure();

            storedProcedure.StoredProcedureName = schema + ".[Data.Person.Notes]";

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@PersonID", ParameterType.DBInteger, personId));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@DivisionID", ParameterType.DBInteger, divisionId));

            return storedProcedure.ExecuteDataSet();
        }
	}
}