using System;
using System.Data;

namespace AirportIQ.Domain.Contracts
{
    
    public interface IApplicant
    {   

       DataSet loadIndividualSpecialAccessRequestForm(Int16 CompanyID, Int16 DivisionID, Int16 LocationID, Int32 userID); // to load Individual Special Access Request Form data
       void saveIndividualSpecialAccessRequestForm(DataTable IndividualSpecialAccessRequestFormToSave, int userID); // to save Individual Special Access Request Form  data   

    }

}
