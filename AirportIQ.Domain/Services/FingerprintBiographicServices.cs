using System;
using System.Data;

using AirportIQ.Data;
using AirportIQ.Data.SqlServer.Repositories;
using AirportIQ.Domain.Contracts;

namespace AirportIQ.Domain.Services
{
	public class FingerprintBiographicServices: IFingerprintBiographic
	{

		#region "Private Variables"
		    private readonly IFingerprintBiographicRepository _FingerprintBiographicRepository;
		#endregion "Private Variables"

		#region "Constructors"
            public FingerprintBiographicServices() : this(new FingerprintBiographicRepository()) { }

            public FingerprintBiographicServices(IFingerprintBiographicRepository fingerprintBiographicRepository)
		    {
                if (fingerprintBiographicRepository == null) throw new ArgumentNullException("fingerprintBiographicRepository");
                _FingerprintBiographicRepository = fingerprintBiographicRepository;
		    }
		#endregion "Constructors"

        #region "Methods"
            public DataSet LoadFingerprintBiographicPerson(int personID, int divisionID, int userID)
		    {
                return _FingerprintBiographicRepository.LoadFingerprintBiographicPerson(personID, divisionID, userID);
		    }

            public bool SaveFingerprintBiographicPerson(DataTable personDataTable, DataTable governmentIdDataTable, DataTable aliasDataTable, int userID)
            {
                return _FingerprintBiographicRepository.SaveFingerprintBiographicPerson(personDataTable, governmentIdDataTable, aliasDataTable, userID);
            }

            public DataSet LoadFingerprintBiographicReferenceData()
            {
                return _FingerprintBiographicRepository.LoadFingerprintBiographicReferenceData();
            }

        #endregion
	}
}

