using System;
using System.Data;

namespace AirportIQ.Domain.Contracts
{
	public interface ICompany
	{
		void AcceptContract(int stateID, int userID);

		void RejectContract(int stateID, int userID);

		DataSet LoadContractVerificationForm(Int32 CompanyID, Int32 DivisionID, Int32 AgreementID, Int32 IndustryTypeID, int UserID); // to load CVF data

		DataSet LoadCompanyEnrollmentForm(Int32 CompanyID, Int32 DivisionID, Int32 AgreementID, int UserID, bool shadowMode); // to load Enrollment data

        DataSet LoadAgreementForm(Int32 AgreementID, Int32 DivisionID, int UserID, int divisionTypeId = -1); // to load Agreement data

        DataSet LoadAccessDefaultForm(Int32 AgreementID, int UserID, bool ShadowMode = false, int divisionTypeId = -1); // to load Access Default data

        DataSet LoadAllowableBadgeColorsForm(Int32 AgreementID, int UserID, bool ShadowMode = false, int divisionTypeId = -1);

        DataSet LoadAllowableIconsForm(Int32 AgreementID, int UserID, bool shadowMode = false);

		DataSet LoadRequirementDocuments(Int32 CompanyID, Int32 DivisionID, Int32 AgreementID, int UserID, bool shadowMode); // to load Requirements data

		DataSet LoadAdditionalAccessRequestForm(Int32 CompanyID, Int32 DivisionID, Int32 LocationID, int UserID); // to load Additional Access Request Form data

		DataSet LoadExtendContractForm(Int32 CompanyID, Int32 DivisionID, Int32 AgreementID, int UserID); // to load Extend Contract Form data

		[Obsolete("Any call to this function is not facility safe")]
		DataTable LoadCompanyList(); // to load Company lookup

		DataTable LoadCompanyList(int userID);

        DataTable LoadCompaniesWithActiveSIDADivisions(int userID);

        DataTable SearchCompaniesWithActiveSIDADivisions(string companyCode, int userID);

		DataTable LoadCompany(int companyID);

		DataTable LoadCompanyIndustryTypes(int companyID);

		DataTable ListContractVerificationForm(int UserID);

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

		void SaveContractVerificationForm(DataSet contractVerificationFormToSave, int userID, out string ID); // to save CVF data

		void SaveCompanyEnrollmentForm(DataSet companyEnrollmentFormToSave, int userID, bool shadowMode); // to save CVF data

        void DeleteJobRoles(DataTable jobRoles, bool shadowMode);
        void DeleteLocations(DataTable locations, bool shadowMode);

		void SaveRequirementDocuments(DataTable requirementdocumentsToSave, int userID); // to save Requirements Documents data

		void SaveAdditionalAccessRequestForm(DataTable additionalAccessRequestFormToSave, int userID); // to save Additional Access Request Form  data

		void SaveExtendContractForm(DataTable extendContractFormToSave, int userID); // to save Extend Contract Form  data

		void SaveDivisionStatus(Int32 CompanyID, DataTable divisionStatusToSave, int userID);

		DataSet LoadSAFEForm(Int32 CompanyID, Int32 DivisionID, Int32 UserID);

		DataSet loadCompanyList(Int32? CompanyID, String CompanyCriteria, String DivisionCriteria, Int32 UserId); // to load a list of companies // jem
		DataSet GetCompanyCodeCompany(String CompanyCode, Int32 UserId); // to load a list of companies
		DataSet GetCompanyCodeCompany(String CompanyCode, String DivisionCode, Int32 UserId); // to load a list of companies

		void ShadowSaveContractVerificationForm(DataSet contractVerificationFormToSave, int userID, String actionCode, out string ID); // to save CVF data
        void SaveContractVerificationFormContacts(DataTable contacts, bool isShadow, bool? isNewDivisionOnly);
		void ShadowSaveRequirementDocuments(DataTable requirementdocumentsToSave, int userID); // to save Requirements Documents data
		DataSet ShadowGetCompanyCodeCompany(String CompanyCode, Int32 UserId); // to load a list of companies
		DataSet ShadowGetCompanyCodeCompany(String CompanyCode, String DivisionCode, Int32 UserId); // to load a list of companies
		DataSet ShadowGetCompanyDivisionLoad(Int32 companyID, Int32? divisionID, int UserID);
        DataTable Search(string searchTerm, int userID);
        DataTable Search(string searchTerm, int userID, bool excludeShadow);
        DataTable SearchForLookahead(string searchTerm, int userID);
        
	}
}