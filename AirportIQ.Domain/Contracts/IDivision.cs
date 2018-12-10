using System;
using System.Data;

namespace AirportIQ.Domain.Contracts
{
	public interface IDivision
	{
		void SaveDivision(int divisionId, int companyId, string divisionCode, string divisionName, Int16 divisionTypeId, string divisionStatusCode, string address1,
						string address2, string address3, string city, string countrySubdivisionCode, string postalCode, string countryCode, string phoneNumber,
						string faxNumber, string emailAddress, bool fingerprintRequired, bool backgroundCheckRequired, string categoryCode, string providerNumber,
                        decimal newBadgeCost, decimal replaceBadgeCost, decimal badgeBillingCost, decimal badgeCreditCost, string RAMSAgreementNumber, DateTime? RAMSExpirationDate, int userId);

		DataTable LoadDivisionDetails(int divisionID);

		DataTable LoadDivisionList();

		DataTable LoadCompanyDivision(int companyID, int userID);

		DataTable LoadDivisionContacts(int companyID, int divisionID);

        DataTable LoadContact(int contactID);

		DataTable LoadDivisionTypes();

		DataTable LoadDivisionCategories();

		DataTable LoadDivisionStatuses();

		DataSet loadCompanyReviewDivisionList(Int32? CompanyID, Int32? DivisionID, String CompanyCriteria, String DivisionCriteria, Int32 UserId); // to load a list of companies // jem

		DataSet loadCompanyReviewDivisionList(Int32? CompanyID, Int32? DivisionID, String CompanyCriteria, String DivisionCriteria, String EmployeeLNCriteria, Int32 UserId); // to load a list of companies // jem

		DataSet loadCompanyReviewDivisionList(Int32? CompanyID, Int32? DivisionID, String CompanyCriteria, String DivisionCriteria, String EmployeeLNCriteria, String EmployeeFNCriteria, Int32 UserId); // to load a list of companies // jem

        DataTable LoadCompanyOrDivisionContactsForm(Int32 CompanyID, Int32 DivisionID, Int32 UserID, bool shadowMode, bool? isNewDivisionOnly); // to load Contacts data

		DataSet LoadTransferEmployeeForm(Int32 DivisionID, Int32 PersonID, Int32 UserID);

        void SaveCompanyOrDivisionContacts(DataTable contactsToSave, int UserID, bool shadowMode, bool? isNewDivisionOnly); // to save Contacts data

		void SaveDivisionChanges(DataTable divisionchangesToSave, int UserID);

		void SaveNewDivision(DataSet divisionToSave, int UserID);

		void SaveTransferEmployeeForm(DataTable EmployeeToSave, DataTable specialAccess, int UserID);

        void SaveTransferEmployeeForm(DataTable EmployeeToSave, DataTable specialAccess, int UserID, Boolean TranAllSpecial, Boolean TranAllIcons);

		void ShadowSaveCompanyOrDivisionContacts(DataTable contactsToSave, int UserID); // to save Contacts data
		void ShadowSaveDivisionChanges(DataTable divisionchangesToSave, int UserID);
		DataTable ShadowLoadUniqueCompanyOrDivisionContactsForm(Int32 CompanyID, Int32 DivisionID, Int32 UserID); // to load Contacts data
		DataTable LoadUniqueCompanyOrDivisionContactsForm(Int32 CompanyID, Int32 DivisionID, Int32 UserID); // to load Contacts data
		void ClearAllShadowRecords(Int32 CompanyID);

		DataTable Search(string searchTerm, int userID);
        DataTable Search(string searchTerm, int companyID, int userID);
        DataTable Search(string searchTerm, string companyCode, int userID);

        DataTable LoadLocations(int divisionID, string facilityCode);
        DataTable LoadNotes(int divisionId);
	}
}