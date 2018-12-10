using System.Data;
using AirportIQ.Model.Models;

namespace AirportIQ.Domain.Contracts
{
    public interface ILEO
	{
		DataTable CardAppointmentGetPeople(string serialNumber, string SSN, string lastName, string OAS_Name, string yearOfBirth);
        bool CardAppointmentSaveLEOSection(LEOResults leoResults);
	}
}