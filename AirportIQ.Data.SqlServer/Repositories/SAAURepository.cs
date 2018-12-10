using System;
using System.Configuration;
using System.Data;
using AirportIQ.Data;
using AirportIQ.Data.SqlServer.Initializers;

namespace AirportIQ.Data.SqlServer.Repositories
{
	public class SAAURepository : ISAAURepository
	{
		#region Private Variables

		private readonly string _Schema = ConfigurationManager.AppSettings["SBO.ApplicationSchema"];

		#endregion Private Variables

		#region Public Methods

		#region Acceptance
		// [SAAU.AdditionalAccessRequests.Acceptance.Load]
		public DataSet AARA_Load(int workID)
		{
			DataSet result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[SAAU.AdditionalAccessRequests.Acceptance.Load]" };

			StoredProcedureParameter paramWorkID = new StoredProcedureParameter("@WorkID", ParameterType.DBInteger, workID);
			storedProcedure.Parameters.Add(paramWorkID);

			result = storedProcedure.ExecuteMultipleDataSet();

			return result;
		}

		public void AARA_Assign(int staffID, int workID, int UserID)
		{
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[SAAU.AdditionalAccessRequests.Acceptance.Assign]" };
						
			StoredProcedureParameter paramStaffID = new StoredProcedureParameter("@StaffID", ParameterType.DBInteger, staffID);
			StoredProcedureParameter paramWorkID = new StoredProcedureParameter("@WorkID", ParameterType.DBInteger, workID);
            StoredProcedureParameter paramUserID = new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID);

			storedProcedure.Parameters.Add(paramStaffID);
			storedProcedure.Parameters.Add(paramWorkID);

			storedProcedure.ExecuteDataSet();			
		}


		public void AARA_Decline(string reason, int workID, int UserID)
		{
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[SAAU.AdditionalAccessRequests.Acceptance.Decline]" };
						
			StoredProcedureParameter paramReason = new StoredProcedureParameter("@Reason", ParameterType.DBString, reason);
			StoredProcedureParameter paramWorkID = new StoredProcedureParameter("@WorkID", ParameterType.DBInteger, workID);
            StoredProcedureParameter paramUserID = new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID);

			storedProcedure.Parameters.Add(paramReason);
			storedProcedure.Parameters.Add(paramWorkID);

