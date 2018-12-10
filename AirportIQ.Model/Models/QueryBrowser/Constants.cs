using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.Models.QueryBrowser
{
    public class LinkTypes
    {
        public const string Any = "Any";
        public const string All = "All";
        public const string None = "None";
    }

    public class SortOrder
    {
        public const string Ascending = "Ascending";
        public const string Descending = "Descending";
    }

    public class QueryTypes
    {
        public const string Public = "Public";
        public const string Private = "Private";
        public const string System = "System";
    }

    public class QueryKeys
    {
        public const string Audit = "Audit";
        public const string RACU = "RACU";
        public const string SAFE = "SAFE";
        public const string GuardPost = "GuardPost";
        public const string CompanyDivisionAgreement = "CompanyDivisionAgreement";
        public const string CompanyDivisionAgreementAll = "CompanyDivisionAgreementAll";
        public const string CompanyDivisionContacts = "CompanyDivisionContacts";
        public const string CompanyDivisionEmployees = "CompanyDivisionEmployees";
        public const string CompanyDivisionInfo = "CompanyDivisionInfo";
        public const string CompanyDivisionPersonBadge = "CompanyDivisionPersonBadge";
        public const string PersonBackgroundCheck = "PersonBackgroundCheck";
        public const string PersonBadge = "PersonBadge";
        public const string BadgeInspections = "BadgeInspections";
    }

    public class DataType
    {
        public const string Guid = "Guid";
        public const string DateTime = "DateTime";
        public const string Int = "Int";
        public const string Float = "Float";
        public const string Bool = "Bool";
        public const string String = "String";
    }

    public class EditorType
    {
        public const string DropDownList = "DropDownList";
        public const string DatePicker = "DatePicker";
        public const string IntegerTextBox = "IntegerTextBox";
        public const string DecimalTextBox = "DecimalTextBox";
        public const string CheckBox = "CheckBox";
        public const string TextBox = "TextBox";
        public const string DependentDropDownList = "DependentDropDownList";

        //AIRS Project Specific Editors
        public const string RegMarkEditor = "RegMarkEditor";
        public const string AircraftType = "AircraftType";
        public const string EngineType = "EngineType";
    }

    public class OperatorType
    {
        public const string Equal = "Equal";
        public const string NotEqual = "NotEqual";
        public const string LessThan = "LessThan";
        public const string LessThanEqual = "LessThanEqual";
        public const string GreaterThan = "GreaterThan";
        public const string GreaterThanEqual = "GreaterThanEqual";
        public const string IsNull = "IsNull";
        public const string IsNotNull = "IsNotNull";
        public const string StartsWith = "StartsWith";
        public const string NotStartsWith = "NotStartWith";
        public const string Contains = "Cotains";
        public const string NotContains = "NotContains";
        public const string EndsWith = "EndsWith";
        public const string NotEndsWith = "NotEndsWith";
    }
}
