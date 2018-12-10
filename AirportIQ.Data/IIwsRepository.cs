using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace AirportIQ.Data
{
   /// <summary>
   /// Access to the data base for the Iws Web service.  May access multiple entities
   /// </summary>
   public interface IIwsRepository
   {
      DataTable GetFingerprintImages(Guid personID);
      bool BiometricUpdate(Guid personID, byte[] image);
      bool UpdateBadgeID(int cardID, int badgeID);
      bool InitiateBackgroundCheck(Guid personID, int TSCTransactionTypeID, string TransactionControlNumber, DateTime TransactionDate, string ProgramIdentification,
                                 string ResponseIdentification, string Status, string StatusText, string XMLdata, string Direction);
      bool UpdateBackgroundCheck(Guid personID, string AgencyCode, string CheckTypeCode, string TransactionTypeCode, string TransactionControlNumber,
                                 DateTime TransactionDate, string Result, DateTime ResultDate, string ResultDetails, DateTime ResultDetailDate);
      string RetreiveFbiHistoryUrl(Guid personID);
      bool UpdateBackgroundCheckStatus(Guid personID, string TransactionName, string AgencyCode, string TransactionControlNumber, DateTime TransactionDate,
                                     string ProgramIdentification, string ResponseIdentification, string TransmissionStatus, string TransmissionStatusText, string XMLdata);
      bool SetFBICaseNumber(Guid personID, string CaseNumber, string result, DateTime resultDate);
      bool SetTSACaseNumber(Guid personID, string CaseNumber, string result, DateTime resultDate);
      bool ExpireBadge(Guid personID, int cardID);
      DataTable ProvisionedByCard(int cardID);
      bool ProvisioningComplete(int cardID);
      bool IsDbAlive();
      DataSet GetPersonsForBatchUpdate();
      bool MarkPersonUpdateDetailAsSent(int PersonUpdateDetailID, int BadgeID);
      DataSet GetAllPersonUpdates();
      DataSet GetPersonUpdate(int PersonUpdateID);
      DataTable GetBadges(Guid personID);
      DataTable GetPerson(Guid personID);
      DataTable GetFingerprints(DateTime start, DateTime end);
      DataTable GetDojStatus(Guid personID);
      DataTable GetDocuments(Guid personID);
      Byte[] GetEBTS(Guid personID);

      bool AddNote(Guid personID, string note);
   }

}
