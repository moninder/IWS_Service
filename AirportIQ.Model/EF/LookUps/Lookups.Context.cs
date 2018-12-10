﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;

namespace AirportIQ.Model.EF.LookUps
{
    public partial class LookupEntities : DbContext
    {
        public LookupEntities()
            : base("name=LookupEntities")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Miscellaneous_Countries> Miscellaneous_Countries { get; set; }
        public DbSet<Miscellaneous_CountrySubdivisions> Miscellaneous_CountrySubdivisions { get; set; }
        public DbSet<Lookups_Badges_Info> Lookups_Badges_Info { get; set; }
        public DbSet<Lookups_Colors> Lookups_Colors { get; set; }
        public DbSet<Lookups_EyeColors> Lookups_EyeColors { get; set; }
        public DbSet<Lookups_HairColors> Lookups_HairColors { get; set; }
        public DbSet<Lookups_NamePrefixes> Lookups_NamePrefixes { get; set; }
        public DbSet<Lookups_NameSuffixes> Lookups_NameSuffixes { get; set; }
        public DbSet<Lookups_Races> Lookups_Races { get; set; }
        public DbSet<Lookups_Sexes> Lookups_Sexes { get; set; }
        public DbSet<Lookups_Division_Agreements> Lookups_Division_Agreements { get; set; }
        public DbSet<Lookups_Person_FelonyQuestions> Lookups_Person_FelonyQuestions { get; set; }
        public DbSet<Lookups_Person_FelonyAnswers> Lookups_Person_FelonyAnswers { get; set; }
        public DbSet<Lookups_Icon_Icons> Lookups_Icon_Icons { get; set; }
        public DbSet<Lookups_Icon_PersonDivisionIconXref> Lookups_Icon_PersonDivisionIconXref { get; set; }
        public DbSet<Person_PersonDivisionXref> Person_PersonDivisionXref { get; set; }
        public DbSet<Lookups_Division_AgreementBadgeColors> Lookups_Division_AgreementBadgeColors { get; set; }
        public DbSet<Lookups_Division_AgreementBadgeIcons> Lookups_Division_AgreementBadgeIcons { get; set; }
        public DbSet<Lookups_Facility_BadgeColors> Lookups_Facility_BadgeColors { get; set; }
        public DbSet<Lookups_Person_BadgeIssuanceReasons> Lookups_Person_BadgeIssuanceReasons { get; set; }
        public DbSet<Lookups_Person_PersonBiographics> Lookups_Person_PersonBiographics { get; set; }
        public DbSet<Lookups_Person_Aliases> Lookups_Person_Aliases { get; set; }
        public DbSet<Lookups_Common_Entities> Lookups_Common_Entities { get; set; }
        public DbSet<Lookups_Access_Categories> Lookups_Access_Categories { get; set; }
        public DbSet<Lookups_Division_AgreementAccessDefaults> Lookups_Division_AgreementAccessDefaults { get; set; }
        public DbSet<Lookups_Person_PersonDivisionJobRoleXref> Lookups_Person_PersonDivisionJobRoleXref { get; set; }
        public DbSet<Lookups_Person_BadgeIconPeriods> Lookups_Person_BadgeIconPeriods { get; set; }
        public DbSet<Lookups_Person_Badges> Lookups_Person_Badges { get; set; }
        public DbSet<Lookups_Person_Notes> Lookups_Person_Notes { get; set; }
        public DbSet<Lookups_Person_NoteTypes> Lookups_Person_NoteTypes { get; set; }
        public DbSet<Lookups_Facility_Staff> Lookups_Facility_Staff { get; set; }
        public DbSet<Lookups_Security_Users> Lookups_Security_Users { get; set; }
        public DbSet<Lookups_Person_FullName> Lookups_Person_FullName { get; set; }
        public DbSet<Lookups_Common_EntityTypes> Lookups_Common_EntityTypes { get; set; }
        public DbSet<Lookups_Common_Requirement_ContentTypes_Extensions> Lookups_Common_Requirement_ContentTypes_Extensions { get; set; }
        public DbSet<Lookups_Division_Divisions> Lookups_Division_Divisions { get; set; }
        public DbSet<Lookups_TaskParam> Lookups_TaskParam { get; set; }
        public DbSet<Lookups_Task> Lookups_Task { get; set; }
        public DbSet<Lookups_TaskStatus> TaskStatuses { get; set; }
        public DbSet<Lookups_TaskType> Lookups_TaskType { get; set; }
        public DbSet<IWS_Person_Person> IWS_Person_Person { get; set; }
        public DbSet<IWS_Person_PersonBadgeXref> IWS_Person_PersonBadgeXref { get; set; }
        public DbSet<IWS_Badge_Badge> IWS_Badge_Badge { get; set; }
        public DbSet<IWS_Person_Demographics> IWS_Person_Demographics { get; set; }
        public DbSet<Lookups_Person_Persons> Lookups_Person_Persons { get; set; }
        public DbSet<Lookups_Person_AuthorizedSigners> Lookups_Person_AuthorizedSigners { get; set; }
        public DbSet<Lookups_Company_Companies> Lookups_Company_Companies { get; set; }
        public DbSet<Lookups_BackgroundCheck_PersonDivisionChecks> Lookups_BackgroundCheck_PersonDivisionChecks { get; set; }
        public DbSet<Lookups_Division_AgreementJobRoles> Lookups_Division_AgreementJobRoles { get; set; }
        public DbSet<Lookups_Division_AgreementLocations> Lookups_Division_AgreementLocations { get; set; }
        public DbSet<Lookups_Division_Contacts> Lookups_Division_Contacts { get; set; }
        public DbSet<Lookups_Division_ContactTypes> Lookups_Division_ContactTypes { get; set; }
        public DbSet<Lookups_Access_PersonDivisionCategoryXref> Lookups_Access_PersonDivisionCategoryXref { get; set; }
        public DbSet<Lookups_Common_Requirement_DocumentsRequired> Lookups_Common_Requirement_DocumentsRequired { get; set; }
        public DbSet<Lookups_Access_Areas> Lookups_Access_Areas { get; set; }
        public DbSet<Lookups_Access_CategoryAreaMapping> Lookups_Access_CategoryAreaMapping { get; set; }
        public DbSet<Lookups_Access_Doors> Lookups_Access_Doors { get; set; }
        public DbSet<Person_STAEmployeeStatuses> Person_STAEmployeeStatuses { get; set; }
        public DbSet<Lookups_Common_Requirement_LegalStatusTypes> Lookups_Common_Requirement_LegalStatusTypes { get; set; }
        public DbSet<Lookups_Facility_Departments> Lookups_Facility_Departments { get; set; }
        public DbSet<Lookups_Facility_StaffDepartmentXref> Lookups_Facility_StaffDepartmentXref { get; set; }
        public DbSet<LEO_OfficerRanksAndTitles> LEO_OfficerRanksAndTitles { get; set; }
        public DbSet<LEO_OfficerTypes> LEO_OfficerTypes { get; set; }
        public DbSet<LEO_PoliceDepartments> LEO_PoliceDepartments { get; set; }
        public DbSet<LEO_Officers> LEO_Officers { get; set; }
        public DbSet<Lookups_Common_Requirement_Requirements> Lookups_Common_Requirement_Requirements { get; set; }
        public DbSet<Lookups_Documents_DocumentTypeExt> Lookups_Documents_DocumentTypeExt { get; set; }
        public DbSet<Lookups_Common_Requirement_Documents> Lookups_Common_Requirement_Documents { get; set; }
        public DbSet<Lookups_Common_Requirement_DocumentTypes> Lookups_Common_Requirement_DocumentTypes { get; set; }
        public DbSet<Lookups_Person_BadgeStatusPeriods> Lookups_Person_BadgeStatusPeriods { get; set; }
        public DbSet<Lookups_Person_BadgeAccessPeriods> Lookups_Person_BadgeAccessPeriods { get; set; }
        public DbSet<Lookups_Audit_AuditSpecifications> Lookups_Audit_AuditSpecifications { get; set; }
        public DbSet<Lookups_Person_BadgeStatuses> Lookups_Person_BadgeStatuses { get; set; }
        public DbSet<Lookups_Person_PersonTypeStatuses> Lookups_Person_PersonTypeStatuses { get; set; }
        public DbSet<Lookups_Person_Trainers> Lookups_Person_Trainers { get; set; }
        public DbSet<Lookups_Person_TrainingSites> Lookups_Person_TrainingSites { get; set; }
        public DbSet<Lookups_Division_DivisionTypes> Lookups_Division_DivisionTypes { get; set; }
        public DbSet<Lookups_Person_JobRoles> Lookups_Person_JobRoles { get; set; }
        public DbSet<Lookups_Division_DivisionNotes> Lookups_Division_DivisionNotes { get; set; }
        public DbSet<Audit_AuditDocumentTypes> Audit_AuditDocumentTypes { get; set; }
        public DbSet<Audit_AuditDocuments> Audit_AuditDocuments { get; set; }
        public DbSet<Audit_AuditGroups> Audit_AuditGroups { get; set; }
        public DbSet<Lookups_Person_BadgeReprintReasons> Lookups_Person_BadgeReprintReasons { get; set; }
        public DbSet<Security_Groups> Security_Groups { get; set; }
    
