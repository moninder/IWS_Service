using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.Models.QueryBrowser.Queries
{
    public class PersonBackgroundCheckQuery : Query
    {
        private const string QueryName = "Person - Background Check";
        private const string DataSourceName = "[App.Sbo].[QueryBrowser.PersonBackgroundCheck]";
        private const string StoredProcedureName = "[App.Sbo].[QueryBrowser.PersonBackgroundCheckLookupLists]";
        private readonly string[] _lookupSets = new[]
                                                 {
                                                    "NamePrefixes",
                                                    "NameSuffixes",
                                                    "Countries",
                                                    "DivisionCodes",
                                                    "DivisionNames",
                                                    "DivisionCategories",
                                                    "CountrySubdivisions"
                                                 };

        public PersonBackgroundCheckQuery()
        {
            CreateQuery();
        }

        protected virtual void CreateQuery()
        {
            Id = -1;
            Name = QueryName;
            DataSource = DataSourceName;
            QueryKey = QueryKeys.PersonBackgroundCheck;
            QueryType = QueryTypes.System;
            FilterByFacilityCode = true;
            LoadColumns();
        }

        protected virtual void LoadColumns()
        {
            this.AvailableColumns.Add(new DisplayColumn("PPB_NamePrefixDescription", "PPB_NamePrefixDescription", "Person Name Prefix", new[] { new DatabaseColumn("PPB_NamePrefixCode", DataType.String, 5) }, EditorType.DropDownList, "NamePrefixes"));
            this.AvailableColumns.Add(new DisplayColumn("PPB_FirstName", "PPB_FirstName", "Person First Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PPB_MiddleName", "PPB_MiddleName", "Person Middle Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PPB_LastName", "PPB_LastName", "Person Last Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PPB_NameSuffixDescription", "PPB_NameSuffixDescription", "Person Name Suffix", new[] { new DatabaseColumn("PPB_NameSuffixCode", DataType.String, 5) }, EditorType.DropDownList, "NameSuffixes"));
            this.AvailableColumns.Add(new DisplayColumn("PPB_Address", "PPB_Address", "Person Address", DataType.String, 50, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PPB_ApartmentNumber", "PPB_ApartmentNumber", "Person Apartment Number", DataType.String, 10, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PPB_City", "PPB_City", "Person City", DataType.String, 25, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PPB_CountrySubdivisionCode", "PPB_CountrySubdivisionName", "Person Country Subdivision", new[] { new DatabaseColumn("PPB_CountrySubdivisionCode", DataType.String, 5) }, EditorType.DependentDropDownList, "CountrySubdivisions"));
            this.AvailableColumns.Add(new DisplayColumn("PPB_PostalCode", "PPB_PostalCode", "Person Postal Code", DataType.String, 10, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PPB_HomePhoneNumber", "PPB_HomePhoneNumber", "Person Home Phone Number", DataType.String, 14, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PPB_WorkPhoneNumber", "PPB_WorkPhoneNumber", "Person Work Phone Number", DataType.String, 14, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PPB_EmailAddress_Primary", "PPB_EmailAddress_Primary", "Person Primary Email Address", DataType.String, 50, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PPB_CountryCode", "PPB_CountryDescription", "Person Country", new[] { new DatabaseColumn("PPB_CountryCode", DataType.String, 5) }, EditorType.DropDownList, "Countries"));
            this.AvailableColumns.Add(new DisplayColumn("PPB_SocialSecurityNumber", "PPB_SocialSecurityNumber", "Person Social Security Number", DataType.String, 11, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PPB_DateOfBirth", "PPB_DateOfBirth", "Person Date Of Birth", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("PPB_CountryCode_Birth", "PPB_CountryDescription_Birth", "Person Birth Country", new[] { new DatabaseColumn("PPB_CountryCode_Birth", DataType.String, 5) }, EditorType.DropDownList, "Countries"));
            this.AvailableColumns.Add(new DisplayColumn("PPB_CountryCode_Citizenship", "PPB_CountryCode_Citizenship", "Person Birth Citizenship", new[] { new DatabaseColumn("PPB_CountryCode_Citizenship", DataType.String, 5) }, EditorType.DropDownList, "Countries"));
            this.AvailableColumns.Add(new DisplayColumn("PPB_EmployeeID", "PPB_EmployeeID", "Person Employee ID", DataType.Int, 4, EditorType.IntegerTextBox));
            this.AvailableColumns.Add(new DisplayColumn("BCPDC_WhenFingerprintTaken", "BCPDC_WhenFingerprintTaken", "Background Check When Fingerprint Taken", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("BCPDC_WhenMessageReceived", "BCPDC_WhenMessageReceived", "Background Check When Message", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("BCWM_WebMessageCode", "BCWM_WebMessageCode", "Background Check Web Message Code", DataType.String, 10, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("BCWM_WebStatus", "BCWM_WebStatus", "Background Check Web Status", DataType.String, 70, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("BCPDC_BadgeIsDenied", "BCPDC_BadgeIsDenied", "Background Check Badge Is Denied", DataType.Bool, 1, EditorType.CheckBox));
            this.AvailableColumns.Add(new DisplayColumn("CC_CompanyCode", "CC_CompanyCode", "Company ID", DataType.String, 4, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("CC_CorporationName", "CC_CorporationName", "Company Name", DataType.String, 60, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("CC_DBAName", "CC_DBAName", "Company DBA Name", DataType.String, 50, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("DD_DivisionCode", "DD_DivisionCode", "Company Division Code", new[] { new DatabaseColumn("DD_DivisionCode", DataType.String, 2) }, EditorType.DropDownList, "DivisionCodes"));
            this.AvailableColumns.Add(new DisplayColumn("DD_DivisionName", "DD_DivisionName", "Company Division Name", new[] { new DatabaseColumn("DD_DivisionID", DataType.Int, 0) }, EditorType.DropDownList, "DivisionNames"));
            this.AvailableColumns.Add(new DisplayColumn("DD_CategoryCode", "DD_CategoryDescription", "Company Division Category", new[] { new DatabaseColumn("DD_CategoryCode", DataType.String, 5) }, EditorType.DropDownList, "DivisionCategories"));
            this.AvailableColumns.Add(new DisplayColumn("FacilityCode", "FacilityCode", "Division Facility Code", DataType.String, 5, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("BadgeCoordinatorFirstName", "BadgeCoordinatorFirstName", "Badge Coordinator First Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("BadgeCoordinatorMiddleName", "BadgeCoordinatorMiddleName", "Badge Coordinator Middle Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("BadgeCoordinatorLastName", "BadgeCoordinatorLastName", "Badge Coordinator Last Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("BadgeCoordinatorSuffixDescription", "BadgeCoordinatorSuffixDescription", "Badge Coordinator Suffix", new[] { new DatabaseColumn("BadgeCoordinatorSuffixCode", DataType.String, 5) }, EditorType.DropDownList, "NameSuffixes"));
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
