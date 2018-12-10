using System;
using System.Data;
using AirportIQ.Domain.Contracts;
using AirportIQ.Data;
using AirportIQ.Data.SqlServer.Repositories;

namespace AirportIQ.Domain.Services
{
	public class ProblemBadgeServices : IProblemBadge
	{
		#region Private Variables

		private readonly IProblemBadgeRepository _ProblemBadgeRepository;

		#endregion

		#region Constructors

		public ProblemBadgeServices() : this(new ProblemBadgeRepository()) { }

		public ProblemBadgeServices(IProblemBadgeRepository problemBadgeRepository)
		{
			if (problemBadgeRepository == null) throw new Exception("problemBadgeRepository is null");
			_ProblemBadgeRepository = problemBadgeRepository;
		}

		#endregion

		#region Public Methods

		public DataSet PersonAndBadges(string badgeNumber)
		{
			return _ProblemBadgeRepository.PersonAndBadges(badgeNumber);
		}
      public DataSet PersonAndAllBadgeInfo(string companyCode, string divisionCode, string SSN, string lastName, string firstName, string BadgeNumber)
      {
         return _ProblemBadgeRepository.PersonAndAllBadgeInfo(companyCode, divisionCode, SSN, lastName, firstName, BadgeNumber);
      }

      public DataTable Locations(int badgeID)
		{
			return _ProblemBadgeRepository.Locations(badgeID);
		}

		public DataTable Doors(int badgeID)
		{
			return _ProblemBadgeRepository.Doors(badgeID);
		}

		public DataTable Readers(int badgeID)
		{
			return _ProblemBadgeRepository.Readers(badgeID);
		}

		public DataTable DoorsByBadgeID_LocationID(int badgeID, int locationID)
		{
			return _ProblemBadgeRepository.DoorsByBadgeID_LocationID(badgeID, locationID);
		}

		public DataTable ReadersByBadgeID_LocationID(int badgeID, int locationID)
		{
			return _ProblemBadgeRepository.ReadersByBadgeID_LocationID(badgeID, locationID);
		}

		public DataTable DoorReaders(int doorID)
		{
			return _ProblemBadgeRepository.DoorReaders(doorID);
		}

		public DataTable ReaderDoor(int readerID)
		{
			return _ProblemBadgeRepository.ReaderDoor(readerID);
		}


		public DataTable DoorsInfoByBadgeID(int badgeID, string accessType = "%")
		{
			return _ProblemBadgeRepository.DoorsInfoByBadgeID(badgeID, accessType);
		}

		public DataTable DoorsInfoByBadgeID_LocationID(int badgeID, int locationID, string accessType = "%")
		{
			return _ProblemBadgeRepository.DoorsInfoByBadgeID_LocationID(badgeID, locationID, accessType);
		}

		public DataTable DoorsInfoByDoorID(int doorID)
		{
			return _ProblemBadgeRepository.DoorsInfoByDoorID(doorID);
		}

		public DataTable DoorInfoByBadgeID_LocationID_DoorID(int badgeID, int locationID, int doorID, string accessType = "%")
		{
			return _ProblemBadgeRepository.DoorInfoByBadgeID_LocationID_DoorID(badgeID, locationID, doorID, accessType);
		}

		public DataTable DoorInfoByBadgeID_LocationID_ReaderID(int badgeID, int locationID, int readerID, string accessType = "%")
		{
			return _ProblemBadgeRepository.DoorInfoByBadgeID_LocationID_ReaderID(badgeID, locationID, readerID, accessType);
		}


		#endregion
	}
}
