using System.Data;
using AirportIQ.Model.Models;

namespace AirportIQ.Data
{
    public interface ILEORepository
	{
		DataTable CardAppointmentGetPeople(string serialNumber, string SSN, string lastName, string OAS_Name, string yearOfBirth);

        bool CardAppointmentSaveLEOSection(LEOResults leoResults);
	}
}