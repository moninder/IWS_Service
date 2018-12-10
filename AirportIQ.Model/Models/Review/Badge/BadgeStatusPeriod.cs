using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace AirportIQ.Model.Models.Review
{
	public class BadgeStatusPeriod
	{
		public BadgeStatusPeriod(){}

		public BadgeStatusPeriod(DataRow row)
		{
			BadgeStatusCode = (string) row["BadgeStatusCode"];
			BadgeStatus = (string) row["BadgeStatus"];
			WhenBecomesActive = (DateTime) row["WhenBecomesActive"];
			WhenExpires = (DateTime)row["WhenExpires"];
		}

		public string BadgeStatusCode { get; set; }
		public string BadgeStatus { get; set; }
		public DateTime WhenBecomesActive { get; set; }
		public DateTime WhenExpires { get; set; }
	}
}
