using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.Models.QueryBrowser.Queries
{
    public class CompanyDivisionEmployeesQuery : Query
    {
        private const string QueryName = "Company - Division Employees";
        private const string DataSourceName = "[App.Sbo].[QueryBrowser.CompanyDivisionEmployees]";
        private const string StoredProcedureName = "[App.Sbo].[QueryBrowser.CompanyDivisionEmployeeLookupLists]";
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
                                                    "Races"
                                                 };

        public CompanyDivisionEmployeesQuery()
        {
            CreateQuery();
        }

        protected virtual void CreateQuery()
        {
            Id = -1;
            Name = QueryName;
            DataSource = DataSourceName;
            QueryKey = QueryKeys.CompanyDivisionEmployees;
            QueryType = QueryTypes.System;
            FilterByFacilityCode = true;
            LoadColumns();
        }

        protected virtual void LoadColumns()
        {
            this.AvailableColumns.Add(new DisplayColumn("FacilityCode", "FacilityCode", "Facility Code", DataType.String, 5, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("FacilityName", "FacilityName", "Facility Name", DataType.String, 50, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("CompanyCode", "CompanyCode", "Company Code", DataType.String, 4, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("CorporationName", "CorporationName", "Company Corporation Name", DataType.String, 60, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("DBAName", "DBAName", "Company DBA Name", DataType.String, 50, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("BadgeName", "BadgeName", "Company Badge Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("DivisionCode", "DivisionCode", "Company Division Code", new[] { new DatabaseColumn("DivisionCode", DataType.String, 2) }, EditorType.DropDownList, "DivisionCodes"));
            this.AvailableColumns.Add(new DisplayColumn("DivisionName", "DivisionName", "Company Division Name", new[] { new DatabaseColumn("DivisionID", DataType.Int, 0) }, EditorType.DropDownList, "DivisionNames"));
            this.AvailableColumns.Add(new DisplayColumn("DivisionTypeName", "DivisionTypeName", "Company Division Type Name", new[] { new DatabaseColumn("DivisionTypeID", DataType.Int, 0) }, EditorType.DropDownList, "DivisionTypeNames"));
            this.AvailableColumns.Add(new DisplayColumn("DivisionTypeDescription", "DivisionTypeDescription", "Company Division Type Description", new[] { new DatabaseColumn("DivisionTypeID", DataType.Int, 0) }, EditorType.DropDownList, "DivisionTypeDescriptions"));
            this.AvailableColumns.Add(new DisplayColumn("NamePrefixDescription", "NamePrefixDescription", "Company Division Employee Name Prefix", new[] { new DatabaseColumn("NamePrefixCode", DataType.String, 5) }, EditorType.DropDownList, "NamePrefixes"));
            this.AvailableColumns.Add(new DisplayColumn("FirstName", "FirstName", "Company Division Employee First Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("FirstName_Sound", "FirstName_Sound", "Company Division Employee First Name Sound", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("MiddleName", "MiddleName", "Company Division Employee Middle Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("MiddleName_Sound", "MiddleName_Sound", "Company Division Employee Middle Name Sound", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("LastName", "LastName", "Company Division Employee Last Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("LastName_Sound", "LastName_Sound", "Company Division Employee Last Name Sound", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("NameSuffixDescription", "NameSuffixDescription", "Company Division Employee Name Suffix", new[] { new DatabaseColumn("NameSuffixCode", DataType.String, 5) }, EditorType.DropDownList, "NameSuffixes"));
            this.AvailableColumns.Add(new DisplayColumn("SocialSecurityNumber", "SocialSecurityNumber", "Company Division Employee SSN", DataType.String, 11, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("Address", "Address", "Company Division Employee Address", DataType.String, 50, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("ApartmentNumber", "ApartmentNumber", "Company Division Employee Apartment Number", DataType.String, 10, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("City", "City", "Company Division Employee City", DataType.String, 25, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("CountrySubdivisionCode", "CountrySubdivisionName", "Company Division Employee Country Subdivision", new[] { new DatabaseColumn("CountrySubdivisionCode", DataType.String, 5) }, EditorType.DependentDropDownList, "CountrySubdivisions"));
            this.AvailableColumns.Add(new DisplayColumn("PostalCode", "PostalCode", "Company Division Employee Postal Code", DataType.String, 10, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("CountryCode", "CountryDescription", "Company Division Employee Country", new[] { new DatabaseColumn("CountryCode", DataType.String, 5) }, EditorType.DropDownList, "Countries"));
            this.AvailableColumns.Add(new DisplayColumn("CountrySubdivisionTypeName", "CountrySubdivisionTypeName", "Company Division Employee Country Subdivision Type", DataType.String, 15, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("HomePhoneNumber", "HomePhoneNumber", "Company Division Employee Home Phone Number", DataType.String, 14, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("WorkPhoneNumber", "WorkPhoneNumber", "Company Division Employee Work Phone Number", DataType.String, 14, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("DateOfBirth", "DateOfBirth", "Company Division Employee Date Of Birth", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("Height", "Height", "Company Division Employee Height", DataType.String, 10, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("WeightInPounds", "WeightInPounds", "Company Division Employee Weight", DataType.Int, 3, EditorType.IntegerTextBox));
            this.AvailableColumns.Add(new DisplayColumn("EyeColorCode", "EyeColorDescription", "Company Division Employee Eye Color", new[] { new DatabaseColumn("EyeColorCode", DataType.String, 10) }, EditorType.DropDownList, "EyeColors"));
            this.AvailableColumns.Add(new DisplayColumn("HairColorCode", "HairColorDescription", "Company Division Employee Hair Color", new[] { new DatabaseColumn("HairColorCode", DataType.String, 10) }, EditorType.DropDownList, "HairColors"));
            this.AvailableColumns.Add(new DisplayColumn("RaceCode", "RaceDescription", "Company Division Employee Ethnicity", new[] { new DatabaseColumn("RaceCode", DataType.String, 10) }, EditorType.DropDownList, "Races"));
            this.AvailableColumns.Add(new DisplayColumn("SexCode", "SexDescription", "Company Division Employee Gender", new[] { new DatabaseColumn("SexCode", DataType.String, 10) }, EditorType.DropDownList, "Sexes"));
            this.AvailableColumns.Add(new DisplayColumn("Birth_CountrySubdivisionCode", "Birth_CountrySubdivisionName", "Company Division Employee Birth Country Subdivision", new[] { new DatabaseColumn("Birth_CountrySubdivisionCode", DataType.String, 5) }, EditorType.DependentDropDownList, "CountrySubdivisions"));
            this.AvailableColumns.Add(new DisplayColumn("Birth_CountryCode", "Birth_CountryDescription", "Company Division Employee Birth Country", new[] { new DatabaseColumn("Birth_CountryCode", DataType.String, 5) }, EditorType.DropDownList, "Countries"));
            this.AvailableColumns.Add(new DisplayColumn("EmailAddress_Primary", "EmailAddress_Primary", "Company Division Employee Email Address Primary", DataType.String, 50, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("EmailAddress_Alternate", "EmailAddress_Alternate", "Company Division Employee Email Address Alternate", DataType.String, 50, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("IsSubmitted", "IsSubmitted", "Company Division Employee  Is Submitted", DataType.Bool, 1, EditorType.CheckBox));
            this.AvailableColumns.Add(new DisplayColumn("Alias_NamePrefixCode", "Alias_NamePrefixDescription", "Company Division Employee Alias Name Prefix", new[] { new DatabaseColumn("Alias_NamePrefixCode", DataType.String, 5) }, EditorType.DropDownList, "NamePrefixes"));
            this.AvailableColumns.Add(new DisplayColumn("Alias_FirstName", "Alias_FirstName", "Company Division Employee Alias First Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("Alias_MiddleName", "Alias_MiddleName", "Company Division Employee Alias Middle Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("Alias_LastName", "Alias_LastName", "Company Division Employee Alias Last Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("Alias_NameSuffixCode", "Alias_NameSuffixDescription", "Company Division Employee Alias Name Suffix", new[] { new DatabaseColumn("Alias_NameSuffixCode", DataType.String, 5) }, EditorType.DropDownList, "NameSuffixes"));
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
