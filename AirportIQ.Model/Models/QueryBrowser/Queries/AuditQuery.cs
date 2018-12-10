using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.Models.QueryBrowser.Queries
{
    public class AuditQuery : Query
    {
        private const string QueryName = "Audit";
        private const string DataSourceName = "[App.Sbo].[QueryBrowser.Audit]";
        private const string StoredProcedureName = "[App.Sbo].[QueryBrowser.AuditLookupLists]";
        private readonly string[] _lookupSets = new[]
                                                 {
                                                    "NamePrefixes",
                                                    "NameSuffixes",
                                                    "DivisionCodes",
                                                    "DivisionNames",
                                                    "DivisionTypeNames",
                                                    "DivisionTypeDescriptions",
                                                    "BadgeColors",
                                                    "AuditCategories",
                                                    "AuditTypes",
                                                    "AuditSpecificationStatuses"
                                                 };

        public AuditQuery()
        {
            CreateQuery();
        }

        protected virtual void CreateQuery()
        {
            Id = -1;
            Name = QueryName;
            DataSource = DataSourceName;
            QueryKey = QueryKeys.Audit;
            QueryType = QueryTypes.System;
            FilterByFacilityCode = true;
            LoadColumns();
        }

        protected virtual void LoadColumns()
        {
            this.AvailableColumns.Add(new DisplayColumn("PNP_NamePrefixDescription", "PNP_NamePrefixDescription", "Person Name Prefix", new[] { new DatabaseColumn("PPB_NamePrefixCode", DataType.String, 5) }, EditorType.DropDownList, "NamePrefixes"));
            this.AvailableColumns.Add(new DisplayColumn("PPB_FirstName", "PPB_FirstName", "Person First Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PPB_MiddleName", "PPB_MiddleName", "Person Middle Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PPB_LastName", "PPB_LastName", "Person Last Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PNS_NameSuffixDescription", "PNS_NameSuffixDescription", "Person Name Suffix", new[] { new DatabaseColumn("PPB_NameSuffixCode", DataType.String, 5) }, EditorType.DropDownList, "NameSuffixes"));
            this.AvailableColumns.Add(new DisplayColumn("SocialSecurityNumber", "SocialSecurityNumber", "Person Social Security Number", DataType.String, 11, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PPB_DateOfBirth", "PPB_DateOfBirth", "Person Date Of Birth", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("PPB_EmailAddress_Primary", "PPB_EmailAddress_Primary", "Person Primary Email Address", DataType.String, 50, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PPB_EmailAddress_Alternate", "PPB_EmailAddress_Alternate", "Person Secondary Email Address", DataType.String, 50, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PPB_EmployeeID", "PPB_EmployeeID", "Person Employee ID", DataType.Int, 4, EditorType.IntegerTextBox));
            this.AvailableColumns.Add(new DisplayColumn("CC_CompanyCode", "CC_CompanyCode", "Company ID", DataType.String, 4, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("CC_CorporationName", "CC_CorporationName", "Company Name", DataType.String, 60, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("CC_DBAName", "CC_DBAName", "Company DBA Name", DataType.String, 50, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("DD_DivisionCode", "DD_DivisionCode", "Company Division Code", new[] { new DatabaseColumn("DD_DivisionCode", DataType.String, 2) }, EditorType.DropDownList, "DivisionCodes"));
            this.AvailableColumns.Add(new DisplayColumn("DD_DivisionName", "DD_DivisionName", "Company Division Name", new[] { new DatabaseColumn("DD_DivisionID", DataType.Int, 0) }, EditorType.DropDownList, "DivisionNames"));
            this.AvailableColumns.Add(new DisplayColumn("DD_DivisionTypeName", "DD_DivisionTypeName", "Company Division Type Name", new[] { new DatabaseColumn("DD_DivisionTypeID", DataType.Int, 0) }, EditorType.DropDownList, "DivisionTypeNames"));
            this.AvailableColumns.Add(new DisplayColumn("DD_DivisionTypeDescription", "DD_DivisionTypeDescription", "Company Division Type Description", new[] { new DatabaseColumn("DD_DivisionTypeID", DataType.Int, 0) }, EditorType.DropDownList, "DivisionTypeDescriptions"));
            this.AvailableColumns.Add(new DisplayColumn("FacilityCode", "FacilityCode", "Division Facility Code", DataType.String, 5, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("BadgeCoordinatorFirstName", "BadgeCoordinatorFirstName", "Division Badge Coordinator First Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("BadgeCoordinatorMiddleName", "BadgeCoordinatorMiddleName", "Division Badge Coordinator Middle Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("BadgeCoordinatorLastName", "BadgeCoordinatorLastName", "Division Badge Coordinator Last Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("BadgeCoordinatorSuffixDescription", "BadgeCoordinatorSuffixDescription", "Division Badge Coordinator Suffix", new[] { new DatabaseColumn("BadgeCoordinatorSuffixCode", DataType.String, 5) }, EditorType.DropDownList, "NameSuffixes"));
            this.AvailableColumns.Add(new DisplayColumn("PB_BadgeNumber", "PB_BadgeNumber", "Person Badge Number", DataType.String, 15, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PB_WhenBecomesActive", "PB_WhenBecomesActive", "Person Badge Active Date", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("PB_WhenExpires", "PB_WhenExpires", "Person Badge Expiry Date", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("FBC_BadgeColor", "FBC_BadgeColor", "Person Badge Color", new[] { new DatabaseColumn("FBC_BadgeColorID", DataType.String, 5) }, EditorType.DropDownList, "BadgeColors"));
            this.AvailableColumns.Add(new DisplayColumn("AAB_SidaTrainingProof", "AAB_SidaTrainingProof", "Audit Badge Sida Training Proof", DataType.Bool, 1, EditorType.CheckBox));
            this.AvailableColumns.Add(new DisplayColumn("AAB_EmploymentProof", "AAB_EmploymentProof", "Audit Badge Employment Proof", DataType.Int, 5, EditorType.DropDownList));
            this.AvailableColumns.Add(new DisplayColumn("AAB_FingerprintProof", "AAB_FingerprintProof", "Audit Badge Fingerprint Proof", DataType.Bool, 1, EditorType.CheckBox));
            this.AvailableColumns.Add(new DisplayColumn("AAB_BadgeHasDriverIcon", "AAB_BadgeHasDriverIcon", "Audit Badge Has Driver Icon", DataType.Bool, 1, EditorType.CheckBox));
            this.AvailableColumns.Add(new DisplayColumn("AAB_DriverLogProof", "AAB_DriverLogProof", "Audit Badge Driver Log Proof", DataType.Bool, 1, EditorType.CheckBox));
            this.AvailableColumns.Add(new DisplayColumn("AAB_Notes", "AAB_Notes", "Audit Badge Notes", DataType.String, -1, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("AAB_IsHotNote", "AAB_IsHotNote", "Audit Badge Is Hot Note", DataType.Bool, 1, EditorType.CheckBox));
            this.AvailableColumns.Add(new DisplayColumn("BadgeStatusBegin", "BadgeStatusBegin", "Badge Status Active Date", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("BadgeStatusEnd", "BadgeStatusEnd", "Badge Status Expiry Date", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("PBSP_BadgeStatusCode", "PBSP_BadgeStatusCode", "Badge Status Code", DataType.String, 5, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("AuditGroupName", "AuditGroupName", "Audit Group Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("AAG_AuditDate", "AAG_AuditDate", "Audit Group Date", DataType.String, 3, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("AAC_AuditCategoryDescription", "AAC_AuditCategoryDescription", "Audit Category", new[] { new DatabaseColumn("AAC_AuditCategoryCode", DataType.String, 5) }, EditorType.DropDownList, "AuditCategories"));
            this.AvailableColumns.Add(new DisplayColumn("AAT_AuditTypeDescription", "AAT_AuditTypeDescription", "Audit Type", new[] { new DatabaseColumn("AAT_AuditTypeCode", DataType.String, 5) }, EditorType.DropDownList, "AuditTypes"));
            this.AvailableColumns.Add(new DisplayColumn("AAD_NumberOfBadgesToAudit", "AAD_NumberOfBadgesToAudit", "Audit Division Total Number of Badges to Audit", DataType.Int, 4, EditorType.IntegerTextBox));
            this.AvailableColumns.Add(new DisplayColumn("AAD_NumberOfDivisionBadges", "AAD_NumberOfDivisionBadges", "Audit Division Number of Division Badges", DataType.Int, 4, EditorType.IntegerTextBox));
            this.AvailableColumns.Add(new DisplayColumn("AAD_Notes", "AAD_Notes", "Audit Division Notes", DataType.String, -1, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("AAD_IsHotNote", "AAD_IsHotNote", "Audit Division Is Hot Note", DataType.Bool, 1, EditorType.CheckBox));
            this.AvailableColumns.Add(new DisplayColumn("AAD_FollowUp", "AAD_FollowUp", "Audit Division Follow Up", DataType.Bool, 1, EditorType.CheckBox));
            this.AvailableColumns.Add(new DisplayColumn("AAD_WhenCompleted", "AAD_WhenCompleted", "Audit Division When Completed", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("AuditDivisionStaffResponsibleFirstName", "AuditDivisionStaffResponsibleFirstName", "Audit Division Staff Responsible First Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("AuditDivisionStaffResponsibleMiddleName", "AuditDivisionStaffResponsibleMiddleName", "Audit Division Staff Responsible Middle Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("AuditDivisionStaffResponsibleLastName", "AuditDivisionStaffResponsibleLastName", "Audit Division Staff Responsible Last Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("AuditDivisionStaffResponsibleSuffixDescription", "AuditDivisionStaffResponsibleSuffixDescription", "Audit Division Staff Responsible Suffix", new[] { new DatabaseColumn("AuditDivisionStaffResponsibleSuffixCode", DataType.String, 5) }, EditorType.DropDownList, "NameSuffixes"));
            this.AvailableColumns.Add(new DisplayColumn("AuditSpecificationName", "AuditSpecificationName", "Audit Specification Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("AuditSpecificationStaffResponsibleFirstName", "AuditSpecificationStaffResponsibleFirstName", "Audit Specification Staff Responsible First Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("AuditSpecificationStaffResponsibleMiddleName", "AuditSpecificationStaffResponsibleMiddleName", "Audit Specification Staff Responsible Middle Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("AuditSpecificationStaffResponsibleLastName", "AuditSpecificationStaffResponsibleLastName", "Audit Specification Staff Responsible Last Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("AuditSpecificationStaffResponsibleSuffixDescription", "AuditSpecificationStaffResponsibleSuffixDescription", "Audit Specification Staff Responsible Suffix", new[] { new DatabaseColumn("AuditSpecificationStaffResponsibleSuffixCode", DataType.String, 5) }, EditorType.DropDownList, "NameSuffixes"));
            this.AvailableColumns.Add(new DisplayColumn("AAS_BadgesToAudit_TotalNumber", "AAS_BadgesToAudit_TotalNumber", "Audit Specification Total Number of Badges to Audit", DataType.Int, 4, EditorType.IntegerTextBox));
            this.AvailableColumns.Add(new DisplayColumn("AAS_BadgesToAudit_PercentInDivision", "AAS_BadgesToAudit_PercentInDivision", "Audit Specification Percent in Division", DataType.String, 1, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("AAS_DivisionBadgeMinimum", "AAS_DivisionBadgeMinimum", "Audit Specification Division Badge Minimum", DataType.Int, 4, EditorType.IntegerTextBox));
            this.AvailableColumns.Add(new DisplayColumn("AAS_DivisionBadgeMaximum", "AAS_DivisionBadgeMaximum", "Audit Specification Division Badge Maximum", DataType.Int, 4, EditorType.IntegerTextBox));
            this.AvailableColumns.Add(new DisplayColumn("AAS_DivisionAuditDateThreshold", "AAS_DivisionAuditDateThreshold", "Audit Specification Division Date Threshold", DataType.String, 3, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("AuditSpecificationStatusDescription", "AuditSpecificationStatusDescription", "Audit Specification Status", new[] { new DatabaseColumn("AuditSpecificationStatusCode", DataType.String, 5) }, EditorType.DropDownList, "AuditSpecificationStatuses"));
        }

        public override string SprocName
        {
            get
            {
                return StoredProcedureName;
            }
        }

        public override string[] LookupSets
        {
            get
            {
                return _lookupSets;
            }
        }
    }
}
