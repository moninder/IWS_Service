using System;
using System.Data;
using System.Configuration;
using AirportIQ.Data.SqlServer.Initializers;

namespace AirportIQ.Data.SqlServer.Repositories
{
    public class OASRepository : IOASRepository
    {
        #region Private Variables

            private string schema = ConfigurationManager.AppSettings["SecuritySchema"].ToString();

        #endregion

        DataSet IOASRepository.loadAppList(Int32 userID, Boolean inProgressOnly, Int32? personID, String appType, String ssn, String loadType, Int32? divisionID)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Data.InProcessApplications.Load]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBString, userID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@inProgressOnly", ParameterType.DBBoolean , inProgressOnly));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@personID", ParameterType.DBString, personID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@appType", ParameterType.DBString, appType));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@ssn", ParameterType.DBString, ssn));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@loadType", ParameterType.DBString, loadType));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionID", ParameterType.DBString, divisionID));
            ret = storedProcedure.ExecuteMultipleDataSet();
            return ret;
        }

        DataSet IOASRepository.loadAliasList(Int32 personID, String loadType)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Data.PersonAlias.Load]";           
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@personID", ParameterType.DBString, personID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@loadType", ParameterType.DBString, loadType));
            ret = storedProcedure.ExecuteMultipleDataSet();
            return ret;
        }

        void IOASRepository.saveFingerprintForm(DataTable appToSave, DataTable aliasToSave, DataTable govIDToSave, int userID, out int ID)
        {
            //DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Data.PersonIWSBiographics.Save]";
            //rguidi 3/4/2013 #20202
            //storedProcedure.Parameters.Add(new StoredProcedureParameter("@PersonBiographicsTableType", ParameterType.DBString, appToSave));
            //storedProcedure.Parameters.Add(new StoredProcedureParameter("@PersonAliasTableType", ParameterType.DBString, aliasToSave));
            //storedProcedure.Parameters.Add(new StoredProcedureParameter("@PersonGovernmentID", ParameterType.DBString, govIDToSave));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@PersonBiographicsTableType", ParameterType.Structured, appToSave));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@PersonAliasTableType", ParameterType.Structured, aliasToSave));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@PersonGovernmentID", ParameterType.Structured, govIDToSave));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID));
            storedProcedure.ExecuteDataSetWithIDOutputParam(out ID);
            //ret = storedProcedure.ExecuteMultipleDataSet();
        }
        
        DataTable IOASRepository.SaveTQTFingerprintForm(DataTable appToSave, DataTable aliasToSave, DataTable govIDToSave, int userID)
        {
            DataSet ret = null;
            DataTable rt = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Data.PersonIWSBiographics.Save]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@PersonBiographicsTableType", ParameterType.Structured, appToSave));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@PersonAliasTableType", ParameterType.Structured, aliasToSave));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@PersonGovernmentID", ParameterType.Structured, govIDToSave));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID));
            ret = storedProcedure.ExecuteMultipleDataSet();
            rt = ret.Tables[0];
            return rt;            
        }     
  
        public void saveRequirementDocuments(DataTable requirementdocumentsToSave, int userID)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Data.RequirementsDocuments.Save]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@RequirementDocumentsTableTypes", ParameterType.Structured, requirementdocumentsToSave));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID));
            
            ret = storedProcedure.ExecuteMultipleDataSet();
        }

        public void SaveOASRequirementDocuments(DataTable requirementdocumentsToSave, int userID)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Data.OASRequirementsDocuments.Save]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@RequirementDocumentsTableTypes", ParameterType.Structured, requirementdocumentsToSave));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID));
            ret = storedProcedure.ExecuteMultipleDataSet();
        }

        void IOASRepository.saveBadgePage1Form(DataTable appToSave, DataTable aliasToSave, DataTable govIDToSave, DataTable felonyAnswers, int userID)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Data.PersonIWSBiographics.Save]";

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@PersonBiographicsTableType", ParameterType.Structured, appToSave));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@PersonAliasTableType", ParameterType.Structured, aliasToSave));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@PersonGovernmentID", ParameterType.Structured, govIDToSave));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@PersonFelonyAnswers", ParameterType.Structured, felonyAnswers));

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID));
            ret = storedProcedure.ExecuteMultipleDataSet();
        }

        void IOASRepository.DeleteInProcessRecords(DataTable recsToDelete, int userID)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Data.PersonInProcess.Delete]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@InProcessType", ParameterType.Structured, recsToDelete));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID));
            ret = storedProcedure.ExecuteMultipleDataSet();
        }

        DataSet IOASRepository.LoadFelonyQuestionNames()
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Data.FelonyQuestions.Load]";
            ret = storedProcedure.ExecuteMultipleDataSet();
            return ret;
        }

        DataSet IOASRepository.LoadInProcessDocuments(Int32 personID, Int32 divisionID)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Data.InProcessDocuments.Load]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@personID", ParameterType.DBString, personID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionID", ParameterType.DBString, divisionID));
            ret = storedProcedure.ExecuteMultipleDataSet();
            return ret;
        }

        DataSet IOASRepository.SaveFPSupplementalForm(DataTable dtCrimHist, Int32 personID, Int32 userID)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Data.FPSupplementalForm.Save]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@PersonFelonyAnswers", ParameterType.Structured, dtCrimHist));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@personID", ParameterType.DBString, personID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBInteger, userID));
            ret = storedProcedure.ExecuteMultipleDataSet();
            return ret;
        }

        DataSet IOASRepository.GetAuthorizedSignerDivisionID(Int32 userID)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Data.GetOASDivisionID]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBInteger, userID));
            ret = storedProcedure.ExecuteMultipleDataSet();
            return ret;
        }

        void IOASRepository.SaveBadgePage2Form(DataTable badgePage2Table, Int32 userID)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Data.BGSavePage2.Save]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@BadgePage2Table", ParameterType.Structured, badgePage2Table));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBInteger, userID));
            ret = storedProcedure.ExecuteMultipleDataSet();
        }       

        public DataTable GetCertifiedTrainers(int companyID, int userID)
        {
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Security.OASCTDisplayCertifiedTrainers]";

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@CompanyID", ParameterType.DBInteger, companyID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBInteger, userID));

            return storedProcedure.ExecuteDataSet();
        }

        public void SaveCertifiedTrainer(int companyID, int personID, short trainingSiteID, string personTypeStatusCode, bool isCertified, bool isFingerPrinted, bool isLEO, DateTime whenTrained, DateTime whenExpires, string instructor, int UserID)
        {
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Security.OASCTSaveCertifiedTrainer]";

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@CompanyID", ParameterType.DBInteger, companyID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@PersonID", ParameterType.DBInteger, personID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@TrainingSiteID", ParameterType.DBInteger, trainingSiteID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@PersonTypeStatusCode", ParameterType.DBString, personTypeStatusCode));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@IsCertified", ParameterType.DBBoolean, isCertified));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@IsFingerPrinted", ParameterType.DBBoolean, isFingerPrinted));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@IsLEO", ParameterType.DBBoolean, isLEO));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@WhenTrained", ParameterType.DBDateTime, whenTrained));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@WhenExpires", ParameterType.DBDateTime, whenExpires));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@Instructor", ParameterType.DBString, instructor));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@Note", ParameterType.DBString, string.Empty));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));

            storedProcedure.ExecuteNonQuery(); 
        }

        public DataTable GetAuthorizedSigners(int divisionID, int userID)
        {
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Security.OASCTDisplayAuthorizedSigners]";

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@DivisionID", ParameterType.DBInteger, divisionID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBInteger, userID));

            return storedProcedure.ExecuteDataSet();
        }

        public void SaveAuthorizedSigner(string personTypeStatusCode, string signerCode, DateTime whenTrained, DateTime whenExpires, Byte[] signatureImage, DateTime signatureDate, char trainingType, string instructor, string loginName, string password, DateTime passwordExpiresEndOf, bool isActive, int personId, int divisionId, string OASGroupName, int UserID, int securityUserId)
        {
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Security.OASCTSaveAuthorizedSigner]";

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@PersonTypeStatusCode", ParameterType.DBSingle, personTypeStatusCode));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@SignerCode", ParameterType.DBString, signerCode));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@WhenTrained", ParameterType.DBDateTime, whenTrained));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@WhenExpires", ParameterType.DBDateTime, whenExpires));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@SignatureImage", ParameterType.DBVarBinary, signatureImage));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@SignatureDate", ParameterType.DBDateTime, signatureDate));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@TrainingType", ParameterType.DBString, trainingType));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@Instructor", ParameterType.DBString, instructor));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@LoginName", ParameterType.DBString, loginName));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@Password", ParameterType.DBString, password));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@PasswordExpiresEndOf", ParameterType.DBDateTime, passwordExpiresEndOf));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@IsActive", ParameterType.DBBoolean, isActive));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@PersonID", ParameterType.DBInteger, personId));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@DivisionID", ParameterType.DBInteger, divisionId));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@OASGroupName", ParameterType.DBString, OASGroupName));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@SecurityUserID", ParameterType.DBInteger, securityUserId));

            storedProcedure.ExecuteNonQuery();
        }

        public byte[] GetAuthorizedSignerImage(int personId, int divisionId)
        {
            var storedProcedure = new StoredProcedure();

            storedProcedure.StoredProcedureName = schema + ".[Badging.CompanyDivision.AuthorizedSignerSignature]";

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@PersonID", ParameterType.DBInteger, personId));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@DivisionID", ParameterType.DBInteger, divisionId));

            var result = storedProcedure.ExecuteDataSet().Rows;

            if (result.Count == 0)
                return null;
            else
                return storedProcedure.ExecuteDataSet().Rows[0][0] as byte[];
        }
      public byte[] GetDocumentImage(int documentID)
      {
         var storedProcedure = new StoredProcedure();

         storedProcedure.StoredProcedureName = schema + ".[Badging.CompanyDivision.PersonDocument]";

         storedProcedure.Parameters.Add(new StoredProcedureParameter("@DocumentID", ParameterType.DBInteger, documentID));

         var result = storedProcedure.ExecuteDataSet().Rows;

         if (result.Count == 0)
            return null;
         else
            return storedProcedure.ExecuteDataSet().Rows[0][0] as byte[];
      }
      public DataTable GetDivisions(int companyID, int userID)
        {
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Security.OASCTDisplayDivisions]";

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@CompanyID", ParameterType.DBInteger, companyID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBInteger, userID));

            return storedProcedure.ExecuteDataSet();
        }

        public DataTable GetCompanies(int userID)
        {
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Security.OASCTDisplayCompanies]";

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBInteger, userID));

            return storedProcedure.ExecuteDataSet();
        }
    }
}