using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.Models.QueryBrowser.Queries
{
    public class CompanyDivisionInfoQuery : Query
    {
        private const string QueryName = "Company - Division Information";
        private const string DataSourceName = "[App.Sbo].[QueryBrowser.CompanyDivisionInfo]";
        private const string StoredProcedureName = "[App.Sbo].[QueryBrowser.CompanyDivisionInfoLookupLists]";
        private readonly string[] _lookupSets = new[]
                                                {
                                                    "BadgeColors",
                                                    "DivisionCodes",
                                                    "DivisionNames",
                                                    "DivisionTypeNames",
                                                    "DivisionTypeDescriptions",
                                                    "DivisionStatus",
                                                    "CountrySubdivisions",
                                                    "Countries",
                                                    "DivisionCategories",
                                                    "IndustryTypeDescription",
                                                    "IndustryTypeAbbreviation"
                                                };

        public CompanyDivisionInfoQuery()
        {
            CreateQuery();
        }

        protected virtual void CreateQuery()
        {
            Id = -1;
            Name = QueryName;
            DataSource = DataSourceName;
            QueryKey = QueryKeys.CompanyDivisionInfo;
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
            this.AvailableColumns.Add(new DisplayColumn("WhenLastUpdated", "WhenLastUpdated", "Company When Last Updated", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("LastUpdatedName", "LastUpdatedName", "Company Staff Last Updated", DataType.String, 104, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("RAMSAgreementNumber", "RAMSAgreementNumber", "Company RAMS Agreement Number", DataType.String, 10, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("RAMSExpirationDate", "RAMSExpirationDate", "Company RAMS Expiration Date", DataType.String, 3, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("BadgeColorID_HighestLevel", "BadgeColor", "Company Highest Level Badge Color", new[] { new DatabaseColumn("BadgeColorID_HighestLevel", DataType.String, 2) }, EditorType.DropDownList, "BadgeColors"));
            this.AvailableColumns.Add(new DisplayColumn("ProviderNumber", "ProviderNumber", "Company Provider Number", DataType.String, 10, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("InsuranceRequired", "InsuranceRequired", "Company Insurance Required", DataType.Bool, 1, EditorType.CheckBox));
            this.AvailableColumns.Add(new DisplayColumn("BTRCRequired", "BTRCRequired", "Company BTRC Required", DataType.Bool, 1, EditorType.CheckBox));
            this.AvailableColumns.Add(new DisplayColumn("FingerprintRequired", "FingerprintRequired", "Company Fingerprint Required", DataType.Bool, 1, EditorType.CheckBox));
            this.AvailableColumns.Add(new DisplayColumn("BackgroundCheckRequired", "BackgroundCheckRequired", "Company Background Check Required", DataType.Bool, 1, EditorType.CheckBox));
            this.AvailableColumns.Add(new DisplayColumn("DivisionCode", "DivisionCode", "Company Division Code", new[] { new DatabaseColumn("DivisionCode", DataType.String, 2) }, EditorType.DropDownList, "DivisionCodes"));
            this.AvailableColumns.Add(new DisplayColumn("DivisionName", "DivisionName", "Company Division Name", new[] { new DatabaseColumn("DivisionID", DataType.Int, 0) }, EditorType.DropDownList, "DivisionNames"));
            this.AvailableColumns.Add(new DisplayColumn("DivisionTypeName", "DivisionTypeName", "Company Division Type Name", new[] { new DatabaseColumn("DivisionTypeID", DataType.Int, 0) }, EditorType.DropDownList, "DivisionTypeNames"));
            this.AvailableColumns.Add(new DisplayColumn("DivisionTypeDescription", "DivisionTypeDescription", "Company Division Type Description", new[] { new DatabaseColumn("DivisionTypeID", DataType.Int, 0) }, EditorType.DropDownList, "DivisionTypeDescriptions"));
            this.AvailableColumns.Add(new DisplayColumn("DivisionStatusCode", "DivisionStatusDescription", "Company Division Status", new[] { new DatabaseColumn("DivisionStatusCode", DataType.String, 10) }, EditorType.DropDownList, "DivisionStatus"));
            this.AvailableColumns.Add(new DisplayColumn("Address1", "Address1", "Company Division Address1", DataType.String, 50, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("Address2", "Address2", "Company Division Address2", DataType.String, 50, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("Address3", "Address3", "Company Division Address3", DataType.String, 50, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("CountrySubdivisionCode", "CountrySubdivisionName", "Company Division Country Subdivision", new[] { new DatabaseColumn("CountrySubdivisionCode", DataType.String, 5) }, EditorType.DependentDropDownList, "CountrySubdivisions"));
            this.AvailableColumns.Add(new DisplayColumn("PostalCode", "PostalCode", "Company Division Postal Code", DataType.String, 10, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("CountryCode", "CountryDescription", "Company Division Country", new[] { new DatabaseColumn("CountryCode", DataType.String, 5) }, EditorType.DropDownList, "Countries"));
            this.AvailableColumns.Add(new DisplayColumn("CountrySubdivisionTypeName", "CountrySubdivisionTypeName", "Company Division Country Subdivision Type Name", DataType.String, 15, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PhoneNumber", "PhoneNumber", "Company Division Phone Number", DataType.String, 14, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("FaxNumber", "FaxNumber", "Company Division Fax Number", DataType.String, 14, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("EmailAddress", "EmailAddress", "Company Division Email Address", DataType.String, 40, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("WhenEnrolled", "WhenEnrolled", "Company Division When Enrolled", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("WhenLastUpdated2", "WhenLastUpdated2", "Company Division When Last Updated", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("LastUpdatedName2", "LastUpdatedName2", "Company Division Staf Last Updated", DataType.String, 104, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("ProviderNumber2", "ProviderNumber2", "Company Division Provider Number", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("NewBadgeCost", "NewBadgeCost", "Company Division New Badge Cost", DataType.String, 5, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("ReplaceBadgeCost", "ReplaceBadgeCost", "Company Division Replace Badge Cost", DataType.String, 5, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("BadgeBillingCost", "BadgeBillingCost", "Company Division Badge Billing Cost", DataType.String, 5, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("BadgeCreditCost", "BadgeCreditCost", "Company Division Badge Credit Cost", DataType.String, 5, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("FingerPrintRequired2", "FingerPrintRequired2", "Company Division Finger Print Required", DataType.Bool, 1, EditorType.CheckBox));
            this.AvailableColumns.Add(new DisplayColumn("BackgroundCheckRequired2", "BackgroundCheckRequired2", "Company Division Background Check Required", DataType.Bool, 1, EditorType.CheckBox));
            this.AvailableColumns.Add(new DisplayColumn("CategoryCode", "CategoryDescription", "Company Division Category", new[] { new DatabaseColumn("CategoryCode", DataType.String, 5) }, EditorType.DropDownList, "DivisionCategories"));
            this.AvailableColumns.Add(new DisplayColumn("IndustryTypeDescription", "IndustryTypeDescription", "Company Division Industry Type Description", new[] { new DatabaseColumn("IndustryTypeID", DataType.String, 5) }, EditorType.DropDownList, "IndustryTypeDescription"));
            this.AvailableColumns.Add(new DisplayColumn("IndustryTypeAbbreviation", "IndustryTypeAbbreviation", "Company Division Industry Type Abbreviation", new[] { new DatabaseColumn("IndustryTypeID", DataType.String, 5) }, EditorType.DropDownList, "IndustryTypeAbbreviation"));
            this.AvailableColumns.Add(new DisplayColumn("ActiveBadges", "ActiveBadges", "Company Division Active Badge Count", DataType.Int, 10, EditorType.IntegerTextBox));
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
