using System;

namespace AirportIQ.Model.Models
{
    public class LEOResults
    {
        public int OfficerBadgeID { get; set; }
        public int PoliceDepartmentID { get; set; }
        public int OfficerRankAndTitleID { get; set; }
        public string SerialNumber { get; set; }
        public DateTime WhenExpires { get; set; }
    }
}