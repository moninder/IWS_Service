using System;
using System.Configuration;
using System.Data;
using AirportIQ.Data.SqlServer.Initializers;

namespace AirportIQ.Data.SqlServer.Repositories
{
	public class CompanyRepository : ICompanyRepository
	{
		#region Private Variables

		private string schema = ConfigurationManager.AppSettings["SBO.ApplicationSchema"].ToString();
		private static string _ShadowSchema = ConfigurationManager.AppSettings["Shadow.ApplicationSchema"];

		#endregion Private Variables

		public void AcceptContract(int stateID, int userID)
		{
			var storedProcedure = new StoredProcedure();

			storedProcedure.StoredProcedureName = schema + ".[Data.ContractVerification.Accept]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@StateID", ParameterType.DBInteger, stateID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID));
			
			storedProcedure.ExecuteNonQuery();
		}

		public void RejectContract(int stateID, int userID)
		{
			var storedProcedure = new StoredProcedure();

			storedProcedure.StoredProcedureName = schema + ".[Data.ContractVerification.Reject]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@StateID", ParameterType.DBInteger, stateID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID));
			
			storedProcedure.ExecuteNonQuery();
		}

		DataSet ICompanyRepository.LoadContractVerificationForm(Int32 CompanyID, Int32 DivisionID, Int32 AgreementID, Int32 IndustryTypeID, int UserID)
		{
			DataSet ret = null;
			var storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = schema + ".[Data.ContractVerificationForm.Load]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyID", ParameterType.DBString, CompanyID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionID", ParameterType.DBString, DivisionID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@agreementID", ParameterType.DBString, AgreementID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@industryTypeID", ParameterType.DBString, IndustryTypeID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));
			ret = storedProcedure.ExecuteMultipleDataSet();
			return ret;
		}

		DataTable ICompanyRepository.ListContractVerificationForm(int UserID)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = schema + ".[Data.ContractVerificationFormList.Load]" };
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));
			result = storedProcedure.ExecuteDataSet();
			return result;
		}

		public void SaveContractVerificationForm(DataSet contractVerificationFormToSave, int UserID, out string ID)
		{
			var storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = schema + ".[Data.ContractVerificationForm.Save]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@CompanyTypes", ParameterType.Structured, (contractVerificationFormToSave.Tables[0].Rows.Count > 0) ? contractVerificationFormToSave.Tables[0] : null));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@CompanyIndustryTypes", ParameterType.Structured, (contractVerificationFormToSave.Tables[1].Rows.Count > 0) ? contractVerificationFormToSave.Tables[1] : null));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@DivisionTypes", ParameterType.Structured, (contractVerificationFormToSave.Tables[2].Rows.Count > 0) ? contractVerificationFormToSave.Tables[2] : null));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@AgreementsTableTypes", ParameterType.Structured, (contractVerificationFormToSave.Tables[3].Rows.Count > 0) ? contractVerificationFormToSave.Tables[3] : null));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@ServicesProvidedTableTypes", ParameterType.Structured, (contractVerificationFormToSave.Tables[4].Rows.Count > 0) ? contractVerificationFormToSave.Tables[4] : null));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@AgreementJobRolesTableTypes", ParameterType.Structured, (contractVerificationFormToSave.Tables[5].Rows.Count > 0) ? contractVerificationFormToSave.Tables[5] : null));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@AgreementLocationsTableTypes", ParameterType.Structured, (contractVerificationFormToSave.Tables[6].Rows.Count > 0) ? contractVerificationFormToSave.Tables[6] : null));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));
			storedProcedure.ExecuteDataSetWithIDOutputParam(out ID);
		}

		public void SaveCompanyEnrollmentForm(DataSet companyEnrollmentFormToSave, int UserID, bool shadowMode)
		{
			var xSchema = shadowMode ? _ShadowSchema : schema;

			var storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = xSchema + ".[Data.CompanyEnrollmentForm.Save]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@CompanyTypes", ParameterType.Structured, (companyEnrollmentFormToSave.Tables[0].Rows.Count > 0) ? companyEnrollmentFormToSave.Tables[0] : null));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@CompanyIndustryTypes", ParameterType.Structured, (companyEnrollmentFormToSave.Tables[1].Rows.Count > 0) ? companyEnrollmentFormToSave.Tables[1] : null));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@DivisionTypes", ParameterType.Structured, (companyEnrollmentFormToSave.Tables[2].Rows.Count > 0) ? companyEnrollmentFormToSave.Tables[2] : null));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@AgreementsTableTypes", ParameterType.Structured, (companyEnrollmentFormToSave.Tables[3].Rows.Count > 0) ? companyEnrollmentFormToSave.Tables[3] : null));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@ServicesProvidedTableTypes", ParameterType.Structured, (companyEnrollmentFormToSave.Tables[4].Rows.Count > 0) ? companyEnrollmentFormToSave.Tables[4] : null));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@AgreementJobRolesTableTypes", ParameterType.Structured, (companyEnrollmentFormToSave.Tables[5].Rows.Count > 0) ? companyEnrollmentFormToSave.Tables[5] : null));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@AgreementLocationsTableTypes", ParameterType.Structured, (companyEnrollmentFormToSave.Tables[6].Rows.Count > 0) ? companyEnrollmentFormToSave.Tables[6] : null));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@AgreementAccessDefaultsTableTypes", ParameterType.Structured, (companyEnrollmentFormToSave.Tables[7].Rows.Count > 0) ? companyEnrollmentFormToSave.Tables[7] : null));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@AllowableBadgeColorsTableTypes", ParameterType.Structured, (companyEnrollmentFormToSave.Tables[8].Rows.Count > 0) ? companyEnrollmentFormToSave.Tables[8] : null));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@AllowableIconsTableTypes", ParameterType.Structured, (companyEnrollmentFormToSave.Tables[9].Rows.Count > 0) ? companyEnrollmentFormToSave.Tables[9] : null));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));
			
			storedProcedure.ExecuteNonQuery();
		}

		public DataSet LoadCompanyEnrollmentForm(Int32 CompanyID, Int32 DivisionID, Int32 AgreementID, int UserID, bool shadowMode)
		{
			var xSchema = shadowMode ? _ShadowSchema : schema;

			DataSet ret = null;
			var storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = xSchema + ".[Data.CompanyEnrollmentForm.Load]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyID", ParameterType.DBString, CompanyID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionID", ParameterType.DBString, DivisionID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@agreementID", ParameterType.DBString, AgreementID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));
			ret = storedProcedure.ExecuteMultipleDataSet();
			return ret;
		}

        public void DeleteJobRoles(DataTable jobRoles, bool shadowMode)
        {
            var xSchema = shadowMode ? _ShadowSchema : schema;

            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = xSchema + ".[Data.CompanyEnrollmentForm.DeleteJobRoles]";

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@AgreementJobRolesTableTypes", ParameterType.Structured, jobRoles));

            storedProcedure.ExecuteNonQuery();
        }

        public void DeleteLocations(DataTable locations, bool shadowMode)
        {
            var xSchema = shadowMode ? _ShadowSchema : schema;

            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = xSchema + ".[Data.CompanyEnrollmentForm.DeleteLocations]";

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@AgreementLocationsTableTypes", ParameterType.Structured, locations));

            storedProcedure.ExecuteNonQuery();
        }

        public DataSet LoadAgreementForm(Int32 AgreementID, Int32 DivisionID, int UserID, int divisionTypeId = -1)
		{
			DataSet ret = null;
			var storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = schema + ".[Data.AgreementForm.Load]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@agreementID", ParameterType.DBString, AgreementID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionID", ParameterType.DBString, DivisionID));

            if (divisionTypeId != -1)
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@DivisionTypeId", ParameterType.DBInteger, divisionTypeId));

			storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));
			ret = storedProcedure.ExecuteMultipleDataSet();
			return ret;
		}

		public DataSet LoadAccessDefaultForm(Int32 AgreementID, int UserID, bool shadowMode=false, int divisionTypeId = -1)
		{
			var xSchema = shadowMode ? _ShadowSchema : schema;

			DataSet ret = null;

			var storedProcedure = new StoredProcedure();

			storedProcedure.StoredProcedureName = xSchema + ".[Data.DefaultAccessForm.Load]";

			storedProcedure.Parameters.Add(new StoredProcedureParameter("@agreementID", ParameterType.DBString, AgreementID));

            if (divisionTypeId != -1)
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@DivisionTypeId", ParameterType.DBInteger, divisionTypeId));

			storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));
			
			ret = storedProcedure.ExecuteMultipleDataSet();
			
			return ret;
		}

        public DataSet LoadAllowableBadgeColorsForm(Int32 AgreementID, int UserID, bool shadowMode = false, int divisionTypeId = -1)
		{
			var xSchema = shadowMode ? _ShadowSchema : schema;

			DataSet ret = null;

			var storedProcedure = new StoredProcedure();

			storedProcedure.StoredProcedureName = xSchema + ".[Data.AllowableBadgeColorsForm.Load]";

			storedProcedure.Parameters.Add(new StoredProcedureParameter("@AgreementID", ParameterType.DBString, AgreementID));

            if (divisionTypeId != -1)
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@DivisionTypeID", ParameterType.DBInteger, divisionTypeId));

			storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));

			ret = storedProcedure.ExecuteMultipleDataSet();

			return ret;
		}

        public DataSet LoadAllowableIconsForm(Int32 AgreementID, int UserID, bool shadowMode = false)
        {
            var xSchema = shadowMode ? _ShadowSchema : schema;

            DataSet ret = null;

            var storedProcedure = new StoredProcedure();

            storedProcedure.StoredProcedureName = xSchema + ".[Data.AllowableIconsForm.Load]";

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@AgreementID", ParameterType.DBString, AgreementID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));

            ret = storedProcedure.ExecuteMultipleDataSet();

            return ret;
        }

		public DataSet LoadRequirementDocuments(Int32 CompanyID, Int32 DivisionID, Int32 AgreementID, int UserID, bool shadowMode)
		{
			DataSet ret = null;

			var xSchema = shadowMode ? _ShadowSchema : schema;
			var storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = xSchema + ".[Data.RequirementsDocuments.Load]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyID", ParameterType.DBString, CompanyID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionID", ParameterType.DBString, DivisionID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@agreementID", ParameterType.DBString, AgreementID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));
			ret = storedProcedure.ExecuteMultipleDataSet();
			return ret;
		}

		public void SaveRequirementDocuments(DataTable requirementdocumentsToSave, int UserID)
		{
			var storedProcedure = new StoredProcedure();

			storedProcedure.StoredProcedureName = schema + ".[Data.RequirementsDocuments.Save]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@RequirementDocumentsTableTypes", ParameterType.Structured, requirementdocumentsToSave));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));

			storedProcedure.ExecuteNonQuery();
		}

		public DataSet LoadAdditionalAccessRequestForm(Int32 CompanyID, Int32 DivisionID, Int32 LocationID, int UserID)
		{
			DataSet ret = null;
			var storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = schema + ".[Data.AdditionalAccessRequestForm.Load]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyID", ParameterType.DBString, CompanyID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionID", ParameterType.DBString, DivisionID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@locationID", ParameterType.DBString, LocationID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));
			ret = storedProcedure.ExecuteMultipleDataSet();
			return ret;
		}

		public void SaveAdditionalAccessRequestForm(DataTable additionalAccessRequestFormToSave, int UserID)
		{
			DataSet ret = null;
			var storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = schema + ".[Data.AdditionalAccessRequestForm.Save]";
			//rguidi 3/4/2013 #20202
			//storedProcedure.Parameters.Add(new StoredProcedureParameter("@AccessRequestsTypes", ParameterType.DBString, additionalAccessRequestFormToSave));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@AccessRequestsTypes", ParameterType.Structured, additionalAccessRequestFormToSave));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));
			ret = storedProcedure.ExecuteMultipleDataSet();
		}

		public DataSet LoadExtendContractForm(Int32 CompanyID, Int32 DivisionID, Int32 AgreementID, int UserID)
		{
			DataSet ret = null;
			var storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = schema + ".[Data.ExtendContractForm.Load]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyID", ParameterType.DBString, CompanyID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionID", ParameterType.DBString, DivisionID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@agreementID", ParameterType.DBString, AgreementID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));
			ret = storedProcedure.ExecuteMultipleDataSet();
			return ret;
		}

		public void SaveExtendContractForm(DataTable extendContractFormToSave, int UserID)
		{
			DataSet ret = null;
			var storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = schema + ".[Data.ExtendContractForm.Save]";
			//rguidi 3/4/2013 #20202
			//storedProcedure.Parameters.Add(new StoredProcedureParameter("@AgreementsTableTypes", ParameterType.DBString, extendContractFormToSave));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@AgreementsTableTypes", ParameterType.Structured, extendContractFormToSave));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));
			ret = storedProcedure.ExecuteMultipleDataSet();
		}

		public void SaveDivisionStatus(Int32 CompanyID, DataTable divisionStatusToSave, int UserID)
		{
			DataSet ret = null;
			var storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = schema + ".[Data.DivisionStatus.Save]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyID", ParameterType.DBString, CompanyID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@DivisionTypes", ParameterType.DBString, divisionStatusToSave));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));
			ret = storedProcedure.ExecuteMultipleDataSet();
		}

		[Obsolete("Any call to this function is not facility safe")]
		public DataTable LoadCompanyList()
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = schema + ".[Audit.Lists.AllCompanies]" };

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

		public DataTable LoadCompanyList(int userID)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = schema + ".[CorporateMaintenance.Companies.Load]" };
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID));

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

		public DataTable LoadCompany(int companyID)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = schema + ".[CorporateMaintenance.Company.Load]" };
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyID", ParameterType.DBString, companyID));

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

        public DataTable LoadCompaniesWithActiveSIDADivisions(int userID)
        {
            DataTable result = null;

            var storedProcedure = new StoredProcedure() { StoredProcedureName = schema + ".[Data.TransferEmployeeForm.GetCompanies]" };

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID));

            result = storedProcedure.ExecuteDataSet();

            return result;
        }

        public DataTable SearchCompaniesWithActiveSIDADivisions(string companyCode, int userID)
        {
            DataTable result = null;

            var storedProcedure = new StoredProcedure() { StoredProcedureName = schema + ".[Data.TransferEmployeeForm.SearchCompanies]" };

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@CompanyCode", ParameterType.DBString, companyCode));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID));

            result = storedProcedure.ExecuteDataSet();

            return result;
        }

		public DataTable LoadCompanyIndustryTypes(int companyID)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = schema + ".[CorporateMaintenance.Lists.Company.IndustryTypes]" };
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyID", ParameterType.DBInteger, companyID));

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

		public void Save(int companyID, string corporationName, string dBAName, string badgeName, string providerNumber, string rAMSAgreementNumber, DateTime? rAMSExpirationDate, DataTable industryTypes, bool BTRCRequired, bool insuranceRequired, int coordinatorID, int UserID)
		{
			var storedProcedure = new StoredProcedure() { StoredProcedureName = schema + ".[Company.Save]" };
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@CompanyID", ParameterType.DBInteger, companyID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@CorporationName", ParameterType.DBString, corporationName));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@DBAName", ParameterType.DBString, dBAName));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@BadgeName", ParameterType.DBString, badgeName));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@ProviderNumber", ParameterType.DBString, providerNumber));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@RAMSAgreementNumber", ParameterType.DBString, rAMSAgreementNumber));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@RAMSExpirationDate", ParameterType.DBDateTime, rAMSExpirationDate));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@CoordinatorID", ParameterType.DBInteger, coordinatorID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@IndustryTypes", ParameterType.Structured, industryTypes));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@BTRCRequired", ParameterType.DBBoolean, BTRCRequired));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@InsuranceRequired", ParameterType.DBBoolean, insuranceRequired));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));
			storedProcedure.ExecuteDataSet();
		}

		public DataSet LoadSAFEForm(Int32 CompanyID, Int32 DivisionID, int UserID)
		{
			DataSet ret = null;
			var storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = schema + ".[Data.CompanySAFEForm.Load]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyID", ParameterType.DBString, CompanyID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionID", ParameterType.DBString, DivisionID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));
			ret = storedProcedure.ExecuteMultipleDataSet();
			return ret;
		}

		public DataSet loadCompanyList(Int32? companyID, string companyCriteria, string divisionCriteria, int UserID)
		{
			DataSet ret = null;
			var storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = schema + ".[Data.Company.Load]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyID", ParameterType.DBString, companyID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyCriteria", ParameterType.DBString, companyCriteria));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionCriteria", ParameterType.DBString, divisionCriteria));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBString, UserID));
			ret = storedProcedure.ExecuteMultipleDataSet();
			return ret;
		}

		public DataSet GetCompanyCodeCompany(String companyCode, int UserID)
		{
			DataSet ret = null;
			var storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = schema + ".[Data.CompanyCodeCompany.Load]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyCode", ParameterType.DBString, companyCode));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBString, UserID));
			ret = storedProcedure.ExecuteMultipleDataSet();
			return ret;
		}

		public DataSet GetCompanyCodeCompany(String companyCode, String divisionCode, int UserID)
		{
			DataSet ret = null;
			var storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = schema + ".[Data.CompanyCodeCompany.Load]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyCode", ParameterType.DBString, companyCode));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionCode", ParameterType.DBString, divisionCode));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBString, UserID));
			ret = storedProcedure.ExecuteMultipleDataSet();
			return ret;
		}

		/// <summary>
		/// Showtable version for saving contract verification information per ContractVerification.aspx
		/// </summary>
		public void ShadowSaveContractVerificationForm(DataSet contractVerificationFormToSave, int userID, String actionCode, out string ID)
		{
			var storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = _ShadowSchema + ".[Data.ContractVerificationForm.Save]";

			storedProcedure.Parameters.Add(new StoredProcedureParameter("@CompanyTypes", ParameterType.Structured, (contractVerificationFormToSave.Tables[0].Rows.Count > 0) ? contractVerificationFormToSave.Tables[0] : null));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@CompanyIndustryTypes", ParameterType.Structured, (contractVerificationFormToSave.Tables[1].Rows.Count > 0) ? contractVerificationFormToSave.Tables[1] : null));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@DivisionTypes", ParameterType.Structured, (contractVerificationFormToSave.Tables[2].Rows.Count > 0) ? contractVerificationFormToSave.Tables[2] : null));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@AgreementsTableTypes", ParameterType.Structured, (contractVerificationFormToSave.Tables[3].Rows.Count > 0) ? contractVerificationFormToSave.Tables[3] : null));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@ServicesProvidedTableTypes", ParameterType.Structured, (contractVerificationFormToSave.Tables[4].Rows.Count > 0) ? contractVerificationFormToSave.Tables[4] : null));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@AgreementJobRolesTableTypes", ParameterType.Structured, (contractVerificationFormToSave.Tables[5].Rows.Count > 0) ? contractVerificationFormToSave.Tables[5] : null));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@AgreementLocationsTableTypes", ParameterType.Structured, (contractVerificationFormToSave.Tables[6].Rows.Count > 0) ? contractVerificationFormToSave.Tables[6] : null));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@ActionCode", ParameterType.DBString, actionCode));
			
			storedProcedure.ExecuteDataSetWithIDOutputParam(out ID);
		}

        public void SaveContractVerificationFormContacts(DataTable contacts, bool isShadow, bool? isNewDivisionOnly)
        {
            var storedProcedure = new StoredProcedure();

            var xSchema = isShadow ? _ShadowSchema : schema;

            storedProcedure.StoredProcedureName = xSchema + ".[Data.ContractVerificationForm.Contacts.Save]";

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@Contacts", ParameterType.Structured, (contacts.Rows.Count > 0) ? contacts : null));

            if (isNewDivisionOnly.HasValue)
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@IsNewDivisionOnly", ParameterType.DBBoolean, isNewDivisionOnly.Value));

            storedProcedure.ExecuteNonQuery();
        }

		/// <summary>
		/// Specialized shadow table version of save routine for requirement documents
		/// </summary>
		/// <param name="requirementdocumentsToSave"></param>
		/// <param name="userID"></param>
		public void ShadowSaveRequirementDocuments(DataTable requirementdocumentsToSave, int userID)
		{
			var storedProcedure = new StoredProcedure();

			storedProcedure.StoredProcedureName = _ShadowSchema + ".[Data.RequirementsDocuments.Save]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@RequirementDocumentsTableTypes", ParameterType.Structured, requirementdocumentsToSave));

			storedProcedure.ExecuteNonQuery();
		}

		/// <summary>
		/// Get company info based on company code  subject to whether or not data already exists in the shadow tables for that company
		/// </summary>
		public DataSet ShadowGetCompanyCodeCompany(String companyCode, int userID)
		{
			DataSet ret = null;

			var storedProcedure = new StoredProcedure();

			storedProcedure.StoredProcedureName = _ShadowSchema + ".[Data.CompanyCodeCompany.Load]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@companySearchTerm", ParameterType.DBString, companyCode));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBString, userID));

			ret = storedProcedure.ExecuteMultipleDataSet();

			return ret;
		}

		/// <summary>
		/// Get company/division info based on company or division code by subject to whether or not data already exists in the shadow tables for that company
		/// </summary>
		public DataSet ShadowGetCompanyCodeCompany(String companyCode, String divisionCode, int userID)
		{
			DataSet ret = null;

			var storedProcedure = new StoredProcedure();

			storedProcedure.StoredProcedureName = _ShadowSchema + ".[Data.CompanyCodeCompany.Load]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@companySearchTerm", ParameterType.DBString, companyCode));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionCode", ParameterType.DBString, divisionCode));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBString, userID));

			ret = storedProcedure.ExecuteMultipleDataSet();
			return ret;
		}

		public DataSet ShadowGetCompanyDivisionLoad(Int32 companyID, Int32? divisionID, int userID)
		{
			DataSet ret = null;
			var storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = _ShadowSchema + ".[Data.CompanyDivision.Load]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyID", ParameterType.DBString, companyID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionID", ParameterType.DBString, divisionID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBString, userID));
			ret = storedProcedure.ExecuteMultipleDataSet();
			return ret;
		}

        public DataTable Search(string searchTerm, int userID)
        {
            return this.Search(searchTerm, userID, false);
        }

        public DataTable Search(string searchTerm, int userID, bool excludeShadow)
        {
            DataTable ret = null;

            var storedProcedure = new StoredProcedure();

            storedProcedure.StoredProcedureName = schema + ".[Data.Company.Search]";

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@CompanySearchTerm", ParameterType.DBString, searchTerm));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@ExcludeShadow", ParameterType.DBBoolean, excludeShadow));

            ret = storedProcedure.ExecuteDataSet();

            return ret;
        }

        
        public DataTable SearchForLookahead(string searchTerm, int userID)
        {
            DataTable ret = null;

            var storedProcedure = new StoredProcedure();

            storedProcedure.StoredProcedureName = schema + ".[Data.Badging.Company.Search]";

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@CompanySearchTerm", ParameterType.DBString, searchTerm));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID));

            ret = storedProcedure.ExecuteDataSet();

            return ret;
        }

	}
}