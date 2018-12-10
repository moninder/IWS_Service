using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using AirportIQ.Data;
using AirportIQ.Data.SqlServer.Repositories;
using AirportIQ.Domain.Contracts;

namespace AirportIQ.Domain.Services
{
	public class PersonServices : IPerson
	{
		#region "Private Variables"

		private readonly IPersonRepository personRepository;

		#endregion "Private Variables"

		#region "Constructors"

		public PersonServices() : this(new PersonRepository()) { }

		public PersonServices(IPersonRepository personRepository)
		{
			if (personRepository == null) throw new ArgumentNullException("personRepository");
			this.personRepository = personRepository;
		}

		#endregion "Constructors"

		public DataSet loadPersonForm(short PersonID)
		{
			return this.personRepository.loadPersonForm(PersonID);
		}

		public DataTable GetPersonLookupList(int userID)
		{
			return this.personRepository.GetPersonLookupList(userID);
		}

        public DataTable LoadNotes(int personId, int divisionId)
        {
            return this.personRepository.LoadNotes(personId, divisionId);
        }
	}
}