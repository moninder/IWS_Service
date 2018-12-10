using System;
using System.Data;
using AirportIQ.Model.Models.Badging.Results;
using AirportIQ.Model.Models.Badging;

// jem 7/14/2012 cleaned up non-standard var naming, changed Int64 to standard 32, besides adding userID to loadBadgeMaintenanceform
namespace AirportIQ.Domain.Contracts
{
    public interface IBadge
    {
        DataSet loadBadgeMaintenanceForm(Int16 companyID, Int16 divisionID, Int16 locationID, Int32 personID, Int32 badgeID, bool IsActive, Int32 userID); // to load Individual Special Access Request Form data

        void saveBadgeMaintenanceForm(DataTable BadgeMaintenanceFormToSave, int userID); // to save Individual Special Access Request Form  data

        #region Problem Badge

        DataTable PersonGetBadges(int personID, bool activeOnly = true);
        DataTable LocationsByBadgeID(int badgeID);

        #endregion Problem Badge

        #region Badge Record

        DataTable BadgesByName(string firstNamePattern, string middleNamePattern, string lastNamePattern, bool activeOnly, int userID);
        DataTable BadgesByBadgeNumber(string badgeNumber, bool activeOnly, int userID);
        DataTable BadgesByPersonID(int personID, int userID);

      DataTable DocumentsByPersonID(int personID);
      DataTable BadgesByPersonDivsionXrefId(int personDivisionXrefId, int userId);
        DataSet BadgeInfoByBadgeNumber(string badgeNumber, int userID);
        DataTable BadgeNotes(int personID, int userID);

      DataTable GetPersonByNumber(string badgeNr, int userID);
        void Police_InvalidateBadges(DataTable data, int userID);

        #endregion

        #region Badging Appointment

        /// <summary>
        /// Inserts the reprint.
        /// </summary>
        /// <param name="badgeId">The badge identifier.</param>
        /// <param name="staffId">The staff identifier.</param>
        /// <param name="reprintReasonCode">The reprint reason.</param>
        /// <param name="printDate">The print date.</param>
        /// <returns></returns>
      bool InsertReprint(int badgeId, int staffId, string reprintReasonCode, DateTime printDate, DateTime whenExpires);
      bool SetCHRCtoPending(int personDivisionCheckID);
      bool InsertIntoRevetTable(int personDivisionCheckID, int staffID, string oldTCN, string newTCN);
      bool InsertRetransmitTransaction(int personDivisionCheckID, int staffId);

        DataTable BadgingAppointmentGetPeople(string companyCode, string divisionCode, string SSN, string lastName, string OAS_Name, string yearOfBirth, string BadgeNumber);
        DataTable BadgingAppointmentGetPeopleMixed(string companyCode, string divisionCode, string SSN, string lastName, string OAS_Name, string yearOfBirth, string BadgeNumber);
        DataTable BadgingAppointmentGetPeopleWithBadges(string companyCode, string divisionCode, string SSN, string lastName, string OAS_Name, string yearOfBirth, string BadgeNumber);
        DataTable BadgingAppointmentGetJobRolesByAgreementNumber(string agreementNumber);
        DataTable BadgingAppointmentGetJobRolesByAgreementID(int agreementID);
        DataTable BadgingAppointmentGetWorkLocationsByAgreementID(int agreementID);
        DataTable BadgingAppointmentGetWorkLocationsByAgreementIDAndJobRoleID(int agreementID, Int16 sJobRoleID);
        bool SaveBadgingResults(BadgingResults badgingResults, int userID, bool CreateBackgroundCheck);
        bool SaveBadgingNotes(BadgingResults badgingResults, int userID);
        DataTable GetNewPersonDivisionXrefID(string prefix, string firstName, string middleName, string lastName, string suffix, string ssn, DateTime dateOfBirth, int companyID, int divisionID, int newCompanyID, int newDivisionID, int UserID);
        DataTable BackgroundCheckLast45Days(int personDivisionXrefID);
        DataTable GetCertifiedTrainers(int companyId);
        DataTable GetAuthorizedSigners(int divisionID);
		DataTable BackgroundCheckCopy(int personDivisionCheckIDCopyFrom, int personDivisionXrefIDCopyTo, int UserID);
        DataTable GetSetNextSSNDummy();
		bool ValidateSSN(int PersonID, string SSN);
        void SetLastCHRCAndSTADates(int personDivisionXrefID);
      void HandleExempt(int empID, string guid);
        void SetCHRCAndSTAResults(int personDivisionXrefID, string CHRCResult, string STAResult);
        void InsertTSCTransaction(int badgeId, int UserID);
      void UpdateIWSBadgeInfo(string boaaBadgeID, string iwsCardID);
      void UpdateBadgeColor(string badgeID, string color);
        DataTable GetShadowData(int divisionId);
        #endregion

        #region Invalidate Badge

        /// <summary>
        /// Returns the badge details for the invalidate badge screen
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="badgeId">The badge id.</param>
        /// <returns></returns>
        DataTable InvalidateBadgeDetails(int userId, int badgeId);

        /// <summary>
        /// Gets the how reported list.
        /// </summary>
        /// <returns></returns>
        DataTable GetHowReportedList();

        /// <summary>
        /// Inserts the badge missing report.
        /// </summary>
        /// <param name="details">The details.</param>
        /// <param name="staffID">The staff ID.</param>
        void InsertBadgeMissingReport(LostBadgeDetails details, int staffID);

        #endregion
    }
}