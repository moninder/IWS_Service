using System;
using System.Data;

namespace AirportIQ.Data
{
	public interface IProblemBadgeRepository
	{
		DataSet PersonAndBadges(string badgeNumber);

      DataSet PersonAndAllBadgeInfo(string companyCode, string divisionCode, string SSN, string lastName, string firstName, string BadgeNumber);
      DataTable Locations(int badgeID);

		DataTable Doors(int badgeID);
		DataTable Readers(int badgeID);
		DataTable DoorsByBadgeID_LocationID(int badgeID, int locationID);
		DataTable ReadersByBadgeID_LocationID(int badgeID, int locationID);
		DataTable DoorReaders(int doorID);
		DataTable ReaderDoor(int readerID);

		DataTable DoorsInfoByBadgeID(int badgeID, string accessType = "%");
		DataTable DoorsInfoByBadgeID_LocationID(int badgeID, int locationID, string accessType = "%");
		DataTable DoorsInfoByDoorID(int doorID);
		DataTable DoorInfoByBadgeID_LocationID_DoorID(int badgeID, int locationID, int doorID, string accessType = "%");
		DataTable DoorInfoByBadgeID_LocationID_ReaderID(int badgeID, int locationID, int readerID, string accessType = "%");
	}
}
