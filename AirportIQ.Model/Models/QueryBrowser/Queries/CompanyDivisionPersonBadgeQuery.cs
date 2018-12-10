using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.Models.QueryBrowser.Queries
{
    public class CompanyDivisionPersonBadgeQuery : Query
    {
        private const string QueryName = "Company - Division Person Badge";
        private const string DataSourceName = "[App.Sbo].[QueryBrowser.CompanyDivisionPersonBadge]";
        private const string StoredProcedureName = "[App.Sbo].[QueryBrowser.CompanyDivisionPersonBadgeLookupLists]";
        private readonly string[] _lookupSets = new[]
                                                 {
                                                    "DivisionCodes",
                                                    "DivisionNames",
                                                    "DivisionTypeNames",
                                                    "DivisionTypeDescriptions",
                                                    "NamePrefixes",
                                                    "NameSuffixes",
                                                    "CountrySubdivisions",
                                                    "Countries",
                                                    "EyeColors",
                                                    "HairColors",
                                                    "Sexes",
                                                    "Races",
                                                    "DivisionCategories",
                                                    "BadgeColors"
                                                 };


        public CompanyDivisionPersonBadgeQuery()
        {
            CreateQuery();
        }

        protected virtual void CreateQuery()
        {
            Id = -1;
            Name = QueryName;
            DataSource = DataSourceName;
            QueryKey = QueryKeys.CompanyDivisionPersonBadge;
            QueryType = QueryTypes.System;
            FilterByFacilityCode = true;
            LoadColumns();
        }

        protected virtual void LoadColumns()
        {
            this.AvailableColumns.Add(new DisplayColumn("CC_CompanyCode", "CC_CompanyCode", "Company Code", DataType.String, 4, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("CC_CorporationName", "CC_CorporationName", "Company Corporation Name", DataType.String, 60, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("CC_DBAName", "CC_DBAName", "Company DBA Name", DataType.String, 50, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("CC_BadgeName", "CC_BadgeName", "Company Badge Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PPBCoordinator_FirstName", "PPBCoordinator_FirstName", "Person Coordinator First Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PPBCoordinator_MiddleName", "PPBCoordinator_MiddleName", "Person Coordinator Middle Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PPBCoordinator_LastName", "PPBCoordinator_LastName", "Person Coordinator Last Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PPBCoordinator_NameSuffixDescription", "PPBCoordinator_NameSuffixDescription", "Person Coordinator Name Suffix", new[] { new DatabaseColumn("PPBCoordinator_NameSuffixCode", DataType.String, 5) }, EditorType.DropDownList, "NameSuffixes"));
            this.AvailableColumns.Add(new DisplayColumn("DD_DivisionCode", "DD_DivisionCode", "Company Division Code", new[] { new DatabaseColumn("DD_DivisionCode", DataType.String, 2) }, EditorType.DropDownList, "DivisionCodes"));
            this.AvailableColumns.Add(new DisplayColumn("DD_DivisionName", "DD_DivisionName", "Company Division Name", new[] { new DatabaseColumn("DivisionID", DataType.Int, 0) }, EditorType.DropDownList, "DivisionNames"));
            this.AvailableColumns.Add(new DisplayColumn("DD_DivisionTypeName", "DD_DivisionTypeName", "Company Division Type Name", new[] { new DatabaseColumn("DD_DivisionTypeID", DataType.Int, 0) }, EditorType.DropDownList, "DivisionTypeNames"));
            this.AvailableColumns.Add(new DisplayColumn("DD_DivisionTypeDescription", "DD_DivisionTypeDescription", "Company Division Type Description", new[] { new DatabaseColumn("DD_DivisionTypeID", DataType.Int, 0) }, EditorType.DropDownList, "DivisionTypeDescriptions"));
            this.AvailableColumns.Add(new DisplayColumn("DD_DivisionStatusCode", "DD_DivisionStatusDescription", "Company Division Status", new[] { new DatabaseColumn("DD_DivisionStatusCode", DataType.String, 10) }, EditorType.DropDownList, "DivisionStatus"));
            this.AvailableColumns.Add(new DisplayColumn("DD_City", "DD_City", "Company Division City", DataType.String, 25, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("DD_CountrySubdivisionCode", "DD_CountrySubdivisionName", "Company Division Country Subdivision", new[] { new DatabaseColumn("DD_CountrySubdivisionCode", DataType.String, 5) }, EditorType.DependentDropDownList, "CountrySubdivisions"));
            this.AvailableColumns.Add(new DisplayColumn("DD_PostalCode", "DD_PostalCode", "Company Division Postal Code", DataType.String, 10, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("DD_CountryCode", "DD_CountryDescription", "Company Division Country", new[] { new DatabaseColumn("DD_CountryCode", DataType.String, 5) }, EditorType.DropDownList, "Countries"));
            this.AvailableColumns.Add(new DisplayColumn("DD_CountrySubdivisionTypeName", "DD_CountrySubdivisionTypeName", "Company Division Country Subdivision Type Name", DataType.String, 15, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("DD_CategoryCode", "DD_CategoryDescription", "Company Division Category", new[] { new DatabaseColumn("DD_CategoryCode", DataType.String, 5) }, EditorType.DropDownList, "DivisionCategories"));
            this.AvailableColumns.Add(new DisplayColumn("FacilityCode", "FacilityCode", "Company Division Facility Code", DataType.String, 5, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PPDX_WhenBegins", "PPDX_WhenBegins", "Person Division Start Date", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("PPDX_WhenEnds", "PPDX_WhenEnds", "Person Division Expiry Date", DataType.DateTime, 10, EditorType.DatePicker));
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
            this.AvailableColumns.Add(new DisplayColumn("PPB_EmployeeID", "PPB_EmployeeID", "Person Employee ID", DataType.Int, 4, EditorType.IntegerTextBox));
            this.AvailableColumns.Add(new DisplayColumn("PPB_PIN", "PPB_PIN", "Person PIN", DataType.String, 5, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PB_BadgeNumber", "PB_BadgeNumber", "Person Badge Number", DataType.String, 15, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PB_BadgeIssuanceReasonCode", "PB_BadgeIssuanceReasonCode", "Person Badge Issuance Reason Description", DataType.String, 5, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PB_BadgeColorID", "PB_BadgeColor", "Person Badge Color", new[] { new DatabaseColumn("PB_BadgeColorID", DataType.String, 5) }, EditorType.DropDownList, "BadgeColors"));
            this.AvailableColumns.Add(new DisplayColumn("PB_WhenBecomesActive", "PB_WhenBecomesActive", "Person Badge Active Date", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("PB_WhenExpires", "PB_WhenExpires", "Person Badge Expiry Date", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("PB_DriveThroughGates", "PB_DriveThroughGates", "Person Badge Drive Through Gates Icon", DataType.Bool, 1, EditorType.CheckBox));
            this.AvailableColumns.Add(new DisplayColumn("PB_DriverLEO", "PB_DriverLEO", "Person Badge Drive LEO Icon", DataType.Bool, 1, EditorType.CheckBox));
            this.AvailableColumns.Add(new DisplayColumn("PBSP_WhenBecomesActive", "PBSP_WhenBecomesActive", "Person Badge Status Period Active Date", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("PBSP_WhenExpires", "PBSP_WhenExpires", "Person Badge Status Period Expiry Date", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("PBSP_BadgeStatusCode", "PBSP_BadgeStatusCode", "Person Badge Status Period Status Description", DataType.String, 5, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("AC_CategoryName", "AC_CategoryName", "Access Category Name", DataType.String, 60, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PBAP_WhenBecomesActive", "PBAP_WhenBecomesActive", "Access Period Active Date", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("PBAP_WhenExpires", "PBAP_WhenExpires", "Access Period Expirt Date", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("PBAP_AccessType", "PBAP_AccessType", "Access Period Access Type", DataType.String, 1, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("II_IconAbbreviation", "II_IconAbbreviation", "Icon Abbreviation", DataType.String, 10, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("II_IconDescription", "II_IconDescription", "Icon Description", DataType.String, 70, EditorType.TextBox));
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
