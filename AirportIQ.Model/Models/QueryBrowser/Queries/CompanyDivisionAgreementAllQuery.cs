using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.Models.QueryBrowser.Queries
{
    public class CompanyDivisionAgreementAllQuery : Query
    {
        private const string QueryName = "Company - Division Agreement - All";
        private const string DataSourceName = "[App.Sbo].[QueryBrowser.CompanyDivisionAgreement_All]";
        private const string StoredProcedureName = "[App.Sbo].[QueryBrowser.CompanyDivisionAgreementLookupLists]";
        private readonly string[] _lookupSets = new[]
                                                 {
                                                    "DivisionCodes",
                                                    "DivisionNames",
                                                    "DivisionTypeNames",
                                                    "DivisionTypeDescriptions",
                                                    "AgreementTypes",
                                                    "DivisionStatusDescriptions"
                                                 };

        public CompanyDivisionAgreementAllQuery()
        {
            CreateQuery();
        }

        protected virtual void CreateQuery()
        {
            Id = -1;
            Name = QueryName;
            DataSource = DataSourceName;
            QueryKey = QueryKeys.CompanyDivisionAgreementAll;
            QueryType = QueryTypes.System;
            FilterByFacilityCode = true;
            LoadColumns();
        }

        protected virtual void LoadColumns()
        {
            this.AvailableColumns.Add(new DisplayColumn("FacilityCode", "FacilityCode", "Company Facility Code", DataType.String, 5, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("FacilityName", "FacilityName", "Company Facility Name", DataType.String, 50, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("CompanyCode", "CompanyCode", "Company Code", DataType.String, 4, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("CorporationName", "CorporationName", "Company Corporation Name", DataType.String, 60, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("DBAName", "DBAName", "Company DBA Name", DataType.String, 50, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("DivisionCode", "DivisionCode", "Company Division Code", new[] { new DatabaseColumn("DivisionCode", DataType.String, 2) }, EditorType.DropDownList, "DivisionCodes"));
            this.AvailableColumns.Add(new DisplayColumn("DivisionName", "DivisionName", "Company Division Name", new[] { new DatabaseColumn("DivisionID", DataType.Int, 0) }, EditorType.DropDownList, "DivisionNames"));
            this.AvailableColumns.Add(new DisplayColumn("DivisionTypeName", "DivisionTypeName", "Company Division Type Name", new[] { new DatabaseColumn("DivisionTypeID", DataType.Int, 0) }, EditorType.DropDownList, "DivisionTypeNames"));
            this.AvailableColumns.Add(new DisplayColumn("DivisionTypeDescription", "DivisionTypeDescription", "Company Division Type Description", new[] { new DatabaseColumn("DivisionTypeID", DataType.Int, 0) }, EditorType.DropDownList, "DivisionTypeDescriptions"));
            this.AvailableColumns.Add(new DisplayColumn("DivisionStatusDescription", "DivisionStatusDescription", "Company Division Status Description", new[] { new DatabaseColumn("DivisionStatusCode", DataType.String, 1) }, EditorType.DropDownList, "DivisionStatusDescriptions"));
            this.AvailableColumns.Add(new DisplayColumn("ContractNumber", "ContractNumber", "Contract Number", DataType.String, 20, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("ContractTypeID", "ContractTypeDescription", "Contract Type Description", new[] { new DatabaseColumn("ContractTypeID", DataType.Int, 0) }, EditorType.DropDownList, "AgreementTypes"));
            this.AvailableColumns.Add(new DisplayColumn("ContractBegins", "ContractBegins", "Contract When Contract Begins", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("ContractEnds", "ContractEnds", "Contract When Contract Ends", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("DirectAuthorityNumber", "DirectAuthorityNumber", "Contract Direct Authority Number", DataType.String, 6, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("RAMSAgreementNumber", "RAMSAgreementNumber", "Contract RAMS Agreement Number", DataType.String, 10, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("RAMSExpirationDate", "RAMSExpirationDate", "Contract RAMS Expiration Date", DataType.DateTime, 10, EditorType.DatePicker));
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
