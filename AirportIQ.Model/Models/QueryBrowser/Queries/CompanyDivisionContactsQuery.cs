using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.Models.QueryBrowser.Queries
{
    public class CompanyDivisionContactsQuery : Query
    {
        private const string QueryName = "Company - Division Contacts";
        private const string DataSourceName = "[App.Sbo].[QueryBrowser.CompanyDivisionContacts]";
        private const string StoredProcedureName = "[App.Sbo].[QueryBrowser.CompanyDivisionContactLookupLists]";
        private readonly string[] _lookupSets = new[]
                                                 {
                                                    "DivisionCodes",
                                                    "DivisionNames",
                                                    "DivisionTypeNames",
                                                    "DivisionTypeDescriptions",
                                                    "NamePrefixes",
                                                    "NameSuffixes",
                                                    "CountrySubdivisions",
                                                    "Countries"
                                                 };

        public CompanyDivisionContactsQuery()
        {
            CreateQuery();
        }

        protected virtual void CreateQuery()
        {
            Id = -1;
            Name = QueryName;
            DataSource = DataSourceName;
            QueryKey = QueryKeys.CompanyDivisionContacts;
            QueryType = QueryTypes.System;
            FilterByFacilityCode = true;
            LoadColumns();
        }

        protected virtual void LoadColumns()
        {
            this.AvailableColumns = new List<DisplayColumn>();
            this.AvailableColumns.Add(new DisplayColumn("FacilityCode", "FacilityCode", "Facility Code", DataType.String, 5, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("FacilityName", "FacilityName", "Facility Name", DataType.String, 50, EditorType.TextBox));
            //this.AvailableColumns.Add(new DisplayColumn("CompanyID", "CompanyID", "CompanyID", DataType.Int, 4, EditorType.IntegerTextBox));
            this.AvailableColumns.Add(new DisplayColumn("CompanyCode", "CompanyCode", "Company Code", DataType.String, 4, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("CorporationName", "CorporationName", "Company Corporation Name", DataType.String, 60, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("DBAName", "DBAName", "Company DBA Name", DataType.String, 50, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("BadgeName", "BadgeName", "Company Badge Name", DataType.String, 30, EditorType.TextBox));
            //this.AvailableColumns.Add(new DisplayColumn("DivisionID", "DivisionID", "DivisionID", DataType.Int, 4, EditorType.IntegerTextBox));
            this.AvailableColumns.Add(new DisplayColumn("DivisionCode", "DivisionCode", "Company Division Code", new[] { new DatabaseColumn("DivisionCode", DataType.String, 2) }, EditorType.DropDownList, "DivisionCodes"));
            this.AvailableColumns.Add(new DisplayColumn("DivisionName", "DivisionName", "Company Division Name", new[] { new DatabaseColumn("DivisionID", DataType.Int, 0) }, EditorType.DropDownList, "DivisionNames"));
            this.AvailableColumns.Add(new DisplayColumn("DivisionTypeName", "DivisionTypeName", "Company Division Type Name", new[] { new DatabaseColumn("DivisionTypeID", DataType.Int, 0) }, EditorType.DropDownList, "DivisionTypeNames"));
            this.AvailableColumns.Add(new DisplayColumn("DivisionTypeDescription", "DivisionTypeDescription", "Company Division Type Description", new[] { new DatabaseColumn("DivisionTypeID", DataType.Int, 0) }, EditorType.DropDownList, "DivisionTypeDescriptions"));
            this.AvailableColumns.Add(new DisplayColumn("NamePrefixDescription", "NamePrefixDescription", "Company Contact Name Prefix", new[] { new DatabaseColumn("NamePrefixCode", DataType.String, 5) }, EditorType.DropDownList, "NamePrefixes"));
            this.AvailableColumns.Add(new DisplayColumn("FirstName", "First Name", "First Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("MiddleName", "Middle Name", "Middle Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("LastName", "Last Name", "Last Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("NameSuffixDescription", "NameSuffixDescription", "Company Contact Name Suffix", new[] { new DatabaseColumn("NameSuffixCode", DataType.String, 5) }, EditorType.DropDownList, "NameSuffixes"));
            this.AvailableColumns.Add(new DisplayColumn("Address1", "Address1", "Address1", DataType.String, 50, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("Address2", "Address2", "Address2", DataType.String, 50, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("City", "City", "City", DataType.String, 25, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("CountrySubdivisionCode", "CountrySubdivisionName", "Company Contact Country Subdivision", new[] { new DatabaseColumn("CountrySubdivisionCode", DataType.String, 5) }, EditorType.DependentDropDownList, "CountrySubdivisions"));
            this.AvailableColumns.Add(new DisplayColumn("PostalCode", "Postal Code", "PostalCode", DataType.String, 10, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("CountryCode", "CountryDescription", "Company Contact Country", new[] { new DatabaseColumn("CountryCode", DataType.String, 5) }, EditorType.DropDownList, "Countries"));
            this.AvailableColumns.Add(new DisplayColumn("CountrySubdivisionTypeName", "CountrySubdivisionTypeName", "Company Contact Country Subdivision Type", DataType.String, 15, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("Work1PhonePrefix", "Work1PhonePrefix", "Company Contact Work 1 Phone Prefix", DataType.String, 3, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("Work1PhoneNumber", "Work1PhoneNumber", "Company Contact Work 1 Phone Number", DataType.String, 14, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("Work1PhoneExtension", "Work1PhoneExtension", "Company Contact Work 1 Phone Extension", DataType.String, 4, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("Work2PhonePrefix", "Work2PhonePrefix", "Company Contact Work 2 Phone Prefix", DataType.String, 3, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("Work2PhoneNumber", "Work2PhoneNumber", "Company Contact Work 2 Phone Number", DataType.String, 14, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("Work2PhoneExtension", "Work2PhoneExtension", "Company Contact Work 2 Phone Extension", DataType.String, 5, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("MobilePhonePrefix", "MobilePhonePrefix", "Company Contact Mobile Phone Prefix", DataType.String, 3, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("MobilePhoneNumber", "MobilePhoneNumber", "Company Contact Mobile Phone Number", DataType.String, 14, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("FaxPhonePrefix", "FaxPhonePrefix", "Company Contact Fax Phone Prefix", DataType.String, 3, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("FaxPhoneNumber", "FaxPhoneNumber", "Company Contact Fax Phone Number", DataType.String, 14, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("EmailAddress", "EmailAddress", "Company Contact Email Address", DataType.String, 40, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("AlternateEmailAddress", "AlternateEmailAddress", "Company Contact Alternate Email Address", DataType.String, 40, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("ContactTypeID", "ContactTypeID", "Company Contact Type ID", DataType.Int, 10, EditorType.IntegerTextBox));
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
