using System;
using System.Configuration;
using System.Data;
using AirportIQ.Data;
using AirportIQ.Data.SqlServer.Initializers;

namespace AirportIQ.Data.SqlServer.Repositories
{
    public class CitationRepository : ICitationRepository
    {
        #region Private Variables

        private string schema = ConfigurationManager.AppSettings["SBO.ApplicationSchema"].ToString();

        #endregion Private Variables

        DataSet ICitationRepository.LoadCitationEntry(int citationID)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = "[App.Sbo].[Data.SAFE.Citation.Load]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@CitationID", ParameterType.DBInteger, citationID));
            ret = storedProcedure.ExecuteMultipleDataSet();
            return ret;
        }

    }
}
