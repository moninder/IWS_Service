using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.Models.QueryBrowser.Queries
{
    public class GuardPostQuery : Query
    {
        private const string QueryName = "Guard Post";
        private const string DataSourceName = "[App.Sbo].[QueryBrowser.GuardPost]";
        private const string StoredProcedureName = "[App.Sbo].[QueryBrowser.GuardPostLookupLists]";
        private readonly string[] _lookupSets = new[]
                                                 {
                                                    "GuardPostDescription",
                                                    "GuardPostAbbreviation",
                                                    "DenialReasonDescription",
                                                    "DenialReasonAbbreviation"
                                                 };

        public GuardPostQuery()
        {
            CreateQuery();
        }

        protected virtual void CreateQuery()
        {
            Id = -1;
            Name = QueryName;
            DataSource = DataSourceName;
            QueryKey = QueryKeys.GuardPost;
            QueryType = QueryTypes.System;
            FilterByFacilityCode = true;
            LoadColumns();
        }


        protected virtual void LoadColumns()
        {
            this.AvailableColumns.Add(new DisplayColumn("FacilityName", "FacilityName", "Company Facility Name", DataType.String, 50, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("FacilityCode", "FacilityCode", "Company Facility Code", DataType.String, 5, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("GuardPostDescription", "GuardPostDescription", "Guard Post Description", new[] { new DatabaseColumn("GuardPostID", DataType.Int, 0) }, EditorType.DropDownList, "GuardPostDescription"));
            this.AvailableColumns.Add(new DisplayColumn("GuardPostAbbreviation", "GuardPostAbbreviation", "Guard Post Abbreviation", new[] { new DatabaseColumn("GuardPostID", DataType.Int, 0) }, EditorType.DropDownList, "GuardPostAbbreviation"));
            this.AvailableColumns.Add(new DisplayColumn("BadgeNumber", "BadgeNumber", "Guard Post Person Badge Number", DataType.String, 15, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("GuardPostID", "GuardPostID", "Guard Post ID", DataType.Int, 0, EditorType.IntegerTextBox));
            this.AvailableColumns.Add(new DisplayColumn("GuardPostPersonName", "GuardPostPersonName", "Guard Post Person Name", DataType.String, 104, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("GuardPostDivisionName", "GuardPostDivisionName", "Guard Post Company Division", DataType.String, 123, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("Workstation", "Workstation", "Guard Post Person Workstation", DataType.String, 24, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("P_WhenAccessOccurred", "P_WhenAccessOccurred", "Guard Post Person When Access Occurred", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("PINEntered", "PINEntered", "Guard Post Person PIN Entered", DataType.String, 4, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("IsDriver", "IsDriver", "Guard Post Person Is Driver", DataType.Bool, 1, EditorType.CheckBox));
            this.AvailableColumns.Add(new DisplayColumn("Gates", "Gates", "Guard Post Person Gates", DataType.Bool, 1, EditorType.CheckBox));
            this.AvailableColumns.Add(new DisplayColumn("P_AccessResult", "P_AccessResult", "Guard Post Person Access Result", DataType.String, 1, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("VehicleNumber", "VehicleNumber", "Guard Post Vehicle Number", DataType.String, 15, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("V_WhenAccessOccurred", "V_WhenAccessOccurred", "Guard Post Vehicle When Access Occurred", DataType.DateTime, 10, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("V_AccessResult", "V_AccessResult", "Guard Post Vehicle Access Result", DataType.String, 1, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("IsActive", "IsActive", "Guard Post Vehicle Is Active", DataType.Bool, 1, EditorType.CheckBox));
            this.AvailableColumns.Add(new DisplayColumn("DenialReasonDescription", "DenialReasonDescription", "Guard Post Denial Reason Description", new[] { new DatabaseColumn("DenialReasonID", DataType.Int, 0) }, EditorType.DropDownList, "DenialReasonDescription"));
            this.AvailableColumns.Add(new DisplayColumn("DenialReasonAbbreviation", "DenialReasonAbbreviation", "Guard Post Denial Reason Abbreviation", new[] { new DatabaseColumn("DenialReasonID", DataType.Int, 0) }, EditorType.DropDownList, "DenialReasonAbbreviation"));
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
