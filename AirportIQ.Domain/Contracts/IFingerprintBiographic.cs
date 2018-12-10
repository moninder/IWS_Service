using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace AirportIQ.Domain.Contracts
{
	public interface IFingerprintBiographic
	{
		DataSet LoadFingerprintBiographicPerson(int personID, int divisionID, int userID);
        bool SaveFingerprintBiographicPerson(DataTable personDataTable, DataTable governmentIdDataTable, DataTable aliasDataTable, int userID);
        DataSet LoadFingerprintBiographicReferenceData();
	}
}
