using System;
using System.Data;
using AirportIQ.Data;
using AirportIQ.Data.SqlServer.Repositories;
using AirportIQ.Domain.Contracts;

namespace AirportIQ.Domain.Services
{
	public class CompanyServices : ICompany
	{
		#region "Private Variables"

		private readonly ICompanyRepository companyRepository;
		//private readonly CacheServices _CS = new CacheServices();

		#endregion "Private Variables"

		#region "Constructors"

		public CompanyServices() : this(new CompanyRepository()) { }

		public CompanyServices(ICompanyRepository companyRepository)
		{
			if (companyRepository == null) throw new ArgumentNullException("companyRepository");
			this.companyRepository = companyRepository;
		}

		#endregion "Constructors"

		#region "Public Methods"

		public void AcceptContract(int stateID, int userID)
		{
			this.companyRepository.AcceptContract(stateID, userID);
		}

		public void RejectContract(int stateID, int userID)
		{
			this.companyRepository.RejectContract(stateID, userID);
		}

		public DataSet LoadContractVerificationForm(Int32 CompanyID, Int32 DivisionID, Int32 AgreementID, Int32 IndustryTypeID, int UserID)
		{
			return this.companyRepository.LoadContractVerificationForm(CompanyID, DivisionID, AgreementID, IndustryTypeID, UserID);
		}

		public DataTable ListContractVerificationForm(int UserID)
		{
			return this.companyRepository.ListContractVerificationForm(UserID);
		}

		public DataSet LoadCompanyEnrollmentForm(Int32 CompanyID, Int32 DivisionID, Int32 AgreementID, int UserID, bool shadowMode)
		{
			return this.companyRepository.LoadCompanyEnrollmentForm(CompanyID, DivisionID, AgreementID, UserID, shadowMode);
		}

        public DataSet LoadAgreementForm(Int32 AgreementID, Int32 DivisionID, int UserID, int divisionTypeId = -1)
		{
			return this.companyRepository.LoadAgreementForm(AgreementID, DivisionID, UserID, divisionTypeId);
		}

        public DataSet LoadAccessDefaultForm(Int32 AgreementID, int UserID, bool shadowMode = false, int divisionTypeId = -1)
		{
			return this.companyRepository.LoadAccessDefaultForm(AgreementID, UserID, shadowMode, divisionTypeId);
		}

        public DataSet LoadAllowableBadgeColorsForm(Int32 AgreementID, int UserID, bool shadowMode = false, int divisionTypeId = -1)
		{
			return this.companyRepository.LoadAllowableBadgeColorsForm(AgreementID, UserID, shadowMode, divisionTypeId);
		}

        public DataSet LoadAllowableIconsForm(Int32 AgreementID, int UserID, bool shadowMode = false)
        {
            return this.companyRepository.LoadAllowableIconsForm(AgreementID, UserID, shadowMode);
        }

		public DataSet LoadRequirementDocuments(Int32 CompanyID, Int32 DivisionID, Int32 AgreementID, int UserID, bool shadowMode)
		{
			return this.companyRepository.LoadRequirementDocuments(CompanyID, DivisionID, AgreementID, UserID, shadowMode);
		}

		public DataSet LoadAdditionalAccessRequestForm(Int32 CompanyID, Int32 DivisionID, Int32 LocationID, int UserID)
		{
			return this.companyRepository.LoadAdditionalAccessRequestForm(CompanyID, DivisionID, LocationID, UserID);
		}

		public DataSet LoadExtendContractForm(Int32 CompanyID, Int32 DivisionID, Int32 AgreementID, int UserID)
		{
			return this.companyRepository.LoadExtendContractForm(CompanyID, DivisionID, AgreementID, UserID);
		}

		public DataSet LoadSAFEForm(Int32 CompanyID, Int32 DivisionID, int UserID)
		{
			return this.companyRepository.LoadSAFEForm(CompanyID, DivisionID, UserID);
		}

		public void SaveContractVerificationForm(DataSet contractVerificationFormToSave, int UserID, out string ID)
		{
			this.companyRepository.SaveContractVerificationForm(contractVerificationFormToSave, UserID, out ID);
		}

		public void SaveCompanyEnrollmentForm(DataSet companyEnrollmentFormToSave, int UserID, bool shadowMode)
		{
			this.companyRepository.SaveCompanyEnrollmentForm(companyEnrollmentFormToSave, UserID, shadowMode);
		}

        public void DeleteJobRoles(DataTable jobRoles, bool shadowMode)
        {
            this.companyRepository.DeleteJobRoles(jobRoles, shadowMode);
        }

        public void DeleteLocations(DataTable locations, bool shadowMode)
        {
            this.companyRepository.DeleteLocations(locations, shadowMode);
        }

		public void SaveRequirementDocuments(DataTable requirementdocumentsToSave, int UserID)
		{
			this.companyRepository.SaveRequirementDocuments(requirementdocumentsToSave, UserID);
		}

