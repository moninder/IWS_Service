using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.Models.Review.Badge
{
	public class Location
	{
		public Location(DataRow dr)
		{
			LocationID = (Int16) dr["LocationID"];
			LocationName = (string)dr["LocationName"];
		}

		public Int16 LocationID { get; set; }
		public string LocationName { get; set; }
	}
}
