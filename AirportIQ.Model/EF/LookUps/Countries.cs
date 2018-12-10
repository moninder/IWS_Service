using System;
using System.Linq;
using System.Collections.Generic;

namespace AirportIQ.Model.EF.LookUps
{
	public partial class Miscellaneous_Countries
	{
		public static IQueryable GetCounties()
		{
			var context = new LookupEntities();

			var results = from result in context.Miscellaneous_Countries
			              select new
			                     	{
			                     		result.CountryCode,
			                     		result.CountryDescription,
			                     		result.CountrySubdivisionTypeName
			                     	};
			
			return results;
		}


	}
}


