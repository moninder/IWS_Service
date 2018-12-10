using System;
using System.Configuration;
using System.Data;
using AirportIQ.Data.SqlServer.Initializers;
using System.Collections.Generic;

namespace AirportIQ.Data.SqlServer.Repositories
{
   public class AuditRepository : IAuditRepository
   {
      #region Private Variables

      private readonly string _schema = ConfigurationManager.AppSettings["SBO.ApplicationSchema"];

      #endregion Private Variables

      #region Public Methods

      public DataTable ListCompaniesWithActiveBadges(string facilityCode)
      {
         DataTable result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Lists.Companies.WithActiveBadges]" };

         var paramFacilityCode = new StoredProcedureParameter("@FacilityCode", ParameterType.DBString, facilityCode);
         storedProcedure.Parameters.Add(paramFacilityCode);

         result = storedProcedure.ExecuteDataSet();

         return result;
      }

      public DataTable ListDivisionsWithActiveBadges(int companyID, string facilityCode)
      {
         DataTable result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Lists.Divisions.WithActiveBadges]" };

         var paramCompanyID = new StoredProcedureParameter("@CompanyId", ParameterType.DBInteger, companyID);
         var paramFacilityCode = new StoredProcedureParameter("@FacilityCode", ParameterType.DBString, facilityCode);

         storedProcedure.Parameters.Add(paramCompanyID);
         storedProcedure.Parameters.Add(paramFacilityCode);

