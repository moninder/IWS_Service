using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace AirportIQ.Data
{
    public interface IReviewRepository
    {
        DataSet loadAuditSpecificationList(Int32 UserId, Int32? CompanyID, Int32? DivisionID, Int32? AuditID);
        DataSet loadReviewEmployeeList(Int32? CompanyID, Int32? DivisionID, String CompanyCriteria, String CompanyTypeCriteria, String BadgeSearchCriteria, String DivisionCriteria, String EmployeeLNCriteria, string facilityCode, Int32 UserId);
        DataSet loadReviewEmployeeList(Int32? CompanyID, Int32? DivisionID, String CompanyCriteria, String CompanyTypeCriteria, String BadgeSearchCriteria, String DivisionCriteria, String EmployeeLNCriteria, String EmployeeFNCriteria, string facilityCode, Int32 UserId);

        DataSet loadReviewBadgeList(Int32? CompanyID, Int32? DivisionID, String CompanyCriteria, String CompanyTypeCriteria, String BadgeSearchCriteria, String DivisionCriteria, String EmployeeLNCriteria, Int32 UserId);
        DataSet loadReviewBadgeList(Int32? CompanyID, Int32? DivisionID, String CompanyCriteria, String CompanyTypeCriteria, String BadgeSearchCriteria, String DivisionCriteria, String EmployeeLNCriteria, String EmployeeFNCriteria, Int32 UserId);

        DataSet loadReviewCompanyList(Int32? CompanyID, Int32? DivisionID, String CompanyCriteria, String CompanyTypeCriteria, String BadgeSearchCriteria, String DivisionCriteria, String EmployeeLNCriteria, String EmployeeFNCriteria, Int32 UserId);
        DataSet loadReviewDivisionList(Int32? CompanyID, Int32? DivisionID, String CompanyCriteria, String CompanyTypeCriteria, String BadgeSearchCriteria, String DivisionCriteria, String EmployeeLNCriteria, String EmployeeFNCriteria, Int32 UserId);
        DataSet loadReviewAgreementList(Int32? CompanyID, Int32? DivisionID, String CompanyCriteria, String CompanyTypeCriteria, String BadgeSearchCriteria, String DivisionCriteria, String EmployeeLNCriteria, String EmployeeFNCriteria, Int32 UserId);
        DataSet loadReviewContactList(Int32? CompanyID, Int32? DivisionID, String CompanyCriteria, String CompanyTypeCriteria, String BadgeSearchCriteria, String DivisionCriteria, String EmployeeLNCriteria, String EmployeeFNCriteria, Int32 UserId);

        #region Change Log -----------
            DataSet loadChangeLogList(Int32? tableGroupID, Int32? tableID, Int32? primaryKey, DateTime beginDateTime, DateTime endDateTime, Int32 userID);
            DataSet loadChangeLogCompanyList(Int32? companyID, DateTime beginDateTime, DateTime endDateTime, String rollupValue, Int32 userID);
            DataSet loadChangeLogDivisionList(Int32? divisionID, DateTime beginDateTime, DateTime endDateTime, String rollupValue, Int32 userID);
            DataSet loadChangeLogContactList(Int32? contactID, DateTime beginDateTime, DateTime endDateTime, String rollupValue, Int32 userID);
            DataSet loadChangeLogAgreementList(Int32? agreementID, DateTime beginDateTime, DateTime endDateTime, String rollupValue, Int32 userID);
        #endregion

    }
}
