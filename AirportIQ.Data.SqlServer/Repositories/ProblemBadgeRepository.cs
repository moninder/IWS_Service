using System;
using System.Configuration;
using System.Data;
using AirportIQ.Data;
using AirportIQ.Data.SqlServer.Initializers;

namespace AirportIQ.Data.SqlServer.Repositories 
{
	public class ProblemBadgeRepository : IProblemBadgeRepository
	{
		#region Private Variables

		private readonly string _Schema = ConfigurationManager.AppSettings["SBO.ApplicationSchema"];

		#endregion Private Variables

		public DataSet PersonAndBadges(string badgeNumber)
		{
			DataSet result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[SAAU.ProblemBadge.PersonAndBadges]" };

			StoredProcedureParameter paramBadgeNumber = new StoredProcedureParameter("@BadgeNumber", ParameterType.DBString, badgeNumber);
			storedProcedure.Parameters.Add(paramBadgeNumber);

			result = storedProcedure.ExecuteMultipleDataSet();

			return result;
		}
      //This method replaces PersonAndBadges for badge and person retirieval for Problem Badge
      public DataSet PersonAndAllBadgeInfo(string companyCode, string divisionCode, string SSN, string lastName, string firstName,  string BadgeNumber)
      {
         DataSet result = null;
         var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[SAAU.ProblemBadge.PersonAndAllBadgeInfo]" };

         StoredProcedureParameter paramBadgeNumber = new StoredProcedureParameter("@BadgeNumber", ParameterType.DBString, BadgeNumber);
         storedProcedure.Parameters.Add(paramBadgeNumber);

         storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyCode", ParameterType.DBString, companyCode));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionCode", ParameterType.DBString, divisionCode));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@SSN", ParameterType.DBString, SSN));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@LastName", ParameterType.DBString, lastName));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@FirstName", ParameterType.DBString, firstName));

         result = storedProcedure.ExecuteMultipleDataSet();

         return result;
      }

      public DataTable Locations(int badgeID)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[SAAU.ProblemBadge.Locations]" };

			StoredProcedureParameter paramBadgeID = new StoredProcedureParameter("@BadgeID", ParameterType.DBInteger, badgeID);
			storedProcedure.Parameters.Add(paramBadgeID);

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

		public DataTable Doors(int badgeID)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[SAAU.ProblemBadge.Doors]" };

			StoredProcedureParameter paramBadgeID = new StoredProcedureParameter("@BadgeID", ParameterType.DBInteger, badgeID);
			storedProcedure.Parameters.Add(paramBadgeID);

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

		public DataTable Readers(int badgeID)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[SAAU.ProblemBadge.Readers]" };

			StoredProcedureParameter paramBadgeID = new StoredProcedureParameter("@BadgeID", ParameterType.DBInteger, badgeID);
			storedProcedure.Parameters.Add(paramBadgeID);

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

		public DataTable DoorsByBadgeID_LocationID(int badgeID, int locationID)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[SAAU.ProblemBadge.DoorsByBadgeID_LocationID]" };

			StoredProcedureParameter paramBadgeID = new StoredProcedureParameter("@BadgeID", ParameterType.DBInteger, badgeID);
			storedProcedure.Parameters.Add(paramBadgeID);

			StoredProcedureParameter paramLocationID = new StoredProcedureParameter("@LocationID", ParameterType.DBInteger, locationID);
			storedProcedure.Parameters.Add(paramLocationID);

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

		public DataTable ReadersByBadgeID_LocationID(int badgeID, int locationID)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[SAAU.ProblemBadge.ReadersByBadgeID_LocationID]" };

			StoredProcedureParameter paramBadgeID = new StoredProcedureParameter("@BadgeID", ParameterType.DBInteger, badgeID);
			storedProcedure.Parameters.Add(paramBadgeID);

			StoredProcedureParameter paramLocationID = new StoredProcedureParameter("@LocationID", ParameterType.DBInteger, locationID);
			storedProcedure.Parameters.Add(paramLocationID);

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

		public DataTable DoorReaders(int doorID)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[SAAU.ProblemBadge.DoorReaders]" };

			StoredProcedureParameter paramDoorID = new StoredProcedureParameter("@DoorID", ParameterType.DBInteger, doorID);
			storedProcedure.Parameters.Add(paramDoorID);

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

		public DataTable ReaderDoor(int readerID)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[SAAU.ProblemBadge.Locations]" };

			StoredProcedureParameter paramReaderID = new StoredProcedureParameter("@ReaderID", ParameterType.DBInteger, readerID);
			storedProcedure.Parameters.Add(paramReaderID);

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

		public DataTable DoorsInfoByBadgeID(int badgeID, string accessType = "%")
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[SAAU.ProblemBadge.DoorsInfoByBadgeID]" };

			StoredProcedureParameter paramBadgeID = new StoredProcedureParameter("@BadgeID", ParameterType.DBInteger, badgeID);
			storedProcedure.Parameters.Add(paramBadgeID);
			StoredProcedureParameter paramAccessType = new StoredProcedureParameter("@AccessType", ParameterType.DBString, accessType);
			storedProcedure.Parameters.Add(paramAccessType);

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

		public DataTable DoorsInfoByBadgeID_LocationID(int badgeID, int locationID, string accessType = "%")
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[SAAU.ProblemBadge.DoorsInfoByBadgeID_LocationID]" };

			StoredProcedureParameter paramBadgeID = new StoredProcedureParameter("@BadgeID", ParameterType.DBInteger, badgeID);
			storedProcedure.Parameters.Add(paramBadgeID);

			StoredProcedureParameter paramLocationID = new StoredProcedureParameter("@LocationID", ParameterType.DBInteger, locationID);
			storedProcedure.Parameters.Add(paramLocationID);

			StoredProcedureParameter paramAccessType = new StoredProcedureParameter("@AccessType", ParameterType.DBString, accessType);
			storedProcedure.Parameters.Add(paramAccessType);

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

		public DataTable DoorsInfoByDoorID(int doorID)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[SAAU.ProblemBadge.DoorInfoByDoorID]" };

			StoredProcedureParameter paramDoorID = new StoredProcedureParameter("@DoorID", ParameterType.DBInteger, doorID);
			storedProcedure.Parameters.Add(paramDoorID);

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

		public DataTable DoorInfoByBadgeID_LocationID_DoorID(int badgeID, int locationID, int doorID, string accessType = "%")
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[SAAU.ProblemBadge.DoorInfoByBadgeID_LocationID_DoorID]" };

			StoredProcedureParameter paramBadgeID = new StoredProcedureParameter("@BadgeID", ParameterType.DBInteger, badgeID);
			storedProcedure.Parameters.Add(paramBadgeID);

			StoredProcedureParameter paramLocationID = new StoredProcedureParameter("@LocationID", ParameterType.DBInteger, locationID);
			storedProcedure.Parameters.Add(paramLocationID);

			StoredProcedureParameter paramDoorID = new StoredProcedureParameter("@DoorID", ParameterType.DBInteger, doorID);
			storedProcedure.Parameters.Add(paramDoorID);

			StoredProcedureParameter paramAccessType = new StoredProcedureParameter("@AccessType", ParameterType.DBString, accessType);
			storedProcedure.Parameters.Add(paramAccessType);

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

		public DataTable DoorInfoByBadgeID_LocationID_ReaderID(int badgeID, int locationID, int readerID, string accessType = "%")
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[SAAU.ProblemBadge.DoorInfoByBadgeID_LocationID_ReaderID]" };

			StoredProcedureParameter paramBadgeID = new StoredProcedureParameter("@BadgeID", ParameterType.DBInteger, badgeID);
			storedProcedure.Parameters.Add(paramBadgeID);

			StoredProcedureParameter paramLocationID = new StoredProcedureParameter("@LocationID", ParameterType.DBInteger, locationID);
			storedProcedure.Parameters.Add(paramLocationID);

			StoredProcedureParameter paramReaderID = new StoredProcedureParameter("@ReaderID", ParameterType.DBInteger, readerID);
			storedProcedure.Parameters.Add(paramReaderID);

			StoredProcedureParameter paramAccessType = new StoredProcedureParameter("@AccessType", ParameterType.DBString, accessType);
			storedProcedure.Parameters.Add(paramAccessType);

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

	}
}
