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
    /// Helper class for creating the Hearing jqgrid for creating JSON code.
    /// </summary>
    public class PersonBadgeGrid : IJsonDatasource
    {
       
        public PersonBadgeGrid(Int32 badgeID, String badgeNumber, Int32 personID, String fullName, Int32 companyID, String companyCode, String corporationName,
                           Int32 divisionID, String divisionCode, String divisionName,String jobRoleDescription,
                           DateTime whenExpires, DateTime whenBecomesActive, Int32 badgeColorID, String badgeColor, String badgeStatus)
        {
            BadgeID = badgeID;
            BadgeNumber = badgeNumber;
            PersonID = personID;
            FullName = fullName;
            CompanyID = companyID;
            CompanyCode = companyCode;
            CorporationName = corporationName;
            DivisionID = divisionID;
            DivisionCode = divisionCode;
            DivisionName = divisionName;
            JobRoleDescription = jobRoleDescription;
            WhenExpires = whenExpires;
            WhenBecomesActive = whenBecomesActive;   // created
            BadgeColorID = badgeColorID;
            BadgeColor = badgeColor;
            BadgeStatus = badgeStatus;
        }

        public Int32 BadgeID { get; set; }
        public string BadgeNumber { get; set; }
        public Int32 PersonID { get; set; }
        public string FullName { get; set; }
        public Int32 CompanyID { get; set; }
        public string CompanyCode { get; set; }
        public string CorporationName { get; set; }
        public string JobRoleDescription {get; set; }
        public Int32 DivisionID { get; set; }
        public string DivisionCode { get; set; }
        public string DivisionName { get; set; }
        public DateTime WhenBecomesActive { get; set; }
        public DateTime WhenExpires { get; set; }
        public Int32 BadgeColorID { get; set; }
        public String BadgeColor { get; set; }
        public String BadgeStatus { get; set; }
    
        /// <summary>
        /// Implementation of the IJsonDatasource interface
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            //return JsonConvert.SerializeObject(this);
            IsoDateTimeConverter idtc = new IsoDateTimeConverter();
            idtc.DateTimeFormat = "MM/dd/yyyy";
            return JsonConvert.SerializeObject(this, idtc);
        }
    }
}
