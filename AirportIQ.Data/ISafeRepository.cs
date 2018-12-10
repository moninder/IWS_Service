using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AirportIQ.Data
{
	public interface ISafeRepository
	{
		DataSet GetCitationTotals(int personID, int userID);
		DataSet GetCitationsForPreviousNMonths(int personID, int userID, int numberOfMonths);
		DataSet GetCitationDetails(int citationID, int userID);
		DataSet LoadSAFE(Int32 userID, Int32 personID, Int32? citationID);
	}
}
