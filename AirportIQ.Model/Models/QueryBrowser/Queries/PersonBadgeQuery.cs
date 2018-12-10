using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.Models.QueryBrowser.Queries
{
    public class PersonBadgeQuery : Query
    {
        private const string QueryName = "Person - Badge";
        private const string DataSourceName = "[App.Sbo].[QueryBrowser.PersonBadge]";
        private const string StoredProcedureName = "[App.Sbo].[QueryBrowser.PersonBadgeLookupLists]";
        private readonly string[] _lookupSets = new[]
                                                 {
                                                    "DivisionCodes",
                                                    "DivisionNames",
                                                    "NamePrefixes",
                                                    "NameSuffixes",
                                                    "CountrySubdivisions",
                                                    "Countries",
                                                    "EyeColors",
                                                    "HairColors",
                                                    "Sexes",
                                                    "Races",
                                                    "BadgeColors",
                                                    "BadgeIssuanceReasons"
                                                 };

        public PersonBadgeQuery()
        {
            CreateQuery();
        }

        protected virtual void CreateQuery()
        {
            Id = -1;
            Name = QueryName;
            DataSource = DataSourceName;
            QueryKey = QueryKeys.PersonBadge;
            QueryType = QueryTypes.System;
            FilterByFacilityCode = true;
            LoadColumns();
        }

        protected virtual void LoadColumns()
        {
            this.AvailableColumns.Add(new DisplayColumn("PPB_PersonID", "PPB_PersonID", "Person ID", DataType.Int, 4, EditorType.IntegerTextBox));
            this.AvailableColumns.Add(new DisplayColumn("PPB_NamePrefixDescription", "PPB_NamePrefixDescription", "Person Name Prefix", new[] { new DatabaseColumn("PPB_NamePrefixCode", DataType.String, 5) }, EditorType.DropDownList, "NamePrefixes"));
            this.AvailableColumns.Add(new DisplayColumn("PPB_FirstName", "PPB_FirstName", "Person First Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PPB_MiddleName", "PPB_MiddleName", "Person Middle Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PPB_LastName", "PPB_LastName", "Person Last Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PPB_NameSuffixDescription", "PPB_NameSuffixDescription", "Person Name Suffix", new[] { new DatabaseColumn("PPB_NameSuffixCode", DataType.String, 5) }, EditorType.DropDownList, "NameSuffixes"));
            this.AvailableColumns.Add(new DisplayColumn("PPB_Address", "PPB_Address", "Person Address", DataType.String, 50, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PPB_ApartmentNumber", "PPB_ApartmentNumber", "Person Apartment Number", DataType.String, 10, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PPB_City", "PPB_City", "Person Biographics City", DataType.String, 25, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PPB_CountrySubdivisionCode", "PPB_CountrySubdivisionName", "Person Country Subdivision", new[] { new DatabaseColumn("PPB_CountrySubdivisionCode", DataType.String, 5) }, EditorType.DependentDropDownList, "CountrySubdivisions"));
            this.AvailableColumns.Add(new DisplayColumn("PPB_PostalCode", "PPB_PostalCode", "Person Postal Code", DataType.String, 10, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PPB_CountryCode", "PPB_CountryDescription", "Person Country", new[] { new DatabaseColumn("PPB_CountryCode", DataType.String, 5) }, EditorType.DropDownList, "Countries"));
            this.AvailableColumns.Add(new DisplayColumn("PPB_SocialSecurityNumber", "PPB_SocialSecurityNumber", "Person Social Security Number", DataType.String, 11, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PPB_HomePhoneNumber", "PPB_HomePhoneNumber", "Person Home Phone Number", DataType.String, 14, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PPB_WorkPhoneNumber", "PPB_WorkPhoneNumber", "Person Work Phone Number", DataType.String, 14, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PPB_DateOfBirth", "PPB_DateOfBirth", "Person Date Of Birth", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("PPB_HeightInInches", "PPB_HeightInInches", "Person Height In Inches", DataType.String, 1, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PPB_WeightInPounds", "PPB_WeightInPounds", "Person Weight In Pounds", DataType.Int, 2, EditorType.IntegerTextBox));
            this.AvailableColumns.Add(new DisplayColumn("PPB_EyeColorCode", "PPB_EyeColorDescription", "Person Eye Color", new[] { new DatabaseColumn("PPB_EyeColorCode", DataType.String, 10) }, EditorType.DropDownList, "EyeColors"));
            this.AvailableColumns.Add(new DisplayColumn("PPB_HairColorCode", "PPB_HairColorDescription", "Person Hair Color", new[] { new DatabaseColumn("PPB_HairColorCode", DataType.String, 10) }, EditorType.DropDownList, "HairColors"));
            this.AvailableColumns.Add(new DisplayColumn("PPB_RaceCode", "PPB_RaceDescription", "Person Ethnicity", new[] { new DatabaseColumn("PPB_RaceCode", DataType.String, 10) }, EditorType.DropDownList, "Races"));
            this.AvailableColumns.Add(new DisplayColumn("PPB_SexCode", "PPB_SexDescription", "Person Gender", new[] { new DatabaseColumn("PPB_SexCode", DataType.String, 10) }, EditorType.DropDownList, "Sexes"));
            this.AvailableColumns.Add(new DisplayColumn("PPB_CountryCode_Birth", "PPB_CountryDescription_Birth", "Person Country of Birth", new[] { new DatabaseColumn("PPB_CountryCode_Birth", DataType.String, 5) }, EditorType.DropDownList, "Countries"));
            this.AvailableColumns.Add(new DisplayColumn("PPB_CountrySubdivisionCode_Birth", "PPB_CountrySubdivisionName_Birth", "Person State of Birth", new[] { new DatabaseColumn("PPB_CountrySubdivisionCode_Birth", DataType.String, 5) }, EditorType.DependentDropDownList, "CountrySubdivisions"));
            this.AvailableColumns.Add(new DisplayColumn("PPB_EmailAddress_Primary", "PPB_EmailAddress_Primary", "Person Primary Email Address", DataType.String, 50, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PPB_EmailAddress_Alternate", "PPB_EmailAddress_Alternate", "Person Alternate Email Address", DataType.String, 50, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PPB_CountryCode_Citizenship", "PPB_CountryDescription_Citizenship", "Person Country Citizenship", new[] { new DatabaseColumn("PPB_CountryCode_Citizenship", DataType.String, 5) }, EditorType.DropDownList, "Countries"));
            this.AvailableColumns.Add(new DisplayColumn("PPB_PIN", "PPB_PIN", "Person PIN", DataType.String, 5, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("DD_DivisionCode", "DD_DivisionCode", "Company Division Code", new[] { new DatabaseColumn("DD_DivisionCode", DataType.String, 2) }, EditorType.DropDownList, "DivisionCodes"));
            this.AvailableColumns.Add(new DisplayColumn("DD_DivisionName", "DD_DivisionName", "Company Division Name", new[] { new DatabaseColumn("DD_DivisionID", DataType.Int, 0) }, EditorType.DropDownList, "DivisionNames"));
            this.AvailableColumns.Add(new DisplayColumn("CC_CompanyCode", "CC_CompanyCode", "Company Code", DataType.String, 4, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("CC_CorporationName", "CC_CorporationName", "Company Corporation Name", DataType.String, 60, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("CC_DBAName", "CC_DBAName", "Company DBA Name", DataType.String, 50, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("CC_BadgeName", "CC_BadgeName", "Company Badge Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PB_BadgeNumber", "PB_BadgeNumber", "Person Badge Number", DataType.String, 15, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PB_ProxNumber", "PB_ProxNumber", "Person Badge Prox Number", DataType.String, 15, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PB_BadgeIssuanceReasonDescription", "PB_BadgeIssuanceReasonDescription", "Person Badge Issuance Reason", new[] { new DatabaseColumn("PB_BadgeIssuanceReasonCode", DataType.String, 5) }, EditorType.DropDownList, "BadgeIssuanceReasons"));
            this.AvailableColumns.Add(new DisplayColumn("FBC_BadgeColorID", "FBC_BadgeColor", "Person Badge Color", new[] { new DatabaseColumn("FBC_BadgeColorID", DataType.String, 5) }, EditorType.DropDownList, "BadgeColors"));
            this.AvailableColumns.Add(new DisplayColumn("BadgeIssue", "BadgeIssue", "Person Badge Issued", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("BadgeExpire", "BadgeExpire", "Person Badge Expire", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("PB_DriveThroughGates", "PB_DriveThroughGates", "Person Badge Drive Through Gates Icon", DataType.Bool, 1, EditorType.CheckBox));
            this.AvailableColumns.Add(new DisplayColumn("PB_DriverLEO", "PB_DriverLEO", "Person Badge Driver LEO Icon", DataType.Bool, 1, EditorType.CheckBox));
            this.AvailableColumns.Add(new DisplayColumn("StatusActive", "StatusActive", "Person Badge Active Date", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("StatusExpire", "StatusExpire", "Person Badge Expiration Date", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("PBSP_BadgeStatusCode", "PBSP_BadgeStatusCode", "Badge Status Code", DataType.String, 5, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("AccessBegin", "AccessBegin", "Badge Access Begin", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("AccessEnd", "AccessEnd", "Badge Access End", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("PBAP_AccessType", "PBAP_AccessType", "Badge Access Type", DataType.String, 1, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("FacilityCode", "FacilityCode", "Access Category Facility Code", DataType.String, 5, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("AC_CategoryName", "AC_CategoryName", "Access Category Name", DataType.String, 60, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("II_IconDescription", "II_IconDescription", "Icon Description", DataType.String, 70, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("II_IconAbbreviation", "II_IconAbbreviation", "Icon Abbreviation", DataType.String, 10, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("JobRoleLocationBegin", "JobRoleLocationBegin", "Job Role Start Date", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("JobRoleLocationEnd", "JobRoleLocationEnd", "Job Role End Date", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("PJR_JobRoleAbbreviation", "PJR_JobRoleAbbreviation", "Job Role Abbreviation", DataType.String, 15, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PJR_JobRoleDescription", "PJR_JobRoleDescription", "Job Role Description", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("FL_LocationAbbreviation", "FL_LocationAbbreviation", "Facility Location Abbreviation", DataType.String, 15, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("FL_LocationName", "FL_LocationName", "Facility Location Name", DataType.String, 40, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("EffectiveBegin", "EffectiveBegin", "Job Role Effective Start Date", DataType.String, 3, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("EffectiveEnd", "EffectiveEnd", "Job Role Effective End Date", DataType.String, 3, EditorType.TextBox));
        }

        public override string SprocName
        {
            get { return StoredProcedureName; }
        }

        public override string[] LookupSets
        {
            get { return _lookupSets; }
        }
    }
}
