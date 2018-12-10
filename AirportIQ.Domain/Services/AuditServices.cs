using System;
using System.Data;
using System.Text.RegularExpressions;
using AirportIQ.Data;
using AirportIQ.Data.SqlServer.Repositories;
using AirportIQ.Domain.Contracts;
using System.Collections.Generic;

namespace AirportIQ.Domain.Services
{
	public class AuditServices : IAudit
	{
		#region "Private Variables"

		private readonly IAuditRepository _AuditRepository;

		#endregion "Private Variables"

		#region "Constructors"

		public AuditServices() : this(new AuditRepository()) { }

		public AuditServices(IAuditRepository auditRepository)
		{
			if (auditRepository == null) throw new ArgumentNullException("AuditRepository");
			_AuditRepository = auditRepository;
		}

		#endregion "Constructors"

		#region Public Methods

		public DataTable ListCompaniesWithActiveBadges(string facilityCode)
		{
			return _AuditRepository.ListCompaniesWithActiveBadges(facilityCode);
		}

		public DataTable ListDivisionsWithActiveBadges(int companyID, string facilityCode)
		{
			return _AuditRepository.ListDivisionsWithActiveBadges(companyID, facilityCode);
		}

		public DataTable GetAuditSpecificationList()
		{
			return _AuditRepository.GetAuditSpecificationList();
		}

		public DataTable GetAuditCompanyList(int auditID)
		{
			return _AuditRepository.GetAuditCompanyList(auditID);
		}

		public DataTable GetCompanyList()
		{
			return _AuditRepository.GetCompanyList();
		}

		public DataTable GetDivisionList(int companyID)
		{
			return _AuditRepository.GetDivisionList(companyID);
		}

		public DataTable GetAuditDivisionList(int auditID, int companyID)
		{
			return _AuditRepository.GetAuditDivisionList(auditID, companyID);
		}

		public DataTable GetAuditorList(string facilityCode)
		{
			return _AuditRepository.GetAuditorList(facilityCode);
		}

		public DataSet AuditFormLoad(int auditID, int divisionID)
		{
			return _AuditRepository.AuditFormLoad(auditID, divisionID);
		}

		public bool AuditFormSave(DataSet ds, int userID)
		{
			return _AuditRepository.AuditFormSave(ds, userID);
		}

		public DataSet AuditProposalLoad(int numBadges, int divPercentageToAudit, DateTime maxLastAuditDate, int divMinBadges, int divMaxBadges, int assignedPersonId, bool doNotExceedNumBadges, string facilityCode, string divisionTypeId, Dictionary<string, bool> agencies)
		{
			return _AuditRepository.AuditProposalLoad(numBadges, divPercentageToAudit, maxLastAuditDate, divMinBadges, divMaxBadges, assignedPersonId, doNotExceedNumBadges, facilityCode, divisionTypeId, agencies);
		}

		public int AuditProposalSave(DataSet ds, int userID)
		{
			return _AuditRepository.AuditProposalSave(ds, userID);
		}

		public DataTable GetAuditsByDivision(int divisionID)
		{
			return _AuditRepository.GetAuditsByDivision(divisionID);
		}

		public DataTable GetAuditsByDivisionAndStaffID(int divisionID, int staffID)
		{
			return _AuditRepository.GetAuditsByDivisionAndStaffID(divisionID, staffID);
		}

		public DataTable GetAuditsByAuditName(string auditName)
		{
			return _AuditRepository.GetAuditsByAuditName(auditName);
		}

		public DataTable GetBadgeStatuses()
		{
			return _AuditRepository.GetBadgeStatuses();
		}

        public DataTable GetTotalBadgeInspec()
        {
            return _AuditRepository.GetTotalBadgeInspec();
        }

		public DataTable GetAuditDivisionInfo(int auditSpecificationID, int divisionID)
		{
			return _AuditRepository.GetAuditDivisionInfo(auditSpecificationID, divisionID);
		}

		public DataSet SaveAuditForCause(int auditGroupID, int divisionID, int staffID_Responsible, int percentage, string auditName, int userID)
		{
			return _AuditRepository.SaveAuditForCause(auditGroupID, divisionID, staffID_Responsible, percentage, auditName, userID);
		}

		public DataSet SaveAuditCargo(int auditGroupID, int divisionID, int staffID_Responsible, int percentage, string auditName, int userID)
		{
			return _AuditRepository.SaveAuditCargo(auditGroupID, divisionID, staffID_Responsible, percentage, auditName, userID);
		}

		public DataTable SaveSelfAuditOld(int auditGroupID, int divisionID, int staffID_Responsible, int percentage, string auditName, int userID)
		{
			return _AuditRepository.SaveSelfAuditOld(auditGroupID, divisionID, staffID_Responsible, percentage, auditName, userID);
		}

		public DataTable SaveSelfAudit(int auditGroupID, int staffID_Responsible, string auditName, int userID)
		{
			return _AuditRepository.SaveSelfAudit(auditGroupID, staffID_Responsible, auditName, userID);
		}

		public DataTable AuditPoliceInspectionLoad(DateTime startDate, DateTime endDate, string facilityCode)
		{
			return _AuditRepository.AuditPoliceInspectionLoad(startDate, endDate, facilityCode);
		}

		public DataTable AuditPoliceInspectionBadgesLoad(int auditInspectionID)
		{
			return _AuditRepository.AuditPoliceInspectionBadgesLoad(auditInspectionID);
		}

		public DataTable AuditNameChecker(string auditNameBase)
		{
			return _AuditRepository.AuditNameChecker(auditNameBase);
		}

