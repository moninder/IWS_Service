using System;
using System.Data;

using AirportIQ.Data;
using AirportIQ.Domain.Contracts;
using AirportIQ.Data.SqlServer.Repositories;
// JEM 3/2012-----------------------------------------------
// for handling OAS application or other forms
//----------------------------------------------------------
namespace AirportIQ.Domain.Services
{
    public class ReviewServices : IReview
    {
         #region "Private Variables"

            private readonly IReviewRepository reviewRepository;

        #endregion

        #region "Constructors"

            public ReviewServices() : this(new ReviewRepository()) { }

            public ReviewServices(IReviewRepository reviewRepository)
            {
                if (reviewRepository == null) throw new ArgumentNullException("reviewRepository");
                this.reviewRepository = reviewRepository;
            }
            
        #endregion               
        #region "Public Methods"

            DataSet IReview.loadAuditSpecificationList(Int32 userID, Int32? companyID, Int32? divisionID, Int32? auditID)
            {
                return this.reviewRepository.loadAuditSpecificationList(userID, companyID, divisionID, auditID);
            }

            #region Employee
            public DataSet loadReviewEmployeeList(Int32? companyID, Int32? divisionID, String companyCriteria, String companyTypeCriteria, String badgeSearchCriteria, String divisionCriteria, String employeeLNCriteria, string facilityCode, Int32 userID)
            {
                return this.reviewRepository.loadReviewEmployeeList(companyID, divisionID, companyCriteria, companyTypeCriteria, badgeSearchCriteria, divisionCriteria, employeeLNCriteria, facilityCode, userID);
            }
            public DataSet loadReviewEmployeeList(Int32? companyID, Int32? divisionID, String companyCriteria, String companyTypeCriteria, String badgeSearchCriteria, String divisionCriteria, String employeeLNCriteria, String employeeFNCriteria, string facilityCode, Int32 userID)
            {
                return this.reviewRepository.loadReviewEmployeeList(companyID, divisionID, companyCriteria, companyTypeCriteria, badgeSearchCriteria, divisionCriteria, employeeLNCriteria, employeeFNCriteria, facilityCode, userID);
            }
            #endregion

            #region Badges
            public DataSet loadReviewBadgeList(Int32? companyID, Int32? divisionID, String companyCriteria, String companyTypeCriteria, String badgeSearchCriteria, String divisionCriteria, String employeeLNCriteria, Int32 userID)
            {
                return this.reviewRepository.loadReviewBadgeList(companyID, divisionID, companyCriteria, companyTypeCriteria, badgeSearchCriteria, divisionCriteria, employeeLNCriteria, userID);
            }
            public DataSet loadReviewBadgeList(Int32? companyID, Int32? divisionID, String companyCriteria, String companyTypeCriteria, String badgeSearchCriteria, String divisionCriteria, String employeeLNCriteria, String employeeFNCriteria, Int32 userID)
            {
                return this.reviewRepository.loadReviewBadgeList(companyID, divisionID, companyCriteria, companyTypeCriteria, badgeSearchCriteria, divisionCriteria, employeeLNCriteria, employeeFNCriteria, userID);
            }
            #endregion

            public DataSet loadReviewCompanyList(Int32? companyID, Int32? divisionID, String companyCriteria, String companyTypeCriteria, String badgeSearchCriteria, String divisionCriteria, String employeeLNCriteria, String employeeFNCriteria, Int32 userID)
            {
                return this.reviewRepository.loadReviewCompanyList(companyID, divisionID, companyCriteria, companyTypeCriteria, badgeSearchCriteria, divisionCriteria, employeeLNCriteria, employeeFNCriteria, userID);
            }

            public DataSet loadReviewDivisionList(Int32? companyID, Int32? divisionID, String companyCriteria, String companyTypeCriteria, String badgeSearchCriteria, String divisionCriteria, String employeeLNCriteria, String employeeFNCriteria, Int32 userID)
            {
                return this.reviewRepository.loadReviewDivisionList(companyID, divisionID, companyCriteria, companyTypeCriteria, badgeSearchCriteria, divisionCriteria, employeeLNCriteria, employeeFNCriteria, userID);
            }

            public DataSet loadReviewAgreementList(Int32? companyID, Int32? divisionID, String companyCriteria, String companyTypeCriteria, String badgeSearchCriteria, String divisionCriteria, String employeeLNCriteria, String employeeFNCriteria, Int32 userID)
            {
                return this.reviewRepository.loadReviewAgreementList(companyID, divisionID, companyCriteria, companyTypeCriteria, badgeSearchCriteria, divisionCriteria, employeeLNCriteria, employeeFNCriteria, userID);
            }

            public DataSet loadReviewContactList(Int32? companyID, Int32? divisionID, String companyCriteria, String companyTypeCriteria, String badgeSearchCriteria, String divisionCriteria, String employeeLNCriteria, String employeeFNCriteria, Int32 userID)
            {
                return this.reviewRepository.loadReviewContactList(companyID, divisionID, companyCriteria, companyTypeCriteria, badgeSearchCriteria, divisionCriteria, employeeLNCriteria, employeeFNCriteria, userID);
            }

            #region Change Log -----------------
            DataSet IReview.loadChangeLogList(Int32? tableGroupID, Int32? tableID, Int32? primaryKey, DateTime beginDateTime, DateTime endDateTime, Int32 userID)
            {
                return this.reviewRepository.loadChangeLogList(tableGroupID, tableID, primaryKey, beginDateTime, endDateTime, userID);
            }
            DataSet IReview.loadChangeLogCompanyList(Int32? companyID, DateTime beginDateTime, DateTime endDateTime, String rollupValue, Int32 userID)
            {
                return this.reviewRepository.loadChangeLogCompanyList(companyID, beginDateTime, endDateTime, rollupValue, userID);
            }
            DataSet IReview.loadChangeLogDivisionList(Int32? divisionID, DateTime beginDateTime, DateTime endDateTime, String rollupValue, Int32 userID)
            {
                return this.reviewRepository.loadChangeLogDivisionList(divisionID, beginDateTime, endDateTime, rollupValue, userID);
            }
            DataSet IReview.loadChangeLogContactList(Int32? contactID, DateTime beginDateTime, DateTime endDateTime, String rollupValue, Int32 userID)
            {
                return this.reviewRepository.loadChangeLogContactList(contactID, beginDateTime, endDateTime, rollupValue, userID);
            }
            DataSet IReview.loadChangeLogAgreementList(Int32? agreementID, DateTime beginDateTime, DateTime endDateTime, String rollupValue, Int32 userID)
            {
                return this.reviewRepository.loadChangeLogAgreementList(agreementID, beginDateTime, endDateTime, rollupValue, userID);
            }
            #endregion



        #endregion
    }
}
