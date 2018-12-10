using System;
using System.Data;
using AirportIQ.Data;
using AirportIQ.Data.SqlServer.Repositories;
using AirportIQ.Domain.Contracts;
using AirportIQ.Model.Models;

namespace AirportIQ.Domain.Services
{
	public class LEOServices : ILEO
	{
		private readonly ILEORepository leoRepository;



        public LEOServices() : this(new LEORepository()) { }

        public LEOServices(ILEORepository LEORepository)
		{
            if (LEORepository == null) throw new ArgumentNullException("LEORepository");
            this.leoRepository = LEORepository;
		}



        public DataTable CardAppointmentGetPeople(string serialNumber, string SSN, string lastName, string OAS_Name, string yearOfBirth)
		{
            return this.leoRepository.CardAppointmentGetPeople(serialNumber, SSN, lastName, OAS_Name, yearOfBirth);
		}

        public bool CardAppointmentSaveLEOSection(LEOResults leoResults)
        {
            return this.leoRepository.CardAppointmentSaveLEOSection(leoResults);
        }
    }
}