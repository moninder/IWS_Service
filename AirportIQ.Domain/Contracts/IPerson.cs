using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AirportIQ.Domain.Contracts
{
	public interface IPerson
	{
		DataSet loadPersonForm(Int16 PersonID);
		DataTable GetPersonLookupList(int userID);
        DataTable LoadNotes(int personId, int divisionId);
	}
}