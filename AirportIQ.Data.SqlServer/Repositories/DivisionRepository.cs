using System;
using System.Configuration;
using System.Data;
using AirportIQ.Data.SqlServer.Initializers;

namespace AirportIQ.Data.SqlServer.Repositories
{
	public class DivisionRepository : IDivisionRepository
	{
		#region Private Members

		private static string _Schema = ConfigurationManager.AppSettings["SBO.ApplicationSchema"];
		private static string _ShadowSchema = ConfigurationManager.AppSettings["Shadow.ApplicationSchema"];


		#endregion Private Members

		public void SaveDivision(int divisionId, int companyId, string divisionCode, string divisionName, Int16 divisionTypeId, string divisionStatusCode,
								string address1, string address2, string address3, string city, string countrySubdivisionCode, string postalCode, string countryCode,
								string phoneNumber, string faxNumber, string emailAddress, bool fingerprintRequired, bool backgroundCheckRequired, string categoryCode,
								string providerNumber, decimal newBadgeCost, decimal replaceBadgeCost, decimal badgeBillingCost, decimal badgeCreditCost, 
                                string RAMSAgreementNumber, DateTime? RAMSExpirationDate, int userId)
		{
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[CorporateMaintenance.Division.Save]" };
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@DivisionID", ParameterType.DBInteger, divisionId));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@CompanyID", ParameterType.DBInteger, companyId));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@DivisionCode", ParameterType.DBString, divisionCode));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@DivisionName", ParameterType.DBString, divisionName));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@DivisionTypeID", ParameterType.DBString, divisionTypeId));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@DivisionStatusCode", ParameterType.DBString, divisionStatusCode));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@Address1", ParameterType.DBString, address1));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@Address2", ParameterType.DBString, address2));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@Address3", ParameterType.DBString, address3));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@City", ParameterType.DBString, city));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@countrySubdivisionCode", ParameterType.DBString, countrySubdivisionCode));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@PostalCode", ParameterType.DBString, postalCode));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@CountryCode", ParameterType.DBString, countryCode));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@PhoneNumber", ParameterType.DBString, phoneNumber));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@FaxNumber", ParameterType.DBString, faxNumber));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@EmailAddress", ParameterType.DBString, emailAddress));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@FingerprintRequired", ParameterType.DBString, fingerprintRequired));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@BackgroundCheckRequired", ParameterType.DBString, backgroundCheckRequired));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@CategoryCode", ParameterType.DBString, categoryCode));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@ProviderNumber", ParameterType.DBString, providerNumber));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@NewBadgeCost", ParameterType.DBDecimal, newBadgeCost));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@ReplaceBadgeCost", ParameterType.DBDecimal, replaceBadgeCost));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@BadgeBillingCost", ParameterType.DBDecimal, badgeBillingCost));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@BadgeCreditCost", ParameterType.DBDecimal, badgeCreditCost));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@RAMSAgreementNumber", ParameterType.DBString, RAMSAgreementNumber));

            if (RAMSExpirationDate.HasValue)
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@RAMSExpirationDate", ParameterType.DBDateTime, RAMSExpirationDate));

			storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userId));
			
			storedProcedure.ExecuteNonQuery();
		}

		public DataTable LoadDivisionDetails(int divisionID)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[CorporateMaintenance.Division.Load]" };
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@DivisionID", ParameterType.DBString, divisionID));

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

		public DataTable LoadDivisionList()
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[Division.Lists.AllDivisions]" };

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

		public DataTable LoadCompanyDivision(int companyID, int userID)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[Division.Lists.ByCompany]" };
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@CompanyID", ParameterType.DBString, companyID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID));

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

		public DataTable LoadDivisionContacts(int companyID, int divisionID)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[CorporateMaintenance.Contacts.Load]" };

			storedProcedure.Parameters.Add(new StoredProcedureParameter("@CompanyID", ParameterType.DBString, companyID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@DivisionID", ParameterType.DBString, divisionID));

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

        public DataTable LoadContact(int contactID)
        {
            var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[Data.Division.Contact.Load]" };

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@ContactID", ParameterType.DBString, contactID));

            return storedProcedure.ExecuteDataSet();
        }

		public DataTable LoadDivisionTypes()
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[CorporateMaintenance.Lists.Division.Types]" };

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

		public DataTable LoadDivisionCategories()
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[CorporateMaintenance.Lists.Division.Categories]" };

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

		public DataTable LoadDivisionStatuses()
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[CorporateMaintenance.Lists.Division.Statuses]" };

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

		public DataSet loadCompanyReviewDivisionList(Int32? companyID, Int32? divisionID, string companyCriteria, string divisionCriteria, int userID)
		{
			DataSet ret = null;
			var storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = _Schema + ".[Data.CompanyReviewDivision.Load]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyID", ParameterType.DBString, companyID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionID", ParameterType.DBString, divisionID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyCriteria", ParameterType.DBString, companyCriteria));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionCriteria", ParameterType.DBString, divisionCriteria));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBString, userID));
			ret = storedProcedure.ExecuteMultipleDataSet();
			return ret;
		}

		public DataSet loadCompanyReviewDivisionList(Int32? companyID, Int32? divisionID, string companyCriteria, string divisionCriteria, string employeeLNCriteria, int userID)
		{
			DataSet ret = null;
			var storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = _Schema + ".[Data.CompanyReviewDivision.Load]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyID", ParameterType.DBString, companyID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionID", ParameterType.DBString, divisionID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyCriteria", ParameterType.DBString, companyCriteria));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionCriteria", ParameterType.DBString, divisionCriteria));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@employeeLNCriteria", ParameterType.DBString, employeeLNCriteria));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBString, userID));
			ret = storedProcedure.ExecuteMultipleDataSet();
			return ret;
		}

		public DataSet loadCompanyReviewDivisionList(Int32? companyID, Int32? divisionID, string companyCriteria, string divisionCriteria, string employeeLNCriteria, string employeeFNCriteria, int userID)
		{
			DataSet ret = null;
			var storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = _Schema + ".[Data.CompanyReviewDivision.Load]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyID", ParameterType.DBString, companyID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionID", ParameterType.DBString, divisionID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyCriteria", ParameterType.DBString, companyCriteria));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionCriteria", ParameterType.DBString, divisionCriteria));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@employeeLNCriteria", ParameterType.DBString, employeeLNCriteria));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@employeeFNCriteria", ParameterType.DBString, employeeFNCriteria));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBString, userID));
			ret = storedProcedure.ExecuteMultipleDataSet();
			return ret;
		}

		public DataTable LoadCompanyOrDivisionContactsForm(Int32 CompanyID, Int32 DivisionID, int UserID, bool shadowMode, bool? isNewDivisionOnly)
		{
			DataTable ret = null;
			var storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = (shadowMode ? _ShadowSchema : _Schema) + ".[Data.CompanyOrDivisionContactsForm.Load]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyID", ParameterType.DBString, CompanyID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionID", ParameterType.DBString, DivisionID));

            if (isNewDivisionOnly.HasValue)
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@isNewDivisionOnly", ParameterType.DBBoolean, isNewDivisionOnly.Value));

			storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));
			ret = storedProcedure.ExecuteDataSet();
			return ret;
		}

        public void SaveCompanyOrDivisionContacts(DataTable contactsToSave, int UserID, bool shadowMode, bool? isNewDivisionOnly)
		{
			var storedProcedure = new StoredProcedure();

			storedProcedure.StoredProcedureName = (shadowMode ? _ShadowSchema : _Schema) + ".[Data.CompanyOrDivisionContactsForm.Save]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@DivisionContactTypes", ParameterType.Structured, contactsToSave));

            if (isNewDivisionOnly.HasValue)
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@isNewDivisionOnly", ParameterType.DBBoolean, isNewDivisionOnly.Value));

			storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));
			
			storedProcedure.ExecuteNonQuery();
		}

		public void SaveNewDivision(DataSet divisionToSave, int UserID)
		{
			var storedProcedure = new StoredProcedure();

			storedProcedure.StoredProcedureName = _Schema + ".[Data.NewDivision.Save]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@DivisionTypes", ParameterType.DBString, (divisionToSave.Tables[0].Rows.Count > 0) ? divisionToSave.Tables[0] : null));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@AgreementsTableTypes", ParameterType.DBString, (divisionToSave.Tables[1].Rows.Count > 0) ? divisionToSave.Tables[1] : null));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@DivisionContactTypes", ParameterType.DBString, (divisionToSave.Tables[2].Rows.Count > 0) ? divisionToSave.Tables[2] : null));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));
			
			storedProcedure.ExecuteNonQuery();
		}

		public void SaveDivisionChanges(DataTable divisionchangesToSave, int UserID)
		{
			var storedProcedure = new StoredProcedure();

			storedProcedure.StoredProcedureName = _Schema + ".[Data.DivisionChanges.Save]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@DivisionChangesTableTypes", ParameterType.Structured, divisionchangesToSave));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));

			storedProcedure.ExecuteNonQuery();
		}

		public DataSet LoadTransferEmployeeForm(Int32 DivisionID, Int32 PersonID, int UserID)
		{
			DataSet ret = null;
			var storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = _Schema + ".[Data.TransferEmployeeForm.Load]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionID", ParameterType.DBString, DivisionID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@personID", ParameterType.DBString, PersonID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));
			ret = storedProcedure.ExecuteMultipleDataSet();
			return ret;
		}

        public void SaveTransferEmployeeForm(DataTable EmployeeToSave, DataTable specialAccess, int UserID)
		{
			DataSet ret = null;
			var storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = _Schema + ".[Data.TransferEmployeeForm.Save]";

			storedProcedure.Parameters.Add(new StoredProcedureParameter("@TransferTableTypes", ParameterType.Structured, EmployeeToSave));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@SpecialAccessTableTypes", ParameterType.Structured, specialAccess));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));

			ret = storedProcedure.ExecuteMultipleDataSet();
		}

        public void SaveTransferEmployeeForm(DataTable EmployeeToSave, DataTable specialAccess, int UserID, Boolean tranAllSpecial, Boolean tranAllIcons)
		{
			DataSet ret = null;
			var storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = _Schema + ".[Data.TransferEmployeeForm.Save]";

			storedProcedure.Parameters.Add(new StoredProcedureParameter("@TransferTableTypes", ParameterType.Structured, EmployeeToSave));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@SpecialAccessTableTypes", ParameterType.Structured, specialAccess));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@TranAllSpecialAccess", ParameterType.DBBoolean , tranAllSpecial));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@TranAllIcons", ParameterType.DBBoolean, tranAllIcons));

			ret = storedProcedure.ExecuteMultipleDataSet();
		}

		public void ShadowSaveCompanyOrDivisionContacts(DataTable contactsToSave, int UserID)
		{
			var storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = _ShadowSchema + ".[Data.CompanyOrDivisionContactsForm.Save]";

			storedProcedure.Parameters.Add(new StoredProcedureParameter("@DivisionContactTypes", ParameterType.Structured, contactsToSave));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));

			storedProcedure.ExecuteNonQuery();
		}

		public void ShadowSaveDivisionChanges(DataTable divisionchangesToSave, int UserID)
		{
			DataSet ret = null;
			var storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = _ShadowSchema + ".[Data.DivisionChanges.Save]";

			storedProcedure.Parameters.Add(new StoredProcedureParameter("@DivisionChangesTableTypes", ParameterType.Structured, divisionchangesToSave));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));
			ret = storedProcedure.ExecuteMultipleDataSet();
		}

		public DataTable ShadowLoadUniqueCompanyOrDivisionContactsForm(Int32 CompanyID, Int32 DivisionID, int UserID)
		{
			DataTable ret = null;
			var storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = _ShadowSchema + ".[Data.CompanyOrDivisionContactsForm.Load]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyID", ParameterType.DBString, CompanyID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionID", ParameterType.DBString, DivisionID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));
			ret = storedProcedure.ExecuteDataSet();
			return ret;
		}

		public DataTable LoadUniqueCompanyOrDivisionContactsForm(Int32 CompanyID, Int32 DivisionID, int UserID)
		{
			DataTable ret = null;
			var storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = _Schema + ".[Data.CompanyOrDivisionContactsForm.UniqueLoad]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyID", ParameterType.DBString, CompanyID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionID", ParameterType.DBString, DivisionID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));
			ret = storedProcedure.ExecuteDataSet();
			return ret;
		}

		public void ClearAllShadowRecords(Int32 companyID)
		{
			DataSet ret = null;
			var storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = _ShadowSchema + ".[Data.DeleteShadowRecords]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyID", ParameterType.DBInteger, companyID));
			ret = storedProcedure.ExecuteMultipleDataSet();
		}

		public DataTable Search(string searchTerm, int userID)
		{
			DataTable ret = null;

			var storedProcedure = new StoredProcedure();

			storedProcedure.StoredProcedureName = _Schema + ".[Data.Division.Search]";

			storedProcedure.Parameters.Add(new StoredProcedureParameter("@CompanySearchTerm", ParameterType.DBString, searchTerm));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID));
			
			ret = storedProcedure.ExecuteDataSet();

			return ret;
		}

        public DataTable Search(string searchTerm, int companyID, int userID)
        {
            DataTable ret = null;

            var storedProcedure = new StoredProcedure();

            storedProcedure.StoredProcedureName = _Schema + ".[Data.Division.SearchByCompanyID]";

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@DivisionSearchTerm", ParameterType.DBString, searchTerm));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@CompanyID", ParameterType.DBInteger, companyID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID));

            ret = storedProcedure.ExecuteDataSet();

            return ret;
        }

        public DataTable Search(string searchTerm, string companyCode, int userID)
        {
            DataTable ret = null;

            var storedProcedure = new StoredProcedure();

            storedProcedure.StoredProcedureName = _Schema + ".[Data.Division.SearchByCompanyCode]";

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@DivisionSearchTerm", ParameterType.DBString, searchTerm));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@CompanyCode", ParameterType.DBString, companyCode));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID));

            ret = storedProcedure.ExecuteDataSet();

            return ret;
        }

        public DataTable LoadLocations(int divisionID, string facilityCode)
        {
            var storedProcedure = new StoredProcedure();

            storedProcedure.StoredProcedureName = _Schema + ".[Data.Division.Locations]";

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@DivisionID", ParameterType.DBInteger, divisionID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@FacilityCode", ParameterType.DBString, facilityCode));

            return storedProcedure.ExecuteDataSet();
        }

        public DataTable LoadNotes(int divisionId)
        {
            var storedProcedure = new StoredProcedure();

            storedProcedure.StoredProcedureName = _Schema + ".[Data.Division.Notes]";

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@DivisionID", ParameterType.DBInteger, divisionId));

            return storedProcedure.ExecuteDataSet();
        }
	}
}