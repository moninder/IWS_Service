using System;
using System.Data;
using System.Configuration;

using AirportIQ.Data;
using AirportIQ.Data.SqlServer.Initializers;

namespace AirportIQ.Data.SqlServer.Repositories
{
    public class FacilityRepository : IFacilityRepository
    {

        #region Private Variables

            private string schema = ConfigurationManager.AppSettings["ReferenceTableSchema"].ToString();

        #endregion    

        public DataTable getWorkLocations(string facilityCode)
        {
            DataTable ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Data.FacilityLocationsForm.Load]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@facilitycode", ParameterType.DBString, facilityCode)); 
            ret = storedProcedure.ExecuteDataSet();
            return ret;
        }

        //JBienvenu 2013-01-10 new
        public DataTable getStaff(string facilityCode)
        {
            DataTable ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Data.FacilityStaffList.Load]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@FacilityCode", ParameterType.DBString, facilityCode));
            ret = storedProcedure.ExecuteDataSet();
            return ret;
        }


    }
}
