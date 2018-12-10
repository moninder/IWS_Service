using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AirportIQ.Data;
using System.Configuration;
using AirportIQ.Data.SqlServer.Initializers;

namespace AirportIQ.Data.SqlServer.Repositories
{
    public class ReviewRepository : IReviewRepository
    {

        #region Private Variables

            private string schema = ConfigurationManager.AppSettings["SecuritySchema"].ToString();

        #endregion

        // this method may be obsolete as of 6/29/2012 ------------------------------------------------
        DataSet IReviewRepository.loadAuditSpecificationList(Int32 userID, Int32? companyID, Int32? divisionID, Int32? auditID)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Data.ReviewDetail.Load]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBString, userID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyID", ParameterType.DBString, companyID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionID", ParameterType.DBString, divisionID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@auditID", ParameterType.DBString, auditID));
            ret = storedProcedure.ExecuteMultipleDataSet();
            return ret;
        }

        public DataSet loadReviewEmployeeList(Int32? companyID, Int32? divisionID, string companyCriteria, string companyTypeCriteria, string badgeSearchCriteria, string divisionCriteria, string employeeLNCriteria, string facilityCode, int userID)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Data.ReviewEmployeeList.Load]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyID", ParameterType.DBString, companyID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionID", ParameterType.DBString, divisionID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyCriteria", ParameterType.DBString, companyCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyTypeCriteria", ParameterType.DBString, companyTypeCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@badgeSearchCriteria", ParameterType.DBString, badgeSearchCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionCriteria", ParameterType.DBString, divisionCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@employeeLNCriteria", ParameterType.DBString, employeeLNCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@facilityCode", ParameterType.DBString, facilityCode));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBString, userID));
            ret = storedProcedure.ExecuteMultipleDataSet();
            return ret;
        }
        public DataSet loadReviewEmployeeList(Int32? companyID, Int32? divisionID, string companyCriteria, string companyTypeCriteria, string badgeSearchCriteria, string divisionCriteria, string employeeLNCriteria, string employeeFNCriteria, string facilityCode, int userID)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Data.ReviewEmployeeList.Load]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyID", ParameterType.DBString, companyID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionID", ParameterType.DBString, divisionID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyCriteria", ParameterType.DBString, companyCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyTypeCriteria", ParameterType.DBString, companyTypeCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@badgeSearchCriteria", ParameterType.DBString, badgeSearchCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionCriteria", ParameterType.DBString, divisionCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@employeeLNCriteria", ParameterType.DBString, employeeLNCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@employeeFNCriteria", ParameterType.DBString, employeeFNCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@facilityCode", ParameterType.DBString, facilityCode));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBString, userID));
            ret = storedProcedure.ExecuteMultipleDataSet();
            return ret;
        }
        public DataSet loadReviewBadgeList(Int32? companyID, Int32? divisionID, string companyCriteria, string companyTypeCriteria, string badgeSearchCriteria, string divisionCriteria, string employeeLNCriteria, int userID)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Data.ReviewBadgeList.Load]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyID", ParameterType.DBString, companyID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionID", ParameterType.DBString, divisionID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyCriteria", ParameterType.DBString, companyCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyTypeCriteria", ParameterType.DBString, companyTypeCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@badgeSearchCriteria", ParameterType.DBString, badgeSearchCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionCriteria", ParameterType.DBString, divisionCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@employeeLNCriteria", ParameterType.DBString, employeeLNCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBString, userID));
            ret = storedProcedure.ExecuteMultipleDataSet();
            return ret;
        }
        public DataSet loadReviewBadgeList(Int32? companyID, Int32? divisionID, string companyCriteria, string companyTypeCriteria, string badgeSearchCriteria, string divisionCriteria, string employeeLNCriteria, string employeeFNCriteria, int userID)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Data.ReviewBadgeList.Load]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyID", ParameterType.DBString, companyID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionID", ParameterType.DBString, divisionID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyCriteria", ParameterType.DBString, companyCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyTypeCriteria", ParameterType.DBString, companyTypeCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@badgeSearchCriteria", ParameterType.DBString, badgeSearchCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionCriteria", ParameterType.DBString, divisionCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@employeeLNCriteria", ParameterType.DBString, employeeLNCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@employeeFNCriteria", ParameterType.DBString, employeeFNCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBString, userID));
            ret = storedProcedure.ExecuteMultipleDataSet();
            return ret;
        }
        public DataSet loadReviewCompanyList(Int32? companyID, Int32? divisionID, string companyCriteria, string companyTypeCriteria, string badgeSearchCriteria, string divisionCriteria, string employeeLNCriteria, string employeeFNCriteria, int userID)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Data.ReviewCompanyList.Load]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyID", ParameterType.DBString, companyID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionID", ParameterType.DBString, divisionID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyCriteria", ParameterType.DBString, companyCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyTypeCriteria", ParameterType.DBString, companyTypeCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@badgeSearchCriteria", ParameterType.DBString, badgeSearchCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionCriteria", ParameterType.DBString, divisionCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@employeeLNCriteria", ParameterType.DBString, employeeLNCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@employeeFNCriteria", ParameterType.DBString, employeeFNCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBString, userID));
            ret = storedProcedure.ExecuteMultipleDataSet();
            return ret;
        }
        public DataSet loadReviewDivisionList(Int32? companyID, Int32? divisionID, string companyCriteria, string companyTypeCriteria, string badgeSearchCriteria, string divisionCriteria, string employeeLNCriteria, string employeeFNCriteria, int userID)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Data.ReviewDivisionList.Load]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyID", ParameterType.DBString, companyID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionID", ParameterType.DBString, divisionID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyCriteria", ParameterType.DBString, companyCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyTypeCriteria", ParameterType.DBString, companyTypeCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@badgeSearchCriteria", ParameterType.DBString, badgeSearchCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionCriteria", ParameterType.DBString, divisionCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@employeeLNCriteria", ParameterType.DBString, employeeLNCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@employeeFNCriteria", ParameterType.DBString, employeeFNCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBString, userID));
            ret = storedProcedure.ExecuteMultipleDataSet();
            return ret;
        }
        public DataSet loadReviewAgreementList(Int32? companyID, Int32? divisionID, string companyCriteria, string companyTypeCriteria, string badgeSearchCriteria, string divisionCriteria, string employeeLNCriteria, string employeeFNCriteria, int userID)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Data.ReviewAgreementList.Load]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyID", ParameterType.DBString, companyID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionID", ParameterType.DBString, divisionID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyCriteria", ParameterType.DBString, companyCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyTypeCriteria", ParameterType.DBString, companyTypeCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@badgeSearchCriteria", ParameterType.DBString, badgeSearchCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionCriteria", ParameterType.DBString, divisionCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@employeeLNCriteria", ParameterType.DBString, employeeLNCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@employeeFNCriteria", ParameterType.DBString, employeeFNCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBString, userID));
            ret = storedProcedure.ExecuteMultipleDataSet();
            return ret;
        }
        public DataSet loadReviewContactList(Int32? companyID, Int32? divisionID, string companyCriteria, string companyTypeCriteria, string badgeSearchCriteria, string divisionCriteria, string employeeLNCriteria, string employeeFNCriteria, int userID)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Data.ReviewContactList.Load]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyID", ParameterType.DBString, companyID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionID", ParameterType.DBString, divisionID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyCriteria", ParameterType.DBString, companyCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyTypeCriteria", ParameterType.DBString, companyTypeCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@badgeSearchCriteria", ParameterType.DBString, badgeSearchCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionCriteria", ParameterType.DBString, divisionCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@employeeLNCriteria", ParameterType.DBString, employeeLNCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@employeeFNCriteria", ParameterType.DBString, employeeFNCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBString, userID));
            ret = storedProcedure.ExecuteMultipleDataSet();
            return ret;
        }

        #region Changelog --------------------------
            // begin obsolete ---------------------
            DataSet IReviewRepository.loadChangeLogList(Int32? tableGroupID, Int32? tableID, Int32? primaryKey, DateTime beginDateTime, DateTime endDateTime, Int32 userID)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[DataChangeLogging.GetChangesWrapper]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@tableGroupID", ParameterType.DBInteger, tableGroupID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@tableID", ParameterType.DBInteger, tableID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@rowID", ParameterType.DBInteger, primaryKey));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@beginDateTime", ParameterType.DBDateTime, beginDateTime));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@endDateTime", ParameterType.DBDateTime, endDateTime));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBString, userID));
            ret = storedProcedure.ExecuteMultipleDataSet();
            return ret;
        }
            // end obsolete -----------------------

            DataSet IReviewRepository.loadChangeLogCompanyList(Int32? companyID, DateTime beginDateTime, DateTime endDateTime, String rollupValue, Int32 userID)
            {
                DataSet ret = null;
                var storedProcedure = new StoredProcedure();
                storedProcedure.StoredProcedureName = schema + ".[DataChangeLogging.ShowEntityChanges.Company]";
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@companyID", ParameterType.DBInteger, companyID));
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@beginDateTime", ParameterType.DBDateTime, beginDateTime));
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@endDateTime", ParameterType.DBDateTime, endDateTime));
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@rollupValue", ParameterType.DBDateTime, rollupValue));
                //storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBString, userID));
                ret = storedProcedure.ExecuteMultipleDataSet();
                return ret;
            }
            DataSet IReviewRepository.loadChangeLogDivisionList(Int32? divisionID, DateTime beginDateTime, DateTime endDateTime, String rollupValue, Int32 userID)
            {
                DataSet ret = null;
                var storedProcedure = new StoredProcedure();
                storedProcedure.StoredProcedureName = schema + ".[DataChangeLogging.ShowEntityChanges.Division]";
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@divisionID", ParameterType.DBInteger, divisionID));
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@beginDateTime", ParameterType.DBDateTime, beginDateTime));
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@endDateTime", ParameterType.DBDateTime, endDateTime));
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@rollupValue", ParameterType.DBDateTime, rollupValue));
                //storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBString, userID));
                ret = storedProcedure.ExecuteMultipleDataSet();
                return ret;
            }
            DataSet IReviewRepository.loadChangeLogContactList(Int32? contactID, DateTime beginDateTime, DateTime endDateTime, String rollupValue, Int32 userID)
            {
                DataSet ret = null;
                var storedProcedure = new StoredProcedure();
                storedProcedure.StoredProcedureName = schema + ".[DataChangeLogging.ShowEntityChanges.Contact]";
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@contactID", ParameterType.DBInteger, contactID));
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@beginDateTime", ParameterType.DBDateTime, beginDateTime));
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@endDateTime", ParameterType.DBDateTime, endDateTime));
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@rollupValue", ParameterType.DBDateTime, rollupValue));
                //storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBString, userID));
                ret = storedProcedure.ExecuteMultipleDataSet();
                return ret;
            }
            DataSet IReviewRepository.loadChangeLogAgreementList(Int32? agreementID, DateTime beginDateTime, DateTime endDateTime, String rollupValue, Int32 userID)
            {
                DataSet ret = null;
                var storedProcedure = new StoredProcedure();
                storedProcedure.StoredProcedureName = schema + ".[DataChangeLogging.ShowEntityChanges.Agreement]";
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@agreementID", ParameterType.DBInteger, agreementID));
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@beginDateTime", ParameterType.DBDateTime, beginDateTime));
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@endDateTime", ParameterType.DBDateTime, endDateTime));
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@rollupValue", ParameterType.DBDateTime, rollupValue));
                //storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBString, userID));
                ret = storedProcedure.ExecuteMultipleDataSet();
                return ret;
            }
        #endregion        

    }
}
