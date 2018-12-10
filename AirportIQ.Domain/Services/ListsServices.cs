using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;

using AirportIQ.Data;
using AirportIQ.Domain.Contracts;
using AirportIQ.Data.SqlServer.Repositories;


namespace AirportIQ.Domain.Services
{
	public class ListsServices : ILists
	{

		#region "Private Variables"

		private readonly IListsRepository _ListsRepository;

		#endregion "Private Variables"

		#region "Constructors"

		public ListsServices() : this(new ListsRepository()) { }

		public ListsServices(IListsRepository listsRepository)
		{
			if (listsRepository == null) throw new ArgumentNullException("ListsRepository");
			this._ListsRepository = listsRepository;
		}

		#endregion "Constructors"
		
		public DataTable NameListOnDemandSearch(DataTable nameList)
		{
			return _ListsRepository.NameListOnDemandSearch(nameList);
		}

        public DataTable PopulateNameListReport(int UserID, DataTable NameListEntryIDs)
        {
            return _ListsRepository.PopulateNameListReport(UserID, NameListEntryIDs);
        }
		
        public DataTable PopulatePersonLookupList(string facilityCode)
		{
			return _ListsRepository.PopulatePersonLookupList(facilityCode);
		}

		public DataTable PopulateBadgeLookupList(string facilityCode)
		{
			return _ListsRepository.PopulateBadgeLookupList(facilityCode);
		}

		public DataTable PopulateCitaitonLookupList()
    {
        return _ListsRepository.PopulateCitaitonLookupList();
    }

	}
	
}
