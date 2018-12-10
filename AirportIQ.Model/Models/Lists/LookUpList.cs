using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace AirportIQ.Model.Models.Lists
{
	/// <summary>
	/// collection of key value pairs. 
	/// </summary>
	/// <remarks>Example list of Person or Badge items</remarks>
	public class LookUpList
	{
		// Fields...
		#region Private Members
		private List<LookUpItem> _LookupItems = new List<LookUpItem>();
		#endregion

		#region Constructors and Destructors
		public LookUpList()
		{
			
		}

		public LookUpList(DataTable dt)
		{
			foreach (DataRow row in dt.Rows)
			{
				_LookupItems.Add(new LookUpItem(row));
			}
		}

        public LookUpList(DataTable dt, string labelColumnName, string valueColumnName)
        {
            foreach (DataRow row in dt.Rows)
            {
                _LookupItems.Add(new LookUpItem(row[labelColumnName].ToString(), row[valueColumnName].ToString()));
            }
        }

        public LookUpList(Dictionary<string, string> dictionary)
        {
            foreach (var element in dictionary)
                _LookupItems.Add(new LookUpItem(element));
        }
		#endregion


		#region Properties
		public List<LookUpItem> LookupItems
		{
			get { return _LookupItems; }
			set { _LookupItems = value; }
		}
		#endregion

		#region Public Methods
		public void Update(DataTable dt)
		{
			_LookupItems.Clear();
			foreach (DataRow row in dt.Rows)
			{
				_LookupItems.Add(new LookUpItem(row));
			}
		}

		public List<LookUpItem> Find(string srch, int maxRows = 20)
		{
			var matches = _LookupItems.Where(e => e.label.Contains(srch.ToUpper())).AsQueryable();
			var result = matches.Take(maxRows).ToList();
			
			//IEnumerable<LookUpItem> result = from item in _LookupItems
			//                                 where item.value.Contains(srch.ToUpper())
			//                                 select item;
			//result = result.Take(maxRows);
			return result;
		}

		#endregion


	}
}