		public DataTable AuditPoliceInspectionsSave(int auditInspectionID, string facilityCode, DateTime auditDate, int watchNumber, string location, int officerBadgeID, int? officerBadgeID2, int? officerBadgeID3, string _Action, int userID)
		{
            return _AuditRepository.AuditPoliceInspectionsSave(auditInspectionID, facilityCode, auditDate, watchNumber, location, officerBadgeID, officerBadgeID2, officerBadgeID3, _Action, userID);
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
			return _AuditRepository.PoliceInspectionBadgesSave
			(
				auditInspectionBadgesID,
				auditInspectionTypeCode,
				auditInspectionID,
				badgeNumber,
				badgeLastName,
				whenBadgeInspected,
				badgeID,
				_Action,
				userID
			);
		}

		public DataTable GetAuditInspectionTypesList()
		{
			return _AuditRepository.GetAuditInspectionTypesList();
		}

		public DataTable GetOfficers_Active()
		{
			return _AuditRepository.GetOfficers_Active();
		}

        public DataSet GetSpecificationInfo(int auditSpecificationID)
		{
			return _AuditRepository.GetSpecificationInfo(auditSpecificationID);
		}

		public DataTable AuditEditorAudits(int auditSpecificationID)
		{
			return _AuditRepository.AuditEditorAudits(auditSpecificationID);
		}

		public DataTable AuditEditorWorkItem(int userID, int? workID, int? auditSpecificationID)
		{
			return _AuditRepository.AuditEditorWorkItem(userID, workID, auditSpecificationID);
		}

		public void AuditEditorComplete(int auditSpecificationID, int workID, int userID)
		{
			_AuditRepository.AuditEditorComplete(auditSpecificationID, workID, userID);
		}

		public void AuditEditorCompleteDivision(int auditSpecificationID, int divisionID, int userID)
		{
			_AuditRepository.AuditEditorCompleteDivision(auditSpecificationID, divisionID, userID);
		}

		public void AuditEditorFail(int auditSpecificationID, int workID, int userID)
		{
			_AuditRepository.AuditEditorFail(auditSpecificationID, workID, userID);
		}

		public void AuditEditorFailDivision(int auditSpecificationID, int divisionID, int userID, DataTable auditBadges)
		{
			_AuditRepository.AuditEditorFailDivision(auditSpecificationID, divisionID, userID, auditBadges);
		}

		public void AuditEditorClose(int auditSpecificationID, int workID, int userID)
		{
			_AuditRepository.AuditEditorClose(auditSpecificationID, workID, userID);
		}

		public void AuditEditorCloseDivision(int auditSpecificationID, int divisionID, int userID)
		{
			_AuditRepository.AuditEditorCloseDivision(auditSpecificationID, divisionID, userID);
		}

        public void AuditEditorCancel(int auditSpecificationID, int workID, int userID)
        {
            _AuditRepository.AuditEditorCancel(auditSpecificationID, workID, userID);
        }

        public void AuditEditorCancelDivision(int auditSpecificationID, int divisionID, int userID)
        {
            _AuditRepository.AuditEditorCancelDivision(auditSpecificationID, divisionID, userID);
        }

		public DataTable AuditEditStaffInfo(int divisionId, int auditSpecificationId)
		{
			return _AuditRepository.AuditEditStaffInfo(divisionId, auditSpecificationId);
		}

		public DataTable AuditEditLetter(int divisionId, int auditSpecificationId)
		{
			return _AuditRepository.AuditEditLetter(divisionId, auditSpecificationId);
		}

		public DataTable AuditEditorLetterFailBadges(int auditSpecificationID, int divisionID)
		{
			return _AuditRepository.AuditEditorLetterFailBadges(auditSpecificationID, divisionID);
		}

        public DataTable AuditEditorLetterLoad(int auditSpecificationID, int divisionID)
        {
            return _AuditRepository.AuditEditorLetterLoad(auditSpecificationID, divisionID);
        }

        public void AuditEditorLetterSave(int auditSpecificationID, int divisionID, string letterTypeCode, string letterHTML, int UserID)
        {
            _AuditRepository.AuditEditorLetterSave(auditSpecificationID, divisionID, letterTypeCode, letterHTML, UserID);
        }

		#region Audit Manager

		public DataTable AuditManagerLoad(string facilityCode)
		{
			return _AuditRepository.AuditManagerLoad(facilityCode);
		}

		public DataTable AuditManagerLoadSpecifications(int auditGroupID)
		{
			return _AuditRepository.AuditManagerLoadSpecifications(auditGroupID);
		}

		public DataTable AuditManagerLoadAudits(int auditSpecificationID)
		{
			return _AuditRepository.AuditManagerLoadAudits(auditSpecificationID);
		}

	    public bool AuditManagerGroupExists(string facilityCode, string groupName, int userID)
	    {
	        return _AuditRepository.AuditManagerGroupExists(facilityCode, groupName, userID);
	    }

        public int AuditManagerGroupSave(int auditGroupID, string facilityCode, string groupName, DateTime auditDate, string _Action, int userID)
		{
			return _AuditRepository.AuditManagerGroupSave(auditGroupID, facilityCode, groupName, auditDate, _Action, userID);
		}

		public DataSet AuditHelperDeactivateBadgesTest(DataTable deactivateBadges)
		{
			return _AuditRepository.AuditHelperDeactivateBadgesTest(deactivateBadges);
		}
		
        public int AuditHelperDeactivateBadges(DataTable deactivateBadges, string badgeStatusCode, int userID)
		{
			return _AuditRepository.AuditHelperDeactivateBadges(deactivateBadges, badgeStatusCode, userID);
		}

		#endregion Audit Manager

		#region Helpers


		public int SpecIdFromWorkId(int workId)
		{
			return _AuditRepository.SpecIdFromWorkId(workId);
		}

		#endregion

		#endregion Public Methods
	}
}