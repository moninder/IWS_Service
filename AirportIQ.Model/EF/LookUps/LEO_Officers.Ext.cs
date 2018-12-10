using System;
using System.Collections.Generic;
using System.Linq;

namespace AirportIQ.Model.EF.LookUps
{
	public partial class LEO_Officers
	{
		public static List<LEO_Officers> GetLEO_Officers()
		{
			List<LEO_Officers> result;
			using (LookupEntities context = new LookupEntities())
			{
				var officers = from officer in context.LEO_Officers
											 select officer;

				result = officers.ToList();
			}

			return result;
		}

		public static List<LEO_Officers> GetActiveLEO_Officers()
		{
			List<LEO_Officers> result;
			using (LookupEntities context = new LookupEntities())
			{
				var officers = from officer in context.LEO_Officers
											 where officer.BadgeStatusCode == "ACTV"
											 select officer;

				result = officers.ToList();
			}

			return result;
		}

		public static LEO_Officers GetLEO_Officer(short officerBadgeID)
		{
			LEO_Officers result = null;
			using (LookupEntities context = new LookupEntities())
			{
				var officers = from officer in context.LEO_Officers
											 where officer.OfficerBadgeID == officerBadgeID
											 select officer;

				result = officers.FirstOrDefault();
			}

			return result;
		}

		public static LEO_Officers GetLEO_Officer(int personID)
		{
			LEO_Officers result = null;
			using (LookupEntities context = new LookupEntities())
			{
				var officers = from officer in context.LEO_Officers
											 where officer.PersonID == personID
											 select officer;

				result = officers.FirstOrDefault();
			}

			return result;
		}
	}
}


