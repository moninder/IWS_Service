using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace AirportIQ.Model.Models.Lists
{
	public class LookUpItem
	{

		public LookUpItem(DataRow row)
		{
			this.label = row[1].ToString();
			this.value = row[0].ToString();
		}

		public LookUpItem(string aKey, string aValue)
		{
			this.label = aKey;
			this.value = aValue;
		}

		public LookUpItem(KeyValuePair<string, string> keyValuePair)
		{
			this.label = keyValuePair.Key;
			this.value = keyValuePair.Value;
		}

		public string label { get; set; }
		public string value { get; set; }
	}
}
