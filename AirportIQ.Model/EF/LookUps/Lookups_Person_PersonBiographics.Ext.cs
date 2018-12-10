using System;

namespace AirportIQ.Model.EF.LookUps
{
	public partial class Lookups_Person_PersonBiographics
	{
		public string FormattedName
		{
			get 
			{
				string result = (String.Format("{0} {1},", NamePrefixCode, LastName)).Trim();
				result += " " + (String.Format("{0} {1}", FirstName, MiddleName)).Trim();
				result += (" " + NameSuffixCode).TrimEnd();
				return result;
			}
		}
	}
}