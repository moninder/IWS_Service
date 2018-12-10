using System;
using System.Data;
using System.Configuration;

using AirportIQ.Data;
using AirportIQ.Data.SqlServer.Initializers;

namespace AirportIQ.Data.SqlServer.Repositories
{
    public class ApplicantRepository : IApplicantRepository
    {

        #region Private Variables

        private string schema = ConfigurationManager.AppSettings["ReferenceTableSchema"].ToString();

        #endregion Private Variables

        #region Public Methods        

		public DataSet loadIndividualSpecialAccessRequestForm(short CompanyID, short DivisionID, short LocationID, Int32 userID)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Data.IndividualSpecialAccessRequestForm.Load]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyID", ParameterType.DBString, CompanyID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionID", ParameterType.DBString, DivisionID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@locationID", ParameterType.DBString, LocationID));
						storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBString, userID));
            ret = storedProcedure.ExecuteMultipleDataSet();
            return ret;
        }

        public void saveIndividualSpecialAccessRequestForm(DataTable IndividualSpecialAccessRequestFormToSave, int userID)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Data.IndividualSpecialAccessRequestForm.Save]";
            //rguidi 3/4/2013 #20202
            //storedProcedure.Parameters.Add(new StoredProcedureParameter("@IndividualSpecialAccessTypes", ParameterType.DBString, IndividualSpecialAccessRequestFormToSave));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@IndividualSpecialAccessTypes", ParameterType.Structured, IndividualSpecialAccessRequestFormToSave));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID));
            ret = storedProcedure.ExecuteMultipleDataSet();
        }

        #endregion

    }
}
