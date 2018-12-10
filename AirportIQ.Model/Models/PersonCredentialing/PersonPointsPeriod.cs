using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AirportIQ.Model.Models.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AirportIQ.Model.Models.PersonCredentialing
{
	public class PersonPointsPeriod  : IJsonDatasource
	{
        public PersonPointsPeriod(Int32 personID, Int32 period, Int32 drivingPoints, Int32 equipmentPoints,  Int32 safetyPoints, Int32 securityPoints, Int32 airfieldPoints, Int32 unknownPoints, Int32 totalPoints)
        {
            PersonID = personID;
			Period = period;
			DrivingPoints = drivingPoints;
            EquipmentPoints = equipmentPoints;
			SafetyPoints = safetyPoints;
            SecurityPoints = securityPoints;
            AirfieldPoints = airfieldPoints;
            UnknownPoints = unknownPoints;
			TotalPoints = totalPoints;
        }

        public Int32 PersonID { get; set; }
        public Int32 Period { get; set; }
        public Int32 DrivingPoints { get; set; }
        public Int32 EquipmentPoints { get; set; }
        public Int32 SafetyPoints { get; set; }
        public Int32 SecurityPoints { get; set; }
        public Int32 AirfieldPoints { get; set; }
        public Int32 UnknownPoints { get; set; }
        public Int32 TotalPoints { get; set; }
    
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
        public string MapBool(Boolean b)
        {
            if (b) { return "Yes"; } else { return "No"; }
        }
    }

}
