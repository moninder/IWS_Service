using System;
using System.Configuration;
using System.Data;
using AirportIQ.Data;
using AirportIQ.Data.SqlServer.Initializers;

namespace AirportIQ.Data.SqlServer.Repositories
{
	public class FingerprintBiographicRepository : IFingerprintBiographicRepository
	{
		#region Private Variables
		    private string schema = ConfigurationManager.AppSettings["SBO.ApplicationSchema"].ToString();
		#endregion Private Variables

		#region Public Methods
		    public DataSet LoadFingerprintBiographicPerson(int personID, int divisionID, int userID)
		    {
			    DataSet ret = null;
			    var storedProcedure = new StoredProcedure();
                storedProcedure.StoredProcedureName = schema + ".[Badging.FingerprintBiographics.LoadPerson]";
			    storedProcedure.Parameters.Add(new StoredProcedureParameter("@personID", ParameterType.DBString, personID));
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionID", ParameterType.DBString, divisionID));
			    storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBString, userID));
			    ret = storedProcedure.ExecuteMultipleDataSet();
			    return ret;
		    }

            public bool SaveFingerprintBiographicPerson(DataTable personDataTable, DataTable governmentIdDataTable, DataTable aliasDataTable, int userID)
            {
                DataTable ret = null;
                var storedProcedure = new StoredProcedure();
                storedProcedure.StoredProcedureName = schema + ".[Badging.FingerprintBiographics.SavePerson]";
                //rguidi 3/4/2013 #20202
                //storedProcedure.Parameters.Add(new StoredProcedureParameter("@personTable", ParameterType.DBString, personDataTable));
                //storedProcedure.Parameters.Add(new StoredProcedureParameter("@governmentIdTable", ParameterType.DBString, governmentIdDataTable));
                //storedProcedure.Parameters.Add(new StoredProcedureParameter("@aliasTable", ParameterType.DBString, aliasDataTable));
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@personTable", ParameterType.Structured, personDataTable));
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@governmentIdTable", ParameterType.Structured, governmentIdDataTable));
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@aliasTable", ParameterType.Structured, aliasDataTable));
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBString, userID));
                ret = storedProcedure.ExecuteDataSet();
                return (bool)ret.Rows[0].ItemArray[0];

            }
            public DataSet LoadFingerprintBiographicReferenceData()
            {
                DataSet ret = null;
                var storedProcedure = new StoredProcedure();
                storedProcedure.StoredProcedureName = schema + ".[Badging.FingerprintBiographics.LoadReferenceData]";
                ret = storedProcedure.ExecuteMultipleDataSet();
                return ret;
            }
        #endregion
	}
}