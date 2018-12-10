using System;
using System.Data;
using AirportIQ.Data;
using AirportIQ.Domain.Contracts;
using AirportIQ.Data.SqlServer.Repositories;

namespace AirportIQ.Domain.Services
{
    public class OASServices : IOAS
    {
        private readonly IOASRepository oasRepository;

        public OASServices() : this(new OASRepository()) { }

        public OASServices(IOASRepository oasRepository)
        {
            if (oasRepository == null) throw new ArgumentNullException("oasRepository");
            this.oasRepository = oasRepository;
        }

        DataSet IOAS.loadAliasList(Int32 personID, String loadType)
        {
            return this.oasRepository.loadAliasList(personID, loadType);
        }

        DataSet IOAS.loadAppList(Int32 userID, Boolean inProgressOnly, Int32? personID, String appType, String ssn, String loadType, Int32? divisionID)
        {
            return this.oasRepository.loadAppList(userID, inProgressOnly, personID, appType, ssn, loadType, divisionID);
        }
        void IOAS.saveFingerprintForm(DataTable appToSave, DataTable aliasToSave, DataTable govIDToSave, int userID, out int ID)
        {
            this.oasRepository.saveFingerprintForm(appToSave, aliasToSave, govIDToSave, userID, out ID);
        }

        /// <summary>
        /// tqt replacement so that an output parameter is not needed in the code
        /// </summary>
        /// <param name="appToSave">biographic data</param>
        /// <param name="aliasToSave">alias data</param>
        /// <param name="govIdToSave">up to two government ID info</param>
        /// <param name="userID">current user</param>
        /// <returns>datatable with a single column of information containing the new personid</returns>
        DataTable IOAS.SaveTQTFingerprintForm(DataTable appToSave, DataTable aliasToSave, DataTable govIdToSave, int userID)
        {
            return this.oasRepository.SaveTQTFingerprintForm(appToSave, aliasToSave, govIdToSave, userID);
        }

        public void saveRequirementDocuments(DataTable requirementdocumentsToSave, int userID)
        {
            this.oasRepository.saveRequirementDocuments(requirementdocumentsToSave, userID);
        }

        public void SaveOASRequirementDocuments(DataTable requirementdocumentsToSave, int userID)
        {
            this.oasRepository.SaveOASRequirementDocuments(requirementdocumentsToSave, userID);
        }

        void IOAS.saveBadgePage1Form(DataTable appToSave, DataTable aliasToSave, DataTable govIDToSave, DataTable felonyAnswers, int userID)
        {
            this.oasRepository.saveBadgePage1Form(appToSave, aliasToSave, govIDToSave, felonyAnswers, userID);
        }

        void IOAS.DeleteInProcessRecords(DataTable recsToDelete, int userID)
        {
            oasRepository.DeleteInProcessRecords(recsToDelete, userID);
        }

        DataSet IOAS.LoadFelonyQuestionNames()
        {
            return this.oasRepository.LoadFelonyQuestionNames();
        }

        DataSet IOAS.LoadInProcessDocuments(Int32 personID, Int32 divisionID)
        {
            return this.oasRepository.LoadInProcessDocuments(personID, divisionID);
        }

        public DataSet SaveFPSupplementalForm(DataTable dtCrimHist, Int32 personID, Int32 userId)
        {
            return this.oasRepository.SaveFPSupplementalForm(dtCrimHist, personID, userId);
        }

        public DataSet GetAuthorizedSignerDivisionID(Int32 userID)
        {
            return this.oasRepository.GetAuthorizedSignerDivisionID(userID);
        }

        public void SaveBadgePage2Form(DataTable badgePage2Table, int userID)
        {
            this.oasRepository.SaveBadgePage2Form(badgePage2Table, userID);
        }

        public DataTable GetCertifiedTrainers(int companyID, int userID)
        {
            return this.oasRepository.GetCertifiedTrainers(companyID, userID);
        }

        public void SaveCertifiedTrainer(int companyID, int personID, short trainingSiteID, string personTypeStatusCode, bool isCertified, bool isFingerPrinted, bool isLEO, DateTime whenTrained, DateTime whenExpires, string instructor, int UserID)
        {
            this.oasRepository.SaveCertifiedTrainer(companyID, personID, trainingSiteID, personTypeStatusCode, isCertified, isFingerPrinted, isLEO, whenTrained, whenExpires, instructor, UserID);
        }

        public DataTable GetAuthorizedSigners(int divisionID, int userID)
        {
            return this.oasRepository.GetAuthorizedSigners(divisionID, userID);
        }

        public void SaveAuthorizedSigner(string personTypeStatusCode, string signerCode, DateTime whenTrained, DateTime whenExpires, Byte[] signatureImage, DateTime signatureDate, char trainingType, string instructor, string loginName, string password, DateTime passwordExpiresEndOf, bool isActive, int personId, int divisionId, string OASGroupName, int UserID, int securityUserId)
        {
            this.oasRepository.SaveAuthorizedSigner(personTypeStatusCode, signerCode, whenTrained, whenExpires, signatureImage, signatureDate, trainingType, instructor, loginName, password, passwordExpiresEndOf, isActive, personId, divisionId, OASGroupName, UserID, securityUserId);
        }

        public byte[] GetAuthorizedSignerImage(int personId, int divisionId)
        {
            return this.oasRepository.GetAuthorizedSignerImage(personId, divisionId);
        }
      public byte[] GetDocumentImage(int documentID)
      {
         return this.oasRepository.GetDocumentImage(documentID);
      }

      public DataTable GetDivisions(int companyID, int userID)
        {
            return this.oasRepository.GetDivisions(companyID, userID);
        }

        public DataTable GetCompanies(int userID)
        {
            return this.oasRepository.GetCompanies(userID);
        }
    }
}
