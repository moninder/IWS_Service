using System;
using System.Data;

using AirportIQ.Data;
using AirportIQ.Data.SqlServer.Repositories;
using AirportIQ.Domain.Contracts;

namespace AirportIQ.Domain.Services
{
	public class SafeServices: ISafe
	{

		#region "Private Variables"

		private readonly ISafeRepository _SafeRepository;

		#endregion "Private Variables"

		#region "Constructors"

		public SafeServices() : this(new SafeRepository()) { }

		public SafeServices(ISafeRepository safeRepository)
		{
			if (safeRepository == null) throw new ArgumentNullException("SafeRepository");
			_SafeRepository = safeRepository;
		}

		#endregion "Constructors"


		public DataSet GetCitationTotals(int personID, int userID)
		{
			return _SafeRepository.GetCitationTotals(personID, userID);
		}
		public DataSet GetCitationsForPreviousNMonths(int personID, int userID, int numberOfMonths)
		{
			return _SafeRepository.GetCitationsForPreviousNMonths(personID, userID, numberOfMonths);
		}

		public DataSet GetCitationDetails(int citationID, int userID)
		{
			return _SafeRepository.GetCitationDetails(citationID, userID);
		}


		public DataSet LoadSAFE(Int32 userID, Int32 personID, Int32? citationID)
		{
			return _SafeRepository.LoadSAFE(userID, personID, citationID);
		}
	}
}
