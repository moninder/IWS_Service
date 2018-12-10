using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace AirportIQ.Model.Models.Helpers
{
    /// <summary>
    /// Helper class for creating the PersonAlias jqgrid for creating JSON code.
    /// </summary>
    public class CitationSummary : IJsonDatasource
    {
        /// <summary>
        /// Constructor off of values.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="text"></param>
		public CitationSummary(Int32 safeCitationID, Int32 personID, String citationNumber, String companyCode, String companyName, String division, String divisionCode, String badgeNumber,
								Int32 violationID, String violationNumber, String violationDescription, DateTime violationDate, String violationStatus, Int32 drivingPoints, Int32 operationsPoints,
								Int32 safetyPoints, Int32 totalPoints)
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
			ViolationNumber = violationNumber;
			ViolationDescription = violationDescription;
			ViolationDate = violationDate;
			ViolationStatus = violationStatus;
			DrivingPoints = drivingPoints;
			OperationsPoints = operationsPoints;
			SafetyPoints = safetyPoints;
			TotalPoints = totalPoints;

		}


		// FIX THIS: ela     Person credentialing uses the constructor below - not sure why...
		public CitationSummary(Int32 safeCitationID, Int32 personID, String citationNumber, String companyCode, String companyName, String division, String divisionCode, String badgeNumber,
								Int32 violationID, String violationDescription, DateTime violationDate, String violationStatus, Int32 drivingPoints, Int32 operationsPoints,
								Int32 safetyPoints, Int32 totalPoints)
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
			OperationsPoints = operationsPoints;
			SafetyPoints = safetyPoints;
			TotalPoints = totalPoints;

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
		public String ViolationNumber { get; set; }
		public String ViolationDescription { get; set; }
		public DateTime ViolationDate { get; set; }
        public String ViolationStatus { get; set; }
        public Int32 DrivingPoints { get; set; }
        public Int32 OperationsPoints { get; set; }
        public Int32 SafetyPoints { get; set; }
        public Int32 TotalPoints { get; set; }

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
