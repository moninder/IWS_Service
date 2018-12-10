using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AirportIQ.Data;
using System.Configuration;
using AirportIQ.Data.SqlServer.Initializers;

namespace AirportIQ.Data.SqlServer.Repositories
{
    public class PersonCredentialingDetailRepository : IPersonCredentialingDetailRepository
    {

        #region Private Variables

            private string schema = ConfigurationManager.AppSettings["SecuritySchema"].ToString();

        #endregion


        public DataSet PerCredPersonLoad(Int32 userID, Int32 personID, Int32? divisionID)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Data.PersonCredentialing.PersonLoad]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBString, userID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@personID", ParameterType.DBString, personID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionID", ParameterType.DBString, divisionID));
            ret = storedProcedure.ExecuteMultipleDataSet();
            return ret;
        }
        public DataSet PerCredSAFELoad(Int32 userID, Int32 personID, Int32? citationID)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Data.SAFEInformation.Load]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBString, userID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@personID", ParameterType.DBString, personID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@citationID", ParameterType.DBString, citationID));
            ret = storedProcedure.ExecuteMultipleDataSet();
            return ret;
        }
		public DataSet PerCredBadgeLoad(Int32 userID, Int32 personID, Int32 badgeID)
		{
			DataSet ret = null;
			var storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = schema + ".[Data.PersonBadgeInformation.Load]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBString, userID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@personID", ParameterType.DBString, personID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@badgeID", ParameterType.DBString, badgeID));
			ret = storedProcedure.ExecuteMultipleDataSet();
			return ret;
		}
		public DataSet PerCredFileLoad(Int32 docID)
		{
			DataSet ret = null;
			var storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = schema + ".[Data.File.Load]";

			storedProcedure.Parameters.Add(new StoredProcedureParameter("@documentID", ParameterType.DBString, docID));
			ret = storedProcedure.ExecuteMultipleDataSet();
			return ret;
		}
    }
}