		public void SaveAdditionalAccessRequestForm(DataTable additionalAccessRequestFormToSave, int UserID)
		{
			this.companyRepository.SaveAdditionalAccessRequestForm(additionalAccessRequestFormToSave, UserID);
		}

		public void SaveExtendContractForm(DataTable extendContractFormToSave, int UserID)
		{
			this.companyRepository.SaveExtendContractForm(extendContractFormToSave, UserID);
		}

		public void SaveDivisionStatus(Int32 CompanyID, DataTable divisionStatusToSave, int UserID)
		{
			this.companyRepository.SaveDivisionStatus(CompanyID, divisionStatusToSave, UserID);
		}

		[Obsolete("Any call to this function is not facility safe")]
		public DataTable LoadCompanyList()
		{
			const string COMPANY_LIST = "CompanyList";
			DataTable result = new DataTable();
			//result = _CS.retrievefromCache(COMPANY_LIST);
			//if (result == null)
			//{
				result = this.companyRepository.LoadCompanyList();
                //if (result.Rows.Count > 0)
                //{
                //    _CS.addtoCache(COMPANY_LIST, result);
                //}
			//}

			return result;
		}

		public DataTable LoadCompanyList(int userID)
		{
			return this.companyRepository.LoadCompanyList(userID);
		}

		public DataTable LoadCompany(int companyID)
		{
			return this.companyRepository.LoadCompany(companyID);
		}

        public DataTable LoadCompaniesWithActiveSIDADivisions(int userID)
        {
            return this.companyRepository.LoadCompaniesWithActiveSIDADivisions(userID);
        }

        public DataTable SearchCompaniesWithActiveSIDADivisions(string companyCode, int userID)
        {
            return this.companyRepository.SearchCompaniesWithActiveSIDADivisions(companyCode, userID);
        }

		public DataTable LoadCompanyIndustryTypes(int companyID)
		{
			return this.companyRepository.LoadCompanyIndustryTypes(companyID);
		}

		public void Save(int companyID, string corporationName, string dBAName, string badgeName, string providerNumber, string rAMSAgreementNumber, DateTime? rAMSExpirationDate, DataTable industryTypes, bool BTRCRequired, bool insuranceRequired, int coordinatorID, int UserID)
		{
			this.companyRepository.Save(companyID, corporationName, dBAName, badgeName, providerNumber, rAMSAgreementNumber, rAMSExpirationDate, industryTypes, BTRCRequired, insuranceRequired, coordinatorID, UserID);
		}

		public DataSet loadCompanyList(Int32? companyID, String companyCriteria, String divisionCriteria, Int32 userID)
		{
			return this.companyRepository.loadCompanyList(companyID, companyCriteria, divisionCriteria, userID);
		}

		public DataSet GetCompanyCodeCompany(String companyCode, Int32 userID)
		{
			return this.companyRepository.GetCompanyCodeCompany(companyCode, userID);
		}

		public DataSet GetCompanyCodeCompany(String companyCode, String divisionCode, Int32 userID)
		{
			return this.companyRepository.GetCompanyCodeCompany(companyCode, divisionCode, userID);
		}

		#endregion "Public Methods"

		public void ShadowSaveContractVerificationForm(DataSet contractVerificationFormToSave, int userID, String actionCode, out string ID)
		{
			this.companyRepository.ShadowSaveContractVerificationForm(contractVerificationFormToSave, userID, actionCode, out ID);
		}

        public void SaveContractVerificationFormContacts(DataTable contacts, bool isShadow, bool? isNewDivisionOnly)
        {
            this.companyRepository.SaveContractVerificationFormContacts(contacts, isShadow, isNewDivisionOnly);
        }

		public void ShadowSaveRequirementDocuments(DataTable requirementdocumentsToSave, int userID)
		{
			this.companyRepository.ShadowSaveRequirementDocuments(requirementdocumentsToSave, userID);
		}

		public DataSet ShadowGetCompanyCodeCompany(String companyCode, Int32 userID)
		{
			return this.companyRepository.ShadowGetCompanyCodeCompany(companyCode, userID);
		}

		public DataSet ShadowGetCompanyCodeCompany(String companyCode, String divisionCode, Int32 userID)
		{
			return this.companyRepository.ShadowGetCompanyCodeCompany(companyCode, divisionCode, userID);
		}

		public DataSet ShadowGetCompanyDivisionLoad(Int32 companyID, Int32? divisionID, int userID)
		{
			return this.companyRepository.ShadowGetCompanyDivisionLoad(companyID, divisionID, userID);
		}

        public DataTable Search(string searchTerm, int userID)
        {
            return this.companyRepository.Search(searchTerm, userID);
        }

        public DataTable Search(string searchTerm, int userID, bool excludeShadow)
        {
            return this.companyRepository.Search(searchTerm, userID, excludeShadow);
        }

        public DataTable SearchForLookahead(string searchTerm, int userID)
        {
            return this.companyRepository.SearchForLookahead(searchTerm, userID);
        }
    }
}