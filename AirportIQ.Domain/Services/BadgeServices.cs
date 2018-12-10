using System;
using System.Data;
using AirportIQ.Data;
using AirportIQ.Data.SqlServer.Repositories;
using AirportIQ.Domain.Contracts;
using AirportIQ.Model.Models.Badging.Results;
using AirportIQ.Model.Models.Badging;

// jem 7/14/2012 cleaned up non-standard var naming, changed Int64 to standard 32, besides adding userID to loadBadgeMaintenanceform
namespace AirportIQ.Domain.Services
{
    public class BadgeServices : IBadge
    {
        #region "Private Variables"

        private readonly IBadgeRepository badgeRepository;

        #endregion "Private Variables"

        #region "Constructors"

        public BadgeServices() : this(new BadgeRepository()) { }

        public BadgeServices(IBadgeRepository BadgeRepository)
        {
            if (BadgeRepository == null) throw new ArgumentNullException("BadgeRepository");
            this.badgeRepository = BadgeRepository;
        }

        #endregion "Constructors"

        #region "Public Methods"

        public DataSet loadBadgeMaintenanceForm(short companyID, short divisionID, short locationID, Int32 personID, Int32 badgeID, bool isActive, Int32 userID)
        {
            return this.badgeRepository.loadBadgeMaintenanceForm(companyID, divisionID, locationID, personID, badgeID, isActive, userID);
        }

        public void saveBadgeMaintenanceForm(DataTable BadgeMaintenanceFormToSave, int userID)
        {
            this.badgeRepository.saveBadgeMaintenanceForm(BadgeMaintenanceFormToSave, userID);
        }

        #region Problem BadgeServices

        public DataTable PersonGetBadges(int personID, bool activeOnly = true)
        {
            return this.badgeRepository.PersonGetBadges(personID, activeOnly);
        }

        public DataTable LocationsByBadgeID(int badgeID)
        {
            return this.badgeRepository.LocationsByBadgeID(badgeID);
        }



        #endregion

        #region Badge Record

        public DataTable BadgesByName(string firstNamePattern, string middleNamePattern, string lastNamePattern, bool activeOnly, int userID)
        {
            return this.badgeRepository.BadgesByName(firstNamePattern, middleNamePattern, lastNamePattern, activeOnly, userID);
        }

        public DataTable BadgesByBadgeNumber(string badgeNumber, bool activeOnly, int userID)
        {
            return this.badgeRepository.BadgesByBadgeNumber(badgeNumber, activeOnly, userID);
        }

        public DataTable BadgesByPersonID(int personID, int userID)
        {
            return this.badgeRepository.BadgesByPersonID(personID, userID);
        }
      public DataTable DocumentsByPersonID(int personID)
      {
         return this.badgeRepository.DocumentsByPersonID(personID);
      }
      public DataTable BadgesByPersonDivsionXrefId(int personDivisionXrefId, int userId)
        {
            return this.badgeRepository.BadgesByPersonDivsionXrefId(personDivisionXrefId, userId);
        }

        public DataSet BadgeInfoByBadgeNumber(string badgeNumber, int userID)
        {
            return this.badgeRepository.BadgeInfoByBadgeNumber(badgeNumber, userID);
        }

        public DataTable BadgeNotes(int personID, int userID)
        {
            return this.badgeRepository.BadgeNotes(personID, userID);
        }

        public DataTable GetPersonByNumber(string badgeNr, int userID)
        {
            return this.badgeRepository.GetPersonByNumber(badgeNr, userID);
        }

        //JBienvenu 19202 2012-12-27 new
        public void Police_InvalidateBadges(DataTable data, int userID)
        {
            this.badgeRepository.Police_InvalidateBadges(data, userID);
        }

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
        public bool InsertReprint(int badgeId, int staffId, string reprintReasonCode, DateTime printDate, DateTime whenExpires)
        {
            return this.badgeRepository.InsertReprint(badgeId, staffId, reprintReasonCode, printDate, whenExpires);
        }

     public  bool InsertIntoRevetTable(int personDivisionCheckID,  int staffID, string oldTCN, string newTCN)
      {
         return this.badgeRepository.InsertIntoRevetTable( personDivisionCheckID,   staffID,  oldTCN,  newTCN);
      }

      public bool SetCHRCtoPending(int personDivisionCheckID)
      {
         return this.badgeRepository.SetCHRCtoPending( personDivisionCheckID);
      }

      public bool InsertRetransmitTransaction(int personDivisionCheckID, int staffId)
      {
         return this.badgeRepository.InsertRetransmitTransaction( personDivisionCheckID,  staffId);
      }

        public DataTable BadgingAppointmentGetPeople(string companyCode, string divisionCode, string SSN, string lastName, string OAS_Name, string yearOfBirth, string BadgeNumber)
        {
            return this.badgeRepository.BadgingAppointmentGetPeople(companyCode, divisionCode, SSN, lastName, OAS_Name, yearOfBirth, BadgeNumber);
        }

        public DataTable BadgingAppointmentGetPeopleMixed(string companyCode, string divisionCode, string SSN, string lastName, string OAS_Name, string yearOfBirth, string BadgeNumber)
        {
            return this.badgeRepository.BadgingAppointmentGetPeopleMixed(companyCode, divisionCode, SSN, lastName, OAS_Name, yearOfBirth, BadgeNumber);
        }

