using System;
using System.Data;

namespace AirportIQ.Data
{
    public interface IFingerprintBiographicRepository
    {
        DataSet LoadFingerprintBiographicPerson(int personID, int divisionID, int userID);
        bool SaveFingerprintBiographicPerson(DataTable personDataTable, DataTable governmentIdDataTable, DataTable aliasDataTable, int userID);
        DataSet LoadFingerprintBiographicReferenceData();
    }
}
