using System;
using System.Data;

using AirportIQ.Data;
using AirportIQ.Domain.Contracts;
using AirportIQ.Data.SqlServer.Repositories;

namespace AirportIQ.Domain.Services
{
    public class ReferenceTableServices : IReferenceTable
    {
      
        #region "Private Variables"
            
            private readonly IReferenceTableRepository referenceTableRepository;
            //private CacheServices cs = new CacheServices();

        #endregion

        #region "Constructors"
        
            public ReferenceTableServices() : this(new ReferenceTableRepository()) {}

            public ReferenceTableServices(IReferenceTableRepository referenceTableRepository)
            {
                if (referenceTableRepository == null) throw new ArgumentNullException("referenceTableRepository");
                this.referenceTableRepository = referenceTableRepository;
            }

        #endregion

        #region "Public Methods"
            //public void clearCache(string tableName)
            //{
            //    //cs.removeCache(tableName);
            //}


            public DataTable loadReferenceTable(string tableName)
            {

                DataTable dtRet = new DataTable();
                //dtRet = cs.retrievefromCache(tableName);
                //if (dtRet == null)
                //{
                    dtRet = referenceTableRepository.fetchReferenceTableInfo(tableName);
                //    if (dtRet.Rows.Count > 0)
                //    {
                //        cs.addtoCache(tableName, dtRet);
                //    }
                //}
                return dtRet;
            }

            public DataTable loadReferenceTablewithIdAndCode(string schemaName, string tableName)
            {
                DataTable dtRet = new DataTable();
                string sID = string.Empty;

                //dtRet = cs.retrievefromCache(tableName);
                //if (dtRet == null)
                //{
                    referenceTableRepository.fetchReferenceTableInfowithIdAndCode(schemaName, tableName, out dtRet, out sID);
                //    if (dtRet.Rows.Count > 0)
                //    {
                //        cs.addtoCache(tableName, dtRet);    
                //    }
                //}
                return dtRet;
            }

            public void saveReferenceTablewithIdAndCode(string schemaName, string tableName, DataTable refTable, int userID)
            {
                //if (refTable.Rows[0][0].ToString() == "Code")
                if (refTable.Columns[0].ColumnName == "Code")
                {
                    referenceTableRepository.saveReferenceTableByCode(schemaName, tableName, refTable, userID);
                }
                else
                {
                    referenceTableRepository.saveReferenceTableByID(schemaName, tableName, refTable, userID);
                }
            }

            public DataSet loadLookupData()
            {
                DataSet dtRet = new DataSet();             

                //dtRet = cs.retrievefromCacheDS("Lookup");
                //if (dtRet == null)
                //{
                   dtRet=referenceTableRepository.fetchLookupData();
                //    if (dtRet != null)
                //    {
                //        cs.addtoCacheDS("Lookup", dtRet);
                //    }
                //}
                return dtRet;
            }

            //---------jem--------------------------------
            public DataSet loadLookupDataFacilFiltered( Int32 userID)
            {
                DataSet dtRet = new DataSet();
                    // removed per Michael 8/21/2012   TODO: remove commented code below.
                    //dtRet = cs.retrievefromCacheDS("LookupFacilFiltered");
                    //if (dtRet == null)
                    //{
                dtRet = referenceTableRepository.fetchLookupDataFacilFiltered(userID);
                    //    if (dtRet != null)
                    //    {
                    //        cs.addtoCacheDS("LookupFacilFiltered", dtRet);
                    //    }
                    //}
                return dtRet;
            }
            //--------------------------------------------
            //---------jem--------------------------------
            public DataSet loadLookupPrimeDataFacilFiltered(Int32? companyID,  Int32 userID)
            {
                DataSet dtRet = new DataSet();
                dtRet = referenceTableRepository.fetchLookupPrimeDataFacilFiltered(companyID,userID);
                return dtRet;
            }
            //--------------------------------------------
            public DataSet loadNonStandardLookupData(String tableKey)
            {
                DataSet dtRet = new DataSet();

                //dtRet = cs.retrievefromCacheDS("NonStandardLookup");
                //if (dtRet == null)
                //{
                    dtRet = referenceTableRepository.fetchNonStandardLookupData(tableKey);
                //    if (dtRet != null)
                //    {
                //        cs.addtoCacheDS(tableKey, dtRet);
                //    }
                //}
                return dtRet;
            }
       #endregion



            
    }
}
