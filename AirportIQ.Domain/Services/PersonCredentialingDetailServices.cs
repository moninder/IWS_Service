using System;
using System.Data;

using AirportIQ.Data;
using AirportIQ.Domain.Contracts;
using AirportIQ.Data.SqlServer.Repositories;

namespace AirportIQ.Domain.Services
{
    public class PersonCredentialingDetailServices : IPersonCredentialingDetail
    {
         #region "Private Variables"

            private readonly IPersonCredentialingDetailRepository personCredentialingDetailRepository;

        #endregion

        #region "Constructors"

            public PersonCredentialingDetailServices() : this(new PersonCredentialingDetailRepository()) { }

            public PersonCredentialingDetailServices(IPersonCredentialingDetailRepository personCredentialingDetailRepository)
            {
                if (personCredentialingDetailRepository == null) throw new ArgumentNullException("personCredentialingDetailRepository");
                this.personCredentialingDetailRepository = personCredentialingDetailRepository;
            }
            
        #endregion               
        #region "Public Methods"

            public DataSet PerCredPersonLoad(Int32 userID, Int32 personID, Int32? divisionID)
            {
                return this.personCredentialingDetailRepository.PerCredPersonLoad(userID, personID, divisionID);
            }
            public DataSet PerCredSAFELoad(Int32 userID, Int32 personID, Int32? citationID)
            {
                return this.personCredentialingDetailRepository.PerCredSAFELoad(userID, personID, citationID);
            }
			public DataSet PerCredBadgeLoad(Int32 userID, Int32 personID, Int32 badgeID)
			{
				return this.personCredentialingDetailRepository.PerCredBadgeLoad(userID, personID, badgeID);
			}
			public DataSet PerCredFileLoad(Int32 docID)
			{
				return this.personCredentialingDetailRepository.PerCredFileLoad(docID);
			}          

        #endregion
    }
}
