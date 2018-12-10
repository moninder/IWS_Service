using System;
using System.Linq;
using System.Collections.Generic;

namespace AirportIQ.Model.EF.LookUps
{
	public partial class Miscellaneous_CountrySubdivisions
	{
		public static IQueryable GetCountrySubdivisions(string countryCode)
		{
			var context = new LookupEntities();

			var results = from result in context.Miscellaneous_CountrySubdivisions
										where result.CountryCode == countryCode
										select new
										{
											result.CountrySubdivisionCode,
											result.CountrySubdivisionName
										};

			return results;
		}
	}
}


