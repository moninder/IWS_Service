using System;
using System.Data;

namespace AirportIQ.Domain.Contracts
{
    public interface IOAS
    {
        DataSet loadAliasList(Int32 PersonID, String loadType); // to load a list of all Aliases attached to a person record
        DataSet loadAppList(Int32 UserId,Boolean InProgressOnly, Int32? PersonID, String AppType, String ssn, String loadType, Int32? DivisionID); // to load a list of all applications
        void saveFingerprintForm(DataTable AppToSave, DataTable AliasToSave, DataTable GovIDToSave, int userID, out int ID);
        DataTable SaveTQTFingerprintForm(DataTable AppToSave, DataTable AliasToSave, DataTable GovIDToSave, int userID);
        void saveRequirementDocuments(DataTable requirementdocumentsToSave, int userID); // to save Requirements Documents data   
        void SaveOASRequirementDocuments(DataTable requirementdocumentsToSave, int userID); // to save Requirements Documents data   
        void saveBadgePage1Form(DataTable AppToSave, DataTable AliasToSave, DataTable GovIDToSave, DataTable FelonyAnswers, int userID);
        void DeleteInProcessRecords(DataTable recsToDelete, int userID);
        DataSet LoadFelonyQuestionNames();
        DataSet LoadInProcessDocuments(Int32 personID, Int32 divisionID);
        DataSet SaveFPSupplementalForm(DataTable dtCrimHist, Int32 personID, Int32 userId); 
        DataSet GetAuthorizedSignerDivisionID(Int32 userID);
        void SaveBadgePage2Form(DataTable badgePage2Table, int userID);
        DataTable GetCertifiedTrainers(int companyID, int userID);
        void SaveCertifiedTrainer(int companyID, int personID, short trainingSiteID, string personTypeStatusCode, bool isCertified, bool isFingerPrinted, bool isLEO, DateTime whenTrained, DateTime whenExpires, string instructor, int UserID);
        DataTable GetAuthorizedSigners(int divisionID, int userID);
        void SaveAuthorizedSigner(string personTypeStatusCode, string signerCode, DateTime whenTrained, DateTime whenExpires, Byte[] signatureImage, DateTime signatureDate, char trainingType, string instructor, string loginName, string password, DateTime passwordExpiresEndOf, bool isActive, int personId, int divisionId, string OASGroupName, int UserID, int securityUserId);
        byte[] GetAuthorizedSignerImage(int personId, int divisionId);
      byte[] GetDocumentImage(int documentID);
      DataTable GetDivisions(int companyID, int userID);
        DataTable GetCompanies(int userID);
    }
}