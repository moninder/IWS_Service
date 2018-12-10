using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AirportIQ.Model.Models.Helpers;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AirportIQ.Model.Models.SAFE
{
    /// <summary>
    /// Model class representation of the citation entry screen
    /// </summary>
    [Serializable]
    public class CitationEntry : IJsonDatasource
    {
        #region "internal classes"

        [Serializable]
        public class Action : IJsonDatasourceChild
        {
            public Int32 Key { get; set; }
            public String _Action { get; set; }

            public Int32 SafeHearingID { get; set; }
            public String HearingNotes { get; set; }

            public string ToJson()
            {
                return JsonConvert.SerializeObject(this);
            }
        }

        [Serializable]
        public class Officer : IJsonDatasourceChild
        {
            public Int32 Key { get; set; }
            public String _Action { get; set; }

            public String SerialNumber { get; set; }
            protected String _officerName;
            public String OfficerName
            {
                get { return String.Format("{0}, {1} {2}", LastName, FirstName, MiddleName); }
                set { _officerName = value; }
            }
            public String FirstName { get; set; }
            public String MiddleName { get; set; }
            public String LastName { get; set; }

            public string ToJson()
            {
                return JsonConvert.SerializeObject(this);
            }
        }


        [Serializable]
        public class Violation : IJsonDatasourceChild
        {
            public Violation()
            {
            }

            public Violation(int key, int violationID, string violationNumber, string description, int totalPoints)
            {
                Key = key;
                ViolationID = violationID;
				ViolationNumber = violationNumber;
                Description = description;
                TotalPoints = totalPoints;
            }

            public Int32 Key { get; set; }
            public String _Action { get; set; }

            public Int32 ViolationID { get; set; }
			public String ViolationNumber { get; set; }
			public String Description { get; set; }
			public Int32 TotalPoints { get; set; }



            public string ToJson()
            {
                return JsonConvert.SerializeObject(this);
            }
        }


#endregion

        #region Properties

        //internal
        public Int32 Key { get; set; }

        //Citation INformation
        public String       CitationNumber      { get; set; }
        public DateTime     CitationDate        { get; set; }

        //violation
        public List<Violation> Violations { get; set; }
        //Location
		public String Location { get; set; }
		public String Conditions { get; set; }

        //person information
        public int BadgeID { get; set; }
        protected String _person = "";
        public String Person {
            get { return String.Format("{0}, {1} {2}", LastName, FirstName, MiddleName);}
            set { _person = value; }
        }
        public String FirstName { get; set; }
        public String MiddleName { get; set; }
        public String LastName { get; set; }
        public bool BadgeDisplayed { get; set; }
        public bool DriverIcon { get; set; }
        //public bool LicensePresented { get; set; } -- not in db
        public bool DriverLicenseisValid { get; set; }

        //license information
        public String PlateNumber { get; set; }
        protected String _stateCode = "";
        public String State
        {
            get { return String.Format("{0} - {1}", PlateCountryCode, PlateSubdivisionCode); }
            set { _stateCode = value; } 
        } //state DDLs are keyed by a code
        public string PlateCountryCode { get; set; }
        public string PlateSubdivisionCode { get; set; }
        public bool MovingViolation { get; set; } //ddl key
        public String SerialNumber { get; set; }
        public Int32 VehicleModel { get; set; } //ddl key
        public Int32 VehicleBody { get; set; } //ddl key

        //Company info
        public String CompanyCode { get; set; }
        public String Company { get; set; }
        public String DivisionCode { get; set; }
        public String Division { get; set; }
        protected String _companyAddress = "";
        public String CompanyAddress 
        {
            get
            {
                StringBuilder ret = new StringBuilder("");
                ret.AppendLine(Address1);
                if (!String.IsNullOrEmpty(Address2)) ret.AppendLine(Address2);
                if (!String.IsNullOrEmpty(Address3)) ret.AppendLine(Address3);
                //ret.Append("<br />");//line break
                ret.AppendFormat("{0}, {1} {2}", City, CountrySubdivisionCode, PostalCode);

                return ret.ToString();
            }
            set{_companyAddress = value;}
        }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string CountrySubdivisionCode  { get; set; }
        public string PostalCode { get; set; }
        public List<KeyValuePair<string, string>> SAFEContacts { get; set; }
        public String CompanyPhones { get; set; }
        protected String _companyEmails = "";
        public String CompanyEmails 
        {
            get
            {
                StringBuilder ret = new StringBuilder("");

                ret.AppendLine(EmailPrimary);
                if (!String.IsNullOrEmpty(EmailSecondary))
                {
                    //ret.Append("<br />");
                    ret.Append(EmailSecondary);
                }

                return ret.ToString();
            }
            set { _companyEmails = value; }
        }
        public String EmailPrimary { get; set; }
        public String EmailSecondary { get; set; }
        public String ContactType { get; set; }

        //officer
        public List<Officer> Officers { get; set; }

        //internal 
        public DateTime DateDeactivated { get; set; }
        public List<Action> Actions { get; set; }
        


        //public String Permit { get; set; }
        //public Int32 VehicleManufacturer { get; set; } //ddl key
        //public Int32 VehicleYear { get; set; }
        //public Int32 SpeedZone { get; set; }
        //public Int32 ApproximateSpeed { get; set; }
        //public bool BadgeDisplayed { get; set; }
        //public bool DriverIcon { get; set; }
        //public bool LicensePresented { get; set; }
        //public String DriverLicenseNumber { get; set; }
        //public String DriverLicenseState { get; set; }  //state DDLs are keyed by a code
        //public Int32 ActionNeeded {get; set;}
        //public DateTime DateAirFieldOpsNotifed { get; set; }
        //public DateTime DateBadgingNotified { get; set; }
        
        //drop down listings
        public List<KeyValuePair<string, string>> States { get; set; }
        //public List<KeyValuePair<string, string>> Manufacturers { get; set; }
        public List<KeyValuePair<string, string>> Models { get; set; }
        public List<KeyValuePair<string, string>> BodyTypes { get; set; }
        //public List<KeyValuePair<string, string>> ViolationTypes { get; set; }
        //public List<KeyValuePair<string, string>> DrivingViolations { get; set; }
        
        #endregion


        public string ToJson()
        {
            IsoDateTimeConverter idtc = new IsoDateTimeConverter();
            idtc.DateTimeFormat = "MM/dd/yyyy";

            return JsonConvert.SerializeObject(this, idtc);
        }
    }
}
