using System;
using System.Data;

namespace AirportIQ.Data
{
    public interface IReferenceTableRepository
    {
        void fetchReferenceTableInfowithIdAndCode(string schemaName, string tableName, out DataTable dtRet, out string sID);
        DataTable fetchReferenceTableInfo(string tableName);
        DataSet fetchLookupData();
        DataSet fetchLookupDataFacilFiltered(Int32 userID);
        DataSet fetchLookupPrimeDataFacilFiltered(Int32? companyID, Int32 userID);
        DataSet fetchNonStandardLookupData(string tableKey);
        void saveReferenceTableByCode(string schemaName, string tableName, DataTable refTable, int userID);
        void saveReferenceTableByID(string schemaName, string tableName, DataTable refTable, int userID);
    }
}
