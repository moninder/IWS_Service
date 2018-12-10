using AirportIQ.Data.Helpers;
using AirportIQ.Data.SqlServer.Initializers;
using AirportIQ.Model.Models.Badging.Results;
using System;
using System.Configuration;
using System.Data;
using System.Text;
using AirportIQ.Model.Models.Badging;

// jem 7/14/2012 cleaned up non-standard var naming, changed Int64 to standard 32, besides adding userID to loadBadgeMaintenanceform
namespace AirportIQ.Data.SqlServer.Repositories
{
   public class BadgeRepository : IBadgeRepository
   {
      #region Private Variables

      private string schema = ConfigurationManager.AppSettings["SBO.ApplicationSchema"].ToString();

      #endregion Private Variables

      #region Public Methods

      /// <summary>
      /// Inserts the reprint.
      /// </summary>
      /// <param name="staffId">The staff identifier.</param>
      /// <param name="personDivisionCheckID">The CheckID for the person who has been retransmitted.</param>
      /// <returns></returns>
      public bool InsertRetransmitTransaction(int personDivisionCheckID, int staffId)
      {
         var storedProcedure = new StoredProcedure() { StoredProcedureName = schema + ".[Badging.InsertRetransmitTransaction]" };

         storedProcedure.Parameters.Add(new StoredProcedureParameter("@StaffID", ParameterType.DBInteger, staffId));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@PersonDivisionCheckID", ParameterType.DBInteger, personDivisionCheckID));
         storedProcedure.ExecuteNonQuery();
         return true;
      }

      /// <summary>
      /// Inserts the reprint.
      /// </summary>
      /// <param name="personDivisionCheckID">The CheckID for the person who has been retransmitted.</param>
      /// <returns></returns>
      public bool SetCHRCtoPending(int personDivisionCheckID)
      {
         var storedProcedure = new StoredProcedure() { StoredProcedureName = schema + ".[Badging.SetCHRCtoPending]" };

         storedProcedure.Parameters.Add(new StoredProcedureParameter("@PersonDivisionCheckID", ParameterType.DBInteger, personDivisionCheckID));
         storedProcedure.ExecuteNonQuery();
         return true;
      }

