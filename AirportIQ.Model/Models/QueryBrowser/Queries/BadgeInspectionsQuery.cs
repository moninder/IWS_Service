using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.Models.QueryBrowser.Queries
{
    public class BadgeInspectionsQuery : Query
    {
        private const string QueryName = "Badge Inspections";
        private const string DataSourceName = "[App.Sbo].[QueryBrowser.BadgeInspections]";
        private const string StoredProcedureName = "[App.Sbo].[QueryBrowser.BadgeInspectionsLookupLists]";

        private readonly string[] _lookupSets = new[]
                                                 {
                                                    "AuditInspectionTypes",
                                                    "BadgeColors",
                                                    "Countries",
                                                    "EyeColors",
                                                    "HairColors",
                                                    "Race",
                                                    "Sex",
                                                    "NamePrefixes",
                                                    "NameSuffixes",
                                                    "Officer1NameSuffixes",
                                                    "Officer2NameSuffixes",
                                                    "Officer3NameSuffixes"
                                                 };

        public BadgeInspectionsQuery()
        {
            CreateQuery();
        }

        protected virtual void CreateQuery()
        {
            Id = -1;
            Name = QueryName;
            DataSource = DataSourceName;
            QueryKey = QueryKeys.BadgeInspections;
            QueryType = QueryTypes.System;
            FilterByFacilityCode = true;
            LoadColumns();
        }

        protected virtual void LoadColumns()
        {
            this.AvailableColumns.Add(new DisplayColumn("AuditDate", "AuditDate", "Audit Date", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("WatchNumber", "WatchNumber", "Watch Number", DataType.Int, 30, EditorType.IntegerTextBox));
            this.AvailableColumns.Add(new DisplayColumn("Location", "Location", "Location", DataType.String, 50, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("Officer1_SerialNumber", "Officer1_SerialNumber", "Officer 1 Serial Number", DataType.String, 10, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("Officer1_LastName", "Officer1_LastName", "Officer 1 Last Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("Officer1_FirstName", "Officer1_FirstName", "Officer 1 First Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("Officer1_MiddleName", "Officer1_MiddleName", "Officer 1 Middle Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("Officer1_NameSuffixCode", "Officer1_NameSuffixCode", "Officer 1 Name Suffix", new[] { new DatabaseColumn("Officer1_NameSuffixCode", DataType.String, 3) }, EditorType.DropDownList, "Officer1NameSuffixes"));
            this.AvailableColumns.Add(new DisplayColumn("Officer2_SerialNumber", "Officer2_SerialNumber", "Officer 2 Serial Number", DataType.String, 10, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("Officer2_LastName", "Officer2_LastName", "Officer 2 Last Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("Officer2_FirstName", "Officer2_FirstName", "Officer 2 First Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("Officer2_MiddleName", "Officer2_MiddleName", "Officer 2 Middle Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("Officer2_NameSuffixCode", "Officer2_NameSuffixCode", "Officer 2 Name Suffix", new[] { new DatabaseColumn("Officer2_NameSuffixCode", DataType.String, 3) }, EditorType.DropDownList, "Officer2NameSuffixes"));
            this.AvailableColumns.Add(new DisplayColumn("Officer3_SerialNumber", "Officer3_SerialNumber", "Officer 3 Serial Number", DataType.String, 10, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("Officer3_LastName", "Officer3_LastName", "Officer 3 Last Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("Officer3_FirstName", "Officer3_FirstName", "Officer 3 First Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("Officer3_MiddleName", "Officer3_MiddleName", "Officer 3 Middle Name", DataType.String, 30, EditorType.TextBox));
            //this.AvailableColumns.Add(new DisplayColumn("Officer3_NameSuffixCode", "Officer3_NameSuffixCode", "Officer 3 Name Suffix Code", DataType.String, 3, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("Officer3_NameSuffixCode", "Officer3_NameSuffixCode", "Officer 3 Name Suffix", new[] { new DatabaseColumn("Officer3_NameSuffixCode", DataType.String, 3) }, EditorType.DropDownList, "Officer3NameSuffixes"));
            this.AvailableColumns.Add(new DisplayColumn("LegacyOfficers", "LegacyOfficers", "Legacy Officers", DataType.String, 75, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("AuditInspectionTypeDescription", "AuditInspectionTypeDescription", "Audit Inspection Type", new[] { new DatabaseColumn("AuditInspectionTypeCode", DataType.String, 5) }, EditorType.DropDownList, "AuditInspectionTypes"));
            this.AvailableColumns.Add(new DisplayColumn("AuditBadgeNumber", "AuditBadgeNumber", "Audit Badge Number", DataType.String, 15, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("AuditLastName", "AuditLastName", "Audit Last Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("BadgeNumber", "BadgeNumber", "Badge Number", DataType.String, 15, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("BadgeColor", "BadgeColor", "Badge Color", new[] { new DatabaseColumn("BadgeColorID", DataType.String, 5) }, EditorType.DropDownList, "BadgeColors"));
            this.AvailableColumns.Add(new DisplayColumn("BadgeIssuedDate", "BadgeIssuedDate", "Badge Issued Date", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("BadgeExpireDate", "BadgeExpireDate", "Badge Expire Date", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("EmployeeID", "EmployeeID", "Employee ID", DataType.Int, 30, EditorType.IntegerTextBox));
            this.AvailableColumns.Add(new DisplayColumn("NamePrefixCode", "NamePrefixCode", "Name Prefix", new[] { new DatabaseColumn("NamePrefixCode", DataType.String, 3) }, EditorType.DropDownList, "NamePrefixes"));
            this.AvailableColumns.Add(new DisplayColumn("FirstName", "FirstName", "First Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("MiddleName", "MiddleName", "Middle Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("LastName", "LastName", "Last Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("NameSuffixCode", "NameSuffixCode", "Name Suffix", new[] { new DatabaseColumn("NameSuffixCode", DataType.String, 3) }, EditorType.DropDownList, "NameSuffixes"));
            this.AvailableColumns.Add(new DisplayColumn("Address", "Address", "Address", DataType.String, 50, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("ApartmentNumber", "ApartmentNumber", "Apartment Number", DataType.String, 10, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("City", "City", "City", DataType.String, 25, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("CountrySubdivisionCode", "CountrySubdivisionCode", "Country Subdivision Code", DataType.String, 3, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PostalCode", "PostalCode", "Postal Code", DataType.String, 10, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("CountryCode", "CountryCode", "Country", new[] { new DatabaseColumn("CountryCode", DataType.String, 3) }, EditorType.DropDownList, "Countries"));
            this.AvailableColumns.Add(new DisplayColumn("DateOfBirth", "DateOfBirth", "Date Of Birth", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("HeightInInches", "HeightInInches", "Height In Inches", DataType.Int, 3, EditorType.IntegerTextBox));
            this.AvailableColumns.Add(new DisplayColumn("WeightInPounds", "WeightInPounds", "Weight In Pounds", DataType.Int, 3, EditorType.IntegerTextBox));
            this.AvailableColumns.Add(new DisplayColumn("EyeColorCode", "EyeColorCode", "Eye Color", new[] { new DatabaseColumn("EyeColorCode", DataType.String, 3) }, EditorType.DropDownList, "EyeColors"));
            this.AvailableColumns.Add(new DisplayColumn("HairColorCode", "HairColorCode", "Hair Color", new[] { new DatabaseColumn("HairColorCode", DataType.String, 3) }, EditorType.DropDownList, "HairColors"));
            this.AvailableColumns.Add(new DisplayColumn("RaceCode", "RaceCode", "Race", new[] { new DatabaseColumn("RaceCode", DataType.String, 1) }, EditorType.DropDownList, "Race"));
            this.AvailableColumns.Add(new DisplayColumn("SexCode", "SexCode", "Sex", new[] { new DatabaseColumn("SexCode", DataType.String, 1) }, EditorType.DropDownList, "Sex"));
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
