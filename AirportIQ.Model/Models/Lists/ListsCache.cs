using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.Models.Lists
{
	public class ListsCache
	{
		// Fields...
		#region Private Members

		private Dictionary<string, LookUpLists> _Facilities = new Dictionary<string, LookUpLists>();

        #endregion


		public ListsCache()
		{
			var persons = new LookUpList();
			var badges = new LookUpList();
			var lax = new LookUpLists("PERSON", persons) { { "BADGES", badges } };

			_Facilities.Add("LAX", lax);

			persons = new LookUpList();
			badges = new LookUpList();
			var ont = new LookUpLists("PERSON", persons) { { "BADGES", badges } };

			_Facilities.Add("ONT", ont);

      var citations = new LookUpList();
      var all = new LookUpLists("Citations", citations);

      _Facilities.Add("ALL", all);
		}


		public Dictionary<string, LookUpLists> Facilities
		{
			get { return _Facilities; }
			set { _Facilities = value; }
		}
	}
}
