using System;
using System.Collections.Generic;
using System.Data;

namespace AirportIQ.Data
{
	public interface IAuditRepository
	{
		DataTable ListCompaniesWithActiveBadges(string facilityCode);
		DataTable ListDivisionsWithActiveBadges(int companyID, string facilityCode);			
		
		DataTable GetAuditSpecificationList();
		DataTable GetAuditCompanyList(int auditID);
		DataTable GetCompanyList();
		DataTable GetDivisionList(int companyID);
		DataTable GetAuditDivisionList(int auditID, int companyID);
		DataTable GetAuditorList(string facilityCode);
		DataSet AuditFormLoad(int auditID, int divisionID);
		bool AuditFormSave(DataSet ds, int userID);
		DataSet AuditProposalLoad(int numBadges, int divPercentageToAudit, DateTime maxLastAuditDate, int divMinBadges, int divMaxBadges, int assignedPersonId, bool doNotExceedNumBadges, string facilityCode, string divisionTypeId, Dictionary<string, bool> agencies);
		int AuditProposalSave(DataSet ds, int userID);
		DataTable GetAuditsByDivision(int divisionID);
		DataTable GetAuditsByDivisionAndStaffID(int divisionID, int staffID);
		DataTable GetAuditsByAuditName(string auditName);
		DataTable GetBadgeStatuses();
        DataTable GetTotalBadgeInspec();
		DataTable GetAuditDivisionInfo(int auditSpecificationID, int divisionID);
		DataSet SaveAuditForCause(int auditGroupID, int divisionID, int staffID_Responsible, int percentage, string auditName, int userID);
		DataSet SaveAuditCargo(int auditGroupID, int divisionID, int staffID_Responsible, int percentage, string auditName, int userID);
		DataTable SaveSelfAuditOld(int auditGroupID, int divisionID, int staffID_Responsible, int percentage, string auditName, int userID);
		DataTable SaveSelfAudit(int auditGroupID, int staffID_Responsible, string auditName, int userID);
		DataTable AuditPoliceInspectionLoad(DateTime startDate, DateTime endDate, string facilityCode);
		DataTable AuditPoliceInspectionBadgesLoad(int auditInspectionID);
		DataTable AuditNameChecker(string auditNameBase);
        DataTable AuditPoliceInspectionsSave(int auditInspectionID, string facilityCode, DateTime auditDate, int watchNumber, string location, int officerBadgeID, int? officerBadgeID2, int? officerBadgeID3, string _Action, int userID);
		DataTable PoliceInspectionBadgesSave
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
			);

		DataTable GetAuditInspectionTypesList();
		DataTable GetOfficers_Active();
        DataSet GetSpecificationInfo(int auditSpecificationID);

#region Audit Editor
		DataTable AuditEditorAudits(int auditSpecificationID);
		DataTable AuditEditorWorkItem(int userID, int? workID, int? auditSpecificationID);
        void AuditEditorComplete(int auditSpecificationID, int workID, int userID);
		void AuditEditorCompleteDivision(int auditSpecificationID, int divisionID, int userID);
		void AuditEditorFail(int auditSpecificationID, int workID, int userID);
		void AuditEditorFailDivision(int auditSpecificationID, int divisionID, int userID, DataTable auditBadges);
		void AuditEditorClose(int auditSpecificationID, int workID, int userID);
		void AuditEditorCloseDivision(int auditSpecificationID, int divisionID, int userID);
        void AuditEditorCancel(int auditSpecificationID, int workID, int userID); //JBienvenu 19205 2013-01-04 +1 new
        void AuditEditorCancelDivision(int auditSpecificationID, int divisionID, int userID);
        DataTable AuditEditStaffInfo(int divisionId, int auditSpecificationId);
		DataTable AuditEditLetter(int divisionId, int auditSpecificationId);
		DataTable AuditEditorLetterFailBadges(int auditSpecificationID, int divisionID);
        DataTable AuditEditorLetterLoad(int auditSpecificationID, int divisionID); //JBienvenu 18567 2013-01-24 new +1
        void AuditEditorLetterSave(int auditSpecificationID, int divisionID, string letterTypeCode, string letterHTML, int UserID);
#endregion

		#region Audit Manager

		DataTable AuditManagerLoad(string facilityCode);
		DataTable AuditManagerLoadSpecifications(int auditGroupID);
		DataTable AuditManagerLoadAudits(int auditSpecificationID);

	    bool AuditManagerGroupExists(string facilityCode, string groupName, int userID);
        int AuditManagerGroupSave(int auditGroupID, string facilityCode, string groupName, DateTime auditDate, string _Action, int userID);
		
        DataSet AuditHelperDeactivateBadgesTest(DataTable deactivateBadges);
		int AuditHelperDeactivateBadges(DataTable deactivateBadges, string badgeStatusCode, int userID);

		#endregion

		#region Helpers


		int SpecIdFromWorkId(int workId);

		#endregion
	}
}