using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace AirportIQ.Domain.Contracts
{
	public interface ILists
	{
        DataTable PopulateNameListReport(int UserID, DataTable NameListEntryIDs);
        DataTable NameListOnDemandSearch(DataTable nameList);
		DataTable PopulatePersonLookupList(string facilityCode);
		DataTable PopulateBadgeLookupList(string facilityCode);
    DataTable PopulateCitaitonLookupList();
	}


}
