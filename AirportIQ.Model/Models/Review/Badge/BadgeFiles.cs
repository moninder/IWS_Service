using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.Models.Review.Badge
{
	public class BadgeFile
	{

		public BadgeFile(string name, string documentID, string filetype)
		{
			DocumentID = documentID;
			Filename = name;
			FileDescription = "test description";
			FileType = filetype;
			WhenCreated = DateTime.Now;
			CreatedBy = "Badge123";
			DivisionCode = "d123";
			Division = "Test Division";
			CompanyCode = "c123";
			Company = "Test Company";
		}

		public BadgeFile(DataRow row)
		{
			DocumentID = Convert.ToString(row["DocumentID"]);
			Filename = (string)row["DocumentFilename"];
			FileDescription = (string)row["DocumentDescription"];
			FileType = (string)row["ContentTypeDescription"];
			WhenCreated = (DateTime)row["WhenCreated"];
			CreatedBy = Convert.ToString(row["BadgeID"]);
			DivisionCode = (string)row["DivisionCode"];
			Division = (string)row["DivisionName"];
			CompanyCode = (string)row["CompanyCode"];
			Company = (string)row["CorporationName"];

		}

		public string DocumentID { get; set; }
		public string Filename { get; set; }
		public string FileDescription { get; set; }
		public string FileType { get; set; }
		public string CreatedBy { get; set; }
		public DateTime WhenCreated { get; set; }
		public string DivisionCode { get; set; }
		public string Division { get; set; }
		public string CompanyCode { get; set; }
		public string Company { get; set; }
	}
}
