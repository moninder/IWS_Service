using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.EF.LookUps
{
	public partial class Lookups_Common_Entities
	{
		public static int NewPersonDivisionXrefEnty(int personDivisionXrefID )
		{
			int result = -1;
			using (LookupEntities context = new LookupEntities())
			{
				var entity = new Lookups_Common_Entities();
				entity.PersonDivisionXrefID = personDivisionXrefID;
				context.SaveChanges();
				result = entity.EntityID;
			}
			return result;
		}
	}
}
