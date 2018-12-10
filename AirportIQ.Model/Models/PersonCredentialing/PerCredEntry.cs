using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AirportIQ.Model.Models.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using AirportIQ.Model.Models.Review;

namespace AirportIQ.Model.Models.PersonCredentialing
{
    /// <summary>
    /// Model class representation of the citation entry screen
    /// </summary>
    [Serializable]
    public class PerCredEntry : IJsonDatasource
    {

        #region Properties for TAB 1:::Person Biographic

            // Alias Grid--------------
            public List<PersonAlias> PersonAliases { get; set;}

            // keys
            public Int32 PerCred_PersonID { get; set; }
            // selected
            public String PerCred_PersonSSN { get; set; }
            public String PerCred_PersonFullName { get; set; }
            // address
            public String PerCred_Address1 { get; set; }
            public String PerCred_City { get; set; }
            public String PerCred_PostalCode { get; set; }
            // phone & email
            public String PerCred_BusinessPhone { get; set; }
            public String PerCred_HomePhone { get; set; }
            public String PerCred_Email { get; set; }
            public String PerCred_AlternateEmail { get; set; }
            // dob & characteristic information
            public DateTime PerCred_DateOfBirth { get; set; }
            public String PlaceOfBirth { get; set; }
            public String PerCred_HeightInFeet { get; set; }
            public String PerCred_HeightInches { get; set; }
            public String PerCred_WeightInPounds { get; set; }
            // id information 1
            public String GovID1Number { get; set; }
			public DateTime? GovID1ExpirationDate { get; set; }
  
            // id information 2
            public String GovID2Number { get; set; }
			public DateTime? GovID2ExpirationDate { get; set; }

            #region dropdown lists & selected values======================================
            // ===
            public List<KeyValuePair<string, string>> DocumentTypes1 { get; set; }
            public String DocumentTypes1Current { get; set; }
            public List<KeyValuePair<string, string>> IssuingCountry1 { get; set; }
            public String IssuingAuthority1_CountryCode_Current { get; set; }
            // ===
            public List<KeyValuePair<string, string>> DocumentTypes2 { get; set; }
            public String DocumentTypes2Current { get; set; }
            public List<KeyValuePair<string, string>> IssuingCountry2 { get; set; }
            public String IssuingAuthority2_CountryCode_Current { get; set; }
            // ===
            public List<KeyValuePair<string, string>> Gender { get; set; }
            public String PerCred_GenderCode { get; set; }
            // ===
            public List<KeyValuePair<string, string>> CountryOfCitizenship { get; set; }
            public String CountryOfCitizenshipCurrent { get; set; }
            // ===
            public List<KeyValuePair<string, string>> Country { get; set; }
            public String PerCred_CountryCode { get; set; }
            // ===
            public List<KeyValuePair<string, string>> State { get; set; }
            public String PerCred_CountrySubdivisionCode { get; set; }
            // ===
            public List<KeyValuePair<string, string>> Ethnicity { get; set; }
            public String PerCred_RaceCode { get; set; }
            // ===
            public List<KeyValuePair<string, string>> HairColor { get; set; }
            public String PerCred_HairColorCode { get; set; }
            // ===
            public List<KeyValuePair<string, string>> EyeColor { get; set; }
            public String PerCred_EyeColorCode { get; set; }

#endregion end dropdown lists & selected values======================================

        #endregion

        #region Properties for TAB 2:::Badge

            public List<PersonBadgeGrid> PersonBadgeGrid { get; set; }
            public String PersonBadgeGridCurrent { get; set; }
            public Int32? BadgeDetailID { get; set; }
            public String BadgeNumberDetail { get; set; }
            public String BadgeColorDetail { get; set; }
            public DateTime? BadgeWhenBecomesActiveDetail { get; set; }
            public DateTime? BadgeWhenExpiresDetail { get; set; }
            public String BadgeStatusDetail { get; set; }

            public String NamePrefixBadgeDetail { get; set; }
            public String FirstNameBadgeDetail { get; set; }
            public String MiddleNameBadgeDetail { get; set; }
            public String LastNameBadgeDetail { get; set; }
            public String NameSuffixBadgeDetail { get; set; }

            public DateTime? FingerprintDateBadgeDetail { get; set; }
            public DateTime? PrintDateBadgeDetail { get; set; }
            public String PrintUserBadgeDetail { get; set; }

