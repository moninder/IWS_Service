using System;
using System.Data;

namespace AirportIQ.Data
{
	public interface ICompanyRepository
	{
		void AcceptContract(int stateID, int userID);

		void RejectContract(int stateID, int userID);

		DataSet LoadContractVerificationForm(Int32 CompanyID, Int32 DivisionID, Int32 AgreementID, Int32 IndustryTypeID, int UserID); // to load CVF data

		void SaveContractVerificationForm(DataSet contractVerificationFormToSave, int userID, out string ID); // to save CVF data

		DataTable ListContractVerificationForm(int UserID);

		DataSet LoadCompanyEnrollmentForm(Int32 CompanyID, Int32 DivisionID, Int32 AgreementID, int UserID, bool shadowMode); // to load CVF data

        DataSet LoadAgreementForm(Int32 AgreementID, Int32 DivisionID, int userID, int divisionTypeId = -1); // to load Agreement data

        DataSet LoadAccessDefaultForm(Int32 AgreementID, int userID, bool ShadowMode = false, int divisionTypeId = -1); // to load Access Default data

        DataSet LoadAllowableBadgeColorsForm(Int32 AgreementID, int UserID, bool shadowMode = false, int divisionTypeId = -1);

		DataSet LoadAllowableIconsForm(Int32 AgreementID, int UserID, bool shadowMode = false);

		DataSet LoadRequirementDocuments(Int32 CompanyID, Int32 DivisionID, Int32 AgreementID, int UserID, bool shadowMode); // to load Requirements data

		void SaveRequirementDocuments(DataTable requirementdocumentsToSave, int userID); // to save Requirements Documents data

		DataSet LoadAdditionalAccessRequestForm(Int32 CompanyID, Int32 DivisionID, Int32 LocationID, int UserID); // to load Additional Access Request Form data

		void SaveAdditionalAccessRequestForm(DataTable additionalAccessRequestFormToSave, int UserID); // to save Additional Access Request Form  data

		DataSet LoadExtendContractForm(Int32 CompanyID, Int32 DivisionID, Int32 AgreementID, int UserID); // to load Extend Contract Form data

		void SaveExtendContractForm(DataTable extendContractFormToSave, int UserID); // to save Extend Contract Form  data

		void SaveDivisionStatus(Int32 CompanyID, DataTable divisionStatusToSave, int UserID);

		void SaveCompanyEnrollmentForm(DataSet companyEnrollmentFormToSave, int UserID, bool shadowMode); // to save CVF data

        void DeleteJobRoles(DataTable jobRoles, bool shadowMode);
        void DeleteLocations(DataTable locations, bool shadowMode);

		[Obsolete("Any call to this function is not facility safe")]
		DataTable LoadCompanyList();

		DataTable LoadCompanyList(int userID);

		DataTable LoadCompany(int companyID);

        DataTable LoadCompaniesWithActiveSIDADivisions(int userID);

        DataTable SearchCompaniesWithActiveSIDADivisions(string companyCode, int userID);

		DataTable LoadCompanyIndustryTypes(int companyID);
		
		DataSet LoadSAFEForm(Int32 CompanyID, Int32 DivisionID, Int32 UserID);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="companyID"></param>
		/// <param name="corporationName"></param>
		/// <param name="dBAName"></param>
		/// <param name="badgeName"></param>
		/// <param name="providerNumber"></param>
		/// <param name="rAMSAgreementNumber"></param>
		/// <param name="rAMSExpirationDate"></param>
		/// <param name="industryTypes"></param>
		/// <remarks>
		/// JBienvenu 2013-01-10 19303 added badge coordinator parameter
		/// </remarks>
		void Save(int companyID, string corporationName, string dBAName, string badgeName, string providerNumber, string rAMSAgreementNumber, DateTime? rAMSExpirationDate, DataTable industryTypes, bool BTRCRequired, bool insuranceRequired, int coordinatorID, int UserID);

		// jem---------------------
		DataSet loadCompanyList(Int32? CompanyID, String CompanyCriteria, String DivisionCriteria, Int32 UserId); // to load a list of companies
		DataSet GetCompanyCodeCompany(String CompanyCode, Int32 UserId); // to load a list of companies
		DataSet GetCompanyCodeCompany(String CompanyCode, String DivisionCode, Int32 UserId); // to load a list of companies

		void ShadowSaveContractVerificationForm(DataSet contractVerificationFormToSave, int userID, String actionCode, out string ID); // to save CVF data
        void SaveContractVerificationFormContacts(DataTable contacts, bool isShadow, bool? isNewDivisionOnly);
		void ShadowSaveRequirementDocuments(DataTable requirementdocumentsToSave, int userID); // to save Requirements Documents data
		DataSet ShadowGetCompanyCodeCompany(String CompanyCode, Int32 UserId); // to load a list of companies
		DataSet ShadowGetCompanyCodeCompany(String CompanyCode, String DivisionCode, Int32 UserId); // to load a list of companies
		DataSet ShadowGetCompanyDivisionLoad(Int32 companyID, Int32? divisionID, int userID); // to load a list of companies
		DataTable Search(string searchTerm, int userID);
        DataTable Search(string searchTerm, int userID, bool excludeShadow);
        DataTable SearchForLookahead(string searchTerm, int userID);
    }
}