using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.Models.QueryBrowser.Queries
{
    public class CompanyDivisionAgreementQuery : Query
    {
        private const string QueryName = "Company - Division Agreement";
        private const string DataSourceName = "[App.Sbo].[QueryBrowser.CompanyDivisionAgreement]";
        private const string StoredProcedureName = "[App.Sbo].[QueryBrowser.CompanyDivisionAgreementLookupLists]";
        private readonly string[] _lookupSets = new[]
                                                 {
                                                    "DivisionCodes",
                                                    "DivisionNames",
                                                    "DivisionTypeNames",
                                                    "DivisionTypeDescriptions",
                                                    "AgreementTypes"
                                                 };

        public CompanyDivisionAgreementQuery()
        {
            CreateQuery();
        }

        protected virtual void CreateQuery()
        {
            Id = -1;
            Name = QueryName;
            DataSource = DataSourceName;
            QueryKey = QueryKeys.CompanyDivisionAgreement;
            QueryType = QueryTypes.System;
            FilterByFacilityCode = true;
            LoadColumns();
        }

        protected virtual void LoadColumns()
        {
            //Sub
            this.AvailableColumns.Add(new DisplayColumn("SubCompanyCode", "SubCompanyCode", "Sub Company Code", DataType.String, 4, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("SubCorporationName", "SubCorporationName", "Sub Company Corporation Name", DataType.String, 60, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("SubDBAName", "SubDBAName", "Sub Company DBA Name", DataType.String, 50, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("SubDivisionCode", "SubDivisionCode", "Sub Company Division Code", new[] { new DatabaseColumn("DivisionCode", DataType.String, 2) }, EditorType.DropDownList, "DivisionCodes"));
            this.AvailableColumns.Add(new DisplayColumn("SubDivisionName", "SubDivisionName", "Sub Company Division Name", new[] { new DatabaseColumn("DivisionID", DataType.Int, 0) }, EditorType.DropDownList, "DivisionNames"));
            this.AvailableColumns.Add(new DisplayColumn("SubDivisionTypeName", "SubDivisionTypeName", "Sub Company Division Type Name", new[] { new DatabaseColumn("DivisionTypeID", DataType.Int, 0) }, EditorType.DropDownList, "DivisionTypeNames"));
            this.AvailableColumns.Add(new DisplayColumn("SubDivisionTypeDescription", "SubDivisionTypeDescription", "Sub Company Division Type Description", new[] { new DatabaseColumn("DivisionTypeID", DataType.Int, 0) }, EditorType.DropDownList, "DivisionTypeDescriptions")); ;
            this.AvailableColumns.Add(new DisplayColumn("SubContractNumber", "SubContractNumber", "Sub Contract Number", DataType.String, 20, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("SubContractTypeID", "SubContractTypeDescription", "Sub Contract Type Description", new[] { new DatabaseColumn("SubContractTypeID", DataType.Int, 0) }, EditorType.DropDownList, "AgreementTypes"));
            this.AvailableColumns.Add(new DisplayColumn("SubContractBegins", "SubContractBegins", "Sub Contract When Contract Begins", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("SubContractEnds", "SubContractEnds", "Sub Contract When Contract Ends", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("SubDirectAuthorityNumber", "SubDirectAuthorityNumber", "Sub Contract Direct Authority Number", DataType.String, 6, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("SubRAMSAgreementNumber", "SubRAMSAgreementNumber", "Sub Contract RAMS Agreement Number", DataType.String, 10, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("SubRAMSExpirationDate", "SubRAMSExpirationDate", "Sub Contract RAMS Expiration Date", DataType.DateTime, 10, EditorType.DatePicker));

            //Prime
            this.AvailableColumns.Add(new DisplayColumn("PrimeFacilityName", "PrimeFacilityName", "Prime Facility Name", DataType.String, 50, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PrimeCompanyCode", "PrimeCompanyCode", "Prime Company Code", DataType.String, 4, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PrimeCorporationName", "PrimeCorporationName", "Prime Company Corporation Name", DataType.String, 60, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PrimeDBAName", "PrimeDBAName", "Prime Company DBA Name", DataType.String, 50, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PrimeDivisionCode", "PrimeDivisionCode", "Prime Company Division Code", new[] { new DatabaseColumn("DivisionCode", DataType.String, 2) }, EditorType.DropDownList, "DivisionCodes"));
            this.AvailableColumns.Add(new DisplayColumn("PrimeDivisionName", "PrimeDivisionName", "Prime Company Division Name", new[] { new DatabaseColumn("DivisionID", DataType.Int, 0) }, EditorType.DropDownList, "DivisionNames"));
            this.AvailableColumns.Add(new DisplayColumn("PrimeDivisionTypeName", "PrimeDivisionTypeName", "Prime Company Division Type Name", new[] { new DatabaseColumn("DivisionTypeID", DataType.Int, 0) }, EditorType.DropDownList, "DivisionTypeNames"));
            this.AvailableColumns.Add(new DisplayColumn("PrimeDivisionTypeDescription", "PrimeDivisionTypeDescription", "Prime Company Division Type Description", new[] { new DatabaseColumn("DivisionTypeID", DataType.Int, 0) }, EditorType.DropDownList, "DivisionTypeDescriptions")); ;
            this.AvailableColumns.Add(new DisplayColumn("PrimeContractNumber", "PrimeContractNumber", "Prime Contract Number", DataType.String, 20, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PrimeContractTypeID", "PrimeContractTypeDescription", "Prime Contract Type Description", new[] { new DatabaseColumn("PrimeContractTypeID", DataType.Int, 0) }, EditorType.DropDownList, "AgreementTypes"));
            this.AvailableColumns.Add(new DisplayColumn("PrimeContractBegins", "PrimeContractBegins", "Prime Contract When Contract Begins", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("PrimeContractEnds", "PrimeContractEnds", "Prime Contract When Contract Ends", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("PrimeDirectAuthorityNumber", "PrimeDirectAuthorityNumber", "Prime Contract Direct Authority Number", DataType.String, 6, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PrimeRAMSAgreementNumber", "PrimeRAMSAgreementNumber", "Prime Contract RAMS Agreement Number", DataType.String, 10, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("PrimeRAMSExpirationDate", "PrimeRAMSExpirationDate", "Prime Contract RAMS Expiration Date", DataType.DateTime, 10, EditorType.DatePicker));
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