        public DataTable BadgingAppointmentGetPeopleWithBadges(string companyCode, string divisionCode, string SSN, string lastName, string OAS_Name, string yearOfBirth, string BadgeNumber)
        {
            return this.badgeRepository.BadgingAppointmentGetPeopleWithBadges(companyCode, divisionCode, SSN, lastName, OAS_Name, yearOfBirth, BadgeNumber);
        }

        public DataTable BadgingAppointmentGetJobRolesByAgreementNumber(string agreementNumber)
        {
            return this.badgeRepository.BadgingAppointmentGetJobRolesByAgreementNumber(agreementNumber);
        }

        public DataTable BadgingAppointmentGetJobRolesByAgreementID(int agreementID)
        {
            return this.badgeRepository.BadgingAppointmentGetJobRolesByAgreementID(agreementID);
        }

        public DataTable BadgingAppointmentGetWorkLocationsByAgreementID(int agreementID)
        {
            return this.badgeRepository.BadgingAppointmentGetWorkLocationsByAgreementID(agreementID);
        }

        public DataTable BadgingAppointmentGetWorkLocationsByAgreementIDAndJobRoleID(int agreementID, Int16 sJobRoleID)
        {
            return this.badgeRepository.BadgingAppointmentGetWorkLocationsByAgreementIDAndJobRoleID(agreementID, sJobRoleID);
        }

        public bool SaveBadgingResults(BadgingResults badgingResults, int userID, bool CreateBackgroundCheck)
        {
            return this.badgeRepository.SaveBadgingResults(badgingResults, userID, CreateBackgroundCheck);
        }

        public bool SaveBadgingNotes(BadgingResults badgingResults, int userID)
        {
            return this.badgeRepository.SaveBadgingNotes(badgingResults, userID);
        }

        public DataTable GetNewPersonDivisionXrefID(string prefix, string firstName, string middleName, string lastName, string suffix, string ssn, DateTime dateOfBirth, int companyID, int divisionID, int newCompanyID, int newDivisionID, int UserID)
        {
            return this.badgeRepository.GetNewPersonDivisionXrefID(prefix, firstName, middleName, lastName, suffix, ssn, dateOfBirth, companyID, divisionID, newCompanyID, newDivisionID, UserID);
        }

        public DataTable BackgroundCheckLast45Days(int personDivisionXrefID)
        {
            return this.badgeRepository.BackgroundCheckLast45Days(personDivisionXrefID);
        }

        public DataTable GetCertifiedTrainers(int companyId)
        {
            return this.badgeRepository.GetCertifiedTrainers(companyId);
        }

        public DataTable GetAuthorizedSigners(int divisionID)
        {
            return this.badgeRepository.GetAuthorizedSigners(divisionID);
        }

		public DataTable BackgroundCheckCopy(int personDivisionCheckIDCopyFrom, int personDivisionXrefIDCopyTo, int UserID)
        {
			return this.badgeRepository.BackgroundCheckCopy(personDivisionCheckIDCopyFrom, personDivisionXrefIDCopyTo, UserID);
        }

        public DataTable GetSetNextSSNDummy()
		{
			return this.badgeRepository.GetSetNextSSNDummy();
		}

		public bool ValidateSSN(int PersonID, string SSN)
		{
			return this.badgeRepository.ValidateSSN(PersonID, SSN);
		}

        public void SetLastCHRCAndSTADates(int personDivisionXrefID)
        {
            this.badgeRepository.SetLastCHRCAndSTADates(personDivisionXrefID);
        }

      public void HandleExempt(int empID, string guid)
      {
         this.badgeRepository.HandleExempt(empID, guid);
      }

        public void SetCHRCAndSTAResults(int personDivisionXrefID, string CHRCResult, string STAResult)
        {
            this.badgeRepository.SetCHRCAndSTAResults(personDivisionXrefID, CHRCResult, STAResult);
        }

        public void InsertTSCTransaction(int badgeId, int UserID)
        {
            this.badgeRepository.InsertTSCTransaction(badgeId, UserID);
        }


      public void UpdateIWSBadgeInfo(string boaaBadgeID, string iwsCardID)
      {
         this.badgeRepository.UpdateIWSBadgeInfo(boaaBadgeID, iwsCardID);
      }

      public void UpdateBadgeColor(string badgeID, string color)
      {
         this.badgeRepository.UpdateBadgeColor(badgeID, color);
      }
      public DataTable GetShadowData(int divisionId)
        {
            return this.badgeRepository.GetShadowData(divisionId);
        }

        #endregion

        #region Invalidate Badge

        /// <summary>
        /// Returns the badge details for the invalidate badge screen
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="badgeId">The badge id.</param>
        /// <returns></returns>
        public DataTable InvalidateBadgeDetails(int userId, int badgeId)
        {
            return this.badgeRepository.InvalidateBadgeDetails(userId, badgeId);
        }

        /// <summary>
        /// Gets the how reported list.
        /// </summary>
        /// <returns></returns>
        public DataTable GetHowReportedList()
        {
            return this.badgeRepository.GetHowReportedList();
        }

        /// <summary>
        /// Inserts the badge missing report.
        /// </summary>
        /// <param name="details">The details.</param>
        /// <param name="staffID">The staff ID.</param>
        public void InsertBadgeMissingReport(LostBadgeDetails details, int staffID)
        {
            this.badgeRepository.InsertBadgeMissingReport(details, staffID);
        }

        #endregion

        #endregion "Public Methods"
    }
}