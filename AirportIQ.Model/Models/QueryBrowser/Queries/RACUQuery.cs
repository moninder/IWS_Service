using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.Models.QueryBrowser.Queries
{
    public class RACUQuery : Query
    {
        private const string QueryName = "RACU";
        private const string DataSourceName = "[App.Sbo].[QueryBrowser.RACU]";
        private const string StoredProcedureName = "[App.Sbo].[QueryBrowser.RACULookupLists]";
        private readonly string[] _lookupSets = new[]
                                                 {
                                                    "AuditTypes",
                                                    "AuditStatuses"
                                                 };

        public RACUQuery()
        {
            CreateQuery();
        }

        protected virtual void CreateQuery()
        {
            Id = -1;
            Name = QueryName;
            DataSource = DataSourceName;
            QueryKey = QueryKeys.RACU;
            QueryType = QueryTypes.System;
            FilterByFacilityCode = true;
            LoadColumns();
        }


        protected virtual void LoadColumns()
        {
            this.AvailableColumns.Add(new DisplayColumn("GroupName", "GroupName", "Audit Group Name", DataType.String, 30, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("AuditName", "AuditName", "Audit Name", DataType.String, 40, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("AuditTypeCode", "AuditTypeDescription", "Audit Type", new[] { new DatabaseColumn("AuditTypeCode", DataType.String, 5) }, EditorType.DropDownList, "AuditTypes"));
            this.AvailableColumns.Add(new DisplayColumn("AuditStatusCode", "AuditStatusDescription", "Audit Status", new[] { new DatabaseColumn("AuditStatusCode", DataType.String, 5) }, EditorType.DropDownList, "AuditStatuses"));
            this.AvailableColumns.Add(new DisplayColumn("NumberOfBadgesToAudit", "NumberOfBadgesToAudit", "Number of badges to audit", DataType.Int, 4, EditorType.IntegerTextBox));
            this.AvailableColumns.Add(new DisplayColumn("NumberOfDivisionBadges", "NumberOfDivisionBadges", "Number of division badges", DataType.Int, 4, EditorType.IntegerTextBox));
            this.AvailableColumns.Add(new DisplayColumn("DivisionNotes", "DivisionNotes", "Division Notes", DataType.String, -1, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("DivisionIsHotNote", "DivisionIsHotNote", "Division Hot Note", DataType.Bool, 1, EditorType.CheckBox));
            this.AvailableColumns.Add(new DisplayColumn("FollowUp", "FollowUp", "Follow Up", DataType.Bool, 1, EditorType.CheckBox));
            this.AvailableColumns.Add(new DisplayColumn("WhenCompleted", "WhenCompleted", "Audit When Completed", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("StaffResponsibleName", "StaffResponsibleName", "Staff Responsible Name", DataType.String, 200, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("CompanyDivision", "CompanyDivision", "Company Division Corporate", DataType.String, 133, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("DBANameDivision", "DBANameDivision", "Company Division DBA", DataType.String, 123, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("BadgeHolderName", "BadgeHolderName", "Company Division Badge Holder Name", DataType.String, 200, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("BadgeNumber", "BadgeNumber", "Company Division Badge Number", DataType.String, 15, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("SidaTrainingProof", "SidaTrainingProof", "SIDA Training", DataType.Bool, 1, EditorType.CheckBox));
            this.AvailableColumns.Add(new DisplayColumn("EmploymentProof", "AAB_EmploymentProof", "Employment Proof", DataType.Int, 5, EditorType.DropDownList));
            this.AvailableColumns.Add(new DisplayColumn("FingerprintProof", "FingerprintProof", "Fingerprinted", DataType.Bool, 1, EditorType.CheckBox));
            this.AvailableColumns.Add(new DisplayColumn("BadgeHasDriverIcon", "BadgeHasDriverIcon", "Driver Icon", DataType.Bool, 1, EditorType.CheckBox));
            this.AvailableColumns.Add(new DisplayColumn("DriverLogProof", "DriverLogProof", "Driver Log", DataType.Bool, 1, EditorType.CheckBox));
            this.AvailableColumns.Add(new DisplayColumn("BadgeNotes", "BadgeNotes", "Badge Notes", DataType.String, -1, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("BadgeIsHotNote", "BadgeIsHotNote", "Badge Is Hot Note", DataType.Bool, 1, EditorType.CheckBox));
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
