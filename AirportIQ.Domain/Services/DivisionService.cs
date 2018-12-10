using System;
using System.Data;
using AirportIQ.Data;
using AirportIQ.Data.SqlServer.Repositories;
using AirportIQ.Domain.Contracts;

namespace AirportIQ.Domain.Services
{
	public class DivisionService : IDivision
	{
		#region Private Members

		private readonly IDivisionRepository divisionRepository;

		#endregion Private Members

		#region Constructors and Distructors

		public DivisionService() : this(new DivisionRepository()) { }

		public DivisionService(IDivisionRepository divisionRepository)
		{
			if (divisionRepository == null) throw new ArgumentNullException("DivisionRepository");
			this.divisionRepository = divisionRepository;
		}

		#endregion Constructors and Distructors

		public void SaveDivision(int divisionId, int companyId, string divisionCode, string divisionName, Int16 divisionTypeId, string divisionStatusCode,
								string address1, string address2, string address3, string city, string countrySubdivisionCode, string postalCode, string countryCode,
								string phoneNumber, string faxNumber, string emailAddress, bool fingerprintRequired, bool backgroundCheckRequired, string categoryCode,
                                string providerNumber, decimal newBadgeCost, decimal replaceBadgeCost, decimal badgeBillingCost, decimal badgeCreditCost, string RAMSAgreementNumber, DateTime? RAMSExpirationDate, int userId)
		{
			divisionRepository.SaveDivision(divisionId, companyId, divisionCode, divisionName, divisionTypeId, divisionStatusCode, address1, address2, address3,
											city, countrySubdivisionCode, postalCode, countryCode, phoneNumber, faxNumber, emailAddress, fingerprintRequired,
											backgroundCheckRequired, categoryCode, providerNumber, newBadgeCost, replaceBadgeCost, badgeBillingCost, badgeCreditCost,
                                            RAMSAgreementNumber, RAMSExpirationDate, userId);
		}
		
		public DataTable LoadDivisionDetails(int divisionID)
		{
			return divisionRepository.LoadDivisionDetails(divisionID);
		}

		public DataTable LoadDivisionList()
		{
			return divisionRepository.LoadDivisionList();
		}

		public DataTable LoadCompanyDivision(int companyID, int userID)
		{
			return divisionRepository.LoadCompanyDivision(companyID, userID);
		}

		public DataTable LoadDivisionContacts(int companyID, int divisionID)
		{
			return divisionRepository.LoadDivisionContacts(companyID, divisionID);
		}

        public DataTable LoadContact(int contactID)
        {
            return divisionRepository.LoadContact(contactID);
        }

		public DataTable LoadDivisionTypes()
		{
			return divisionRepository.LoadDivisionTypes();
		}

		public DataTable LoadDivisionCategories()
		{
			return divisionRepository.LoadDivisionCategories();
		}

		public DataTable LoadDivisionStatuses()
		{
			return divisionRepository.LoadDivisionStatuses();
		}

		public DataSet loadCompanyReviewDivisionList(Int32? companyID, Int32? divisionID, String companyCriteria, String divisionCriteria, Int32 userID)
		{
			return this.divisionRepository.loadCompanyReviewDivisionList(companyID, divisionID, companyCriteria, divisionCriteria, userID);
		}

		public DataSet loadCompanyReviewDivisionList(Int32? companyID, Int32? divisionID, String companyCriteria, String divisionCriteria, String employeeLNCriteria, Int32 userID)
		{
			return this.divisionRepository.loadCompanyReviewDivisionList(companyID, divisionID, companyCriteria, divisionCriteria, employeeLNCriteria, userID);
		}

		public DataSet loadCompanyReviewDivisionList(Int32? companyID, Int32? divisionID, String companyCriteria, String divisionCriteria, String employeeLNCriteria, String employeeFNCriteria, Int32 userID)
		{
			return this.divisionRepository.loadCompanyReviewDivisionList(companyID, divisionID, companyCriteria, divisionCriteria, employeeLNCriteria, employeeFNCriteria, userID);
		}

