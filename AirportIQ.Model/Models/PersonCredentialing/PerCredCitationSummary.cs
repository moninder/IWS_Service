using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AirportIQ.Model.Models.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace AirportIQ.Model.Models.PersonCredentialing
{
    /// <summary>
    /// Helper class for creating the PersonAlias jqgrid for creating JSON code.
    /// </summary>
    public class PerCredCitationSummary : IJsonDatasource
    {
        /// <summary>
        /// Constructor off of values.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="text"></param>
        public PerCredCitationSummary(Int32 safeCitationID, Int32 personID, String citationNumber, String companyCode, String companyName, String division, String divisionCode, String badgeNumber,
                                Int32 violationID, String violationDescription, DateTime violationDate, String violationStatus, Int32 drivingPoints, Int32 EquipmentPoints, 
                                Int32 safetyPoints, Int32 totalPoints, string key)
        {
            SafeCitationID = safeCitationID;
            PersonID = personID;
            CitationNumber = citationNumber;
            CompanyCode = companyCode;
            CompanyName = companyName;
            Division = division;
            DivisionCode = divisionCode;
            BadgeNumber = badgeNumber;
            ViolationID = violationID;
            ViolationDescription = violationDescription;
            ViolationDate = violationDate;
            ViolationStatus = violationStatus;
            DrivingPoints = drivingPoints;
            OperationsPoints = EquipmentPoints;
            SafetyPoints = safetyPoints;
            TotalPoints = totalPoints;
            Key = key;

        }

        public Int32 SafeCitationID { get; set; }
        public Int32 PersonID { get; set; }
        public String CitationNumber  { get; set; }
        public String CompanyCode { get; set; }
        public String CompanyName { get; set; }
        public String Division { get; set; }
        public String DivisionCode { get; set; }
        public String BadgeNumber { get; set; }
        public Int32 ViolationID { get; set; }
        public String ViolationDescription { get; set; }
        public DateTime ViolationDate { get; set; }
        public String ViolationStatus { get; set; }
        public Int32 DrivingPoints { get; set; }
        public Int32 OperationsPoints { get; set; }
        public Int32 SafetyPoints { get; set; }
        public Int32 TotalPoints { get; set; }
        public string Key { get; set; }

        /// <summary>
        /// Implementation of the IJsonDatasource interface
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
