using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AirportIQ.Model.Models.Lists
{
	/// <summary>
	/// Collection of lists
	/// </summary>
	/// <remarks>example People : </remarks>
	public class LookUpLists : Dictionary<string, LookUpList>
	{
		public  LookUpLists(string key, LookUpList value):base()
		{
			this.Add(key, value);
		}

		public LookUpLists(int capacity)
			: base(capacity)
		{
			
		}
		public LookUpLists(IEqualityComparer<string> comparer)
			: base(comparer)
		{
			
		}
		public LookUpLists(int capacity, IEqualityComparer<string> comparer)
			: base(capacity, comparer)
		{
			
		}
		public LookUpLists(IDictionary<string, LookUpList> dictionary)
			: base(dictionary)
		{
			
		}
		public LookUpLists(IDictionary<string, LookUpList> dictionary, IEqualityComparer<string> comparer)
			: base(dictionary, comparer)
		{
			
		}
		protected LookUpLists(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
			: base(info, context)
		{
			
		}
	}
}
