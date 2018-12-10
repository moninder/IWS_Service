using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace AirportIQ.Model.Models.Helpers
{
    /// <summary>
    /// Helper class for creating the Hearing jqgrid for creating JSON code.
    /// </summary>
    public class HearingSummary : IJsonDatasource
    {
       
        public HearingSummary(Int32 safeHearingID, Int32 safeCitationID, DateTime safeHearingDate, String safeHearingNotes)
        {
            SafeHearingID = safeHearingID;
            SafeCitationID = safeCitationID;
            SafeHearingDate = safeHearingDate;
            SafeHearingNotes = safeHearingNotes;          
        }
        public Int32 SafeHearingID { get; set; }
        public Int32 SafeCitationID { get; set; }
        public DateTime SafeHearingDate { get; set; }
        public String SafeHearingNotes { get; set; }
       
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