        public virtual ObjectResult<Audit_AuditDivisionInfo_Result> Lookups_Audit_AuditDivisionInfo(Nullable<int> auditSpecificationID, Nullable<int> divisionID)
        {
            ((IObjectContextAdapter)this).ObjectContext.MetadataWorkspace.LoadFromAssembly(typeof(Audit_AuditDivisionInfo_Result).Assembly);
    
            var auditSpecificationIDParameter = auditSpecificationID.HasValue ?
                new ObjectParameter("AuditSpecificationID", auditSpecificationID) :
                new ObjectParameter("AuditSpecificationID", typeof(int));
    
            var divisionIDParameter = divisionID.HasValue ?
                new ObjectParameter("DivisionID", divisionID) :
                new ObjectParameter("DivisionID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Audit_AuditDivisionInfo_Result>("Lookups_Audit_AuditDivisionInfo", auditSpecificationIDParameter, divisionIDParameter);
        }
    
        public virtual ObjectResult<string> Lookups_Helpers_GetContentType(string fileExtention)
        {
            var fileExtentionParameter = fileExtention != null ?
                new ObjectParameter("FileExtention", fileExtention) :
                new ObjectParameter("FileExtention", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("Lookups_Helpers_GetContentType", fileExtentionParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> Lookups_Badging_GetPersonDivisionXrefEnityID(Nullable<int> personDivisionXrefID)
        {
            var personDivisionXrefIDParameter = personDivisionXrefID.HasValue ?
                new ObjectParameter("PersonDivisionXrefID", personDivisionXrefID) :
                new ObjectParameter("PersonDivisionXrefID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("Lookups_Badging_GetPersonDivisionXrefEnityID", personDivisionXrefIDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> Lookups_Entities_GetEntityID_EX(Nullable<int> iD, string entityTypeCode)
        {
            var iDParameter = iD.HasValue ?
                new ObjectParameter("ID", iD) :
                new ObjectParameter("ID", typeof(int));
    
            var entityTypeCodeParameter = entityTypeCode != null ?
                new ObjectParameter("EntityTypeCode", entityTypeCode) :
                new ObjectParameter("EntityTypeCode", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("Lookups_Entities_GetEntityID_EX", iDParameter, entityTypeCodeParameter);
        }
    
        public virtual ObjectResult<Lookups_Badging_GetExpirationDates_Result> Lookups_Badging_GetExpirationDates(Nullable<int> personDivisionXrefID, Nullable<int> agreementID, Nullable<int> userID)
        {
            ((IObjectContextAdapter)this).ObjectContext.MetadataWorkspace.LoadFromAssembly(typeof(Lookups_Badging_GetExpirationDates_Result).Assembly);
    
            var personDivisionXrefIDParameter = personDivisionXrefID.HasValue ?
                new ObjectParameter("PersonDivisionXrefID", personDivisionXrefID) :
                new ObjectParameter("PersonDivisionXrefID", typeof(int));
    
            var agreementIDParameter = agreementID.HasValue ?
                new ObjectParameter("AgreementID", agreementID) :
                new ObjectParameter("AgreementID", typeof(int));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Lookups_Badging_GetExpirationDates_Result>("Lookups_Badging_GetExpirationDates", personDivisionXrefIDParameter, agreementIDParameter, userIDParameter);
        }
    
        public virtual ObjectResult<Badging_GetSpecialAccessCategories_Result> Lookups_Badging_GetSpecialAccessCategories(Nullable<int> doorId)
        {
            ((IObjectContextAdapter)this).ObjectContext.MetadataWorkspace.LoadFromAssembly(typeof(Badging_GetSpecialAccessCategories_Result).Assembly);
    
            var doorIdParameter = doorId.HasValue ?
                new ObjectParameter("doorId", doorId) :
                new ObjectParameter("doorId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Badging_GetSpecialAccessCategories_Result>("Lookups_Badging_GetSpecialAccessCategories", doorIdParameter);
        }
    
        public virtual ObjectResult<Badging_GetDoorsInCategory_Result> Lookups_Badging_GetDoorsInCategory(Nullable<int> categoryId)
        {
            ((IObjectContextAdapter)this).ObjectContext.MetadataWorkspace.LoadFromAssembly(typeof(Badging_GetDoorsInCategory_Result).Assembly);
    
            var categoryIdParameter = categoryId.HasValue ?
                new ObjectParameter("categoryId", categoryId) :
                new ObjectParameter("categoryId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Badging_GetDoorsInCategory_Result>("Lookups_Badging_GetDoorsInCategory", categoryIdParameter);
        }
    
        public virtual int Lookups_Badging_AddSpecialAccessCategory(Nullable<int> categoryId, Nullable<int> personDivisionXrefId, Nullable<int> userId)
        {
            var categoryIdParameter = categoryId.HasValue ?
                new ObjectParameter("categoryId", categoryId) :
                new ObjectParameter("categoryId", typeof(int));
    
            var personDivisionXrefIdParameter = personDivisionXrefId.HasValue ?
                new ObjectParameter("personDivisionXrefId", personDivisionXrefId) :
                new ObjectParameter("personDivisionXrefId", typeof(int));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Lookups_Badging_AddSpecialAccessCategory", categoryIdParameter, personDivisionXrefIdParameter, userIdParameter);
        }
    
        public virtual ObjectResult<Nullable<bool>> Lookups_Badging_Appointment_HasActiveBadge(Nullable<int> personDivisionXrefID)
        {
            var personDivisionXrefIDParameter = personDivisionXrefID.HasValue ?
                new ObjectParameter("PersonDivisionXrefID", personDivisionXrefID) :
                new ObjectParameter("PersonDivisionXrefID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<bool>>("Lookups_Badging_Appointment_HasActiveBadge", personDivisionXrefIDParameter);
        }
    
        public virtual ObjectResult<Nullable<bool>> Lookups_Badging_CancelBadge(Nullable<int> badgeId, Nullable<int> userId)
        {
            var badgeIdParameter = badgeId.HasValue ?
                new ObjectParameter("BadgeId", badgeId) :
                new ObjectParameter("BadgeId", typeof(int));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<bool>>("Lookups_Badging_CancelBadge", badgeIdParameter, userIdParameter);
        }
    
        public virtual ObjectResult<Lookups_Badging_NewBadgeExt_Result> Lookups_Badging_NewBadgeExt(Nullable<int> personDivisionXrefID, Nullable<int> userID, string badgeIssuanceReasonCode, Nullable<short> badgeColorID, Nullable<System.DateTime> whenExpires)
        {
            ((IObjectContextAdapter)this).ObjectContext.MetadataWorkspace.LoadFromAssembly(typeof(Lookups_Badging_NewBadgeExt_Result).Assembly);
    
            var personDivisionXrefIDParameter = personDivisionXrefID.HasValue ?
                new ObjectParameter("PersonDivisionXrefID", personDivisionXrefID) :
                new ObjectParameter("PersonDivisionXrefID", typeof(int));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var badgeIssuanceReasonCodeParameter = badgeIssuanceReasonCode != null ?
                new ObjectParameter("BadgeIssuanceReasonCode", badgeIssuanceReasonCode) :
                new ObjectParameter("BadgeIssuanceReasonCode", typeof(string));
    
            var badgeColorIDParameter = badgeColorID.HasValue ?
                new ObjectParameter("BadgeColorID", badgeColorID) :
                new ObjectParameter("BadgeColorID", typeof(short));
    
            var whenExpiresParameter = whenExpires.HasValue ?
                new ObjectParameter("WhenExpires", whenExpires) :
                new ObjectParameter("WhenExpires", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Lookups_Badging_NewBadgeExt_Result>("Lookups_Badging_NewBadgeExt", personDivisionXrefIDParameter, userIDParameter, badgeIssuanceReasonCodeParameter, badgeColorIDParameter, whenExpiresParameter);
        }
    
        public virtual ObjectResult<Lookups_Badge_GetStatusCode_Result> Lookups_Badge_GetStatusCode(Nullable<int> badgeId)
        {
            ((IObjectContextAdapter)this).ObjectContext.MetadataWorkspace.LoadFromAssembly(typeof(Lookups_Badge_GetStatusCode_Result).Assembly);
    
            var badgeIdParameter = badgeId.HasValue ?
                new ObjectParameter("BadgeId", badgeId) :
                new ObjectParameter("BadgeId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Lookups_Badge_GetStatusCode_Result>("Lookups_Badge_GetStatusCode", badgeIdParameter);
        }
    
        public virtual ObjectResult<Lookups_Badge_SetStatus_Result> Lookups_Badge_SetStatus(Nullable<int> badgeId, string statusCode, Nullable<System.DateTime> expirationDate, Nullable<int> userId)
        {
            ((IObjectContextAdapter)this).ObjectContext.MetadataWorkspace.LoadFromAssembly(typeof(Lookups_Badge_SetStatus_Result).Assembly);
    
            var badgeIdParameter = badgeId.HasValue ?
                new ObjectParameter("BadgeId", badgeId) :
                new ObjectParameter("BadgeId", typeof(int));
    
            var statusCodeParameter = statusCode != null ?
                new ObjectParameter("StatusCode", statusCode) :
                new ObjectParameter("StatusCode", typeof(string));
    
            var expirationDateParameter = expirationDate.HasValue ?
                new ObjectParameter("ExpirationDate", expirationDate) :
                new ObjectParameter("ExpirationDate", typeof(System.DateTime));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Lookups_Badge_SetStatus_Result>("Lookups_Badge_SetStatus", badgeIdParameter, statusCodeParameter, expirationDateParameter, userIdParameter);
        }
    
        public virtual int Lookups_Badging_Appointment_AddBadgeStatus(Nullable<int> badgeId, string badgeStatusCode, Nullable<System.DateTime> whenExpires, Nullable<int> userId)
        {
            var badgeIdParameter = badgeId.HasValue ?
                new ObjectParameter("BadgeId", badgeId) :
                new ObjectParameter("BadgeId", typeof(int));
    
            var badgeStatusCodeParameter = badgeStatusCode != null ?
                new ObjectParameter("BadgeStatusCode", badgeStatusCode) :
                new ObjectParameter("BadgeStatusCode", typeof(string));
    
            var whenExpiresParameter = whenExpires.HasValue ?
                new ObjectParameter("WhenExpires", whenExpires) :
                new ObjectParameter("WhenExpires", typeof(System.DateTime));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Lookups_Badging_Appointment_AddBadgeStatus", badgeIdParameter, badgeStatusCodeParameter, whenExpiresParameter, userIdParameter);
        }
    
        public virtual ObjectResult<string> Lookups_Badging_GetBadgeStatus(Nullable<int> badgeId)
        {
            var badgeIdParameter = badgeId.HasValue ?
                new ObjectParameter("BadgeId", badgeId) :
                new ObjectParameter("BadgeId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("Lookups_Badging_GetBadgeStatus", badgeIdParameter);
        }
    
        public virtual ObjectResult<Nullable<bool>> Lookups_Badging_Appointment_HasActiveBadgeWrapper(Nullable<int> personDivisionXrefID)
        {
            var personDivisionXrefIDParameter = personDivisionXrefID.HasValue ?
                new ObjectParameter("PersonDivisionXrefID", personDivisionXrefID) :
                new ObjectParameter("PersonDivisionXrefID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<bool>>("Lookups_Badging_Appointment_HasActiveBadgeWrapper", personDivisionXrefIDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> Lookups_BL_User_GetId(string loginName)
        {
            var loginNameParameter = loginName != null ?
                new ObjectParameter("LoginName", loginName) :
                new ObjectParameter("LoginName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("Lookups_BL_User_GetId", loginNameParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> Lookups_BL_User_GetStaffId(Nullable<int> userId)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("Lookups_BL_User_GetStaffId", userIdParameter);
        }
    
        public virtual ObjectResult<Lookups_Badging_Access_GetCategoriesByLocations_Result> Lookups_Badging_Access_GetCategoriesByLocations(Nullable<int> agreementId, Nullable<int> jobRoleId, string locationList)
        {
            ((IObjectContextAdapter)this).ObjectContext.MetadataWorkspace.LoadFromAssembly(typeof(Lookups_Badging_Access_GetCategoriesByLocations_Result).Assembly);
    
            var agreementIdParameter = agreementId.HasValue ?
                new ObjectParameter("AgreementId", agreementId) :
                new ObjectParameter("AgreementId", typeof(int));
    
            var jobRoleIdParameter = jobRoleId.HasValue ?
                new ObjectParameter("JobRoleId", jobRoleId) :
                new ObjectParameter("JobRoleId", typeof(int));
    
            var locationListParameter = locationList != null ?
                new ObjectParameter("LocationList", locationList) :
                new ObjectParameter("LocationList", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Lookups_Badging_Access_GetCategoriesByLocations_Result>("Lookups_Badging_Access_GetCategoriesByLocations", agreementIdParameter, jobRoleIdParameter, locationListParameter);
        }
    
        public virtual ObjectResult<Badging_Access_GetLocations_Result> Lookups_Badging_Access_GetLocations(Nullable<int> personDivisionXrefID, Nullable<int> agreementID, Nullable<int> jobRoleId)
        {
            ((IObjectContextAdapter)this).ObjectContext.MetadataWorkspace.LoadFromAssembly(typeof(Badging_Access_GetLocations_Result).Assembly);
    
            var personDivisionXrefIDParameter = personDivisionXrefID.HasValue ?
                new ObjectParameter("PersonDivisionXrefID", personDivisionXrefID) :
                new ObjectParameter("PersonDivisionXrefID", typeof(int));
    
            var agreementIDParameter = agreementID.HasValue ?
                new ObjectParameter("AgreementID", agreementID) :
                new ObjectParameter("AgreementID", typeof(int));
    
            var jobRoleIdParameter = jobRoleId.HasValue ?
                new ObjectParameter("JobRoleId", jobRoleId) :
                new ObjectParameter("JobRoleId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Badging_Access_GetLocations_Result>("Lookups_Badging_Access_GetLocations", personDivisionXrefIDParameter, agreementIDParameter, jobRoleIdParameter);
        }
    
        public virtual ObjectResult<Trainers_Result> Lookups_Trainers(Nullable<int> companyId)
        {
            ((IObjectContextAdapter)this).ObjectContext.MetadataWorkspace.LoadFromAssembly(typeof(Trainers_Result).Assembly);
    
            var companyIdParameter = companyId.HasValue ?
                new ObjectParameter("CompanyId", companyId) :
                new ObjectParameter("CompanyId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Trainers_Result>("Lookups_Trainers", companyIdParameter);
        }
    
        public virtual ObjectResult<string> Lookups_IWS_GetFBICaseNumber(Nullable<int> personID)
        {
            var personIDParameter = personID.HasValue ?
                new ObjectParameter("PersonID", personID) :
                new ObjectParameter("PersonID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("Lookups_IWS_GetFBICaseNumber", personIDParameter);
        }
    
        public virtual ObjectResult<Badging_GetLastCHRCAndSTADates_Result> Lookups_Badging_GetLastCHRCAndSTADates(Nullable<int> personDivisionXrefID)
        {
            ((IObjectContextAdapter)this).ObjectContext.MetadataWorkspace.LoadFromAssembly(typeof(Badging_GetLastCHRCAndSTADates_Result).Assembly);
    
            var personDivisionXrefIDParameter = personDivisionXrefID.HasValue ?
                new ObjectParameter("PersonDivisionXrefID", personDivisionXrefID) :
                new ObjectParameter("PersonDivisionXrefID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Badging_GetLastCHRCAndSTADates_Result>("Lookups_Badging_GetLastCHRCAndSTADates", personDivisionXrefIDParameter);
        }
    
        public virtual ObjectResult<Miscellaneous_Countries_Load_Result> Miscellaneous_Countries_Load()
        {
            ((IObjectContextAdapter)this).ObjectContext.MetadataWorkspace.LoadFromAssembly(typeof(Miscellaneous_Countries_Load_Result).Assembly);
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Miscellaneous_Countries_Load_Result>("Miscellaneous_Countries_Load");
        }
    
        public virtual ObjectResult<Nullable<System.DateTime>> Lookups_Badging_GetCALDoJFPAcceptedDate(Nullable<int> personDivisionXrefID)
        {
            var personDivisionXrefIDParameter = personDivisionXrefID.HasValue ?
                new ObjectParameter("PersonDivisionXrefID", personDivisionXrefID) :
                new ObjectParameter("PersonDivisionXrefID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<System.DateTime>>("Lookups_Badging_GetCALDoJFPAcceptedDate", personDivisionXrefIDParameter);
        }
    
        public virtual ObjectResult<Security_Users_GetCurrentGroupsForUser_Result> Security_Users_GetCurrentGroupsForUser(string userName)
        {
            ((IObjectContextAdapter)this).ObjectContext.MetadataWorkspace.LoadFromAssembly(typeof(Security_Users_GetCurrentGroupsForUser_Result).Assembly);
    
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Security_Users_GetCurrentGroupsForUser_Result>("Security_Users_GetCurrentGroupsForUser", userNameParameter);
        }
    }
}