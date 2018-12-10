using System;
using System.Data;
using System.Configuration;

using AirportIQ.Data;
using AirportIQ.Data.SqlServer.Initializers;
using AirportIQ.Data.Helper;

namespace AirportIQ.Data.SqlServer.Repositories
{
    public class ReferenceTableRepository : IReferenceTableRepository
    {

        #region Private Variables

            private string schema = ConfigurationManager.AppSettings["ReferenceTableSchema"].ToString();

        #endregion

        #region Public Methods
        
            public void fetchReferenceTableInfowithIdAndCode(String schemaName, String tableName, out DataTable dtRet, out String sID)
            {
                DataTable ret = null;
                String ID = string.Empty;
                var storedProcedure = new StoredProcedure();
                storedProcedure.StoredProcedureName = schema + ".[AdministrativeFunctions.EditReferenceTableForm.Load]";
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@schemaName", ParameterType.DBString, schemaName));
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@tableName", ParameterType.DBString, tableName));               
                storedProcedure.ExecuteDataSetWithOutputParam(out ret,out ID);
                dtRet = ret; sID = ID;
            }

            DataTable IReferenceTableRepository.fetchReferenceTableInfo(string tableName)
            {
                DataTable ret = null;
                var storedProcedure = new StoredProcedure();
                switch (tableName)
                {
                    case "States":
                        storedProcedure.StoredProcedureName = schema + ".[GetStates]";
                        break;                    
                    default:
                        break;
                }
                ret = storedProcedure.ExecuteDataSet();
                return ret;
            }

            public void saveReferenceTableByID(String schemaName, String tableName, DataTable refTable, int userID)
            {
                //Test method to check the values in DataTable.
                DTHelper.DisplayDT(refTable);
                
                DataTable ret = null;                
                var storedProcedure = new StoredProcedure();
                storedProcedure.StoredProcedureName = schema + ".[AdministrativeFunctions.EditReferenceTableForm.Save -- ID-based tables]";
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@schemaName", ParameterType.DBString, schemaName));
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@tableName", ParameterType.DBString, tableName));
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@ReferenceTableChanges", ParameterType.Structured, refTable));
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID));

                ret = storedProcedure.ExecuteDataSet();  
            }

            public void saveReferenceTableByCode(String schemaName, String tableName, DataTable refTable, int userID)
            {
                //Test method to check the values in DataTable.
                DTHelper.DisplayDT(refTable);

                DataTable ret = null;                
                var storedProcedure = new StoredProcedure();
                storedProcedure.StoredProcedureName = schema + ".[AdministrativeFunctions.EditReferenceTableForm.Save -- Code-based tables]";
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@schemaName", ParameterType.DBString, schemaName));
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@tableName", ParameterType.DBString, tableName));
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@ReferenceTableChanges", ParameterType.Structured, refTable));
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID));
                ret = storedProcedure.ExecuteDataSet();                                
            }

            public DataSet fetchLookupData()
            {
                DataSet ret = null;               
                var storedProcedure = new StoredProcedure();
                storedProcedure.StoredProcedureName = schema + ".[LookupData.Load]";
                ret = storedProcedure.ExecuteMultipleDataSet();
                return ret;
            }
        
            //---------jem 7/13/2012-------------------------
            public DataSet fetchLookupDataFacilFiltered(Int32 userID)
            {
                DataSet ret = null;
                var storedProcedure = new StoredProcedure();
                storedProcedure.StoredProcedureName = schema + ".[LookupDataFacilFiltered.Load]";
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBString, userID));
                ret = storedProcedure.ExecuteMultipleDataSet();
                return ret;
            }
            //----------------------------------------------
            //---------jem 8/27/2012-------------------------
            public DataSet fetchLookupPrimeDataFacilFiltered(Int32? companyID, Int32 userID)
            {
                DataSet ret = null;
                var storedProcedure = new StoredProcedure();
                storedProcedure.StoredProcedureName = schema + ".[LookupPrimeDataFacilFiltered.Load]";
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyID", ParameterType.DBString, companyID));
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBString, userID));
                ret = storedProcedure.ExecuteMultipleDataSet();
                return ret;
            }
            //-----
            public DataSet fetchNonStandardLookupData(String tableKey)
            {
                DataSet ret = null;
                var storedProcedure = new StoredProcedure();
                storedProcedure.StoredProcedureName = schema + ".[LookupNonStandardData.Load]";
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@tableKey", ParameterType.DBString, tableKey));
                ret = storedProcedure.ExecuteMultipleDataSet();
                return ret;
            }    

        #endregion





           
    }
}