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
    public class PersonNote : IJsonDatasource
    {

		public PersonNote(string uniqueKey, int noteID, string staffFullName, DateTime whenBegins, DateTime whenEnds, bool isHotNote, string note, string noteType)
        {
            UniqueKey = uniqueKey;
			NoteID = noteID;
            StaffFullName = staffFullName;
            WhenBegins = whenBegins;
            WhenEnds = whenEnds;
            IsHotNote = MapBool(isHotNote);
            Note = note;
            NoteType = noteType;
        }

		public string UniqueKey { get; set; }
		public Int32 NoteID { get; set; }
		public string NoteType { get; set; }
        public Int32 PersonID { get; set; }
        public string CompanyCode { get; set; }
        public string CorporationName { get; set; }
        public string DivisionCode { get; set; }
        public string DivisionName { get; set; }
        public string Note { get; set; }
        public DateTime WhenBegins { get; set; }
        public DateTime WhenEnds { get; set; }
        public string IsHotNote { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime? NoteDate { get; set; }
        public string StaffFullName { get; set; }
    
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
