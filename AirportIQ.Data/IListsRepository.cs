using System;
using System.Data;

namespace AirportIQ.Data
{
	public interface IListsRepository
	{
        DataTable PopulateNameListReport(int UserID, DataTable NameListEntryIDs); 
        DataTable NameListOnDemandSearch(DataTable nameList);
		DataTable PopulatePersonLookupList(string facilityCode);
		DataTable PopulateBadgeLookupList(string facilityCode);
		DataTable PopulateCitaitonLookupList();
	}
}
