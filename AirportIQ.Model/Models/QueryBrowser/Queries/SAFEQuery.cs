using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.Models.QueryBrowser.Queries
{
    public class SAFEQuery : Query
    {
        private const string QueryName = "SAFE";
        private const string DataSourceName = "[App.Sbo].[QueryBrowser.SAFE]";
        private const string StoredProcedureName = "[App.Sbo].[QueryBrowser.SAFELookupLists]";
        private readonly string[] _lookupSets = new[]
                                                 {
                                                    "VehicleMake",
                                                    "VehicleBody",
                                                    "Divisions",
                                                    "Countries",
                                                    "CountrySubdivisions",
                                                    "ViolationTypeDesc",
                                                    "ViolationTypeAbbr",
                                                    "Officers"
                                                 };

        public SAFEQuery()
        {
            CreateQuery();
        }

        protected virtual void CreateQuery()
        {
            Id = -1;
            Name = QueryName;
            DataSource = DataSourceName;
            QueryKey = QueryKeys.SAFE;
            QueryType = QueryTypes.System;
            FilterByFacilityCode = true;
            LoadColumns();
        }


        protected virtual void LoadColumns()
        {
            //this.AvailableColumns.Add(new DisplayColumn("SafeCitationID", "SafeCitationID", "SafeCitationID", DataType.Int, 10, EditorType.IntegerTextBox));
            this.AvailableColumns.Add(new DisplayColumn("BadgeUnknown", "BadgeUnknown", "SAFE Citation Badge Unknown", DataType.Bool, 1, EditorType.CheckBox));
            this.AvailableColumns.Add(new DisplayColumn("BadgeDisplayed", "BadgeDisplayed", "SAFE Citation Badge Displayed", DataType.Bool, 1, EditorType.CheckBox));
            this.AvailableColumns.Add(new DisplayColumn("DriverIcon", "DriverIcon", "SAFE Citation Driver Icon", DataType.Bool, 1, EditorType.CheckBox));
            this.AvailableColumns.Add(new DisplayColumn("LicenseValid", "LicenseValid", "SAFE Citation License Valid", DataType.Bool, 1, EditorType.CheckBox));
            this.AvailableColumns.Add(new DisplayColumn("DriverTrainingRequired", "DriverTrainingRequired", "SAFE Citation Driver Training Required", DataType.Bool, 1, EditorType.CheckBox));
            this.AvailableColumns.Add(new DisplayColumn("SecurityTrainingRequired", "SecurityTrainingRequired", "SAFE Citation Security Training Required", DataType.Bool, 1, EditorType.CheckBox));
            this.AvailableColumns.Add(new DisplayColumn("IsDeleted", "IsDeleted", "SAFE Citation Is Deleted", DataType.Bool, 1, EditorType.CheckBox));
			this.AvailableColumns.Add(new DisplayColumn("ViolationLocation", "ViolationLocation", "SAFE Citation Violation Location", DataType.String, 0, EditorType.TextBox));
			//this.AvailableColumns.Add(new DisplayColumn("Conditions", "Conditions", "SAFE Citation Violation Conditions", DataType.String, 0, EditorType.TextBox));
			this.AvailableColumns.Add(new DisplayColumn("Cancelled", "Cancelled", "SAFE Citation Cancelled", DataType.String, 0, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("FollowUp", "FollowUp", "SAFE Citation Follow Up", DataType.String, 0, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("WhenCreated", "WhenCreated", "SAFE Citation When Created", DataType.DateTime, 23, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("WhenDue", "WhenDue", "SAFE Citation When Due", DataType.DateTime, 23, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("WhenResolved", "WhenResolved", "SAFE Citation When Resolved", DataType.DateTime, 23, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("CitationNumber", "CitationNumber", "SAFE Citation Citation Number", DataType.String, 0, EditorType.TextBox));
            //this.AvailableColumns.Add(new DisplayColumn("PersonID", "PersonID", "PersonID", DataType.Int, 10, EditorType.IntegerTextBox));
            this.AvailableColumns.Add(new DisplayColumn("PersonName", "PersonName", "SAFE Citation Person Name", DataType.String, 0, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("DivisionID", "DivisionDescription", "SAFE Citation Company Division", new[] { new DatabaseColumn("DivisionID", DataType.Int, 0) }, EditorType.DropDownList, "Divisions"));
            this.AvailableColumns.Add(new DisplayColumn("BadgeNumber", "BadgeNumber", "SAFE Citation Badge Number", DataType.String, 0, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("VehicleMakeID", "VehicleMakeDescription", "SAFE Citation Vehicle Make", new[] { new DatabaseColumn("VehicleMakeID", DataType.Int, 0) }, EditorType.DropDownList, "VehicleMake"));
            this.AvailableColumns.Add(new DisplayColumn("VehicleBodyID", "VehicleBodyDescription", "SAFE Citation Vehicle Body", new[] { new DatabaseColumn("VehicleBodyID", DataType.Int, 0) }, EditorType.DropDownList, "VehicleBody"));
            this.AvailableColumns.Add(new DisplayColumn("LicensePlate", "LicensePlate", "SAFE Citation License Plate", DataType.String, 0, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("LicensePlateCountryCode", "LicensePlateCountryDescription", "SAFE Citation License Plate Country", new[] { new DatabaseColumn("LicensePlateCountryCode", DataType.String, 3) }, EditorType.DropDownList, "Countries"));
            this.AvailableColumns.Add(new DisplayColumn("LicensePlateCountrySubdivisionCode", "LicensePlateCountrySubdivisionName", "SAFE Citation License Plate Country Subdivision", new[] { new DatabaseColumn("LicensePlateCountrySubdivisionCode", DataType.String, 3) }, EditorType.DependentDropDownList, "CountrySubdivisions"));
            this.AvailableColumns.Add(new DisplayColumn("ViolationNumber", "ViolationNumber", "SAFE Citation Violation Number", DataType.String, 0, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("ViolationDescription", "ViolationDescription", "SAFE Citation Violation Description", DataType.String, 0, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("ViolationTypeDescription", "ViolationTypeDescription", "SAFE Citation Violation Type Description", new[] { new DatabaseColumn("ViolationTypeID", DataType.Int, 0) }, EditorType.DropDownList, "ViolationTypeDesc"));
            this.AvailableColumns.Add(new DisplayColumn("ViolationTypeAbbreviation", "ViolationTypeAbbreviation", "SAFE Citation Violation Type Abbreviation", new[] { new DatabaseColumn("ViolationTypeID", DataType.Int, 0) }, EditorType.DropDownList, "ViolationTypeAbbr"));
            this.AvailableColumns.Add(new DisplayColumn("ViolationPoints", "ViolationPoints", "SAFE Citation Violation Points", DataType.Int, 3, EditorType.IntegerTextBox));
            this.AvailableColumns.Add(new DisplayColumn("IsMovingViolation", "IsMovingViolation", "SAFE Citation Is Moving Violation", DataType.Bool, 1, EditorType.CheckBox));
            this.AvailableColumns.Add(new DisplayColumn("ViolationPoints2", "ViolationPoints2", "SAFE Citation Total Citation Points", DataType.Int, 3, EditorType.IntegerTextBox));
            this.AvailableColumns.Add(new DisplayColumn("OfficerBadgeNumber", "OfficerBadgeNumber", "SAFE Citation Officer Badge Number", DataType.String, 0, EditorType.TextBox));
            this.AvailableColumns.Add(new DisplayColumn("SafeOfficerID", "OfficerName", "SAFE Citation Hearing Officer Name", new[] { new DatabaseColumn("SafeOfficerID", DataType.Int, 0) }, EditorType.DropDownList, "Officers"));
            this.AvailableColumns.Add(new DisplayColumn("WhenHearingHeld", "WhenHearingHeld", "SAFE Hearing When Hearing Held", DataType.DateTime, 23, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("WhenAppealToBeHeld", "WhenAppealToBeHeld", "SAFE Hearing When Appeal To Be Held", DataType.DateTime, 23, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("WhenTrainingHeld", "WhenTrainingHeld", "SAFE Hearing When Training Held", DataType.DateTime, 23, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("WhenCompleted", "WhenCompleted", "SAFE Hearing When Completed", DataType.DateTime, 23, EditorType.DatePicker));
            this.AvailableColumns.Add(new DisplayColumn("HearingNotes", "HearingNotes", "SAFE Hearing Hearing Notes", DataType.String, 0, EditorType.TextBox));
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
