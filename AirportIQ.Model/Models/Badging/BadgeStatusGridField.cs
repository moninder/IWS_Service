using System;
using System.Collections.Generic;
using System.Linq;


namespace AirportIQ.Model.Models.Badging
{
	public class BadgeStatusGridField
	{
		public readonly string name = "BadgeStatusCode";
		public readonly string index = "BadgeStatusCode";
		public readonly bool editable = true;
		public readonly string edittype = "select";
		public readonly string formatter = "select";
		public readonly Dictionary<string, object> editoptions = new Dictionary<string, object>();

		public BadgeStatusGridField(Dictionary<string, string> values)
		{
			editoptions.Add("value", values.ToArray());
		}
	}
}
