using System;
using System.Data;

namespace AirportIQ.Data
{
	public interface IPersonRepository
	{
		DataSet loadPersonForm(Int16 PersonID);
		DataTable GetPersonLookupList(int userID);
        DataTable LoadNotes(int personId, int divisionId);
	}
}