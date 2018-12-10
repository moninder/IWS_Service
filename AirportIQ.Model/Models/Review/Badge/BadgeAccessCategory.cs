using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace AirportIQ.Model.Models.Review.Badge
{
	public class BadgeAccessCategory
	{

		public BadgeAccessCategory()
		{
			AccessType = "test type";
			AgreementNumber = "agmt num";
			JobRole = "job role";
			CategoryName = "cat name";
			//Location = "location";
			WhenBegins = DateTime.Now;
			WhenEnds = DateTime.Now;
		}
		
		//public BadgeAccessCategory(string accessType, string agreementNumber, string jobRole, string category, string location, DateTime begins, DateTime ends)
        public BadgeAccessCategory(string accessType, string agreementNumber, string jobRole, string category, DateTime begins, DateTime ends)
		{
			AccessType = accessType;
			AgreementNumber = agreementNumber;
			JobRole = jobRole;
			CategoryName = category;
			//Location = location;
			WhenBegins = begins;
			WhenEnds = ends;
		}


		public BadgeAccessCategory(DataRow row)
		{
			AccessType = (string)row["AccessType"];
			AgreementNumber = (string)row["AgreementNumber"];
			JobRole = (string)row["JobRoleDescription"];
			CategoryName = (string)row["CategoryName"];
			//Location = (string)row["LocationName"];
			if (row.IsNull("WhenBegins"))
			{
				WhenBegins = DateTime.Now;
			}
			else
			{
				WhenBegins = (DateTime)row["WhenBegins"];
			} 
			if (row.IsNull("WhenEnds"))
			{
				WhenEnds = DateTime.Now;
			}
			else
			{
				WhenEnds = (DateTime)row["WhenBegins"];
			}
		}

		public string AccessType { get; set; }
		public string AgreementNumber { get; set; }
		public string JobRole { get; set; }
		public string CategoryName { get; set; }
		//public string Location { get; set; }
		public DateTime WhenBegins { get; set; }
		public DateTime WhenEnds { get; set; }
	}
}
