using System;
using System.Data;

namespace AirportIQ.Data
{
	public interface IAgreementRepository
	{		
		DataTable LoadDivisionAgreements(int divisionID);
		DataSet LoadAgreement(int agreementID);
		DataTable LoadAgreementTypes();
		DataTable LoadCompanyAndDivsion(int agreementID);
		DataTable LoadDefaultAccess(int agreementID, string facilityCode);
		DataTable LoadAccessCategories(int locationID);
		DataTable LoadLocationsForAgreement(int agreementID, int jobroleID, string facilityCode);
        DataTable ApprovedIconsByBadgeColor(string facilityCode);
        DataTable LoadLocationsWithAgreementIndicator(int agreementID, int divisionTypeId, string facilityCode);
		DataTable LoadLocationsForFacitity(string facilityCode);
		DataTable LoadJobRolesForAgreement(int agreementID);
        DataTable LoadBadgeColorsForAgreement(int agreementID, int divisionTypeId, string facilityCode);
		DataTable LoadJobRolesWithAgreementIndicator(int agreementID);
		DataTable LoadJobRoles();
        void UpdateBadgeAccess(int agreementID, int jobRoleID, int locationID, int staffID, int UserID);
        AirportIQ.Data.Helper.AgreementHelper.DateLimits DateLimits(int? agreementID, int? newPrimeAgreementID);
        void Save(int agreementID, int divisionID, string agreementNumber, short agreementTypeID, string directAuthorityNumber, string otherAgreementType, DateTime whenAgreementBegins, DateTime whenAgreementEnds, int? agreementID_Prime, DataTable services, int UserID);
        void DeleteDefaultAccessBadgeColorsAndIcons(int agreementId);
        void DeleteDefaultAccessBadgeColorsAndIcons(int agreementId, bool useShadowTables);
        void LoadIndustryTypes(DataTable newIndustryTypes);
    }
}
