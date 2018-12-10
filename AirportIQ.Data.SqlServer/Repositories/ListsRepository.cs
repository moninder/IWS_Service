using System;
using System.Configuration;
using System.Data;
using AirportIQ.Data;
using AirportIQ.Data.SqlServer.Initializers;

namespace AirportIQ.Data.SqlServer.Repositories
{
	public class ListsRepository : IListsRepository
	{
		#region Private Variables

		private readonly string _Schema = ConfigurationManager.AppSettings["SBO.ApplicationSchema"];

		#endregion Private Variables

		#region Public Methods

		public DataTable NameListOnDemandSearch(DataTable nameList)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[NameList.OnDemandSearch]" };

			StoredProcedureParameter paramNameList = new StoredProcedureParameter("@NameList", ParameterType.Structured, nameList);

			storedProcedure.Parameters.Add(paramNameList);

			result = storedProcedure.ExecuteDataSet();
			return result;
		}

		public DataTable PopulatePersonLookupList(string facilityCode)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[Lookup.Lists.Person]" };

			var paramNameList = new StoredProcedureParameter("@FacilityCode", ParameterType.DBString, facilityCode);

			storedProcedure.Parameters.Add(paramNameList);

			result = storedProcedure.ExecuteDataSet();
			return result;
		}

		public DataTable PopulateBadgeLookupList(string facilityCode)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[Lookup.Lists.Badges]" };

			var paramNameList = new StoredProcedureParameter("@FacilityCode", ParameterType.DBString, facilityCode);

			storedProcedure.Parameters.Add(paramNameList);

			result = storedProcedure.ExecuteDataSet();
			return result;
		}

		public DataTable PopulateCitaitonLookupList()
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[Lookup.Lists.Citations]" };
			result = storedProcedure.ExecuteDataSet();
			return result;
		}

        public DataTable PopulateNameListReport(int UserID, DataTable NameListEntryIDs)
        {
            DataTable result = null;
            var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[NameList.NameListOnDemandMatchReport]" };

            var paramUserID = new StoredProcedureParameter()
            {
                Name = "@UserID",
                DBValueType = ParameterType.DBInteger,
                Value = UserID
            };
            storedProcedure.Parameters.Add(paramUserID);

            var paramNameList = new StoredProcedureParameter
            {
                Name = "@NameListMatches",
                DBValueType = ParameterType.Structured,
                Value = NameListEntryIDs
            };
            storedProcedure.Parameters.Add(paramNameList);

            result = storedProcedure.ExecuteDataSet();
            return result;
        }

		#endregion Public Methods
	}
}