      /// <summary>
      /// Inserts the reprint.
      /// </summary>
      /// <param name="personDivisionCheckID">The CheckID for the person who has been retransmitted.</param>
      /// <param name="staffId">The staff identifier.</param>
      /// <param name="personID">The identifier for all companies/divisions.</param>
      /// <param name="oldTCN">The TCN used to trasnmit</param>
      /// <param name="newTCN">The TCN returned from EBTS server.</param>
      /// <returns></returns>
      public bool InsertIntoRevetTable(int personDivisionCheckID,  int staffID, string oldTCN, string newTCN)
      {
         var storedProcedure = new StoredProcedure() { StoredProcedureName = schema + ".[Badging.InsertIntoRevetTable]" };

         storedProcedure.Parameters.Add(new StoredProcedureParameter("@PersonDivisionCheckID", ParameterType.DBString, personDivisionCheckID));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@StaffId", ParameterType.DBInteger, staffID));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@OldTCN", ParameterType.DBString, oldTCN));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@NewTCN", ParameterType.DBString, newTCN));
         storedProcedure.ExecuteNonQuery();
         return true;
      }

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
         var storedProcedure = new StoredProcedure() { StoredProcedureName = schema + ".[Badging.InsertReprint]" };
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@badgeId", ParameterType.DBInteger, badgeId));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@reprintDate", ParameterType.DBDateTime, printDate));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@staffId", ParameterType.DBInteger, staffId));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@reprintReasonCode", ParameterType.DBString, reprintReasonCode));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@whenExpires", ParameterType.DBDateTime, whenExpires));
         storedProcedure.ExecuteNonQuery();
         return true;
      }

      public DataSet loadBadgeMaintenanceForm(short companyID, short divisionID, short locationID, Int32 personID, Int32 badgeID, bool isActive, Int32 userID)
      {
         DataSet result = null;
         var storedProcedure = new StoredProcedure() { StoredProcedureName = schema + ".[Data.BadgeMaintenanceForm.Load]" };
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyID", ParameterType.DBString, companyID));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionID", ParameterType.DBString, divisionID));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@locationID", ParameterType.DBString, locationID));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@personID", ParameterType.DBString, personID));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@badgeID", ParameterType.DBString, badgeID));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@isActive", ParameterType.DBString, isActive));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBString, userID));
         result = storedProcedure.ExecuteMultipleDataSet();
         return result;
      }

      public void saveBadgeMaintenanceForm(DataTable BadgeMaintenanceFormToSave, int userID)
      {
         DataSet ret = null;
         var storedProcedure = new StoredProcedure();
         storedProcedure.StoredProcedureName = schema + ".[Data.BadgeMaintenanceForm.Save]";

         //rguidi 3/4/2013 #20202
         //storedProcedure.Parameters.Add(new StoredProcedureParameter("@BadgeMaintenanceTypes", ParameterType.DBString, BadgeMaintenanceFormToSave));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@BadgeMaintenanceTypes", ParameterType.Structured, BadgeMaintenanceFormToSave));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID));
         ret = storedProcedure.ExecuteMultipleDataSet();
      }

      #region Problem Badges

      public DataTable LocationsByBadgeID(int badgeID)
      {
         DataTable result = null;
         StoredProcedure storedProcedure = new StoredProcedure() { StoredProcedureName = schema + ".[Locations.ByBadge]" };

         StoredProcedureParameter sppBadgeID = new StoredProcedureParameter("@BadgeID", ParameterType.DBInteger, badgeID);
         storedProcedure.Parameters.Add(sppBadgeID);

         result = storedProcedure.ExecuteDataSet();

         return result;
      }

      public DataTable PersonGetBadges(int personID, bool activeOnly = true)
      {
         DataTable result = null;
         StoredProcedure storedProcedure = new StoredProcedure() { StoredProcedureName = schema + ".[Person.GetBadges]" };

         StoredProcedureParameter sppPersonID = new StoredProcedureParameter("@PersonID", ParameterType.DBInteger, personID);
         StoredProcedureParameter sppActiveOnly = new StoredProcedureParameter("@ActiveOnly", ParameterType.DBBoolean, activeOnly);

         storedProcedure.Parameters.Add(sppPersonID);
         storedProcedure.Parameters.Add(sppActiveOnly);

         result = storedProcedure.ExecuteDataSet();

         return result;
      }

      #endregion Problem Badges

      #region Badge Record

      public DataSet BadgeInfoByBadgeNumber(string badgeNumber, int userID)
      {
         DataSet result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = schema + ".[Review.Badge.Info.ByBadgeNumber]" };
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@BadgeNumber", ParameterType.DBString, badgeNumber));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID));

         result = storedProcedure.ExecuteMultipleDataSet();
         return result;
      }

      public DataTable BadgeNotes(int personID, int userID)
      {
         DataTable result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = schema + ".[Review.Badge.NotesByPersonID]" };
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@PersonID", ParameterType.DBInteger, personID));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID));

         result = storedProcedure.ExecuteDataSet();
         return result;
      }

      public DataTable BadgesByBadgeNumber(string badgeNumber, bool activeOnly, int userID)
      {
         DataTable result = null;
         var storedProcedure = new StoredProcedure();
         storedProcedure.StoredProcedureName = schema + ".[Data.BadgeReviewByBadgeNumber.Load]";
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@BadgeNumber", ParameterType.DBString, badgeNumber));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@ActiveOnly", ParameterType.DBBoolean, activeOnly));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID));

         result = storedProcedure.ExecuteDataSet();
         return result;
      }

      public DataTable BadgesByName(string firstNamePattern, string middleNamePattern, string lastNamePattern, bool activeOnly, int userID)
      {
         DataTable result = null;
         var storedProcedure = new StoredProcedure();
         storedProcedure.StoredProcedureName = schema + ".[Data.BadgeReviewByName.Load]";
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@FirstNamePattern", ParameterType.DBString, firstNamePattern));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@MiddleNamePattern", ParameterType.DBString, middleNamePattern));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@LastNamePattern", ParameterType.DBString, lastNamePattern));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@ActiveOnly", ParameterType.DBBoolean, activeOnly));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID));

         result = storedProcedure.ExecuteDataSet();
         return result;
      }

      public DataTable BadgesByPersonID(int personID, int userID)
      {
         DataTable result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = schema + ".[Badging.Badges.ByPersonID]" };
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@PersonID", ParameterType.DBInteger, personID));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID));

         result = storedProcedure.ExecuteDataSet();
         return result;
      }
      public DataTable DocumentsByPersonID(int personID)
      {
         DataTable result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = schema + ".[Badging.Documents.ByPersonID]" };
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@PersonID", ParameterType.DBInteger, personID));

         result = storedProcedure.ExecuteDataSet();
         return result;
      }

      public DataTable BadgesByPersonDivsionXrefId(int personDivisionXrefId, int userId)
      {
         DataTable result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = schema + ".[Badging.Badges.ByPersonDivsionXrefId]" };
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@PersonDivisionXrefId", ParameterType.DBInteger, personDivisionXrefId));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userId));

         result = storedProcedure.ExecuteDataSet();
         return result;
      }

      public DataTable GetPersonByNumber(string badgeNr, int userID)
      {
         DataTable result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = schema + ".[Review.Bage.PersonByBadgeNr]" };
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@BadgeNr", ParameterType.DBString, badgeNr));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID));

         result = storedProcedure.ExecuteDataSet();
         return result;
      }

      //JBienvenu 19202 2012-12-27 new
      public void Police_InvalidateBadges(DataTable data, int userID)
      {
         DataTable result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = schema + ".[Police.InvalidateBadge.Save]" };
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@Data", ParameterType.Structured, data));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID));

         result = storedProcedure.ExecuteDataSet();

         //return result;
      }

      #endregion Badge Record

      #region Badging Appointment

      public DataTable BadgingAppointmentGetJobRolesByAgreementID(int agreementID)
      {
         DataTable ret = null;
         var storedProcedure = new StoredProcedure();
         storedProcedure.StoredProcedureName = schema + ".[Badging.Appointment.GetJobRolesByAgreementID]";
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@AgreementID", ParameterType.DBInteger, agreementID));

         ret = storedProcedure.ExecuteDataSet();
         return ret;
      }

      public DataTable BadgingAppointmentGetJobRolesByAgreementNumber(string agreementNumber)
      {
         DataTable ret = null;
         var storedProcedure = new StoredProcedure();
         storedProcedure.StoredProcedureName = schema + ".[Badging.Appointment.GetJobRolesByAgreementNumber]";
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@AgreementNumber", ParameterType.DBString, agreementNumber));

         ret = storedProcedure.ExecuteDataSet();
         return ret;
      }

      public DataTable BadgingAppointmentGetPeople(string companyCode, string divisionCode, string SSN, string lastName, string OAS_Name, string yearOfBirth, string BadgeNumber)
      {
         DataTable ret = null;
         var storedProcedure = new StoredProcedure();
         storedProcedure.StoredProcedureName = schema + ".[Badging.Appointment.GetPeople]";
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyCode", ParameterType.DBString, companyCode));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionCode", ParameterType.DBString, divisionCode));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@SSN", ParameterType.DBString, SSN));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@LastName", ParameterType.DBString, lastName));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@OAS_Name", ParameterType.DBString, OAS_Name));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@YearOfBirth", ParameterType.DBString, yearOfBirth));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@BadgeNumber", ParameterType.DBString, BadgeNumber));

         ret = storedProcedure.ExecuteDataSet();
         return ret;
      }

      public DataTable BadgingAppointmentGetPeopleMixed(string companyCode, string divisionCode, string SSN, string lastName, string OAS_Name, string yearOfBirth, string BadgeNumber)
      {
         DataTable ret = null;
         var storedProcedure = new StoredProcedure();
         storedProcedure.StoredProcedureName = schema + ".[Badging.Appointment.GetPeopleMixed]";
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyCode", ParameterType.DBString, companyCode));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionCode", ParameterType.DBString, divisionCode));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@SSN", ParameterType.DBString, SSN));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@LastName", ParameterType.DBString, lastName));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@OAS_Name", ParameterType.DBString, OAS_Name));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@YearOfBirth", ParameterType.DBString, yearOfBirth));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@BadgeNumber", ParameterType.DBString, BadgeNumber));

         ret = storedProcedure.ExecuteDataSet();
         return ret;
      }

      //Rguidi 6/24/2013 TFS 21953: Fingerprinting and Badging share the same search form and query, so need a 2nd query if want Badging searches to limit to people with badges
      public DataTable BadgingAppointmentGetPeopleWithBadges(string companyCode, string divisionCode, string SSN, string lastName, string OAS_Name, string yearOfBirth, string BadgeNumber)
      {
         DataTable ret = null;
         var storedProcedure = new StoredProcedure();
         storedProcedure.StoredProcedureName = schema + ".[Badging.Appointment.GetPeopleWithBadges]";
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyCode", ParameterType.DBString, companyCode));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionCode", ParameterType.DBString, divisionCode));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@SSN", ParameterType.DBString, SSN));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@LastName", ParameterType.DBString, lastName));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@OAS_Name", ParameterType.DBString, OAS_Name));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@YearOfBirth", ParameterType.DBString, yearOfBirth));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@BadgeNumber", ParameterType.DBString, BadgeNumber));

         ret = storedProcedure.ExecuteDataSet();
         return ret;
      }

      public DataTable BadgingAppointmentGetWorkLocationsByAgreementID(int agreementID)
      {
         DataTable ret = null;
         var storedProcedure = new StoredProcedure();
         storedProcedure.StoredProcedureName = schema + ".[Badging.Appointment.GetWorkLocationsByAgreementID]";
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@AgreementID", ParameterType.DBInteger, agreementID));

         ret = storedProcedure.ExecuteDataSet();
         return ret;
      }

      public DataTable BadgingAppointmentGetWorkLocationsByAgreementIDAndJobRoleID(int agreementID, Int16 sJobRoleID)
      {
         DataTable ret = null;
         var storedProcedure = new StoredProcedure();
         storedProcedure.StoredProcedureName = schema + ".[Badging.Appointment.GetWorkLocationsByAgreementIDAndJobRoleID]";
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@AgreementID", ParameterType.DBInteger, agreementID));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@JobRoleID", ParameterType.DBSingle, sJobRoleID));

         ret = storedProcedure.ExecuteDataSet();
         return ret;
      }

      public bool SaveBadgingResults(BadgingResults badgingResults, int userID, bool CreateBackgroundCheck)
      {
         bool result = false;

         try
         {
            StoredProcedure storedProcedure = new StoredProcedure()
            {
               StoredProcedureName = schema + ".[Badging.Appointment.Save]"
            };

            string countryCode = badgingResults.BiographicModel.CountryCode;
            if (countryCode == null || countryCode == string.Empty)
            {
               badgingResults.BiographicModel.CountryCode = "USA";
            }

            string countrySubdivisionCode = badgingResults.BiographicModel.CountrySubdivisionCode;
            if (countrySubdivisionCode == null)
            {
               badgingResults.BiographicModel.CountrySubdivisionCode = string.Empty;
            }

            string countryCode_Birth = badgingResults.BiographicModel.CountryCode_Birth;
            if (countryCode_Birth == null || countryCode_Birth == string.Empty)
            {
               badgingResults.BiographicModel.CountryCode_Birth = "USA";
            }

            //JBienvenu 2013-01-09 new block
            string countrySubdivisionCode_Birth = badgingResults.BiographicModel.CountrySubdivisionCode_Birth;
            if (countrySubdivisionCode_Birth == null)
            {
               badgingResults.BiographicModel.CountrySubdivisionCode_Birth = string.Empty;
            }

            string strBadgingResults = XmlHelper.Serialize(badgingResults);
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@BadgingResults", ParameterType.DBString, strBadgingResults));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@CreateBackgroundCheck", ParameterType.DBBoolean, CreateBackgroundCheck));
            storedProcedure.ExecuteDataSet();

            result = true;
         }
         catch (Exception ex)
         {
            result = false;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SaveBadgingResults() Exception.Message: " + ex.Message);
            sb.AppendLine("SaveBadgingResults()  Exception.InnerException: " + ex.InnerException);
            sb.AppendLine("SaveBadgingResults()  Exception.Source: " + ex.Source);
            sb.AppendLine("SaveBadgingResults()  Exception.StackTrace: " + ex.StackTrace);

            log4net.ILog log = log4net.LogManager.GetLogger("AirportIQ.Web.Verbose");
            log.Debug(sb.ToString());
            throw ex;
         }

         return result;
      }

      public bool SaveBadgingNotes(BadgingResults badgingResults, int userID)
      {
         bool result = false;

         try
         {
            StoredProcedure storedProcedure = new StoredProcedure()
            {
               StoredProcedureName = schema + ".[Badging.Appointment.Notes.Save]"
            };

            string strBadgingResults = XmlHelper.Serialize(badgingResults);
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@BadgingResults", ParameterType.DBString, strBadgingResults));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@PersonID", ParameterType.DBInteger, badgingResults.BiographicModel.PersonID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID));

            storedProcedure.ExecuteDataSet();

            result = true;
         }
         catch (Exception ex)
         {
            result = false;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SaveBadgingNotes() Exception.Message: " + ex.Message);
            sb.AppendLine("SaveBadgingNotes()  Exception.InnerException: " + ex.InnerException);
            sb.AppendLine("SaveBadgingNotes()  Exception.Source: " + ex.Source);
            sb.AppendLine("SaveBadgingNotes()  Exception.StackTrace: " + ex.StackTrace);

            log4net.ILog log = log4net.LogManager.GetLogger("AirportIQ.Web.Verbose");
            log.Debug(sb.ToString());
            throw ex;
         }

         return result;
      }

      public DataTable GetNewPersonDivisionXrefID(string prefix, string firstName, string middleName, string lastName, string suffix, string ssn, DateTime dateOfBirth, int companyID, int divisionID, int newCompanyID, int newDivisionID, int UserID)
      {
         DataTable ret = null;
         DataSet ds;
         try
         {
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Badging.GetNewPersonDivisionXrefID_NEW]";

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@NamePrefixCode", ParameterType.DBString, prefix));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@FirstName", ParameterType.DBString, firstName));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@MiddleName", ParameterType.DBString, middleName));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@LastName", ParameterType.DBString, lastName));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@NameSuffixCode", ParameterType.DBString, suffix));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@Ssn", ParameterType.DBString, ssn));

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@DateOfBirth", ParameterType.DBDateTime, dateOfBirth));

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@CompanyID", ParameterType.DBInteger, companyID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@DivisionID", ParameterType.DBInteger, divisionID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@NewCompanyID", ParameterType.DBInteger, newCompanyID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@NewDivisionID", ParameterType.DBInteger, newDivisionID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@Retval", ParameterType.DBInteger, 1, AirportIQ.Data.SqlServer.Initializers.ParameterDirection.Out, 0));
            ds = storedProcedure.ExecuteMultipleDataSet();

            //int retval = (int)storedProcedure.Parameters[12].Value;
            //if (retval == -1) //Arrgghhh: the DAL does not return the updated value, soooooo ....

            if (ds.Tables.Count > 1)
            {
               ret = ds.Tables[1]; // was a new person
            }
            else
            {
               ret = ds.Tables[0]; // found person, or error msg
            }
         }
         catch (Exception ex)
         {
            throw ex;
         }

         return ret;
      }

      public DataTable BackgroundCheckLast45Days(int personDivisionXrefID)
      {
         DataTable ret = null;

         try
         {
            var storedProcedure = new StoredProcedure();

            storedProcedure.StoredProcedureName = schema + ".[BackgroundCheck.BackgroundCheckLast45Days]";

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@PersonDivisionXrefID", ParameterType.DBInteger, personDivisionXrefID));

            ret = storedProcedure.ExecuteDataSet();
         }
         catch (Exception ex)
         {
            throw ex;
         }

         return ret;
      }

      public DataTable GetCertifiedTrainers(int companyId)
      {
         DataTable ret = null;

         try
         {
            var storedProcedure = new StoredProcedure();

            storedProcedure.StoredProcedureName = schema + ".[Badging.Company.CertifiedTrainers]";

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@CompanyID", ParameterType.DBInteger, companyId));

            ret = storedProcedure.ExecuteDataSet();
         }
         catch (Exception ex)
         {
            throw ex;
         }

         return ret;
      }

      public DataTable GetAuthorizedSigners(int divisionID)
      {
         DataTable ret = null;

         try
         {
            var storedProcedure = new StoredProcedure();

            storedProcedure.StoredProcedureName = schema + ".[Badging.CompanyDivision.AuthorizedSigners]";

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@DivisionID", ParameterType.DBInteger, divisionID));

            ret = storedProcedure.ExecuteDataSet();
         }
         catch (Exception ex)
         {
            throw ex;
         }

         return ret;
      }

      public DataTable BackgroundCheckCopy(int personDivisionCheckIDCopyFrom, int personDivisionXrefIDCopyTo, int UserID)
      {
         var storedProcedure = new StoredProcedure();

         storedProcedure.StoredProcedureName = schema + ".[BackgroundCheck.BackgroundCheckCopy]";

         storedProcedure.Parameters.Add(new StoredProcedureParameter("@PersonDivisionCheckIDCopyFrom", ParameterType.DBInteger, personDivisionCheckIDCopyFrom));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@PersonDivisionXrefIDCopyTo", ParameterType.DBInteger, personDivisionXrefIDCopyTo));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));

         return storedProcedure.ExecuteDataSet();
      }

      public DataTable GetSetNextSSNDummy()
      { //rguidi 8/29/2013
         DataTable result = null;

         try
         {
            StoredProcedure storedProcedure = new StoredProcedure()
            {
               StoredProcedureName = schema + ".[Badging.GetNextNonUSCitSSN]"
            };
            result = storedProcedure.ExecuteDataSet();
         }
         catch (Exception ex)
         {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("GetSetNextSSNDummy() Exception.Message: " + ex.Message);
            sb.AppendLine("GetSetNextSSNDummy()  Exception.InnerException: " + ex.InnerException);
            sb.AppendLine("GetSetNextSSNDummy()  Exception.Source: " + ex.Source);
            sb.AppendLine("GetSetNextSSNDummy()  Exception.StackTrace: " + ex.StackTrace);

            log4net.ILog log = log4net.LogManager.GetLogger("AirportIQ.Web.Verbose");
            log.Debug(sb.ToString());
            throw ex;
         }

         return result;
      }

      public bool ValidateSSN(int PersonID, string SSN)
      {  //rguidi 8/30/2013
         bool result = false;

         try
         {
            StoredProcedure storedProcedure = new StoredProcedure()
            {
               StoredProcedureName = schema + ".[Badging.ValidateSSN]"
            };

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@PersonID", ParameterType.DBInteger, PersonID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@SSN", ParameterType.DBString, SSN));

            var proc_result = "-1";
            //storedProcedure.ExecuteDataSetWithIDOutputParam(out proc_result); //this does not accept input params??
            DataTable dt = null;
            dt = storedProcedure.ExecuteDataSet(); //why don't we have an storedProcedure.ExecuteScalar function??
            proc_result = dt.Rows[0][0].ToString();

            if (proc_result == "-1")
            {
               result = false;
            }
            else
            {
               result = true;
            }

         }
         catch (Exception ex)
         {
            result = false;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("ValidateSSN() Exception.Message: " + ex.Message);
            sb.AppendLine("ValidateSSN()  Exception.InnerException: " + ex.InnerException);
            sb.AppendLine("ValidateSSN()  Exception.Source: " + ex.Source);
            sb.AppendLine("ValidateSSN()  Exception.StackTrace: " + ex.StackTrace);

            log4net.ILog log = log4net.LogManager.GetLogger("AirportIQ.Web.Verbose");
            log.Debug(sb.ToString());
            throw ex;
         }

         return result;
      }

      public void SetLastCHRCAndSTADates(int personDivisionXrefID)
      {
         var storedProcedure = new StoredProcedure();
         storedProcedure.StoredProcedureName = schema + ".[Badging.SetLastCHRCAndSTADates]";

         storedProcedure.Parameters.Add(new StoredProcedureParameter("@PersonDivisionXrefID", ParameterType.DBInteger, personDivisionXrefID));

         storedProcedure.ExecuteNonQuery();
      }

      public void HandleExempt(int empID, string guid)
      {
         var storedProcedure = new StoredProcedure();
         storedProcedure.StoredProcedureName = "[Utility].[SBO.ExemptPush]";

         storedProcedure.Parameters.Add(new StoredProcedureParameter("@EMP_ID", ParameterType.DBInteger, empID));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@GUID", ParameterType.DBString, guid));

         storedProcedure.ExecuteNonQuery();
      }

      public void SetCHRCAndSTAResults(int personDivisionXrefID, string CHRCResult, string STAResult)
      {
         var storedProcedure = new StoredProcedure();
         storedProcedure.StoredProcedureName = schema + ".[Badging.SetCHRCAndSTAResults]";

         storedProcedure.Parameters.Add(new StoredProcedureParameter("@PersonDivisionXrefID", ParameterType.DBInteger, personDivisionXrefID));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@CHRCResult", ParameterType.DBString, CHRCResult));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@STAResult", ParameterType.DBString, STAResult));

         storedProcedure.ExecuteNonQuery();
      }

      public void UpdateIWSBadgeInfo(string boaaBadgeID, string iwsCardID)
      {
         var storedProcedure = new StoredProcedure();
         storedProcedure.StoredProcedureName ="[App.Sbo].[IWS.NegateIWSBadgeID]";

         storedProcedure.Parameters.Add(new StoredProcedureParameter("@BOAA_BadgeID", ParameterType.DBInteger, boaaBadgeID));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@IWS_CardID", ParameterType.DBInteger, iwsCardID));
         

         try
         {
            storedProcedure.ExecuteNonQuery();
         }
         catch (Exception ex)
         {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("IwsBadgeInversion() Exception.Message: " + ex.Message);
            sb.AppendLine("IwsBadgeInversion()  Exception.InnerException: " + ex.InnerException);
            sb.AppendLine("IwsBadgeInversion()  Exception.Source: " + ex.Source);
            sb.AppendLine("IwsBadgeInversion()  Exception.StackTrace: " + ex.StackTrace);

            log4net.ILog log = log4net.LogManager.GetLogger("AirportIQ.Web.Verbose");
            log.Debug(sb.ToString());
            throw ex;
         }
      }

      public void UpdateBadgeColor(string badgeID, string color)
      {
         var storedProcedure = new StoredProcedure();
         storedProcedure.StoredProcedureName = "[App.Sbo].[Badging.UpdateBadgeColor]";

         storedProcedure.Parameters.Add(new StoredProcedureParameter("@BadgeID", ParameterType.DBInteger, int.Parse(badgeID)));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@BadgeColor", ParameterType.DBInteger, int.Parse(color)));

         try
         {
            storedProcedure.ExecuteNonQuery();
         }
         catch (Exception ex)
         {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("UpdateBadgeColor() Exception.Message: " + ex.Message);
            sb.AppendLine("UpdateBadgeColor()  Exception.InnerException: " + ex.InnerException);
            sb.AppendLine("UpdateBadgeColor()  Exception.Source: " + ex.Source);
            sb.AppendLine("UpdateBadgeColor()  Exception.StackTrace: " + ex.StackTrace);

            log4net.ILog log = log4net.LogManager.GetLogger("AirportIQ.Web.Verbose");
            log.Debug(sb.ToString());
            throw ex;
         }
      }


      public void InsertTSCTransaction(int badgeId, int UserID)
      {
         var storedProcedure = new StoredProcedure();
         storedProcedure.StoredProcedureName = schema + ".[Badging.InsertTSCTransaction]";

         storedProcedure.Parameters.Add(new StoredProcedureParameter("@BadgeID", ParameterType.DBInteger, badgeId));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));
         storedProcedure.ExecuteNonQuery();
      }

      public DataTable GetShadowData(int divisionId)
      {
         DataTable ret = null;

         try
         {
            var storedProcedure = new StoredProcedure();

            storedProcedure.StoredProcedureName = schema + ".[Badging.GetShadowData]";

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@DivisionID", ParameterType.DBInteger, divisionId));

            ret = storedProcedure.ExecuteDataSet();
         }
         catch (Exception ex)
         {
            throw ex;
         }

         return ret;
      }

      #endregion Badging Appointment

      #region Invalidate Badge

      /// <summary>
      /// Returns the badge details for the invalidate badge screen
      /// </summary>
      /// <param name="userId">The user id.</param>
      /// <param name="badgeNumber">The badge number.</param>
      /// <returns></returns>
      public DataTable InvalidateBadgeDetails(int userId, int badgeId)
      {
         var storedProcedure = new StoredProcedure() { StoredProcedureName = schema + ".[Badging.InvalidateBadgeDetails]" };
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@userId", ParameterType.DBInteger, userId));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@badgeId", ParameterType.DBInteger, badgeId));
         var result = storedProcedure.ExecuteMultipleDataSet();
         return result != null && result.Tables.Count > 0 ? result.Tables[0] : null;
      }

      /// <summary>
      /// Gets the how reported list.
      /// </summary>
      /// <returns></returns>
      public DataTable GetHowReportedList()
      {
         var storedProcedure = new StoredProcedure() { StoredProcedureName = schema + ".[Badging.GetHowReportedList]" };
         var result = storedProcedure.ExecuteMultipleDataSet();
         return result != null && result.Tables.Count > 0 ? result.Tables[0] : null;
      }

      /// <summary>
      /// Inserts the badge missing report.
      /// </summary>
      /// <param name="details">The details.</param>
      /// <param name="staffID">The staff ID.</param>
      public void InsertBadgeMissingReport(LostBadgeDetails details, int staffID)
      {
         var storedProcedure = new StoredProcedure() { StoredProcedureName = schema + ".[Badging.InsertBadgeMissingReport]" };

         storedProcedure.Parameters.Add(new StoredProcedureParameter("@BadgeID", ParameterType.DBInteger, details.BadgeID));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@WhenReported", ParameterType.DBDateTime, details.ReportedDate));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@DateOfOccurrence", ParameterType.DBDateTime, details.DateOfOccurance));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@BadgeStatusCode", ParameterType.DBString, details.Type));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@CustomSeal", ParameterType.DBString, details.Type));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@ReportedByName", ParameterType.DBString, details.ReportedBy));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@ReportedByTitle", ParameterType.DBString, details.Title));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@ReportedByPhoneNumber", ParameterType.DBString, details.Phone));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@BadgeMissingReportMethodID", ParameterType.DBInteger, details.HowReported));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@Notes", ParameterType.DBString, details.Details));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@StaffID", ParameterType.DBInteger, staffID));

         storedProcedure.ExecuteNonQuery();
      }

      #endregion

      #endregion Public Methods
   }
}