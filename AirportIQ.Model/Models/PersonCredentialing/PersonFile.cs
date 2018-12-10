using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AirportIQ.Model.Models.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AirportIQ.Model.Models.PersonCredentialing
{
	public class PersonFile : IJsonDatasource
	{

		public PersonFile(String documentID, String filename, String fileDescription, String fileType, DateTime whenCreated, 
							String createdBy, String divisionCode, String division, String companyCode, String company)
		{
				DocumentID = documentID;
				Filename = filename;
				FileDescription = fileDescription;
				FileType = fileType;
				WhenCreated = whenCreated;
				CreatedBy = createdBy;
				DivisionCode = divisionCode;
				Division = division;
				CompanyCode = companyCode;
				Company = company;
		}

		public PersonFile(string name, string documentID, string filetype)
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


		public string ToJson()
		{
			//return JsonConvert.SerializeObject(this);
			IsoDateTimeConverter idtc = new IsoDateTimeConverter();
			idtc.DateTimeFormat = "MM/dd/yyyy";
			return JsonConvert.SerializeObject(this, idtc);
		}
		public string MapBool(Boolean b)
		{
			if (b) { return "Yes"; } else { return "No"; }
		}
	}
}