			storedProcedure.ExecuteDataSet();			

		}

		

		#endregion

		#region Processing

		public DataSet AARP_Load(int workID)
		{
			DataSet result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[SAAU.AdditionalAccessRequests.Processing.Load]" };

			StoredProcedureParameter paramWorkID = new StoredProcedureParameter("@WorkID", ParameterType.DBInteger, workID);
			storedProcedure.Parameters.Add(paramWorkID);

			result = storedProcedure.ExecuteMultipleDataSet();

			return result;
		}

		public DataSet AARP_Alert(int workID, int UserID)
		{
			DataSet result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[SAAU.AdditionalAccessRequests.Processing.Alert]" };

			StoredProcedureParameter paramWorkID = new StoredProcedureParameter("@WorkID", ParameterType.DBInteger, workID);
            StoredProcedureParameter paramUserID = new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID);
			storedProcedure.Parameters.Add(paramWorkID);
            storedProcedure.Parameters.Add(paramUserID);

			result = storedProcedure.ExecuteMultipleDataSet();

			return result;
		}

		public DataSet AARP_Commit(int workID, int UserID)
		{
			DataSet result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[SAAU.AdditionalAccessRequests.Processing.Commit]" };

			StoredProcedureParameter paramWorkID = new StoredProcedureParameter("@WorkID", ParameterType.DBInteger, workID);
            StoredProcedureParameter paramUserID = new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID);
			storedProcedure.Parameters.Add(paramWorkID);
            storedProcedure.Parameters.Add(paramUserID);

			result = storedProcedure.ExecuteMultipleDataSet();

			return result;
		}	

		#endregion

		#region Approval

		public DataSet AR_Load(int workID)
		{
			DataSet result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[SAAU.AccessRequest.Load]" };

			StoredProcedureParameter paramWorkID = new StoredProcedureParameter("@WorkID", ParameterType.DBInteger, workID);
			storedProcedure.Parameters.Add(paramWorkID);

			result = storedProcedure.ExecuteMultipleDataSet();

			return result;
		}

		public DataTable SAAU_ListDoorCategories(int doorID, int UserID)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[SAAU.AdditionalAccessRequests.Processing.Commit]" };

			StoredProcedureParameter paramDoorID = new StoredProcedureParameter("@DoorID", ParameterType.DBInteger, doorID);
            StoredProcedureParameter paramUserID = new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID);
			storedProcedure.Parameters.Add(paramDoorID);
            storedProcedure.Parameters.Add(paramUserID);

			result = storedProcedure.ExecuteDataSet();

			return result;
		}


		public DataTable SAAU_ListCategoryDoors(int categoryID)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[SAAU.List.Category.Doors]" };

			StoredProcedureParameter paramCategoryID = new StoredProcedureParameter("@CategoryID", ParameterType.DBInteger, categoryID);
			storedProcedure.Parameters.Add(paramCategoryID);

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

		#endregion
		


		public DataTable DivisionRequestsLoad()
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[SAAU.DivisionRequests.Load]" };

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

		public DataSet RequestsLoad(int divisionRequestID)
		{
			DataSet result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[SAAU.DivisionRequests.Load]" };

			StoredProcedureParameter paramDivisionRequestID = new StoredProcedureParameter("@DivisionRequestID", ParameterType.DBInteger, divisionRequestID);
			storedProcedure.Parameters.Add(paramDivisionRequestID);

			result = storedProcedure.ExecuteMultipleDataSet();

			return result;
		}


		#region Problem Badges

		public DataTable ProblemBadgeLoad(int personID)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[SAAU.ProblemBadge.Load]" };

			StoredProcedureParameter sppPersonID = new StoredProcedureParameter("@PersonID", ParameterType.DBInteger, personID);
			storedProcedure.Parameters.Add(sppPersonID);

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

		public DataTable GetBadgeCategories(int badgeID)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[SAAU.Badge.Categories]" };

			StoredProcedureParameter sppBadgeID = new StoredProcedureParameter("@BadgeID", ParameterType.DBInteger, badgeID);
			storedProcedure.Parameters.Add(sppBadgeID);

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

		public DataTable GetPersonID_ByBadgeNumber(string badgeNumber)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[SAAU.ProblemBadge.GetPersionID]" };

			StoredProcedureParameter sppBadgeNumber = new StoredProcedureParameter("@BadgeNumber", ParameterType.DBInteger, badgeNumber);
			storedProcedure.Parameters.Add(sppBadgeNumber);

			result = storedProcedure.ExecuteDataSet();
			
			return result;
		}

		#endregion

		public DataTable GetLocationsForBadge(int badgeID, bool activeOnly = true)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[SAAU.GetLocationsForBadge]" };

			StoredProcedureParameter sppBadgeID = new StoredProcedureParameter("@BadgeID", ParameterType.DBInteger, badgeID);
			StoredProcedureParameter sppActiveOnly = new StoredProcedureParameter("@ActiveOnly", ParameterType.DBBoolean, activeOnly);
			
			storedProcedure.Parameters.Add(sppBadgeID);
			storedProcedure.Parameters.Add(sppActiveOnly);

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

		public DataTable CategoriesByBadgeID(int badgeID, bool activeOnly = true, string accessType = "%")
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[SAAU.CategoriesByBadgeID]" };

			StoredProcedureParameter sppBadgeID = new StoredProcedureParameter("@BadgeID", ParameterType.DBInteger, badgeID);
			StoredProcedureParameter sppActiveOnly = new StoredProcedureParameter("@ActiveOnly", ParameterType.DBBoolean, activeOnly);
			StoredProcedureParameter sppAccessType = new StoredProcedureParameter("@AccessType", ParameterType.DBSingle, accessType);
			
			storedProcedure.Parameters.Add(sppBadgeID);
			storedProcedure.Parameters.Add(sppActiveOnly);
			storedProcedure.Parameters.Add(sppAccessType);

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

		public DataTable BadgeChanges(int badgeID)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[SAAU.Badge.Changes]" };

			StoredProcedureParameter sppBadgeID = new StoredProcedureParameter("@BadgeID", ParameterType.DBInteger, badgeID);			
			storedProcedure.Parameters.Add(sppBadgeID);			

			result = storedProcedure.ExecuteDataSet();

			return result;
		}


		#endregion

		
	}
}
