using System;
using System.Data;

using AirportIQ.Data;
using AirportIQ.Domain.Contracts;
using AirportIQ.Data.SqlServer.Repositories;

namespace AirportIQ.Domain.Services
{
    public class ApplicantServices : IApplicant
    {

        #region "Private Variables"

            private readonly IApplicantRepository applicantRepository;

        #endregion

        #region "Constructors"
        
            public ApplicantServices() : this(new ApplicantRepository()) { }

            public ApplicantServices(IApplicantRepository applicantRepository)
            {
                if (applicantRepository == null) throw new ArgumentNullException("applicantRepository");
                this.applicantRepository = applicantRepository;
            }
            
        #endregion

        #region "Public Methods"                  

            public DataSet loadIndividualSpecialAccessRequestForm(short CompanyID, short DivisionID, short LocationID, Int32 userID)
            {
							return this.applicantRepository.loadIndividualSpecialAccessRequestForm(CompanyID, DivisionID, LocationID, userID);
            }

            public void saveIndividualSpecialAccessRequestForm(DataTable IndividualSpecialAccessRequestFormToSave, int userID)
            {
                this.applicantRepository.saveIndividualSpecialAccessRequestForm(IndividualSpecialAccessRequestFormToSave, userID);
            }

        #endregion
           
    }
}
