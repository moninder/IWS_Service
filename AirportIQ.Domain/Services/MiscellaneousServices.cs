using System;
using System.Data;

using AirportIQ.Data;
using AirportIQ.Data.SqlServer.Repositories;
using AirportIQ.Domain.Contracts;

namespace AirportIQ.Domain.Services
{
	public class MiscellaneousServices : IMiscellaneous
	{

		#region "Private Variables"

		private readonly IMiscellaneousRepository _MiscellaneousRepository;

		#endregion "Private Variables"

		#region "Constructors"

		public MiscellaneousServices() : this(new MiscellaneousRepository()) { }

		public MiscellaneousServices(IMiscellaneousRepository miscellaneousRepository)
		{
			if (miscellaneousRepository == null) throw new ArgumentNullException("MiscellaneousRepository");
			_MiscellaneousRepository = miscellaneousRepository;
		}

		#endregion "Constructors"

		#region Public Methods

		public DataTable LoadCountries()
		{
			return _MiscellaneousRepository.LoadCountries();
		}

		public DataTable LoadStates(string countryCode)
		{
			return _MiscellaneousRepository.LoadStates(countryCode);
		}

		public DataTable LoadContactTypes()
		{
			return _MiscellaneousRepository.LoadContactTypes();
		}


		#endregion
	}
}
