using System;
using System.Data;

using AirportIQ.Data;
using AirportIQ.Data.SqlServer.Repositories;
using AirportIQ.Domain.Contracts;

namespace AirportIQ.Domain.Services
{
	public class SAAUServices : ISAAU
	{
		#region "Private Variables"

		private readonly ISAAURepository _SAAURepository;

		#endregion "Private Variables"


		#region "Constructors"

		public SAAUServices() : this(new SAAURepository()) { }

		public SAAUServices(ISAAURepository sAAURepository)
		{
			if (sAAURepository == null) throw new ArgumentNullException("SAAURepository");
			this._SAAURepository = sAAURepository;
		}

		#endregion "Constructors"

		#region Public Methods
		#region Acceptance
		// [SAAU.AdditionalAccessRequests.Acceptance.Load]
		public DataSet AARA_Load(int workID)
		{
			return this._SAAURepository.AARA_Load(workID);
		}

		public void AARA_Assign(int staffID, int workID, int UserID)
		{
			this._SAAURepository.AARA_Assign(staffID, workID, UserID);
		}


		public void AARA_Decline(string reason, int workID, int UserID)
		{
			this._SAAURepository.AARA_Decline(reason, workID, UserID);

		}

		#endregion

		#region Processing

		public DataSet AARP_Load(int workID)
		{
			return this._SAAURepository.AARP_Load(workID);
		}

		public DataSet AARP_Alert(int workID, int UserID)
		{
			return this._SAAURepository.AARP_Alert(workID, UserID);
		}

		public DataSet AARP_Commit(int workID, int UserID)
		{
			return this._SAAURepository.AARP_Commit(workID, UserID);
		}		

		#endregion


		#region Approval

		public DataSet AR_Load(int workID)
		{
			return this._SAAURepository.AR_Load(workID);
		}

		public DataTable SAAU_ListDoorCategories(int doorID, int UserID)
		{
			return this._SAAURepository.SAAU_ListDoorCategories(doorID, UserID);
		}

		public DataTable SAAU_ListCategoryDoors(int categoryID)
		{
			return this._SAAURepository.SAAU_ListCategoryDoors(categoryID);
		}

		#endregion
		

		public DataTable DivisionRequestsLoad()
		{
			return this._SAAURepository.DivisionRequestsLoad();
		}

		public DataSet RequestsLoad(int divisionRequestID)
		{
			return this._SAAURepository.RequestsLoad(divisionRequestID);
		}

		#region Problem Badge

		public DataTable ProblemBadgeLoad(int personID)
		{
			return this._SAAURepository.ProblemBadgeLoad(personID);
		}

		public DataTable GetBadgeCategories(int badgeID)
		{
			return this._SAAURepository.GetBadgeCategories(badgeID);
		}

		public DataTable GetPersonID_ByBadgeNumber(string badgeNumber)
		{
			return this._SAAURepository.GetPersonID_ByBadgeNumber(badgeNumber);
		}

		#endregion

		public DataTable GetLocationsForBadge(int badgeID, bool activeOnly = true)
		{
			return this._SAAURepository.GetLocationsForBadge(badgeID, activeOnly);
		}

		public DataTable CategoriesByBadgeID(int badgeID, bool activeOnly = true, string accessType = "%")
		{
			return this._SAAURepository.CategoriesByBadgeID(badgeID, activeOnly, accessType);
		}

		public DataTable BadgeChanges(int badgeID)
		{
			return this._SAAURepository.BadgeChanges(badgeID);

		}



		#endregion
	}	
}

