using System;
using System.Data;

namespace AirportIQ.Domain.Contracts
{
    public interface IReferenceTable
    {
        DataTable loadReferenceTable(string tableName);
        DataTable loadReferenceTablewithIdAndCode(string schemaName, string tableName);
        DataSet loadLookupData();
        //---------jem 7/13/2012-------------------------
        DataSet loadLookupDataFacilFiltered(Int32 userID);
        //---------end jem 7/13/2012-------------------------
        DataSet loadNonStandardLookupData(string tableKey);
        void saveReferenceTablewithIdAndCode(string schemaName, string tableName, DataTable refTable, int userID);
        DataSet loadLookupPrimeDataFacilFiltered(Int32? companyID, Int32 userID);
        //void clearCache(string tableName);

    }

}
