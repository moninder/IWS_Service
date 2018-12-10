using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.Models.Review
{
	public class BadgeIcon
	{

		public BadgeIcon(DataRow row)
		{
			IconID = (Int16) row["IconID"];
			IconDescription = (string)row["IconDescription"];
			IconAbbreviation = (string)row["IconAbbreviation"];
			WhenBecomesActive = (DateTime)row["WhenBecomesActive"];
			WhenExpires = (DateTime) row["WhenExpires"];
		}

		public Int16 IconID { get; set; }
		public string IconDescription { get; set; }
		public string IconAbbreviation { get; set; }
		public DateTime WhenBecomesActive { get; set; }
		public DateTime WhenExpires { get; set; }
	}
}
