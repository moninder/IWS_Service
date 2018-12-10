using System.Data;

namespace AirportIQ.Domain.Contracts
{
	public interface IMiscellaneous
	{
		DataTable LoadCountries();
		DataTable LoadStates(string countryCode);
		DataTable LoadContactTypes();
	}
}