         result = storedProcedure.ExecuteDataSet();
         return result;
      }

      public DataTable GetAuditSpecificationList()
      {
         DataTable result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.List.Specifications]" };

         result = storedProcedure.ExecuteDataSet();

         return result;
      }

      public DataTable GetAuditCompanyList(int auditID)
      {
         DataTable result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Lists.AuditCompanies]" };

         var paramAuditID = new StoredProcedureParameter("@AuditSpecificationID", ParameterType.DBInteger, auditID);

         storedProcedure.Parameters.Add(paramAuditID);

         result = storedProcedure.ExecuteDataSet();
         return result;
      }

      public DataTable GetCompanyList()
      {
         DataTable result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Lists.Companies]" };
         result = storedProcedure.ExecuteDataSet();
         return result;
      }

      public DataTable GetAuditDivisionList(int auditID, int companyID)
      {
         DataTable result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Lists.AuditDivisions]" };

         var paramAuditID = new StoredProcedureParameter("@AuditSpecificationID", ParameterType.DBInteger, auditID);
         var paramCompanyID = new StoredProcedureParameter("@CompanyId", ParameterType.DBInteger, companyID);

         storedProcedure.Parameters.Add(paramAuditID);
         storedProcedure.Parameters.Add(paramCompanyID);

         result = storedProcedure.ExecuteDataSet();
         return result;
      }

      public DataTable GetDivisionList(int companyID)
      {
         DataTable result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Lists.Divisions]" };

         var paramCompanyID = new StoredProcedureParameter("@CompanyId", ParameterType.DBInteger, companyID);
         storedProcedure.Parameters.Add(paramCompanyID);

         result = storedProcedure.ExecuteDataSet();
         return result;
      }

      public DataTable GetAuditorList(string facilityCode)
      {
         DataTable result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Lists.Auditors]" };
         var paramFacilityCode = new StoredProcedureParameter("@FacilityCode", ParameterType.DBString, facilityCode);
         storedProcedure.Parameters.Add(paramFacilityCode);

         result = storedProcedure.ExecuteDataSet();

         return result;
      }

      public DataSet AuditFormLoad(int auditID, int divisionID)
      {
         DataSet result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Editor.Load]" };
         var paramAuditID = new StoredProcedureParameter("@AuditId", ParameterType.DBInteger, auditID);
         var paramDivisionID = new StoredProcedureParameter("@DivisionId", ParameterType.DBInteger, divisionID);

         storedProcedure.Parameters.Add(paramAuditID);
         storedProcedure.Parameters.Add(paramDivisionID);

         result = storedProcedure.ExecuteMultipleDataSet();
         return result;
      }

      public bool AuditFormSave(DataSet ds, int userID)
      {
         const int tblDivisions = 0;
         const int tblBadges = 1;
         StoredProcedure storedProcedure;
         StoredProcedureParameter paramAuditBadgeTableType;
         var paramAuditDivisionTable = new StoredProcedureParameter();
         var paramUserID = new StoredProcedureParameter();

         bool result;
         try
         {
            storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Editor.Save]" };

            paramAuditDivisionTable = new StoredProcedureParameter
            {
               Name = "@AuditDivisionTable",
               DBValueType = ParameterType.Structured,
               Value = ds.Tables[tblDivisions]
            };

            //copy and drop the note date column which is not a part of the user defined table type
            DataTable table = ds.Tables[tblBadges].Copy();
            //if (table.Columns.Contains("NoteDate"))
            //    table.Columns.Remove("NoteDate");

            paramAuditBadgeTableType = new StoredProcedureParameter
            {
               Name = "@AuditBadgeTable",
               DBValueType = ParameterType.Structured,
               Value = table
            };

            paramUserID = new StoredProcedureParameter
            {
               Name = "@UserID",
               DBValueType = ParameterType.DBString,
               Value = userID
            };

            storedProcedure.Parameters.Add(paramAuditDivisionTable);
            storedProcedure.Parameters.Add(paramAuditBadgeTableType);
            storedProcedure.Parameters.Add(paramUserID);
            storedProcedure.ExecuteDataSet();
            result = true;
         }
         catch (Exception ex)
         {
            throw ex;
         }

         return result;
      }

      public DataSet AuditProposalLoad(int numBadges, int divPercentageToAudit, DateTime maxLastAuditDate, int divMinBadges, int divMaxBadges, int assignedPersonId, bool doNotExceedNumBadges, string facilityCode, string divisionTypeId, Dictionary<string, bool> agencies)
      {
         DataSet result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Proposal.Load]" };

         var pNumBadges = new StoredProcedureParameter("@NumBadges", ParameterType.DBInteger, numBadges);
         var pDivPercentageToAudit = new StoredProcedureParameter("@DivPercentageToAudit", ParameterType.DBInteger, divPercentageToAudit);
         var pMaxLastAuditDate = new StoredProcedureParameter("@MaxLastAuditDate", ParameterType.DBDateTime, maxLastAuditDate);
         var pDivMinBadge = new StoredProcedureParameter("@DivMinBadges", ParameterType.DBInteger, divMinBadges);
         var pDivMaxBadge = new StoredProcedureParameter("@DivMaxBadges", ParameterType.DBInteger, divMaxBadges);
         var pAssignedPersonId = new StoredProcedureParameter("@AssignedPersonId", ParameterType.DBInteger, assignedPersonId);
         var pReturnExactNumBadges = new StoredProcedureParameter("@ReturnExactNumBadges", ParameterType.DBBoolean, doNotExceedNumBadges);
         var pFacilityCode = new StoredProcedureParameter("@FacilityCode", ParameterType.DBString, facilityCode);

         storedProcedure.Parameters.Add(pNumBadges);
         storedProcedure.Parameters.Add(pDivPercentageToAudit);
         storedProcedure.Parameters.Add(pMaxLastAuditDate);
         storedProcedure.Parameters.Add(pDivMinBadge);
         storedProcedure.Parameters.Add(pDivMaxBadge);
         storedProcedure.Parameters.Add(pAssignedPersonId);
         storedProcedure.Parameters.Add(pReturnExactNumBadges);
         storedProcedure.Parameters.Add(pFacilityCode);

         var pFire = !agencies["FIRE"] ? "FIRE" : "";
         var pLawa = !agencies["LAWA"] ? "LAWA" : "";
         var pPol =  !agencies["POL"]  ? "POL" : "";
         var pGovt = !agencies["GOVT"] ? "GOVT" : "";
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@FIRE", ParameterType.DBString, pFire));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@LAWA", ParameterType.DBString, pLawa));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@POL", ParameterType.DBString, pPol));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@GOVT", ParameterType.DBString, pGovt));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@DivisionTypeId", ParameterType.DBString, divisionTypeId));

         result = storedProcedure.ExecuteMultipleDataSet();
         return result;
      }

      public int AuditProposalSave(DataSet ds, int userID)
      {
         const int tblAuditSpecifications = 0;
         const int tblAuditDivisions = 1;

         int result = -1;
         StoredProcedure storedProcedure;
         StoredProcedureParameter sppAuditSpecificationsTable;
         StoredProcedureParameter sppAuditDivisionsTable;
         StoredProcedureParameter sppUserID;

         //TODO: use names for tables and columns
         try
         {
            sppAuditSpecificationsTable = new StoredProcedureParameter
            {
               Name = "@AuditSpecificationsTable",
               DBValueType = ParameterType.Structured,
               Value = ds.Tables[tblAuditSpecifications]
            };

            sppAuditDivisionsTable = new StoredProcedureParameter
            {
               Name = "@AuditDivisionsTable",
               DBValueType = ParameterType.Structured,
               Value = ds.Tables[tblAuditDivisions]
            };

            sppUserID = new StoredProcedureParameter
            {
               Name = "@UserID",
               DBValueType = ParameterType.DBInteger,
               Value = userID
            };

            storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Proposal.Save]" };
            storedProcedure.Parameters.Add(sppAuditSpecificationsTable);
            storedProcedure.Parameters.Add(sppAuditDivisionsTable);
            storedProcedure.Parameters.Add(sppUserID);

            storedProcedure.ExecuteDataSetWithIDOutputParam(out result);

            return result;
         }
         catch (Exception ex)
         {
            throw ex;
         }
      }

      public DataTable GetAuditsByDivision(int divisionID)
      {
         DataTable result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.ByDivision]" };
         var paramDivisionID = new StoredProcedureParameter("@DivisionId", ParameterType.DBInteger, divisionID);
         storedProcedure.Parameters.Add(paramDivisionID);
         result = storedProcedure.ExecuteDataSet();

         return result;
      }

      public DataTable GetAuditsByDivisionAndStaffID(int divisionID, int staffID)
      {
         DataTable result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.ByDivisionAndStaffID]" };
         var paramDivisionID = new StoredProcedureParameter("@DivisionID", ParameterType.DBInteger, divisionID);
         storedProcedure.Parameters.Add(paramDivisionID);

         var paramStaffID = new StoredProcedureParameter("@StaffID", ParameterType.DBInteger, staffID);
         storedProcedure.Parameters.Add(paramStaffID);

         result = storedProcedure.ExecuteDataSet();

         return result;
      }

      public DataTable GetAuditsByAuditName(string auditName)
      {
         DataTable result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.AuditsByName]" };
         var paramAuditName = new StoredProcedureParameter("@AuditName", ParameterType.DBString, auditName);
         storedProcedure.Parameters.Add(paramAuditName);
         result = storedProcedure.ExecuteDataSet();

         return result;
      }

      public DataTable GetBadgeStatuses()
      {
         DataTable result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Person.Lists.BadgeStatuses]" };
         result = storedProcedure.ExecuteDataSet();

         return result;
      }

      public DataTable GetTotalBadgeInspec()
      {
         DataTable result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.TotalAuditCount]" };
         result = storedProcedure.ExecuteDataSet();

         return result;
      }

      public DataTable GetAuditDivisionInfo(int auditSpecificationID, int divisionID)
      {
         DataTable result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.AuditDivisionInfo]" };
         var paramAuditID = new StoredProcedureParameter("@AuditSpecificationID", ParameterType.DBInteger, auditSpecificationID);
         var paramDivisionID = new StoredProcedureParameter("@DivisionId", ParameterType.DBInteger, divisionID);

         storedProcedure.Parameters.Add(paramAuditID);
         storedProcedure.Parameters.Add(paramDivisionID);

         result = storedProcedure.ExecuteDataSet();

         return result;
      }

      public DataSet SaveAuditForCause(int auditGroupID, int divisionID, int staffID_Responsible, int percentage, string auditName, int userID)
      {
         DataSet result = null;

         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.ForCause.Save]" };

         var paramAuditGroupID =
             new StoredProcedureParameter("@AuditGroupID", ParameterType.DBInteger, auditGroupID);
         var paramDivisionID =
             new StoredProcedureParameter("@DivisionId", ParameterType.DBInteger, divisionID);
         var paramStaffID_Responsible =
             new StoredProcedureParameter("@StaffID_Responsible", ParameterType.DBInteger, staffID_Responsible);
         var paramPercentage =
             new StoredProcedureParameter("@Percentage", ParameterType.DBInteger, percentage);
         var paramAuditName =
             new StoredProcedureParameter("@AuditName", ParameterType.DBString, auditName);
         var paramUserID =
             new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID);

         storedProcedure.Parameters.Add(paramAuditGroupID);
         storedProcedure.Parameters.Add(paramDivisionID);
         storedProcedure.Parameters.Add(paramStaffID_Responsible);
         storedProcedure.Parameters.Add(paramPercentage);
         storedProcedure.Parameters.Add(paramAuditName);
         storedProcedure.Parameters.Add(paramUserID);

         result = storedProcedure.ExecuteMultipleDataSet();

         return result;
      }

      public DataSet SaveAuditCargo(int auditGroupID, int divisionID, int staffID_Responsible, int percentage, string auditName, int userID)
      {
         DataSet result = null;

         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Cargo.Save]" };

         var paramAuditGroupID =
             new StoredProcedureParameter("@AuditGroupID", ParameterType.DBInteger, auditGroupID);
         var paramDivisionID =
             new StoredProcedureParameter("@DivisionId", ParameterType.DBInteger, divisionID);
         var paramStaffID_Responsible =
             new StoredProcedureParameter("@StaffID_Responsible", ParameterType.DBInteger, staffID_Responsible);
         var paramPercentage =
             new StoredProcedureParameter("@Percentage", ParameterType.DBInteger, percentage);
         var paramAuditName =
             new StoredProcedureParameter("@AuditName", ParameterType.DBString, auditName);
         var paramUserID =
             new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID);

         storedProcedure.Parameters.Add(paramAuditGroupID);
         storedProcedure.Parameters.Add(paramDivisionID);
         storedProcedure.Parameters.Add(paramStaffID_Responsible);
         storedProcedure.Parameters.Add(paramPercentage);
         storedProcedure.Parameters.Add(paramAuditName);
         storedProcedure.Parameters.Add(paramUserID);

         result = storedProcedure.ExecuteMultipleDataSet();

         return result;
      }

      public DataTable SaveSelfAuditOld(int auditGroupID, int divisionID, int staffID_Responsible, int percentage, string auditName, int userID)
      {
         DataTable result = null;

         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Self.Save.Old]" };

         var paramAuditGroupID =
             new StoredProcedureParameter("@AuditGroupID", ParameterType.DBInteger, auditGroupID);
         var paramDivisionID =
             new StoredProcedureParameter("@DivisionId", ParameterType.DBInteger, divisionID);
         var paramStaffID_Responsible =
             new StoredProcedureParameter("@StaffID_Responsible", ParameterType.DBInteger, staffID_Responsible);
         var paramPercentage =
             new StoredProcedureParameter("@Percentage", ParameterType.DBInteger, percentage);
         var paramAuditName =
             new StoredProcedureParameter("@AuditName", ParameterType.DBString, auditName);
         var paramUserID =
             new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID);

         storedProcedure.Parameters.Add(paramAuditGroupID);
         storedProcedure.Parameters.Add(paramDivisionID);
         storedProcedure.Parameters.Add(paramStaffID_Responsible);
         storedProcedure.Parameters.Add(paramPercentage);
         storedProcedure.Parameters.Add(paramAuditName);
         storedProcedure.Parameters.Add(paramUserID);

         result = storedProcedure.ExecuteDataSet();

         return result;
      }

      public DataTable SaveSelfAudit(int auditGroupID, int staffID_Responsible, string auditName, int userID)
      {
         DataTable result = null;

         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Self.Save]" };
         storedProcedure.CommandTimeOut = 3600;

         var paramAuditGroupID =
             new StoredProcedureParameter("@AuditGroupID", ParameterType.DBInteger, auditGroupID);
         var paramStaffID_Responsible =
             new StoredProcedureParameter("@StaffID_Responsible", ParameterType.DBInteger, staffID_Responsible);
         var paramAuditName =
             new StoredProcedureParameter("@AuditName", ParameterType.DBString, auditName);
         var paramUserID =
             new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID);

         storedProcedure.Parameters.Add(paramAuditGroupID);
         storedProcedure.Parameters.Add(paramStaffID_Responsible);
         storedProcedure.Parameters.Add(paramAuditName);
         storedProcedure.Parameters.Add(paramUserID);

         result = storedProcedure.ExecuteDataSet();

         return result;
      }

      public DataTable AuditPoliceInspectionLoad(DateTime startDate, DateTime endDate, string facilityCode)
      {
         DataTable result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.PoliceInspection.Load]" };

         var paramStartDate = new StoredProcedureParameter("@StartDate", ParameterType.DBDateTime, startDate);
         var paramEndDate = new StoredProcedureParameter("@EndDate", ParameterType.DBDateTime, endDate);
         var paramFacilityCode = new StoredProcedureParameter("@FacilityCode", ParameterType.DBString, facilityCode);

         storedProcedure.Parameters.Add(paramStartDate);
         storedProcedure.Parameters.Add(paramEndDate);
         storedProcedure.Parameters.Add(paramFacilityCode);

         result = storedProcedure.ExecuteDataSet();
         return result;
      }

      public DataTable AuditPoliceInspectionBadgesLoad(int auditInspectionID)
      {
         DataTable result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.PoliceInspectionBadges.Load]" };

         var paramAuditInspectionID = new StoredProcedureParameter("@AuditInspectionID", ParameterType.DBInteger, auditInspectionID);

         storedProcedure.Parameters.Add(paramAuditInspectionID);

         result = storedProcedure.ExecuteDataSet();
         return result;
      }

      public DataTable AuditNameChecker(string auditNameBase)
      {
         DataTable result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.AuditNameChecker]" };

         var paramAuditNameBase = new StoredProcedureParameter("@AuditNameBase", ParameterType.DBString, auditNameBase);

         storedProcedure.Parameters.Add(paramAuditNameBase);

         result = storedProcedure.ExecuteDataSet();
         return result;
      }

      public DataTable AuditPoliceInspectionsSave(int auditInspectionID, string facilityCode, DateTime auditDate, int watchNumber, string location, int officerBadgeID, int? officerBadgeID2, int? officerBadgeID3, string _Action, int userID)
      {
         DataTable result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.PoliceInspections.Save]" };

         var paramAuditInspectionID = new StoredProcedureParameter("@AuditInspectionID", ParameterType.DBInteger, auditInspectionID);
         var paramFacilityCode = new StoredProcedureParameter("@FacilityCode", ParameterType.DBString, facilityCode);
         var paramAuditDate = new StoredProcedureParameter("@AuditDate", ParameterType.DBDateTime, auditDate);
         var paramwWatchNumber = new StoredProcedureParameter("@WatchNumber", ParameterType.DBInteger, watchNumber);
         var paramLocation = new StoredProcedureParameter("@Location", ParameterType.DBString, location);
         var paramOfficerBadgeID = new StoredProcedureParameter("@OfficerBadgeID", ParameterType.DBInteger, officerBadgeID);
         var paramOfficerBadgeID2 = new StoredProcedureParameter("@OfficerBadgeID2", ParameterType.DBInteger, officerBadgeID2 != null ? (object)officerBadgeID2.Value : DBNull.Value);
         var paramOfficerBadgeID3 = new StoredProcedureParameter("@OfficerBadgeID3", ParameterType.DBInteger, officerBadgeID3 != null ? (object)officerBadgeID3.Value : DBNull.Value);
         var param_Action = new StoredProcedureParameter("@_Action", ParameterType.DBInteger, _Action);
         var paramUserID = new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID);

         storedProcedure.Parameters.Add(paramAuditInspectionID);
         storedProcedure.Parameters.Add(paramFacilityCode);
         storedProcedure.Parameters.Add(paramAuditDate);
         storedProcedure.Parameters.Add(paramwWatchNumber);
         storedProcedure.Parameters.Add(paramLocation);
         storedProcedure.Parameters.Add(paramOfficerBadgeID);
         storedProcedure.Parameters.Add(paramOfficerBadgeID2);
         storedProcedure.Parameters.Add(paramOfficerBadgeID3);
         storedProcedure.Parameters.Add(param_Action);
         storedProcedure.Parameters.Add(paramUserID);

         result = storedProcedure.ExecuteDataSet();
         return result;
      }

      public DataTable PoliceInspectionBadgesSave
          (
          int auditInspectionBadgesID,
          string auditInspectionTypeCode,
          int auditInspectionID,
          string badgeNumber,
          string badgeLastName,
          DateTime whenBadgeInspected,
          int? badgeID,
          string _Action,
          int userID
          )
      {
         DataTable result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.PoliceInspectionBadges.Save]" };

         var paramAuditInspectionBadgesID = new StoredProcedureParameter("@AuditInspectionBadgesID", ParameterType.DBInteger, auditInspectionBadgesID);
         var paramAuditInspectionTypeCode = new StoredProcedureParameter("@AuditInspectionTypeCode", ParameterType.DBString, auditInspectionTypeCode);
         var paramAuditInspectionID = new StoredProcedureParameter("@AuditInspectionID", ParameterType.DBInteger, auditInspectionID);
         var paramBadgeNumber = new StoredProcedureParameter("@BadgeNumber", ParameterType.DBString, badgeNumber);
         var paramBadgeLastName = new StoredProcedureParameter("@BadgeLastName", ParameterType.DBString, badgeLastName);
         var paramWhenBadgeInspected = new StoredProcedureParameter("@WhenBadgeInspected", ParameterType.DBDateTime, whenBadgeInspected);
         var paramBadgeID = new StoredProcedureParameter("@BadgeID", ParameterType.DBInteger, badgeID);
         var param_Action = new StoredProcedureParameter("@_Action", ParameterType.DBInteger, _Action);
         var paramUserID = new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID);

         storedProcedure.Parameters.Add(paramAuditInspectionBadgesID);
         storedProcedure.Parameters.Add(paramAuditInspectionTypeCode);
         storedProcedure.Parameters.Add(paramAuditInspectionID);
         storedProcedure.Parameters.Add(paramBadgeNumber);
         storedProcedure.Parameters.Add(paramBadgeLastName);
         storedProcedure.Parameters.Add(paramWhenBadgeInspected);
         storedProcedure.Parameters.Add(paramBadgeID);
         storedProcedure.Parameters.Add(param_Action);
         storedProcedure.Parameters.Add(paramUserID);

         result = storedProcedure.ExecuteDataSet();
         return result;
      }

      public DataTable GetAuditInspectionTypesList()
      {
         DataTable result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Lists.InspectionTypes]" };
         result = storedProcedure.ExecuteDataSet();

         return result;
      }

      public DataTable GetOfficers_Active()
      {
         DataTable result = null;

         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Lists.Officers.Active]" };

         result = storedProcedure.ExecuteDataSet();

         return result;
      }

      public DataSet GetSpecificationInfo(int auditSpecificationID)
      {
         DataSet result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Info.Inspections]" };

         var paramAuditSpecificationID = new StoredProcedureParameter("@AuditSpecificationID", ParameterType.DBInteger, auditSpecificationID);

         storedProcedure.Parameters.Add(paramAuditSpecificationID);

         result = storedProcedure.ExecuteMultipleDataSet();
         return result;
      }

      public DataTable AuditEditorAudits(int auditSpecificationID)
      {
         DataTable result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Editor.Audits]" };

         var paramAuditSpecificationID = new StoredProcedureParameter("@AuditSpecificationID", ParameterType.DBInteger, auditSpecificationID);

         storedProcedure.Parameters.Add(paramAuditSpecificationID);

         result = storedProcedure.ExecuteDataSet();
         return result;
      }

      public DataTable AuditEditorWorkItem(int userID, int? workID, int? auditSpecificationID)
      {
         if (workID == null && auditSpecificationID == null)
         {
            throw new Exception("When calling AuditEditorWorkItem workID or auditSpecificationID must have a valid (int) value.");
         }

         if (workID == null)
            workID = -1;

         if (auditSpecificationID == null)
            auditSpecificationID = -1;

         DataTable result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Editor.WorkItem]" };

         var paramUserID = new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID);
         var paramWorkID = new StoredProcedureParameter("@WorkID", ParameterType.DBInteger, workID);
         var paramAuditSpecificationID = new StoredProcedureParameter("@AuditSpecificationID", ParameterType.DBInteger, auditSpecificationID);

         storedProcedure.Parameters.Add(paramUserID);
         storedProcedure.Parameters.Add(paramWorkID);
         storedProcedure.Parameters.Add(paramAuditSpecificationID);

         result = storedProcedure.ExecuteDataSet();
         return result;
      }

      public void AuditEditorComplete(int auditSpecificationID, int workID, int userID)
      {
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Editor.Complete]" };

         var paramAuditSpecificationID = new StoredProcedureParameter("@AuditSpecificationID", ParameterType.DBInteger, auditSpecificationID);
         var paramWorkID = new StoredProcedureParameter("@WorkID", ParameterType.DBInteger, workID);
         var paramUserID = new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID);

         storedProcedure.Parameters.Add(paramAuditSpecificationID);
         storedProcedure.Parameters.Add(paramWorkID);
         storedProcedure.Parameters.Add(paramUserID);

         storedProcedure.ExecuteDataSet();
      }

      public void AuditEditorCompleteDivision(int auditSpecificationID, int divisionID, int userID)
      {
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Editor.Complete.Division]" };

         var paramAuditSpecificationID = new StoredProcedureParameter("@AuditSpecificationID", ParameterType.DBInteger, auditSpecificationID);
         var paramDivisionID = new StoredProcedureParameter("@DivisionID", ParameterType.DBInteger, divisionID);
         var paramUserID = new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID);

         storedProcedure.Parameters.Add(paramAuditSpecificationID);
         storedProcedure.Parameters.Add(paramDivisionID);
         storedProcedure.Parameters.Add(paramUserID);

         storedProcedure.ExecuteDataSet();
      }

      public void AuditEditorFail(int auditSpecificationID, int workID, int userID)
      {
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Editor.Fail]" };

         var paramAuditSpecificationID = new StoredProcedureParameter("@AuditSpecificationID", ParameterType.DBInteger, auditSpecificationID);
         var paramWorkID = new StoredProcedureParameter("@WorkID", ParameterType.DBInteger, workID);
         var paramUserID = new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID);

         storedProcedure.Parameters.Add(paramAuditSpecificationID);
         storedProcedure.Parameters.Add(paramWorkID);
         storedProcedure.Parameters.Add(paramUserID);

         storedProcedure.ExecuteDataSet();
      }

      public void AuditEditorFailDivision(int auditSpecificationID, int divisionID, int userID, DataTable auditBadges)
      {
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Editor.Fail.Division]" };

         var paramAuditSpecificationID = new StoredProcedureParameter("@AuditSpecificationID", ParameterType.DBInteger, auditSpecificationID);
         var paramDivisionID = new StoredProcedureParameter("@DivisionID", ParameterType.DBInteger, divisionID);
         var paramUserID = new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID);
         var paramAuditBadges = new StoredProcedureParameter("@AuditBadges", ParameterType.Structured, auditBadges);

         storedProcedure.Parameters.Add(paramAuditSpecificationID);
         storedProcedure.Parameters.Add(paramDivisionID);
         storedProcedure.Parameters.Add(paramUserID);
         storedProcedure.Parameters.Add(paramAuditBadges);

         storedProcedure.ExecuteDataSet();
      }

      public void AuditEditorClose(int auditSpecificationID, int workID, int userID)
      {
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Editor.Close]" };

         var paramAuditSpecificationID = new StoredProcedureParameter("@AuditSpecificationID", ParameterType.DBInteger, auditSpecificationID);
         var paramWorkID = new StoredProcedureParameter("@WorkID", ParameterType.DBInteger, workID);
         var paramUserID = new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID);

         storedProcedure.Parameters.Add(paramAuditSpecificationID);
         storedProcedure.Parameters.Add(paramWorkID);
         storedProcedure.Parameters.Add(paramUserID);

         storedProcedure.ExecuteDataSet();
      }

      public void AuditEditorCloseDivision(int auditSpecificationID, int divisionID, int userID)
      {
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Editor.Close.Division]" };

         var paramAuditSpecificationID = new StoredProcedureParameter("@AuditSpecificationID", ParameterType.DBInteger, auditSpecificationID);
         var paramDivisionID = new StoredProcedureParameter("@DivisionID", ParameterType.DBInteger, divisionID);
         var paramUserID = new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID);

         storedProcedure.Parameters.Add(paramAuditSpecificationID);
         storedProcedure.Parameters.Add(paramDivisionID);
         storedProcedure.Parameters.Add(paramUserID);

         storedProcedure.ExecuteDataSet();
      }

      //JBienvenu 19205 2013-01-04 new method based on AuditEditorClose
      public void AuditEditorCancel(int auditSpecificationID, int workID, int userID)
      {
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Editor.Cancel]" };

         var paramAuditSpecificationID = new StoredProcedureParameter("@AuditSpecificationID", ParameterType.DBInteger, auditSpecificationID);
         var paramWorkID = new StoredProcedureParameter("@WorkID", ParameterType.DBInteger, workID);
         var paramUserID = new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID);

         storedProcedure.Parameters.Add(paramAuditSpecificationID);
         storedProcedure.Parameters.Add(paramWorkID);
         storedProcedure.Parameters.Add(paramUserID);

         storedProcedure.ExecuteDataSet();
      }

      //JBienvenu 19205 2013-01-04 new method based on AuditEditorCloseDivision
      public void AuditEditorCancelDivision(int auditSpecificationID, int divisionID, int userID)
      {
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Editor.Cancel.Division]" };

         var paramAuditSpecificationID = new StoredProcedureParameter("@AuditSpecificationID", ParameterType.DBInteger, auditSpecificationID);
         var paramDivisionID = new StoredProcedureParameter("@DivisionID", ParameterType.DBInteger, divisionID);
         var paramUserID = new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID);

         storedProcedure.Parameters.Add(paramAuditSpecificationID);
         storedProcedure.Parameters.Add(paramDivisionID);
         storedProcedure.Parameters.Add(paramUserID);

         storedProcedure.ExecuteDataSet();
      }

      public DataTable AuditEditorLetterFailBadges(int auditSpecificationID, int divisionID)
      {
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Editor.Letter.FailBadges]" };

         var paramAuditSpecificationID = new StoredProcedureParameter("@AuditSpecificationID", ParameterType.DBInteger, auditSpecificationID);
         var paramDivisionID = new StoredProcedureParameter("@DivisionID", ParameterType.DBInteger, divisionID);


         storedProcedure.Parameters.Add(paramAuditSpecificationID);
         storedProcedure.Parameters.Add(paramDivisionID);

         return storedProcedure.ExecuteDataSet();
      }

      //JBienvenu 18567 2013-01-24 new method
      public DataTable AuditEditorLetterLoad(int auditSpecificationID, int divisionID)
      {
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Editor.Letter.Load]" };

         storedProcedure.Parameters.Add(new StoredProcedureParameter("@AuditSpecificationID", ParameterType.DBInteger, auditSpecificationID));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@DivisionID", ParameterType.DBInteger, divisionID));

         return storedProcedure.ExecuteDataSet();
      }

      //JBienvenu 18567 2013-01-24 new method
      public void AuditEditorLetterSave(int auditSpecificationID, int divisionID, string letterTypeCode, string letterHTML, int UserID)
      {
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Editor.Letter.Save]" };

         storedProcedure.Parameters.Add(new StoredProcedureParameter("@AuditSpecificationID", ParameterType.DBInteger, auditSpecificationID));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@DivisionID", ParameterType.DBInteger, divisionID));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@LetterTypeCode", ParameterType.DBString, letterTypeCode));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@LetterHTML", ParameterType.DBString, letterHTML));
         storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));

         storedProcedure.ExecuteDataSet();
      }

      #region Audit Manager

      public DataTable AuditManagerLoad(string facilityCode)
      {
         DataTable result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Manager.Load]" };

         var paramFacilityCode = new StoredProcedureParameter("@FacilityCode", ParameterType.DBString, facilityCode);

         storedProcedure.Parameters.Add(paramFacilityCode);

         result = storedProcedure.ExecuteDataSet();
         return result;
      }

      public DataTable AuditManagerLoadSpecifications(int auditGroupID)
      {
         DataTable result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Manager.Load.Specifications]" };

         var paramAuditGroupID = new StoredProcedureParameter("@AuditGroupID", ParameterType.DBInteger, auditGroupID);

         storedProcedure.Parameters.Add(paramAuditGroupID);

         result = storedProcedure.ExecuteDataSet();
         return result;
      }

      public DataTable AuditManagerLoadAudits(int auditSpecificationID)
      {
         DataTable result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Manager.Load.Audits]" };

         var paramAuditSpecificationID = new StoredProcedureParameter("@AuditSpecificationID", ParameterType.DBInteger, auditSpecificationID);

         storedProcedure.Parameters.Add(paramAuditSpecificationID);

         result = storedProcedure.ExecuteDataSet();
         return result;
      }

      public bool AuditManagerGroupExists(string facilityCode, string groupName, int userID)
      {
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Manager.Group.NameCount]" };

         var paramFacilityCode = new StoredProcedureParameter("@FacilityCode", ParameterType.DBString, facilityCode);
         var paramGroupName = new StoredProcedureParameter("@GroupName", ParameterType.DBString, groupName);
         var paramAuditGroupID = new StoredProcedureParameter("@AuditGroupID", ParameterType.DBInteger, DBNull.Value);
         var paramUserID = new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID);

         storedProcedure.Parameters.Add(paramAuditGroupID);
         storedProcedure.Parameters.Add(paramFacilityCode);
         storedProcedure.Parameters.Add(paramGroupName);
         storedProcedure.Parameters.Add(paramUserID);

         var dt = storedProcedure.ExecuteDataSet();
         int count = int.Parse(dt.Rows[0][0].ToString());
         return (count > 0);
      }

      public int AuditManagerGroupSave(int auditGroupID, string facilityCode, string groupName, DateTime auditDate, string _Action, int userID)
      {
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Manager.Group.Save]" };

         var paramAuditGroupID = new StoredProcedureParameter("@AuditGroupID", ParameterType.DBInteger, auditGroupID);
         var paramFacilityCode = new StoredProcedureParameter("@FacilityCode", ParameterType.DBString, facilityCode);
         var paramGroupName = new StoredProcedureParameter("@GroupName", ParameterType.DBString, groupName);
         var paramAuditDate = new StoredProcedureParameter("@AuditDate", ParameterType.DBDateTime, auditDate);
         var param_Action = new StoredProcedureParameter("@_Action", ParameterType.DBString, _Action);
         var paramUserID = new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID);

         storedProcedure.Parameters.Add(paramAuditGroupID);
         storedProcedure.Parameters.Add(paramFacilityCode);
         storedProcedure.Parameters.Add(paramGroupName);
         storedProcedure.Parameters.Add(paramAuditDate);
         storedProcedure.Parameters.Add(param_Action);
         storedProcedure.Parameters.Add(paramUserID);

         DataTable dt = storedProcedure.ExecuteDataSet();

         int newIdentity = int.Parse(dt.Rows[0][0].ToString());

         return newIdentity;
      }

      public DataTable AuditEditLetter(int divisionId, int auditSpecificationId)
      {
         DataTable result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Editor.Letter]" };

         var paramDivisionId = new StoredProcedureParameter("@DivisionID", ParameterType.DBInteger, divisionId);
         var paramAuditSpecificationID = new StoredProcedureParameter("@AuditSpecificationID", ParameterType.DBInteger, auditSpecificationId);

         storedProcedure.Parameters.Add(paramDivisionId);
         storedProcedure.Parameters.Add(paramAuditSpecificationID);

         result = storedProcedure.ExecuteDataSet();
         return result;
      }

      public DataTable AuditEditStaffInfo(int divisionId, int auditSpecificationId)
      {
         DataTable result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Editor.StaffInfo]" };

         var paramDivisionId = new StoredProcedureParameter("@DivisionID", ParameterType.DBInteger, divisionId);
         var paramAuditSpecificationID = new StoredProcedureParameter("@AuditSpecificationID", ParameterType.DBInteger, auditSpecificationId);

         storedProcedure.Parameters.Add(paramDivisionId);
         storedProcedure.Parameters.Add(paramAuditSpecificationID);

         result = storedProcedure.ExecuteDataSet();
         return result;
      }

      public DataSet AuditHelperDeactivateBadgesTest(DataTable deactivateBadges)
      {
         DataSet result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Helper.DeactivateBadgesTest]" };

         var paramDeactivateBadges = new StoredProcedureParameter("@DeactivateBadges", ParameterType.Structured, deactivateBadges);

         storedProcedure.Parameters.Add(paramDeactivateBadges);
         result = storedProcedure.ExecuteMultipleDataSet(); //whats wrong here???????

         return result;
      }

      public int AuditHelperDeactivateBadges(DataTable deactivateBadges, string badgeStatusCode, int userID)
      {
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Helper.DeactivateBadges]" };
         var paramDeactivateBadges = new StoredProcedureParameter("@DeactivateBadges", ParameterType.Structured, deactivateBadges);
         var paramBadgeStatusCode = new StoredProcedureParameter("@BadgeStatusCode", ParameterType.DBString, badgeStatusCode);
         var paramUserID = new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID);

         storedProcedure.Parameters.Add(paramDeactivateBadges);
         storedProcedure.Parameters.Add(paramBadgeStatusCode);
         storedProcedure.Parameters.Add(paramUserID);

         return int.Parse(storedProcedure.ExecuteDataSet().Rows[0][0].ToString());
      }

      #endregion Audit Manager

      #region Helpers

      public int SpecIdFromWorkId(int workId)
      {
         const int FIRST_ROW = 0;
         const int FIRST_COL = 0;

         DataTable result = null;
         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Helper.SpecIdFromWorkId]" };

         var paramWorkId = new StoredProcedureParameter("@WorkID", ParameterType.DBInteger, workId);

         storedProcedure.Parameters.Add(paramWorkId);

         result = storedProcedure.ExecuteDataSet();
         return (int)result.Rows[FIRST_ROW][FIRST_COL];
      }

      #endregion

      public DataTable GetActiveOfficersWithEmptyRow()
      {
         DataTable result = null;

         var storedProcedure = new StoredProcedure { StoredProcedureName = _schema + ".[Audit.Lists.Officers.Active]" };

         result = storedProcedure.ExecuteDataSet();

         DataRow row = result.NewRow();

         row["NAME"] = "--Select One--";
         row["OfficerBadgeId"] = "";

         result.Rows.InsertAt(row, 0);

         return result;
      }

      #endregion Public Methods
   }
}