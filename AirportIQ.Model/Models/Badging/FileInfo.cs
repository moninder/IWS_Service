using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.Models.Badging
{
	public class FileInfo
	{		
		public int PersonDivisionXrefID { get; set; }
		public Int16 DocumentTypeNumber { get; set; }
		public string DocumentFileName { get; set; }
		public string DocumentDescription { get; set; }
		public string IssuingAuthority_CountryCode { get; set; }
		public string IssuingAuthority_CountrySubdivisionCode { get; set; }
		public string IdentificationNumber { get; set; }
		public string ExpirationDate { get; set; }
		public string RequirementCode { get; set; }
	}
}
