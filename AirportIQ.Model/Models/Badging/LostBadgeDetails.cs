using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.Models.Badging
{
    public class LostBadgeDetails
    {
        public int BadgeID { get; set; }
        public int PersonID { get; set; }
        public string BadgeNumber { get; set; }
        public string SSN { get; set; }
        public string BadgeIssueDate { get; set; }
        public string EmployeeName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }

        public string Company { get; set; }
        public string Division { get; set; }
        public string ReportedBy { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }

        public string ReportedDate { get; set; }
        public string DateOfOccurance { get; set; }
        public string HowReported { get; set; }
        public string CustomsSeal { get; set; }
        public string Type { get; set; }
        public string Details { get; set; }
    }
}
