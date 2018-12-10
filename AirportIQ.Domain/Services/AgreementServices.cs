using System;
using System.Data;

using AirportIQ.Data;
using AirportIQ.Data.SqlServer.Repositories;
using AirportIQ.Domain.Contracts;

namespace AirportIQ.Domain.Services
{
	public class AgreementServices : IAgreement
	{
		#region "Private Variables"

		private readonly IAgreementRepository _AgreementRepository;

		#endregion "Private Variables"

		#region "Constructors"

		public AgreementServices() : this(new AgreementRepository()) { }

		public AgreementServices(IAgreementRepository agreementRepository)
		{
			if (agreementRepository == null) throw new ArgumentNullException("AgreementRepository");
			_AgreementRepository = agreementRepository;
		}

		#endregion "Constructors"






		#region Public Methods

		public DataTable LoadDivisionAgreements(int divisionID)
		{
			return _AgreementRepository.LoadDivisionAgreements(divisionID);
		}

		public DataSet LoadAgreement(int agreementID)
		{
			return _AgreementRepository.LoadAgreement(agreementID);
		}

		public DataTable LoadAgreementTypes()
		{
			return _AgreementRepository.LoadAgreementTypes();
		}

		public DataTable LoadCompanyAndDivsion(int agreementID)
		{
			return _AgreementRepository.LoadCompanyAndDivsion(agreementID);
		}

		public DataTable LoadDefaultAccess(int agreementID, string facilityCode)
		{
			return _AgreementRepository.LoadDefaultAccess(agreementID, facilityCode);
		}

		public DataTable LoadAccessCategories(int locationID)
		{
			return _AgreementRepository.LoadAccessCategories(locationID);
		}

		public DataTable LoadLocationsForAgreement(int agreementID, int jobroleID, string facilityCode)
		{
			return _AgreementRepository.LoadLocationsForAgreement(agreementID, jobroleID, facilityCode);
		}

        public DataTable ApprovedIconsByBadgeColor(string facilityCode)
        {
            return _AgreementRepository.ApprovedIconsByBadgeColor(facilityCode);
        }

        public DataTable LoadLocationsWithAgreementIndicator(int agreementID, int divisionTypeId, string facilityCode)
		{
			return _AgreementRepository.LoadLocationsWithAgreementIndicator(agreementID, divisionTypeId, facilityCode);
		}
		
		public DataTable LoadLocationsForFacitity(string facilityCode)
		{
			return _AgreementRepository.LoadLocationsForFacitity(facilityCode);
		}

		public DataTable LoadJobRolesForAgreement(int agreementID)
		{
			return _AgreementRepository.LoadJobRolesForAgreement(agreementID);
		}

        public DataTable LoadBadgeColorsForAgreement(int agreementID, int divisionTypeId, string facilityCode)
        {
            return _AgreementRepository.LoadBadgeColorsForAgreement(agreementID, divisionTypeId, facilityCode);
        }

		public DataTable LoadJobRoles()
		{
			return _AgreementRepository.LoadJobRoles();
		}

        public void UpdateBadgeAccess(int agreementID, int jobRoleID, int locationID, int staffID, int UserID)
        {
            _AgreementRepository.UpdateBadgeAccess(agreementID, jobRoleID, locationID, staffID, UserID);
        }

		public DataTable LoadJobRolesWithAgreementIndicator(int agreementID)
		{
			return _AgreementRepository.LoadJobRolesWithAgreementIndicator(agreementID);
		}

        public AirportIQ.Data.Helper.AgreementHelper.DateLimits DateLimits(int? agreementID, int? newPrimeAgreementID)
        {
            return _AgreementRepository.DateLimits(agreementID, newPrimeAgreementID);
        }

        public void Save(int agreementID, int divisionID, string agreementNumber, short agreementTypeID, string directAuthorityNumber, string otherAgreementType, DateTime whenAgreementBegins, DateTime whenAgreementEnds, int? agreementID_Prime, DataTable services, int UserID)
        {
            _AgreementRepository.Save(agreementID, divisionID, agreementNumber, agreementTypeID, directAuthorityNumber, otherAgreementType, whenAgreementBegins, whenAgreementEnds, agreementID_Prime, services, UserID);
        }

        public void DeleteDefaultAccessBadgeColorsAndIcons(int agreementId)
        {
            this.DeleteDefaultAccessBadgeColorsAndIcons(agreementId, false);
        }

        public void DeleteDefaultAccessBadgeColorsAndIcons(int agreementId, bool useShadowTables)
        {
            _AgreementRepository.DeleteDefaultAccessBadgeColorsAndIcons(agreementId, useShadowTables);
        }

        public void LoadIndustryTypes(DataTable newIndustryTypes)
        {
            _AgreementRepository.LoadIndustryTypes(newIndustryTypes);
        }

		#endregion
	}
}
