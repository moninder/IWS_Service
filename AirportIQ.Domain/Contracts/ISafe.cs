using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace AirportIQ.Domain.Contracts
{
	public interface ISafe
	{
		DataSet GetCitationTotals(int personID, int userID);
		DataSet GetCitationsForPreviousNMonths(int personID, int userID, int numberOfMonths);
		DataSet GetCitationDetails(int citationID, int userID);
		DataSet LoadSAFE(Int32 userID, Int32 personID, Int32? citationID);
	}
}
