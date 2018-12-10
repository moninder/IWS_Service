using System;
using System.Data;

namespace AirportIQ.Domain.Contracts
{
	public interface ISAAU
	{
		#region Acceptance
		
		// [SAAU.AdditionalAccessRequests.Acceptance.Load]
		DataSet AARA_Load(int workID);
		void AARA_Assign(int staffID, int workID, int UserID);
		void AARA_Decline(string reason, int workID, int UserID);
		
		#endregion

		#region Processing

		DataSet AARP_Load(int workID);
		DataSet AARP_Alert(int workID, int UserID);
		DataSet AARP_Commit(int workID, int UserID);				

		#endregion


		#region Approval

		DataSet AR_Load(int workID);
		DataTable SAAU_ListDoorCategories(int doorID, int UserID);
		DataTable SAAU_ListCategoryDoors(int categoryID);
		

		#endregion

		DataTable DivisionRequestsLoad();
		DataSet RequestsLoad(int divisionRequestID);

		#region Problem Badge

		DataTable ProblemBadgeLoad(int personID);

		DataTable GetBadgeCategories(int badgeID);

		DataTable GetLocationsForBadge(int badgeID, bool activeOnly = true);

		DataTable CategoriesByBadgeID(int badgeID, bool activeOnly = true, string accessType = "%");

		DataTable BadgeChanges(int badgeID);

		DataTable GetPersonID_ByBadgeNumber(string badgeNumber);

		#endregion

	}
}
