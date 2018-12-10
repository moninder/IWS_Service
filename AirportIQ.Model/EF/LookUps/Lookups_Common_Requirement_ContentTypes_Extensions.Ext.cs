using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.EF.LookUps
{
	public partial class Lookups_Common_Requirement_ContentTypes_Extensions
	{
		public static string GetMimeTypeFromFileName(string fileName)
		{
			string result = string.Empty;
			string ext = Path.GetExtension(fileName).Replace(".", "");

			using (LookupEntities context = new LookupEntities())
			{
				var extentions = from extention in context.Lookups_Common_Requirement_ContentTypes_Extensions
												 where extention.FileExtention == ext
												 select extention;

				result = extentions.FirstOrDefault().ContentTypeCode;
			}

			return result;
		}
	}
}