            public Int32? CompanyIDBadgeDetail { get; set; }
            public String CompanyCodeBadgeDetail { get; set; }
            public String CorporationNameBadgeDetail { get; set; }

            public Int32? DivisionIDBadgeDetail { get; set; }
            public String DivisionCodeBadgeDetail { get; set; }
            public String DivisionNameBadgeDetail { get; set; }
            public string DivisionTypeNameBadgeDetail { get; set; }
            public String JobRoleBadgeDetail { get; set; }

			public List<BadgeIcon> CurrentIcons { get; set; }
			public List<BadgeStatusPeriod> StatusHistory { get; set; }

        #endregion

        #region Properties for TAB 3:::SAFE
            // Safe Point Count
			public List<PersonPointsPeriod> PersonPointsPeriods { get; set; }
 
            public String Driving { get; set; }
            public String Equipment { get; set; }
            public String Safety { get; set; }
            public String Security { get; set; }
            public String Airfield { get; set; }
            public String Unknown { get; set; }
            public String TotalPoints { get; set; }

            // Citation Summary (table) grids
            public List<PerCredCitationSummary> CitationSummaries { get; set; }
            public List<HearingSummary> HearingSummaries { get; set; }

            // Citation Code
            public String CitationNumber { get; set; } 
            public String CitationDate { get; set; }
            public Int32? PointValue { get; set; }
            public List<KeyValuePair<string, string>> BadgeDisplayed { get; set; }
            public String BadgeDisplayedCurrent { get; set; }
            public Boolean LicenseValid { get; set; }   // checkbox
            public Boolean DriverIcon { get; set; }     // checkbox
            public List<KeyValuePair<string, string>> Group { get; set; }           // ddl
            public String GroupCurrent { get; set; }              // ddl current value
            public List<KeyValuePair<string, string>> Violation { get; set; }       // ddl
            public Int32? ViolationCurrent { get; set; }          // ddl current value
            //public List<KeyValuePair<string, string>> Location { get; set; }        // ddl
			public String LocationCurrent { get; set; }
			public String ConditionsCurrent { get; set; }
			public List<KeyValuePair<string, string>> BadgeNumber { get; set; }     // ddl
            public String BadgeNumberCurrent { get; set; }        // ddl current value
            // public List<KeyValuePair<string, string>> Company { get; set; }         // ddl   per Michael, make a text box instead because of slow load times
            public String CompanyCurrent { get; set; }            
            public List<KeyValuePair<string, string>> Division { get; set; }        // ddl
            public String DivisionCurrent { get; set; }           // ddl current value
            public String CitingOfficerName { get; set; }
            public String CitingOfficerBadgeNumber { get; set; }

            // Vehicle Manufactured
            public String VehicleManufacturer { get; set; }
            public String LicenseNumber { get; set; }
            public String VehicleModel { get; set; }        // ddl current value
            public List<KeyValuePair<string, string>> LicenseCountry { get; set; }   // ddl
            public String LicenseCountryCurrent { get; set; }      // ddl current value
            public List<KeyValuePair<string, string>> LicenseState { get; set; }     // ddl
            public String LicenseStateCurrent { get; set; }        // ddl current value

            // administration
            public DateTime? DateReceived { get; set; }
            public DateTime? DateCreated { get; set; }
            public DateTime?  ReturnToSAFEBy { get; set; }
            public DateTime? DateResolved { get; set; }
            public Boolean IsCancelled { get; set; }        // checkbox
            public Boolean IsDeleted { get; set; }          // checkbox
            public Boolean ScheduleHearing { get; set; }    // checkbox
            public List<KeyValuePair<string, string>> Watch { get; set; }            // ddl
            public String WatchCurrent { get; set; }               // ddl current value
            public List<KeyValuePair<string, string>> CitingOfficer { get; set; }    // ddl
            public String CitingOfficerCurrent { get; set; }       // ddl current value

        #endregion

        #region Properties for TAB 5:::Notes
                public List<PersonNote> PersonNotes { get; set; }
                public String PersonNotesCurrent { get; set; }
        #endregion

		#region Properties for Files Tab
				public List<PersonFile> PersonFiles { get; set; }
		#endregion



        public string ToJson()
        {
            //return new JavaScriptSerializer().Serialize(this);
            IsoDateTimeConverter idtc = new IsoDateTimeConverter();
            idtc.DateTimeFormat = "MM/dd/yyyy";
            return JsonConvert.SerializeObject(this,idtc);
        }
    }
}
