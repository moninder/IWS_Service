using System.Data;

namespace AirportIQ.Data
{
	public interface IMiscellaneousRepository
	{
		DataTable LoadCountries();
		DataTable LoadStates(string countryCode);
		DataTable LoadContactTypes();
	}
}