        public DataTable LoadCompanyOrDivisionContactsForm(Int32 CompanyID, Int32 DivisionID, int UserID, bool shadowMode, bool? isNewDivisionOnly)
		{
			return this.divisionRepository.LoadCompanyOrDivisionContactsForm(CompanyID, DivisionID, UserID, shadowMode, isNewDivisionOnly);
		}

		public DataSet LoadTransferEmployeeForm(int DivisionID, Int32 PersonID, int UserID)
		{
			return this.divisionRepository.LoadTransferEmployeeForm(DivisionID, PersonID, UserID);
		}

        public void SaveTransferEmployeeForm(DataTable EmployeeToSave, DataTable specialAccess, int UserID)
		{
			this.divisionRepository.SaveTransferEmployeeForm(EmployeeToSave, specialAccess, UserID);
		}

        public void SaveTransferEmployeeForm(DataTable EmployeeToSave, DataTable specialAccess, int UserID, Boolean tranAllSpecial, Boolean tranAllIcons)
		{
			this.divisionRepository.SaveTransferEmployeeForm(EmployeeToSave, specialAccess, UserID, tranAllSpecial, tranAllIcons);
		}

		public void SaveNewDivision(DataSet divisionToSave, int userID)
		{
			this.divisionRepository.SaveNewDivision(divisionToSave, userID);
		}

        public void SaveCompanyOrDivisionContacts(DataTable contactsToSave, int UserID, bool shadowMode, bool? isNewDivisionOnly)
		{
			this.divisionRepository.SaveCompanyOrDivisionContacts(contactsToSave, UserID, shadowMode, isNewDivisionOnly);
		}

		public void SaveDivisionChanges(DataTable divisionchangesToSave, int UserID)
		{
			this.divisionRepository.SaveDivisionChanges(divisionchangesToSave, UserID);
		}

		public void ShadowSaveCompanyOrDivisionContacts(DataTable contactsToSave, int UserID)
		{
			this.divisionRepository.ShadowSaveCompanyOrDivisionContacts(contactsToSave, UserID);
		}

		public void ShadowSaveDivisionChanges(DataTable divisionchangesToSave, int UserID)
		{
			this.divisionRepository.ShadowSaveDivisionChanges(divisionchangesToSave, UserID);
		}

		public DataTable ShadowLoadUniqueCompanyOrDivisionContactsForm(Int32 CompanyID, Int32 DivisionID, int UserID)
		{
			return this.divisionRepository.ShadowLoadUniqueCompanyOrDivisionContactsForm(CompanyID, DivisionID, UserID);
		}

		public DataTable LoadUniqueCompanyOrDivisionContactsForm(Int32 CompanyID, Int32 DivisionID, int UserID)
		{
			return this.divisionRepository.LoadUniqueCompanyOrDivisionContactsForm(CompanyID, DivisionID, UserID);
		}

		public void ClearAllShadowRecords(Int32 companyID)
		{
			this.divisionRepository.ClearAllShadowRecords(companyID);
		}

		public DataTable Search(string searchTerm, int userID)
		{
			return this.divisionRepository.Search(searchTerm, userID);
		}

        public DataTable Search(string searchTerm, int companyID, int userID)
        {
            return this.divisionRepository.Search(searchTerm, companyID, userID);
        }

        public DataTable Search(string searchTerm, string companyCode, int userID)
        {
            return this.divisionRepository.Search(searchTerm, companyCode, userID);
        }

        public DataTable LoadLocations(int divisionID, string facilityCode)
        {
            return this.divisionRepository.LoadLocations(divisionID, facilityCode);
        }

        public DataTable LoadNotes(int divisionId)
        {
            return this.divisionRepository.LoadNotes(divisionId);
        }
	}
}